using System;
using System.Collections.Generic;
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
				ISTDDestination		GetDestination( Guid		ID );
				ISTDDestination		GetDestination( string	ID );
				IREPDestination		GetRepDestination( Guid		ID );
				IREPDestination		GetRepDestination( string	ID );
				//.................................................
				void	LoadGlobalConfig( IConfigGlobal config );
				//.................................................
				void	Reset();

			#endregion

		}
}