using System;
using System.Collections.Generic;
using System.Threading;
////.........................................................
using SMC	= SAP.Middleware.Connector;
//using BxS_SAPConn.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	internal class SAPLogonINI
		{
			#region "Declarations"

				private readonly	Lazy<SMC.SapLogonIniConfiguration>	_SAPCnf
														= new Lazy<SMC.SapLogonIniConfiguration>(	() => SMC.SapLogonIniConfiguration.Create()					,
																																						LazyThreadSafetyMode.ExecutionAndPublication		);

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadRepository(DestinationRepository destinationRepository)
					{
						foreach (string lc_ID in this._SAPCnf.Value.GetEntries())
							{
								SMC.RfcConfigParameters lo = this._SAPCnf.Value.GetParameters(lc_ID);
								if (lo is null)		return;
								destinationRepository.AddConfig(lc_ID, lo);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<string>	GetEntries()
					{
						return	this._SAPCnf.Value.GetEntries();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcConfigParameters GetConfig(string ID)
					{
						return	this._SAPCnf.Value.GetParameters(ID)	?? new SMC.RfcConfigParameters();
					}

			#endregion

		}
}
