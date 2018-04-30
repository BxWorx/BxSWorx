﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
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
						this._SlimLock	= new SemaphoreSlim(0 , 1);

						if ( autoReady )
							{
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	bool					_UseAltFnc	;
				private						bool					_IsReady		;
				private	readonly	SemaphoreSlim _SlimLock				;
				//.................................................
				private	readonly	Lazy< BDC_Session_Factory	>		_Factory			;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr	;
				//.................................................
				private	readonly	Lazy< BDC_ParserPipeline >	_PLParser;
				private	readonly	Lazy< BDC_TranPipeline		>	_PLBDCTrn;
				private	readonly	Lazy< BDC_SAPMsgPipeline >	_PLSAPMsg;

				private	readonly	Lazy< ObjectPool<BDC_Parser> >	_Pool			;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IRfcDestination			RfcDestination	{ get; }
				internal	SMC.RfcDestination	SMCDestination	{ get	{	return	this.RfcDestination.SMCDestination; } }
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
				public async Task<bool>	ReadyAsync( bool optimise = true )
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
										this._SlimLock.Release();
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
						var	lo_SsnQueue		=	new BlockingCollection<ISession>();

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
			#endregion
		}
}
