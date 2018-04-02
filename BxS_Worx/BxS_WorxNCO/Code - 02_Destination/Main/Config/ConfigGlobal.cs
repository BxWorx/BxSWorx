using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Config
{
	internal class ConfigGlobal : IConfigGlobal
		{
			#region "Properties"

				public	string	SNCLibPath	{ get; set; }

			#endregion

		}
}
