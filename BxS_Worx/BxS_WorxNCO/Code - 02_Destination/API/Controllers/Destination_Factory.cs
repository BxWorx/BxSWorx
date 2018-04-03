using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.Config;
using BxS_WorxNCO.Destination.Main.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public static class Destination_Factory
		{
			#region "Methods: Exposed"

				internal	static	ISAPSystemReference			CreateSAPSystemReference	()=>	new SAPSystemReference			();
				//.................................................
				public	static	SMC.RfcConfigParameters		CreateNCOConfig						()=>	new	SMC.RfcConfigParameters	();
				//.................................................
				public	static	IConfigRepository					CreateRepositoryConfig		()=>	new ConfigRepository				();
				public	static	IConfigDestination				CreateDestinationConfig		()=>	new ConfigDestination				();
				public	static	IConfigGlobal							CreateGlobalConfig				()=>	new ConfigGlobal						();
				//.................................................
				public	static	IConfigLogon							CreateLogonConfig					( bool ForRepository = false	)=>	new ConfigLogon ( ForRepository );

			#endregion

		}
}