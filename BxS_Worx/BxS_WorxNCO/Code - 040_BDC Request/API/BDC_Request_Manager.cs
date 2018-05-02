using System;
//using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//using System.Linq;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxUtil.Progress;

using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public class BDC_Request_Manager : IBDC_Request_Manager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Request_Manager(		IBxSDestination	BxSDestination
																			, bool						useAltBDCFunction	= false
																			, bool						autoReady					= false )
					{
						this.BxSDestination	= BxSDestination	??	throw		new	ArgumentException( $"{typeof(BDC_Request_Manager).Namespace}:- RfcDest null" );
						this._UseAltFnc			= useAltBDCFunction	;
						//.............................................
						this._Factory				= new	Lazy< BDC_Session_Factory >	( ()=>	BDC_Session_Factory.Instance								, cz_LM );
						this._RfcFncCntlr		= new	Lazy< IRfcFncController		>	(	()=>	new	RfcFncController( this.BxSDestination )	,	cz_LM );
						//.............................................
						this._PLParser	= new	Lazy<BDC_ParserPipeline>	( ()=>	new	BDC_ParserPipeline	( this._Factory ) );
						this._PLBDCTrn	= new	Lazy<BDC_TranPipeline>		( ()=>	new BDC_TranPipeline		( this.BxSDestination , this._RfcFncCntlr , this._Factory	, useAltBDCFunction ) );
						this._PLSAPMsg	= new	Lazy<BDC_SAPMsgPipeline>	( ()=>	new BDC_SAPMsgPipeline	( this.BxSDestination , this._RfcFncCntlr , this._Factory											) );
						//.............................................
						this._IsReady		= false;
						this._SlimLock	= new SemaphoreSlim(0 , 1);

						if ( autoReady )
							{
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private 					bool					_UseAltFnc	;
				private						bool					_IsReady		;
				private	readonly	SemaphoreSlim _SlimLock		;
				//.................................................
				private	readonly	Lazy< BDC_Session_Factory	>		_Factory			;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr	;
				//.................................................
				private	readonly	Lazy< BDC_ParserPipeline >	_PLParser;
				private	readonly	Lazy< BDC_TranPipeline		>	_PLBDCTrn;
				private	readonly	Lazy< BDC_SAPMsgPipeline >	_PLSAPMsg;

				//private	readonly	Lazy< ObjectPool<BDC_Parser> >	_Pool			;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IBxSDestination			BxSDestination	{ get; }
				internal	SMC.RfcDestination	SMCDestination	{ get	{	return	this.BxSDestination.SMCDestination; } }
				//.................................................
				//internal ObjectPoolConfig< BDC_Parser						> ParserConfiguration				{ get { return	this._ParserCfg		.Value	; } }
				//internal ObjectPoolConfig< BDC_TranConsumer			> BDCConsumerConfiguration	{ get { return	this._BDCConsCfg	.Value	; } }
				//internal ObjectPoolConfig< BDC_TranProcessor		> BDCSessionConfiguration		{ get { return	this._BDCSessCfg	.Value	; } }
				//internal ObjectPoolConfig< BDC_SAPMsgProcessor	> SAPMessageConfiguration		{ get { return	this._SAPMsgCfg		.Value	; } }
				//internal ObjectPoolConfig< BDC_SAPMsgConsumer		> MsgConsumerConfiguration	{ get { return	this._MsgConsCfg	.Value	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Create BDC session config DTO to configure session environment
				//
				public DTO_BDC_SessionConfig CreateSessionConfig()
					{
						return	new DTO_BDC_SessionConfig {
																									IsSequential			= true
																								, ConsumersMax			= 1
																								,	ConsumersNo				= 1
																								,	PauseTime					= 0
																								, ConsumerThreshold	= 0
																								, QueueAddTimeout		= 0
																								, ProgressInterval	= 10
																																						};
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ProgressHandler< DTO_BDC_Progress >	CreateProgressHandler() => this._Factory.Value.CreateProgressHandler();


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<bool> ProcessAsync(		IRequest														request
																							,	CancellationToken										CT
																							, ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
						this._UseAltFnc	= request.Config.UseAltBDC	;


						//if ( ! this._IsReady )
						//	{
						//		if ( ! await this.ReadyAsync().ConfigureAwait(false) )
						//			{
						//				return	false;
						//			}
						//	}
						//.............................................
						var	lo_TrnQueue	  =	new BlockingCollection<DTO_BDC_Session>	();
						this._PLParser.Value.Process( request , lo_TrnQueue , CT , progressHndlr );
						


						//Task<int>[] lt_SsnTasks	=
						//	Enumerable.Range(0,1)
						//		.Select( _ =>
						//			Task.Run( ()	=>
						//				{
						//					var	lo_PsrQueue	  =	new BlockingCollection<DTO_BDC_Session>	();
						//					//var	lo_MsgQueue	  =	new BlockingCollection<DTO_BDC_Session>	();

						//					//Task lo_TaskMPL = this._PLSAPMsg.Value.ProcessAsync( lo_TrnQueue , lo_MsgQueue , CT , progressHndlr )	;	//.ConfigureAwait(false);
						//					//Task lo_TaskTPL = this._PLBDCTrn.Value.ProcessAsync( lo_PsrQueue , lo_TrnQueue , CT , progressHndlr )	;	//.ConfigureAwait(false);

						//					lo_TaskPPL.Wait( CT );
						//					lo_PsrQueue.CompleteAdding();
						//					//lo_TaskTPL.Wait( CT );
						//					//lo_TrnQueue.CompleteAdding();
						//					//lo_TaskMPL.Wait( CT );
						//					//lo_MsgQueue.CompleteAdding();
						//					return	lo_PsrQueue.Count;
						//				}	))
						//				.ToArray();

						//// Load session into start pipeline
						////
						//foreach ( KeyValuePair<int , ISession> ls_kvp in request.Sessions )
						//	{
						//		lo_SsnQueue.Add( ls_kvp.Value );
						//	}
						//lo_SsnQueue.CompleteAdding();


						//var	lo_PsrQueue	  =	new BlockingCollection<DTO_BDC_Session>	();
						//var	lo_TrnQueue	  =	new BlockingCollection<DTO_BDC_Session>	();
						//var	lo_MsgQueue	  =	new BlockingCollection<DTO_BDC_Session>	();

						//await this._PLSAPMsg.Value.ProcessAsync( lo_TrnQueue , lo_MsgQueue , CT , progressHndlr ).ConfigureAwait(false);
						//await	this._PLBDCTrn.Value.ProcessAsync( lo_PsrQueue , lo_TrnQueue , CT , progressHndlr ).ConfigureAwait(false);
						//await this._PLParser.Value.ProcessAsync( lo_SsnQueue , lo_PsrQueue , CT , progressHndlr ).ConfigureAwait(false);
						//...
						//...
						//Task.WaitAll(lt_SsnTasks);
						//int x =0;
						//foreach ( Task<int> lt in lt_SsnTasks )
						//	{
						//		x +=	lt.Result;
						//	}
						return	true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Reconfigure"

				// Applies changes made to individual configurations (made direct in the configuration).
				// This is done as a number of rules exist within the configuration which tracks if any
				// changes made are relevant based on the status of the individual pools.
				//
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public void ReConfigureBDCSessionPool		()=>	this._BDCSessPool	.Value	.ConfigurePool( this._BDCSessCfg .Value );
				//public void ReConfigureBDCConsumerPool	()=>	this._BDCConsPool	.Value	.ConfigurePool( this._BDCConsCfg .Value );
				//public void ReConfigureParserPool				()=>	this._ParserPool	.Value	.ConfigurePool( this._ParserCfg	 .Value );
				//public void ReConfigureSAPMsgPool				()=>	this._SAPMsgPool	.Value	.ConfigurePool( this._SAPMsgCfg	 .Value );

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task<bool>	ReadyAsync( bool optimise = true )
					{
						if ( ! this._IsReady )
							{
								await	this._SlimLock.WaitAsync().ConfigureAwait(false);

								try
									{
										if ( ! this._IsReady )
											{
												// register the relevant SAP RFC functions
												//
												if ( this._UseAltFnc )	{	this._RfcFncCntlr.Value.RegisterBDCAlt();	}
												else										{	this._RfcFncCntlr.Value.RegisterBDCStd();	}

												this._RfcFncCntlr.Value.RegisterSAPMsg();
												//.........................................
												// fetch metadata from SAP destination, update profiles
												//
												await this.BxSDestination.FetchMetadataAsync( optimise ).ConfigureAwait(false);
												await	this._RfcFncCntlr.Value.UpdateProfilesAsync()			.ConfigureAwait(false);

												this._IsReady	=	true;
											}
									}
								catch ( Exception ex)
									{
										throw new Exception( "BDC Session ready fail" , ex );
									}
								finally
									{
										this._SlimLock.Release();
									}
							}
						//.................................................
						return	this._IsReady;
					}

			#endregion
		}
}
