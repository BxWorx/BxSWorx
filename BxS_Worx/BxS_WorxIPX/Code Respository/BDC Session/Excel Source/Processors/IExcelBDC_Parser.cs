//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal interface IExcelBDC_Parser
		{
			#region "Methods: Exposed"

				void ParseWStoRequest( IExcelBDC_WS ws , IExcelBDC_Request request );

			#endregion
		}
}