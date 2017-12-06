using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public class SAPNCOFactory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NCOController CreateNCOController(bool LoadSAPIni	= true)
					{
						var lo_DestRepo	= new DestinationRepository();
						//.............................................
						if (LoadSAPIni)
							{
								var	lo_SAPCnf	= SMC.SapLogonIniConfiguration.Create();
								var lo_SAPIni	= new SAPLogonINI(lo_SAPCnf);

								lo_SAPIni.LoadRepository(lo_DestRepo);
							}
						//.............................................
						var lo_DestMngr	= new DestinationManager(lo_DestRepo);

						return	new NCOController(lo_DestMngr);
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal SMC.RfcConfigParameters	Parse(IDTOConnParameters DTO)
				//	{
				//		var lo = new SMC.RfcConfigParameters();
				//		//.............................................
				//		foreach (KeyValuePair<string, string> ls_kvp in DTO.Parameters)
				//			{
				//				lo[ls_kvp.Key] = ls_kvp.Value;
				//			}
				//		//.............................................
				//		return	lo;
				//	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"
			#endregion

		}
}
