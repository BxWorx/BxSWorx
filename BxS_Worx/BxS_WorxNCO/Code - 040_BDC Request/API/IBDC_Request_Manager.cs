using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;

using BxS_WorxIPX.BDC;
using BxS_WorxUtil.Progress;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	public interface IBDC_Request_Manager
		{
			#region "Methods: Exposed"

				DTO_BDC_SessionConfig CreateSessionConfig();

				Task<bool>	ReadySessionAsync( bool optimise = true );

				Task<bool>	Process(	ISession														request
														,	CancellationToken										CT
														, ProgressHandler< DTO_BDC_Progress >	progressHndlr );

				Task<bool>	ProcessAsync(	IRequest												request
														,	CancellationToken										CT
														, ProgressHandler< DTO_BDC_Progress >	progressHndlr );

				void ReConfigureBDCSessionPool		();
				void ReConfigureBDCConsumerPool		();
				void ReConfigureParserPool				();
				void ReConfigureSAPMsgPool				();

			#endregion

		}
}