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
				bool LoadRfcFunctionProfileMetadata( IRfcFncProfile profile );
				bool LoadRfcFunction( IRfcFncBase rfcFunction );

				bool LoadFunctionIndexing	<T>	( T obj ) where T:class;
				bool LoadStructureIndexing<T>	( T obj ) where T:class;
				//.................................................
				SMC.IRfcStructure		CreateRfcStructure( string strName );
				SMC.IRfcTable				CreateRfcTable		( string strName );
				SMC.IRfcFunction		CreateRfcFunction	( string fncName );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				SMC.RfcConfigParameters	  CreateNCOConfig()					;
				IConfigSetupDestination		CreateDestinationConfig()	;
				IConfigSetupGlobal				CreateGlobalConfig()			;

				void LoadConfig( SMC.RfcConfigParameters	config );
				void LoadConfig( IConfigSetupDestination	config );
				void LoadConfig( IConfigSetupGlobal				config );

			#endregion

		}
}