using BxS_SAPNCO.Common;
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCController_Funcxx
		{
			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCProfile GetAddBDCProfile(DestinationRfc destination)
					{
						IBDCProfile	lo_Profile	= null;
						//.............................................
						destination.TryGetProfile(	SAPFncConstants.BDCCallTransaction	,
																				out object lo_ProfileObj											);

						if (lo_ProfileObj == null)
							{
								lo_Profile	= new BDCFncProfile(	destination																	,
																									SAPFncConstants.BDCCallTransaction		);

								destination.RegisterProfile(lo_Profile);

								destination.TryGetProfile(	SAPFncConstants.BDCCallTransaction	,
																						out lo_ProfileObj															);
							}
						//.............................................
						return	lo_Profile;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IBDCTranProcessor CreateBDCTransactionProcessor(IBDCProfile profile)
					{
						return	new BDCTranProcessor( new RFCFunction()	, profile);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_CTUParms	CreateCTUParameters()
					{
						return	new DTO_CTUParms();
					}

			#endregion

		}
}
