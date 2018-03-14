using System;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API.Destination
{
	public interface IRfcDestination
		{
			#region "Properties"

				Guid		SAPGUIID		{ get; }
				//.................................................
				string	Client			{ set; }
				string	Language		{ set; }
				string	User				{ set; }
				string	Password		{ set; }
				bool		LogonCheck	{ set; }
				//.................................................
				SecureString SecurePassword	{ set; }
				//.................................................
				bool	IsConnected		{ get; }
				//.................................................
				SMC.RfcCustomDestination	NCODestination	{ get; }
				SMC.RfcRepository					NCORepository		{ get; }

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