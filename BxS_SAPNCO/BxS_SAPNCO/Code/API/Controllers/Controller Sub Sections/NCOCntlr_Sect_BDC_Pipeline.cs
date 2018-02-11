using BxS_SAPNCO.API.SAPFunctions.BDC;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Internal: BDC Processing: Pipeline"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline<IBDCTranData, DTO_SessionProgressInfo>	CreateBDCPipeline(OpEnv<IBDCTranData, DTO_SessionProgressInfo>	opEnv							,
																																						BDCConsumerMaker											bdcConsumerMaker		)
					{
						return	new Pipeline<	IBDCTranData, DTO_SessionProgressInfo>(	opEnv	,
																																	bdcConsumerMaker	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCConsumerMaker	CreateBDCConsumerMaker(	OpEnv<IBDCTranData, DTO_SessionProgressInfo>	opEnv		,
																													BDC2RfcParser													parser	,
																													IBDCProfile														profile		)
					{
						return	new BDCConsumerMaker(opEnv, parser, profile);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal OpEnv<IBDCTranData, DTO_SessionProgressInfo>	CreateBDCOpEnv(	int	noOfConsumers		= 01	,
																																			int	interval				= 10	,
																																			int	queueAddTimeout	= 10		)
					{
						var lo_PI		= new DTO_SessionProgressInfo();

						return	this.CreateOpEnv< IBDCTranData, DTO_SessionProgressInfo >(	lo_PI						,
																																				noOfConsumers		,
																																				interval				,
																																				queueAddTimeout		);
					}

			#endregion

		}
}
