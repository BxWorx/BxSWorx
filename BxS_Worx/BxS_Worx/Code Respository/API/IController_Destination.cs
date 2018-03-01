using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxDestination.API.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.API
{
	public interface IController_Destination
		{
			#region "Methods: Exposed: Destination"

				IList<ISAPSystemReference>	GetSAPSystems();
				IDestination								GetDestination( Guid ID );
				IDestination								GetDestination( string ID );

			#endregion

		}
}