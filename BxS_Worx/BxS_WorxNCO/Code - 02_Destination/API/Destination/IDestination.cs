using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IDestination
		{
			#region "Properties"

				Guid		MyID				{ get; }
				Guid		SAPGUIID		{ get; }
				//.................................................
				SMC.RfcCustomDestination	NCODestination	{ get; }
				SMC.RfcRepository					NCORepository		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Rfc Functions"

				void RegisterRfcFunctionForMetadata( string fncName );
				void FetchMetadata();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				void LoadConfig( SMC.RfcConfigParameters	config );
				void LoadConfig( IConfigLogon							config );
				void LoadConfig( IConfigDestination				config );
				void LoadConfig( IConfigGlobal						config );

			#endregion

		}
}