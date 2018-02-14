using System;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCOpFnc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpFnc()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Func< Guid				, DTO_SessionTran >		SessionTran			{ get; set; }
				internal	Func< IBDCProfile	, BDC2RfcParser		>		Parser					{ get; set; }
				internal	Func< IBDCProfile	, BDCCallTranProcessor	>	TranProcessor		{ get; set; }
				//.................................................
				internal	Func< DTO_SessionHeader >								SessionHeader		{ get; set; }
				internal	Func< DTO_SessionOptions >							SessionOptions	{ get; set; }
				internal	Func< DTO_RFCHeader >										RFCHeader				{ get	{ return	CreateRFCHeader; } }
				internal	Func< DTO_RFCTran >											RFCTran					{ get; set; }
				internal	Func< IProgress<DTO_ProgressInfo> >			ProgressHndlr		{ get; set; }
				internal	Func< DTO_ProgressInfo	>								ProgressInfo		{ get; set; }
				internal	Func< BDCProfileConfigurator >					ProfileConfig		{ get; set; }
				//.................................................
				internal	Func<		BDCOpEnv
												,	PipelineOpEnv	< DTO_RFCTran	,	DTO_ProgressInfo >	>	PLOpEnv		{ get; set; }

				internal	Func<		PipelineOpEnv	< DTO_RFCTran , DTO_ProgressInfo >
												,	Pipeline			< DTO_RFCTran , DTO_ProgressInfo >	>	Pipeline	{ get; set; }

				internal	Func<		PipelineOpEnv	< DTO_RFCTran	,	DTO_ProgressInfo >
												, BDCCallTranProcessor
												,	IConsumer	< DTO_RFCTran >													>	Consumer	{ get; set; }

			#endregion


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static DTO_RFCHeader	CreateRFCHeader()
					{
						return	new	DTO_RFCHeader();
					}

		}
}
