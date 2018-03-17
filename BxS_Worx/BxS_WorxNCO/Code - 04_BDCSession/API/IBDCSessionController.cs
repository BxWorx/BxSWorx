using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSessionController
		{
			#region "Methods: Exposed"

				IList< ISAPSystemReference >	GetSAPSystems();

				IBDCSession	CreateBDCSession( Guid destinationID );

			#endregion

		}
}