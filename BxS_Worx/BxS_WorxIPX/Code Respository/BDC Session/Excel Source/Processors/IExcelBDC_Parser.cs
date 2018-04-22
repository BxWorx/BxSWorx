//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal interface IExcelBDC_Parser
		{
			#region "Methods: Exposed"

				void ParseWStoRequest( IExcel_WSSource ws , IExcel_WSRequest request );

			#endregion
		}
}