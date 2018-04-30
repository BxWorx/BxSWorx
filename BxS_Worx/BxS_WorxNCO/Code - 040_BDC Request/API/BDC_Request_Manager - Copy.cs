using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public class BDC_Request_Manager : IBDC_Request_Manager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Request_Manager(		IRfcDestination	rfcDestination
																			, bool						useAltBDCFunction	= false
																			, bool						autoReady					= false )
					{
						this.RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_Request_Manager).Namespace}:- RfcDest null" );
						this._UseAltFnc			= useAltBDCFunction	;
						//.............................................
						this._Factory				= new	Lazy< BDC_Session_Factory >	( ()=>	BDC_Session_Factory.Instance								, cz_LM );
						this._RfcFncCntlr		= new	Lazy< IRfcFncController		>	(	()=>	new	RfcFncController( this.RfcDestination )	,	cz_LM );
						//.............................................
						this._PLParser	= new	Lazy< BDC_ParserPipeline >	( ()=> new BDC_ParserPipeline	( this.RfcDestination , this._RfcFncCntlr , this._Factory											) );
						this._PLBDCTrn	= new	Lazy< BDC_TranPipeline	 >	( ()=> new BDC_TranPipeline		( this.RfcDestination , this._RfcFncCntlr , this._Factory	, useAltBDCFunction ) );
						this._PLSAPMsg	= new	Lazy< BDC_SAPMsgPipeline >	( ()=> new BDC_SAPMsgPipeline	( this.RfcDestination , this._RfcFncCntlr , this._Factory											) );
						//.............................................
						this._IsReady		= false;
						this._Lock			= new SemaphoreSlim(0 , 1);





						//this._ParserCfg			= new	Lazy< ObjectPoolConfig< BDC_Parser > >										(	()=>	this._Factory.Value.CreateParserPoolConfig()					,	cz_LM );
						//this._ParserPool		= new	Lazy< ObjectPool			< BDC_Parser > >										(	()=>	this._Factory.Value.CreateParserPool()								, cz_LM );


						//this._BDCSessCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_TranProcessor > >			(	()=>	this._Factory.Value.CreateBDCSessionPoolConfig()			,	cz_LM );
						//this._BDCSessPool		= new	Lazy< ObjectPool			< BDC_Session_TranProcessor > >			(	()=>	this._Factory.Value.CreateBDCSessionPool()						, cz_LM );

						//this._SAPMsgCfg			= new	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgProcessor > >		(	()=>	this._Factory.Value.CreateSAPMsgsPoolConfig()					,	cz_LM );
						//this._SAPMsgPool		= new	Lazy< ObjectPool			< BDC_Session_SAPMsgProcessor > >		(	()=>	this._Factory.Value.CreateSAPMsgsPool()								, cz_LM );

						//this._BDCConsCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_TranConsumer > >			(	()=>	this._Factory.Value.CreateBDCTransConsumerPoolConfig	( this.CreateBDCTranConsumer , true )	,	cz_LM );
						//this._BDCConsPool		= new	Lazy< ObjectPool			< BDC_Session_TranConsumer > >			(	()=>	this._Factory.Value.CreateBDCTransConsumerPool				( this.CreateBDCTranConsumer )				, cz_LM );

						//this._MsgConsCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgConsumer > >		(	()=>	this._Factory.Value.CreateBDCSAPMsgConsumerPoolConfig	( this.CreateBDCSAPMsgConsumer , true )	,	cz_LM );
						//this._MsgConsPool		= new	Lazy< ObjectPool			< BDC_Session_SAPMsgConsumer > >		(	()=>	this._Factory.Value.CreateBDCSAPMsgConsumerPool				( this.CreateBDCSAPMsgConsumer )				, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	bool					_UseAltFnc	;
				private						bool					_IsReady		;
				private	readonly	SemaphoreSlim _Lock				;
				//.................................................
				private	readonly	Lazy< BDC_Session_Factory	>		_Factory			;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr	;
				//.................................................
				private	readonly Lazy< BDC_ParserPipeline >	_PLParser;
				private	readonly Lazy< BDC_TranPipeline		>	_PLBDCTrn;
				private	readonly Lazy< BDC_SAPMsgPipeline >	_PLSAPMsg;
				//private	readonly Lazy< BDC_ParserPipeline >	_PLResult;



				//private	readonly	Lazy< ObjectPoolConfig< BDC_Parser > >										_ParserCfg		;
				//private	readonly	Lazy< ObjectPool			< BDC_Parser > >										_ParserPool		;

				//private	readonly	Lazy< ObjectPoolConfig< BDC_Session_TranProcessor > >			_BDCSessCfg		;
				//private	readonly	Lazy< ObjectPool			< BDC_Session_TranProcessor >	>			_BDCSessPool	;

				//private	readonly	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgProcessor > >		_SAPMsgCfg		;
				//private	readonly	Lazy< ObjectPool			< BDC_Session_SAPMsgProcessor > >		_SAPMsgPool		;

				//private	readonly	Lazy< ObjectPoolConfig< BDC_Session_TranConsumer > >			_BDCConsCfg		;
				//private	readonly	Lazy< ObjectPool			< BDC_Session_TranConsumer > >			_BDCConsPool	;

				//private	readonly	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgConsumer > >		_MsgConsCfg		;
				//private	readonly	Lazy< ObjectPool			< BDC_Session_SAPMsgConsumer > >		_MsgConsPool	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IRfcDestination			RfcDestination	{ get; }
				internal	SMC.RfcDestination	SMCDestination	{ get	{	return	this.RfcDestination.SMCDestination; } }
				//.................................................
				internal ObjectPoolConfig< BDC_Parser										> ParserConfiguration				{ get { return	this._ParserCfg		.Value	; } }
				internal ObjectPoolConfig< BDC_TranConsumer			> BDCConsumerConfiguration	{ get { return	this._BDCConsCfg	.Value	; } }
				internal ObjectPoolConfig< BDC_TranProcessor		> BDCSessionConfiguration		{ get { return	this._BDCSessCfg	.Value	; } }
				internal ObjectPoolConfig< BDC_SAPMsgProcessor	> SAPMessageConfiguration		{ get { return	this._SAPMsgCfg		.Value	; } }
				internal ObjectPoolConfig< BDC_SAPMsgConsumer		> MsgConsumerConfiguration	{ get { return	this._MsgConsCfg	.Value	; } }

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
				public async Task<bool>	ReadyAsync( bool optimise = true )
					{
						if ( ! this._IsReady )
							{
								await	this._Lock.WaitAsync().ConfigureAwait(false);

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
												await this.RfcDestination.FetchMetadataAsync( optimise ).ConfigureAwait(false);
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
										this._Lock.Release();
									}

							}
						//.................................................
						return	this._IsReady;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<bool> ProcessAsync(		IRequest														request
																							,	CancellationToken										CT
																							, ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
						if ( ! this._IsReady )
							{
								if ( ! await this.ReadyAsync().ConfigureAwait(false) )
									{
										return	false;
									}
							}
						//.............................................

						var	lo_SsnQueue		=	new BlockingCollection<ISession>				();
						var	lo_PsrQueue	  =	new BlockingCollection<DTO_BDC_Session>	();
						var	lo_TrnQueue	  =	new BlockingCollection<DTO_BDC_Session>	();
						var	lo_MsgQueue	  =	new BlockingCollection<DTO_BDC_Session>	();

						await this._PLSAPMsg.Value.ProcessAsync( lo_TrnQueue , lo_MsgQueue , CT , progressHndlr ).ConfigureAwait(false);
						await	this._PLBDCTrn.Value.ProcessAsync( lo_PsrQueue , lo_TrnQueue , CT , progressHndlr ).ConfigureAwait(false);
						await this._PLParser.Value.ProcessAsync( lo_SsnQueue , lo_PsrQueue , CT , progressHndlr ).ConfigureAwait(false);
						//...
						foreach ( KeyValuePair<int , ISession> ls_kvp in request.Sessions )
							{
								lo_SsnQueue.Add( ls_kvp.Value );
							}
						lo_SsnQueue.CompleteAdding();
						//...
						return	true;
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<bool> Process(	ISession														request
																				,	CancellationToken										CT
																				, ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
						bool	lb_ParseOk	= false	;
					  bool	lb_Ret			= false	;

						DTO_BDC_Session lo_DTOSession		=	this._Factory.Value.CreateSessionDTO();
						//.............................................
						// Parse request, data from an excel spreadsheet, into an BDC Session DTO.
						// used by Process Session.
						//
						using (	BDC_Parser lo_Parser = this._ParserPool.Value.Acquire() )
							{
								lb_ParseOk	=	await Task.Run(	()=>	lo_Parser.Parse( request , lo_DTOSession ) ).ConfigureAwait(false);
							}
						//.............................................
						if ( lb_ParseOk )
							{
								using ( BDC_TranProcessor lo_BDCSession = await this._BDCSessPool.Value.Acquire() )
									{
										int ln_Trn	=	await	lo_BDCSession.Process_SessionAsync(		lo_DTOSession
																																						, CT
																																						, progressHndlr
																																						, this._BDCConsPool.Value
																																						,	this.SMCDestination ).ConfigureAwait(false);
									}
								//.............................................
								using ( BDC_SAPMsgProcessor lo_SAPMsgs	= this._SAPMsgPool.Value.Acquire() )
									{
										int ln_Msg	=	await	lo_SAPMsgs.Process_SessionAsync(	lo_DTOSession
																																				, CT
																																				, progressHndlr
																																				, this._MsgConsPool.Value
																																				,	this.SMCDestination ).ConfigureAwait(false);
									}
								lb_Ret	= true;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Reconfigure"

				// Applies changes made to individual configurations (made direct in the configuration).
				// This is done as a number of rules exist within the configuration which tracks if any
				// changes made are relevant based on the status of the individual pools.
				//
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ReConfigureBDCSessionPool		()=>	this._BDCSessPool	.Value	.ConfigurePool( this._BDCSessCfg .Value );
				public void ReConfigureBDCConsumerPool	()=>	this._BDCConsPool	.Value	.ConfigurePool( this._BDCConsCfg .Value );
				public void ReConfigureParserPool				()=>	this._ParserPool	.Value	.ConfigurePool( this._ParserCfg	 .Value );
				public void ReConfigureSAPMsgPool				()=>	this._SAPMsgPool	.Value	.ConfigurePool( this._SAPMsgCfg	 .Value );

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//private BDC_Session_SAPMsgConsumer	CreateBDCSAPMsgConsumer	()=>	new	BDC_Session_SAPMsgConsumer	( this._RfcFncCntlr.Value.CreateSAPMsgFunction	() );

				//private BDC_Session_TranConsumer		CreateBDCTranConsumer		()=>	new	BDC_Session_TranConsumer		( this._UseAltFnc ? this._RfcFncCntlr.Value.CreateBDCFunctionAlt()
				//																																																										: this._RfcFncCntlr.Value.CreateBDCFunctionStd() );

			#endregion
		}
}
