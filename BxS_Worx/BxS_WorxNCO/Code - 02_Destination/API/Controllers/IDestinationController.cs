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
				void	LoadGlobalConfig( IConfigGlobal config );
				//.................................................
				IRfcDestination		GetDestination( Guid		ID );
				IRfcDestination		GetDestination( string	ID );
				//.................................................
				void	Reset();

			#endregion

		}
}