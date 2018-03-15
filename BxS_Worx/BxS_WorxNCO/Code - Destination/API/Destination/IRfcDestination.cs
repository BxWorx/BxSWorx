using System;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API.Config;
using BxS_WorxNCO.RfcFunction.Common;
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
				string	UseSAPGui		{ set; }
				bool		LogonCheck	{ set; }
				//.................................................
				SecureString SecurePassword	{ set; }
				//.................................................
				bool	OptimiseMetadataFetch	{ get; set; }
				//.................................................
				bool	IsConnected		{ get; }
				//.................................................
				SMC.RfcCustomDestination	NCODestination	{ get; }
				SMC.RfcRepository					NCORepository		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Rfc Functions"

				void RegisterRfcFunctionForMetadata( string fncName , bool triggerFetch = false );
				bool LoadRfcFunctionProfileMetadata( IRfcFncProfile lo_Prof );
				//.................................................
				SMC.IRfcStructure	CreateRfcStructure( string strName );
				SMC.IRfcTable			CreateRfcTable		( string strName );
				SMC.IRfcFunction	CreateRfcFunction	( string fncName );
				//bool ProcureX();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				void LoadConfig( SMC.RfcConfigParameters	config );
				void LoadConfig( IConfigSetupDestination	config );
				void LoadConfig( IConfigSetupGlobal				config );

			#endregion

		}
}