using BxS_SAPNCO.API.SAPFunctions.BDC;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Internal: BDC Processing: Pipeline"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline<IBDCTranData, BDCProgressInfo>	CreateBDCPipeline(OpEnv<IBDCTranData, BDCProgressInfo>	opEnv							,
																																						BDCConsumerMaker											bdcConsumerMaker		)
					{
						return	new Pipeline<	IBDCTranData, BDCProgressInfo>(	opEnv	,
																																	bdcConsumerMaker	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCConsumerMaker	CreateBDCConsumerMaker(	OpEnv<IBDCTranData, BDCProgressInfo>	opEnv		,
																													BDC2RfcParser													parser	,
																													IBDCProfile														profile		)
					{
						return	new BDCConsumerMaker(opEnv, parser, profile);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal OpEnv<IBDCTranData, BDCProgressInfo>	CreateBDCOpEnv(	int	noOfConsumers		= 01	,
																																			int	interval				= 10	,
																																			int	queueAddTimeout	= 10		)
					{
						var lo_PI		= new BDCProgressInfo();

						return	this.CreateOpEnv< IBDCTranData, BDCProgressInfo >(	lo_PI						,
																																				noOfConsumers		,
																																				interval				,
																																				queueAddTimeout		);
					}

			#endregion

		}
}
