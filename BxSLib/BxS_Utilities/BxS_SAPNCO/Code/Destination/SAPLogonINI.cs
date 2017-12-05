using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using BxS_SAPConn.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
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
				internal void	GetConfig(string ID, IDTOConnParameters DTO)
					{
						SMC.RfcConfigParameters lo_Cfg = this._SAPCnf.Value.GetParameters(ID);
						if (lo_Cfg == null)	return;
						//.............................................
						foreach (KeyValuePair<string, string> ls_kvp in lo_Cfg)
							{
								DTO.Parameters.Add(ls_kvp.Key, ls_kvp.Value);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<string>	GetEntries()
					{
						return	this._SAPCnf.Value.GetEntries();
					}

			#endregion

		}
}
