using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSessionController
		{
			#region "Methods: Exposed"

				BDC_SessionManager	CreateBDCSessionManager( IRfcDestination	rfcDestination );

			#endregion

		}
}