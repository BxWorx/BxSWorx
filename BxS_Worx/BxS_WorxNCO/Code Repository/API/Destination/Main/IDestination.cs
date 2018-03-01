using System;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API.Destination
{
	public interface IDestination
		{
			#region "Properties"

				bool	IsConnected	{ get; }
				//.................................................
				Guid											SAPGUIID					{ get; }

				SMC.RfcDestination				NCORfcDestination	{ get; }
				SMC.RfcConfigParameters		NCORfcConfig			{ get; }
				//.................................................
				string Client			{ set; }
				string Language		{ set; }
				string User				{ set; }
				string Password		{ set; }

				SecureString SecurePassword	{ set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				void LoadConfig( SMC.RfcConfigParameters	Config );
				void LoadConfig( IConfigSetupDestination	Config );
				void LoadConfig( IConfigSetupBase					Config );
				void LoadConfig( IConfigSetupGlobal				Config );

			#endregion

		}
}