using System;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Internal: BDC Processing: Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline<IBDCTranData, BDCProgressInfo>	CreateBDCPipelineXX(	Guid	destinationID					,
																																							int		noOfConsumers		= 01	,
																																							int		interval				= 10	,
																																							int		queueAddTimeout	= 10		)
					{
						Destination.DestinationRfc	lo_Dest		= this.CreateDestinationRFC						(destinationID)	;
						IBDCProfile									lo_Prof		= this.GetAddBDCTranProcessorProfile	(lo_Dest)				;
						BDC2RfcParser								lo_Pars		=	this.CreateBDC2RfcParser						(lo_Prof)				;
						//.............................................
						var lo_PI		= new BDCProgressInfo();
						OpEnv<IBDCTranData, BDCProgressInfo>	lo_OE	=	this.CreateOpEnv<IBDCTranData, BDCProgressInfo>( lo_PI						,
																																																													noOfConsumers		,
																																																													interval				,
																																																													queueAddTimeout		);
						var lo_CM		= new BDCConsumerMaker(lo_OE, lo_Pars, lo_Prof);
						//.............................................
						return	new Pipeline<	IBDCTranData, BDCProgressInfo> ( lo_OE, lo_CM );
					}

			#endregion

		}
}
