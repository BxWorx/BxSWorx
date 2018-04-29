using System;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.Main;

using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session_SAPMsgPipeline
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_SAPMsgPipeline(	IRfcDestination							rfcDestination
																						, Lazy< IRfcFncController		>	rfcFncCntlr
																						, Lazy< BDC_Session_Factory > factory					)
					{
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- RfcDest null"		);
						this._Factory					= factory					??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- Factory null"		);
						this._RfcFncCntlr			= rfcFncCntlr			??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- FNC Cntlr null" );
						//...
						this._SAPMsgCfg			= new	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgProcessor > >		(	()=>	this._Factory.Value.CreateSAPMsgsPoolConfig()	,	cz_LM );
						this._SAPMsgPool		= new	Lazy< ObjectPool			< BDC_Session_SAPMsgProcessor > >		(	()=>	this._Factory.Value.CreateSAPMsgsPool()				, cz_LM );

						this._MsgConsCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgConsumer > >		(	()=>	this._Factory.Value.CreateBDCSAPMsgConsumerPoolConfig	( this.CreateBDCSAPMsgConsumer , true )	,	cz_LM );
						this._MsgConsPool		= new	Lazy< ObjectPool			< BDC_Session_SAPMsgConsumer > >		(	()=>	this._Factory.Value.CreateBDCSAPMsgConsumerPool				( this.CreateBDCSAPMsgConsumer )				, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IRfcDestination								_RfcDestination	;
				private	readonly	Lazy< BDC_Session_Factory	>		_Factory				;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr		;
				//...
				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgProcessor > >		_SAPMsgCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_SAPMsgProcessor > >		_SAPMsgPool		;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgConsumer > >		_MsgConsCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_SAPMsgConsumer > >		_MsgConsPool	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	SMC.RfcDestination	SMCDestination	{ get	{	return	this._RfcDestination.SMCDestination; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task ProcessSAPMsgPipelineAsync(		BlockingCollection<DTO_BDC_Session>				queueIn
																											, BlockingCollection<DTO_BDC_Session>				queueOut
																											, CancellationToken													CT
																											,	ProgressHandler< DTO_BDC_Progress >				progressHndlr )
					{
						using ( BDC_Session_SAPMsgProcessor lo_SAPMsgs	= this._SAPMsgPool.Value.Acquire() )
							{
								foreach ( DTO_BDC_Session lo_DTOSession in queueIn.GetConsumingEnumerable() )
									{
										int ln_Msg	=	await	lo_SAPMsgs.Process_SessionAsync(	lo_DTOSession
																																				, CT
																																				, progressHndlr
																																				, this._MsgConsPool.Value
																																				,	this.SMCDestination ).ConfigureAwait(false);
										queueOut.Add(lo_DTOSession);
									}
							}
					}




				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigureSession( DTO_BDC_SessionConfig dto )	=>	this.Config.Configure( dto );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				// Returns no of transactions processesed
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> Process_SessionAsync(	DTO_BDC_Session														bdcSession
																										, CancellationToken													CT
																										,	ProgressHandler< DTO_BDC_Progress >				progressHndlr
																										, ObjectPool< BDC_Session_SAPMsgConsumer >	pool
																										,	SMC.RfcDestination												rfcDestination	)
					{
						this.PrepareSession	( bdcSession				);
						this.LoadQueue			( bdcSession.Trans	);
						//.............................................
						if ( this._Queue.Count > 0 )
							{
								this.StartConsumers( CT , pool , rfcDestination );

								if ( ! CT.IsCancellationRequested )
									{
										this.TransactionsProcessed	=	await ProcessConsumerResultsAsync(	CT
																																										,	progressHndlr )
																													.ConfigureAwait(false);
									}
							}
						//.............................................
						this.CloseSession();

						return	this.TransactionsProcessed;
					}

			#endregion


			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Session_SAPMsgConsumer	CreateBDCSAPMsgConsumer	()=>	new	BDC_Session_SAPMsgConsumer	( this._RfcFncCntlr.Value.CreateSAPMsgFunction	() );

			#endregion

		}
}
