//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcelBDCSession_Parser
		{
			#region "Methods: Exposed"

				void ParseWStoRequest( IExcelBDCSessionWS ws , IExcelBDCSessionRequest request );

			#endregion
		}
}