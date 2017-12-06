using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	internal class DestinationManager
		{
			#region "Constructors"

				internal DestinationManager(DestinationRepository repository)
					{
						this._Repos	= repository;
						SDM.RegisterDestinationConfiguration(this._Repos);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly DestinationRepository	_Repos;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool IsRegistered	{ get { return	SDM.IsDestinationConfigurationRegistered(); } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcDestination	GetRfcDestination(string destinationName)
					{
						return	SDM.GetDestination(destinationName);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.RfcDestination	GetRfcDestination(SMC.RfcConfigParameters RfcConfig)
					{
						return	SDM.GetDestination(RfcConfig);
					}

			#endregion

		}
}
