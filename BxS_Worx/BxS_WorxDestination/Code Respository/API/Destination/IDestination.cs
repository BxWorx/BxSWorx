using System;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxDestination.API.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.API.Destination
{
	public interface IDestination
		{
			#region "Properties"

				Guid		SAPGUIID		{ get; }
				//.................................................
				string	Client			{ set; }
				string	Language		{ set; }
				string	User				{ set; }
				string	Password		{ set; }
				//.................................................
				SecureString SecurePassword	{ set; }
				//.................................................
				bool	IsConnected		{ get; }
				//.................................................
				SMC.RfcDestination	NCODestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				bool Procure();
				//.................................................
				void LoadConfig( SMC.RfcConfigParameters	config );
				void LoadConfig( IConfigSetupDestination	config );
				void LoadConfig( IConfigSetupGlobal				config );

			#endregion

		}
}