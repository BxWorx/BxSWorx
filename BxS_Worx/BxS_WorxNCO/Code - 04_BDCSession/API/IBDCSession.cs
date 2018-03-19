using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.API.BDC;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSession
		{
			#region "Methods: Exposed"

				void					ConfigureOperation		( DTO_BDC_SessionConfig		dto );
				void					ConfigureDestination	( IConfigSetupDestination	dto );
				Task< int >		Process_SessionAsync	( DTO_BDC_Session					dto );
				//.................................................
				void CancelProcessing();

			#endregion

		}
}