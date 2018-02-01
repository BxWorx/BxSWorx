using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPConn.API;
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal class Parser
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcConfigParameters Parse(IDTOConnParameters DTO)
					{
						var lo_RfcCnfParms	= new SMC.RfcConfigParameters();
						//.............................................
						foreach (KeyValuePair<string, string> ls_kvp in DTO.Parameters)
							{
								lo_RfcCnfParms[ls_kvp.Key] = ls_kvp.Value;
							}
						//.............................................
						return	lo_RfcCnfParms;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal void LoadConfig(IDTOConfigSetupBase DTOBase, SMC.RfcConfigParameters rfcConfig)
				//	{
				//		foreach (KeyValuePair<string, string> ls_kvp in DTOBase.Settings)
				//			{
				//				rfcConfig[ls_kvp.Key]	= ls_kvp.Value;
				//			}
				//	}

			#endregion

		}
}
