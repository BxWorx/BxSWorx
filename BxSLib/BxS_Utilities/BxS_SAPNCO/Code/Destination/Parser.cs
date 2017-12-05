using System.Collections.Generic;
//.........................................................
using BxS_SAPConn.API;
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	internal class Parser
		{
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcConfigParameters	Parse(IDTOConnParameters DTO)
					{
						var lo = new SMC.RfcConfigParameters();
						//.............................................
						foreach (KeyValuePair<string, string> ls_kvp in DTO.Parameters)
							{
								lo[ls_kvp.Key] = ls_kvp.Value;
							}
						//.............................................
						return	lo;
					}

			#endregion

		}
}
