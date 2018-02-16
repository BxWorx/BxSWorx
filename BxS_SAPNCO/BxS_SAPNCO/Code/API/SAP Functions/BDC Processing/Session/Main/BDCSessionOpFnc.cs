using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCSessionOpFnc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionOpFnc(
																	Func< DTO_RFCTran		>		createRFCTran

																, Func<		SMC.RfcFunctionMetadata
																				, BDCCallTranIndexSetup		>		CreateIndexConfigurator

																,	Func<	CancellationToken
																			,	Pipeline< DTO_SessionTran , DTO_ProgressInfo > >	createBDCPipeline

																,	Func< BDCCallTranProfile , BDCCallTranProcessor >				createBDCCallTran

																,	Func<	ConsumerOpEnv<	DTO_SessionTran
																											, DTO_ProgressInfo >
																			,	DTO_SessionHeader
																			, BDCCallTranProcessor
																			, BDCCallTranParser
																			, BDCCallTranConsumer<	DTO_SessionTran
																														, DTO_ProgressInfo > >				createBDCCallConsumer	)
					{
						//this.CreateRfcHead						= createRfcHead						;
						this.CreateRFCTran						= createRFCTran						;
						this.CreateIdxCnfg						= CreateIndexConfigurator	;
						this.CreateBDCPipeline				= createBDCPipeline				;
						this.CreateBDCCallTran				= createBDCCallTran				;
						this.CreateBDCCallConsumer		= createBDCCallConsumer		;

						this.CreateRfcHead	= this.CreateRfcHead;


																	//Func< DTO_RFCHeader >		createRfcHead

					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Func< DTO_RFCHeader >								CreateRfcHead		{ get; }
				internal	Func< DTO_RFCTran		>								CreateRFCTran		{ get; }

				internal  Func<		SMC.RfcFunctionMetadata
												,	BDCCallTranIndexSetup		>		CreateIdxCnfg		{ get; }

				internal	Func<		CancellationToken
												,	Pipeline< DTO_SessionTran , DTO_ProgressInfo > >	CreateBDCPipeline			{ get; }

				internal	Func< BDCCallTranProfile	, BDCCallTranProcessor	>				CreateBDCCallTran			{ get; }

				internal	Func<		ConsumerOpEnv<	DTO_SessionTran
																				, DTO_ProgressInfo >
												,	DTO_SessionHeader
												, BDCCallTranProcessor
												, BDCCallTranParser
												, BDCCallTranConsumer<	DTO_SessionTran
																							, DTO_ProgressInfo > >				CreateBDCCallConsumer	{ get; }

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_RFCHeader CreateRFCHead()
					{
						return	new DTO_RFCHeader();
					}

			#endregion

		}
}