using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public class NCOFactory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NCOController CreateNCOController(bool LoadSAPIni	= true, bool AutoRegister = true)
					{
						return	new NCOController(LoadSAPIni, AutoRegister);
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
