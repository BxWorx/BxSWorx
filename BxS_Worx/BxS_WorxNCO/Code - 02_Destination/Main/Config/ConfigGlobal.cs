using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigGlobal : ConfigBase , IConfigGlobal
		{
			#region "Properties"

				public	string	SNCLibPath	{ set { this.Set( SMC.RfcConfigParameters.SncLibraryPath , value ); } }

			#endregion

		}
}
