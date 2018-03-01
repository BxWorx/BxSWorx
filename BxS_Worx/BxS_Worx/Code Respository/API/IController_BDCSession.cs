using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.API.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.API
{
	public interface IController_BDCSession
		{
			#region "Methods: Exposed: BDCSession"

				Task<IBDCSessionResult> ProcessBDCSessionAsync( IBDCSessionRequest BDCSessionRequest );

				void CancelBDCSession();

			#endregion

		}
}