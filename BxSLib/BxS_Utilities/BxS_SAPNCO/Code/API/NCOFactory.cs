//using System;
//using System.Threading;
//using SMC	= SAP.Middleware.Connector;
//.........................................................
//using BxS_SAPNCO.Destination;
//using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public class NCOFactory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NCOController CreateNCOController(	bool	LoadSAPGUIConfig	= true	,
																									bool	FirstReset				= false		)
					{
						return	new NCOController(LoadSAPGUIConfig, FirstReset);
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
			#region "Methods: Private"
			#endregion

		}
}
