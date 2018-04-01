using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public static class Destination_Factory
		{
			#region "Methods: Exposed"

				public static	SMC.RfcConfigParameters		CreateNCOConfig					()=>	new	SMC.RfcConfigParameters	();
				public static	IConfigDestination				CreateDestinationConfig	()=>	new ConfigDestination				();
				public static	IConfigGlobal							CreateGlobalConfig			()=>	new ConfigGlobal						();

				public static	IConfigLogon							CreateLogonConfig				( bool ForRepository = false )=>	new ConfigLogon ( ForRepository );

			#endregion

		}
}