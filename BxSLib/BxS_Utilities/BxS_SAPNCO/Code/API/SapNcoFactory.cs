using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPConn.API;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	internal class SAPNCOFactory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Controller CreateNCOController(bool LoadSAPIni	= true)
					{
						var lo_DestRepo	= new DestinationRepository();
						//.............................................
						if (LoadSAPIni)
							{
								var lo_SAPIni	= new SAPLogonINI();
								lo_SAPIni.LoadRepository(lo_DestRepo);
							}
						//.............................................
						var lo_DestMngr	= new DestinationManager(lo_DestRepo);
					}


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
