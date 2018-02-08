using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Exposed: Destination"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOConfigSetupDestination CreateConfigSetupDestination()
					{
						return	new	DTOConfigSetupDestination();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc CreateDestinationRFC(Guid ID)
					{
						this.Startup();
						//.............................................
						SMC.RfcConfigParameters	lo_rfcConfig	=	this._DestRepos.Value.GetParameters(ID);
						return	new DestinationRfc(lo_rfcConfig)	{	SAPGUIID = ID	};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DestinationRfc CreateDestinationRFC(string ID)
					{
						this.Startup();
						//.............................................
						Guid lg = this._DestRepos.Value.GetAddIDFor(ID);
						//.............................................
						return	CreateDestinationRFC(lg);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ProcureDestination(DestinationRfc DestinationRFC)
					{
						bool lb_Ret	= true;
						//.............................................
						DestinationRFC.LoadConfig(this._GlobalSetup.Value);

						try
							{
								DestinationRFC.RfcDestination	= SDM.GetDestination(DestinationRFC.RfcConfig);
							}
						catch (Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
