using System;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
using BxS_SAPNCO.API.SAPFunctions.BDC.Session;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Internal: BDC Processing: Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBDCSession CreateBDCSession(string destinationID)
					{
						Guid lg_ID	= this._DestRepos.Value.GetAddIDFor(destinationID);
						//.............................................
						return	this.CreateBDCSession(lg_ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBDCSession CreateBDCSession(Guid destinationID)
					{
						BDCOpEnv<DTO_SessionProgressInfo> lo_OE	= this.CreateBDCOpEnv(destinationID);
						//.............................................
						var lo_SO	= new DTO_SessionOptions();

						var lo_SH = new DTO_BDCSessionHeader	{	CTUOptions = new DTO_CTUOptions()	};
						//.............................................
						return	new BDCSession(	lo_OE, lo_SO, lo_SH	);
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal Pipeline<IBDCTranData, BDCProgressInfo>	CreateBDCPipelineXX(	Guid	destinationID					,
				//																																			int		noOfConsumers		= 01	,
				//																																			int		interval				= 10	,
				//																																			int		queueAddTimeout	= 10		)
				//	{
				//		Destination.DestinationRfc	lo_Dest		= this.CreateDestinationRFC						(destinationID)	;
				//		IBDCProfile									lo_Prof		= this.GetAddBDCTranProcessorProfile	(lo_Dest)				;
				//		BDC2RfcParser								lo_Pars		=	this.CreateBDC2RfcParser						(lo_Prof)				;
				//		//.............................................
				//		var lo_PI		= new BDCProgressInfo();
				//		OpEnv<IBDCTranData, BDCProgressInfo>	lo_OE	=	this.CreateOpEnv<IBDCTranData, BDCProgressInfo>( lo_PI						,
				//																																																									noOfConsumers		,
				//																																																									interval				,
				//																																																									queueAddTimeout		);
				//		var lo_CM		= new BDCConsumerMaker(lo_OE, lo_Pars, lo_Prof);
				//		//.............................................
				//		return	new Pipeline<	IBDCTranData, BDCProgressInfo> ( lo_OE, lo_CM );
				//	}

			#endregion

		}
}
