using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.API.BDC;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDCSession
		{
			#region "Properties"

				int TransactionsProcessed { get; }
				//.................................................
				ConcurrentQueue< Task<int> >	TasksCompleted	{ get; }
				ConcurrentQueue< Task<int> >	TasksFaulty			{ get; }
				ConcurrentQueue< Task<int> >	TasksOther			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	ConfigureOperation	( DTO_BDC_SessionConfig		dto );
				void	ConfigureDestination( IConfigSetupDestination	dto );
				//.................................................
				Task<DTO_BDC_Session>	Parse_SessionAsync		(		IBDCSessionRequest	bdcRequest );
				Task<int>							Process_SessionAsync	(		DTO_BDC_Session			bdcSession );
				Task<int>							Process_SessionAsync	(		IBDCSessionRequest	bdcRequest
																											, bool								ignoreDestinationConfig	= true
																											, bool								ignoreSessionConfig			= true );
				//.................................................
				void CancelProcessing();

			#endregion

		}
}