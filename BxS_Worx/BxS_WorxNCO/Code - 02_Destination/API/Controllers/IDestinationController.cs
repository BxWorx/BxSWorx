using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	internal interface IDestinationController
		{
			#region "Properties"

				int LoadedSystemCount	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();
				//.................................................
				IRfcDestination	GetDestination( Guid ID		);
				IRfcDestination	GetDestination( string ID );
				//.................................................
				void LoadGlobalConfig( IConfigGlobal config );
				//.................................................
				SMC.RfcConfigParameters		CreateNCOConfig()					;
				IConfigDestination				CreateDestinationConfig()	;
				IConfigGlobal							CreateGlobalConfig()			;

				IConfigLogon							CreateLogonConfig( bool ForRepository = false )	;
				//.................................................
				void Reset();

			#endregion

		}
}