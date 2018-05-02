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
	internal class BDC_SAPMsgPipeline
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_SAPMsgPipeline(	IBxSDestination							rfcDestination
																		, Lazy< IRfcFncController		>	rfcFncCntlr
																		, Lazy< BDC_Session_Factory > factory					)
					{
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_SAPMsgPipeline).Namespace}:- RfcDest null"		);
						this._Factory					= factory					??	throw		new	ArgumentException( $"{typeof(BDC_SAPMsgPipeline).Namespace}:- Factory null"		);
						this._RfcFncCntlr			= rfcFncCntlr			??	throw		new	ArgumentException( $"{typeof(BDC_SAPMsgPipeline).Namespace}:- FNC Cntlr null" );
						//...
						this._SAPMsgCfg			= new	Lazy< ObjectPoolConfig<BDC_SAPMsgProcessor> >		(	()=>	this.CreateSAPMsgsPoolConfig()	,	cz_LM );
						this._SAPMsgPool		= new	Lazy< ObjectPool			<BDC_SAPMsgProcessor> >		(	()=>	this.CreateSAPMsgsPool()				, cz_LM );

						this._MsgConsCfg		= new	Lazy< ObjectPoolConfig<BDC_SAPMsgConsumer> >		(	()=>	this.CreateBDCSAPMsgConsumerPoolConfig	( this.CreateConsumer , true )	,	cz_LM );
						this._MsgConsPool		= new	Lazy< ObjectPool			<BDC_SAPMsgConsumer> >		(	()=>	this.CreateSAPMsgConsumerPool				( this.CreateConsumer )				, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IBxSDestination							_RfcDestination	;
				//...
				private	readonly	Lazy<BDC_Session_Factory>		_Factory				;
				private	readonly	Lazy<IRfcFncController>			_RfcFncCntlr		;
				//...
				private	readonly	Lazy< ObjectPoolConfig<BDC_SAPMsgProcessor> >		_SAPMsgCfg		;
				private	readonly	Lazy< ObjectPool			<BDC_SAPMsgProcessor> >		_SAPMsgPool		;

				private	readonly	Lazy< ObjectPoolConfig<BDC_SAPMsgConsumer> >		_MsgConsCfg		;
				private	readonly	Lazy< ObjectPool			<BDC_SAPMsgConsumer> >		_MsgConsPool	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IUTL_Controller			UTL_Cntlr				{ get	{	return	UTL_Controller.Instance							; } }
				private	SMC.RfcDestination	SMCDestination	{ get	{	return	this._RfcDestination.SMCDestination	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task ProcessAsync(		BlockingCollection<DTO_BDC_Session>	queueIn
																				, BlockingCollection<DTO_BDC_Session>	queueOut
																				, CancellationToken										CT
																				,	ProgressHandler<DTO_BDC_Progress>		progressHndlr )
					{
						using ( BDC_SAPMsgProcessor lo_SAPMsgs	= this._SAPMsgPool.Value.Acquire() )
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
				private	BDC_SAPMsgProcessor CreateProcessor()	=>	new	BDC_SAPMsgProcessor	( this._Factory.Value.CreateBDCSessionConfig()		);
				private BDC_SAPMsgConsumer	CreateConsumer()	=>	new	BDC_SAPMsgConsumer	( this._RfcFncCntlr.Value.CreateSAPMsgFunction()	);
				//...
				private	ObjectPoolConfig<BDC_SAPMsgProcessor> CreateSAPMsgsPoolConfig( bool defaults = true )		=>	ObjectPoolFactory.CreateConfig( this.CreateProcessor , defaults );
				private	ObjectPool<BDC_SAPMsgProcessor>				CreateSAPMsgsPool()																=>	this.UTL_Cntlr.CreateObjectPool( this.CreateProcessor );
				private	ObjectPool<BDC_SAPMsgConsumer>				CreateSAPMsgConsumerPool( Func<BDC_SAPMsgConsumer> factory )	=>	this.UTL_Cntlr.CreateObjectPool( factory );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ObjectPoolConfig<BDC_SAPMsgConsumer> CreateBDCSAPMsgConsumerPoolConfig(		Func<BDC_SAPMsgConsumer>	factory
																																													, bool												defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig( factory , defaults );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

			#endregion

		}
}
