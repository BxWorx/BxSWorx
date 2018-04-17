using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.Config;
using BxS_WorxNCO.Destination.Main.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	internal static class Destination_Factory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	ISAPSystemReference			CreateSAPSystemReference	(		Guid		id		= default(Guid)
																																							, string	name	= default(string)
																																							, bool		isSSO = false						)
					{
						return	new SAPSystemReference( id , name	, isSSO );
					}
				//.................................................
				internal	static	SMC.RfcConfigParameters		CreateNCOConfig						()=>	new	SMC.RfcConfigParameters	();
				//.................................................
				internal	static	IConfigRepository					CreateRepositoryConfig		()=>	new ConfigRepository				();
				internal	static	IConfigDestination				CreateDestinationConfig		()=>	new ConfigDestination				();
				internal	static	IConfigGlobal							CreateGlobalConfig				()=>	new ConfigGlobal						();
				//.................................................
				internal	static	IConfigLogon							CreateLogonConfig					( bool ForRepository = false )=>	new ConfigLogon ( ForRepository );

			#endregion

		}
}