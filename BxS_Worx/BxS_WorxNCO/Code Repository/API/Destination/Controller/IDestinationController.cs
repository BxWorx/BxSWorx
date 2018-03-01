using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API.Destination
{
	public interface IDestinationController
		{
			#region "Methods: Exposed"

				int Count { get; }

				IList<ISAPSystemReference>	GetSAPSystems();

				IDestination GetDestination( Guid ID );
				IDestination GetDestination( string ID );

				bool LoadSAPINI();
				void Reset();

			#endregion

		}
}