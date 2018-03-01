using System;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.Destination
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
				SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				void LoadConfig( IConfigSetupDestination	Config );
				void LoadConfig( IConfigSetupGlobal				Config );

			#endregion

		}
}