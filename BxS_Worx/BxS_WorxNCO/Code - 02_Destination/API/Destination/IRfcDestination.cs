using System;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IRfcDestination
		{
			#region "Properties"

				Guid		MyID				{ get; }
				Guid		SAPGUIID		{ get; }
				//.................................................
				string	Client			{ set; }
				string	Language		{ set; }
				string	User				{ set; }
				string	Password		{ set; }
				string	UseSAPGui		{ set; }
				bool		ShowSAPGui	{ set; }

				SecureString SecurePassword	{ set; }
				//.................................................
				bool	OptimiseMetadataFetch	{ set; }
				bool	LogonCheck						{ set; }
				//.................................................
				bool	IsConnected		{ get; }
				//.................................................
				SMC.RfcCustomDestination	NCODestination	{ get; }
				SMC.RfcRepository					NCORepository		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Rfc Functions"

				void RegisterRfcFunctionForMetadata( string fncName , bool loadMetaData = false );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				SMC.RfcConfigParameters	  CreateNCOConfig()		;

				IConfigLogon				CreateLogonConfig()				;
				IConfigDestination	CreateDestinationConfig()	;
				IConfigGlobal				CreateGlobalConfig()			;
				//.................................................
				void LoadConfig( SMC.RfcConfigParameters	config );

				void LoadConfig( IConfigLogon							config );
				void LoadConfig( IConfigDestination				config );
				void LoadConfig( IConfigGlobal						config );

			#endregion

		}
}