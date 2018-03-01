using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.API.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Main
{
	public interface IController
		{
			#region "Methods: Exposed"

				IList<string>								GetSAPINIList();

				IList<ISAPSystemReference>	GetSAPSystems();
				IDestination								GetDestination( Guid ID );
				IDestination								GetDestination( string ID );
				//.................................................
				bool LoadSAPINI();
				void Reset();

			#endregion

		}
}