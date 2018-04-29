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

using BxS_WorxUtil.Main;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session_TranPipeline
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_TranPipeline(		IRfcDestination							rfcDestination
																						, Lazy< IRfcFncController		>	rfcFncCntlr
																						, Lazy< BDC_Session_Factory > factory
																						, bool						useAltBDCFunction	= false )
					{
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- RfcDest null"		);
						this._Factory					= factory					??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- Factory null"		);
						this._RfcFncCntlr			= rfcFncCntlr			??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- FNC Cntlr null" );
						this._UseAltFnc				= useAltBDCFunction	;
						//...
						this._BDCSessCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_TranProcessor > >			(	()=>	this.CreateBDCSessionPoolConfig()			,	cz_LM );
						this._BDCSessPool		= new	Lazy< ObjectPool			< BDC_Session_TranProcessor > >			(	()=>	this.CreateBDCSessionPool()						, cz_LM );

						this._BDCConsCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_TranConsumer > >			(	()=>	this.CreateBDCTransConsumerPoolConfig	( this.CreateBDCTranConsumer , true )	,	cz_LM );
						this._BDCConsPool		= new	Lazy< ObjectPool			< BDC_Session_TranConsumer > >			(	()=>	this.CreateBDCTransConsumerPool				( this.CreateBDCTranConsumer )				, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	bool	_UseAltFnc	;
				//...
				private	readonly	IRfcDestination								_RfcDestination	;
				private	readonly	Lazy< BDC_Session_Factory	>		_Factory				;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr		;
				//...
				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_TranProcessor > >			_BDCSessCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_TranProcessor >	>			_BDCSessPool	;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_TranConsumer > >			_BDCConsCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_TranConsumer > >			_BDCConsPool	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IUTL_Controller			UTL_Cntlr				{ get	{	return	UTL_Controller.Instance	; } }
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

			#endregion


			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	ObjectPool< BDC_Session_TranProcessor >	CreateBDCSessionPool()	=>	this.UTL_Cntlr.CreateObjectPool( this.CreateBDCSession );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session_TranProcessor > CreateBDCSessionPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig( this.CreateBDCSession , defaults );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_TranProcessor CreateBDCSession()
					{
						return	new	BDC_Session_TranProcessor( this._Factory.Value.CreateBDCSessionConfig() );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU		();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed , this.CreateBDCTransaction );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Request CreateRequestDTO()
					{
						return	new DTO_BDC_Request( this.CreateSessionDTO );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	DTO_BDC_Transaction	CreateBDCTransaction( int No )	=>	new DTO_BDC_Transaction( No );


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ObjectPool< BDC_Session_TranConsumer > CreateBDCTransConsumerPool( Func< BDC_Session_TranConsumer > factory )
					{
						return	this.UTL_Cntlr.CreateObjectPool( factory );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ObjectPoolConfig< BDC_Session_TranConsumer > CreateBDCTransConsumerPoolConfig(		Func< BDC_Session_TranConsumer >	factory
																																																,	bool															defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig( factory , defaults );
					}

				private BDC_Session_TranConsumer		CreateBDCTranConsumer		()=>	new	BDC_Session_TranConsumer		( this._UseAltFnc ? this._RfcFncCntlr.Value.CreateBDCFunctionAlt()
																																																														: this._RfcFncCntlr.Value.CreateBDCFunctionStd() );

			#endregion

		}
}
