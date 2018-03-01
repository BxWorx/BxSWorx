using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxDestination.API.Destination;
using BxS_WorxDestination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Main
{
	public interface IController
		{
			#region "Properties"

				int LoadedSystemCount	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();
				//.................................................
				IDestination	GetDestination( Guid ID )		;
				IDestination	GetDestination( string ID )	;
				//.................................................
				void LoadGlobalConfig( IConfigSetupGlobal config );
				//.................................................
				IConfigSetupDestination		CreateDestinationConfig()	;
				IConfigSetupGlobal				CreateGlobalConfig()			;
				//.................................................
				void Reset();

			#endregion

		}
}