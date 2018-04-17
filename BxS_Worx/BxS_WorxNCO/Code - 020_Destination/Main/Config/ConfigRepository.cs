using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigRepository : ConfigBase , IConfigRepository
		{
			#region "Properties"

				public	int IdleTimeout		{ set { this.Set( SMC.RfcConfigParameters.RepositoryConnectionIdleTimeout , value.ToString() ); } }

			#endregion

		}
}
