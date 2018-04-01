using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.BDCSession.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSessionController
		{
			#region "Methods: Exposed"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();

				IConfigDestination				CreateDestinationConfig();
				//.................................................
				BDC_SessionManager				CreateBDCSessionManager( string	destinationID );
				BDC_SessionManager				CreateBDCSessionManager( Guid		destinationID );

		#endregion

		}
}