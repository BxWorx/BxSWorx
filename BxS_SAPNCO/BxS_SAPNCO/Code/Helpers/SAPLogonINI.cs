using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal static class SAPLogonINI
		{
			#region "Declarations"

				private static readonly
					Lazy<SMC.SapLogonIniConfiguration>	_SAPINI		= new Lazy<SMC.SapLogonIniConfiguration>
																															(	() => SMC.SapLogonIniConfiguration.Create()
																																, LazyThreadSafetyMode.ExecutionAndPublication	);

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static SMC.RfcConfigParameters GetConfigParameters(string ID)
					{
						return	_SAPINI.Value.GetParameters(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static IList<string>	GetSAPGUIConfigEntries()
					{
						return	_SAPINI.Value.GetEntries();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static void LoadRepository(DestinationRepository destinationRepository)
					{
						string[] la_List	= _SAPINI.Value.GetEntries();
						Array.Sort(la_List);
						//.............................................
						foreach (string lc_ID in la_List)
							{
								SMC.RfcConfigParameters lo_RfcCfgParms	= _SAPINI.Value.GetParameters(lc_ID)	?? new SMC.RfcConfigParameters();
								destinationRepository.AddConfig(lc_ID, lo_RfcCfgParms);
							}
					}

			#endregion

		}
}
