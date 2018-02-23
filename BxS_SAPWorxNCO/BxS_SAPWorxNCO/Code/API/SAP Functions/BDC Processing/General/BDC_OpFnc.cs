using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDC_OpFnc
		{
			#region "Constructors"

				internal BDC_OpFnc()
					{
						this.CreateRfcHead					= () => new DTO_RFCHeader()	;
						this.CreateRFCTran					= () => new DTO_RFCTran()		;

						this.CreateSessionTran			= ( Guid ID															) => new DTO_SessionTran( ID = default(Guid) )							;
						this.CreateIndxSetup				= ( SMC.RfcFunctionMetadata FncMetadata ) => new BDCCallTranIndexSetup( FncMetadata )								;
						this.CreateBDCPipeline			= ( CancellationToken				CT					)	=> new Pipeline< DTO_SessionTran , DTO_ProgressInfo >( CT )	;
						this.CreateBDCCallTran			= ( BDCCallTranProfile			profile			)	=> new BDCCallTranProcessor( profile )										;

						this.CreateBDCCallConsumer	= (		ConsumerOpEnv<		DTO_SessionTran
																															, DTO_ProgressInfo >	conOpEnv
																						, DTO_SessionHeader											sessionHeader
																						, BDCCallTranProfile										profile
																						, BDCCallTranProcessor									callTranProcessor	)

																							=> new BDCCallTranConsumer< DTO_SessionTran , DTO_ProgressInfo>(	conOpEnv
																																																							, sessionHeader
																																																							, profile
																																																							, callTranProcessor	)	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Func< DTO_RFCHeader >								CreateRfcHead		{ get; }
				internal	Func< DTO_RFCTran		>								CreateRFCTran		{ get; }

				internal	Func<		Guid
												, DTO_SessionTran					>		CreateSessionTran	{ get; }

				internal  Func<		SMC.RfcFunctionMetadata
												,	BDCCallTranIndexSetup		>		CreateIndxSetup		{ get; }

				internal	Func<		CancellationToken
												,	Pipeline< DTO_SessionTran , DTO_ProgressInfo > >	CreateBDCPipeline			{ get; }

				internal	Func< BDCCallTranProfile	, BDCCallTranProcessor	>				CreateBDCCallTran			{ get; }

				internal	Func<		ConsumerOpEnv<	DTO_SessionTran
																				, DTO_ProgressInfo >
												,	DTO_SessionHeader
												, BDCCallTranProfile
												, BDCCallTranProcessor
												, BDCCallTranConsumer<	DTO_SessionTran
																							, DTO_ProgressInfo > >				CreateBDCCallConsumer	{ get; }

			#endregion

		}
}
