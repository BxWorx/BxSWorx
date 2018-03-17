using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.API.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSession
		{
			#region "Methods: Exposed"

				void				ConfigureOperation		( DTO_BDC_SessionConfig	dto );
				Task<bool>	Process_SessionAsync	( DTO_BDC_Session				dto );

				void CancelProcessing();

			#endregion

		}
}