using System;
using System.Collections.Generic;
using BxS_WorxDestination.API.Destination;
//.........................................................
using BxS_WorxIPX.API.Destination;
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