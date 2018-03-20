using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSessionController
		{
			#region "Methods: Exposed"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();
				IConfigSetupDestination				CreateDestinationConfig();
				//.................................................
				DTO_BDC_Session					CreateSessionDTO();
				DTO_BDC_SessionConfig		CreateSessionConfig();

				IBDCSession		CreateBDCSession( string	destinationID );
				IBDCSession		CreateBDCSession( Guid		destinationID );



				
				DTO_BDC_Session ParseRequest( IBDCSessionRequest bdcSessionRequest );

			#endregion

		}
}