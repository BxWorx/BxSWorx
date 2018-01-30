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
	internal class SAPLogonINI
		{
			#region "Declarations"

				private readonly	Lazy<SMC.SapLogonIniConfiguration>	_SAPINI
														= new Lazy<SMC.SapLogonIniConfiguration>
															(	() => SMC.SapLogonIniConfiguration.Create()
																, LazyThreadSafetyMode.ExecutionAndPublication	);

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadRepository(DestinationRepository destinationRepository)
					{
						foreach (string lc_ID in this.GetEntries())
							{
								SMC.RfcConfigParameters lo = this.GetParameters(lc_ID);
								destinationRepository.AddConfig(lc_ID, lo);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<string> GetEntries()
					{
						return	this._SAPINI.Value.GetEntries();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcConfigParameters GetParameters(string ID)
					{
						return	this._SAPINI.Value.GetParameters(ID)	?? new SMC.RfcConfigParameters();
					}

			#endregion

		}
}
