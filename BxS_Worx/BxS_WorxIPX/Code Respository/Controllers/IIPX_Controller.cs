using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				IExcelBDCSession_Parser		CreateBDCSessionParser	();
				IExcelBDCSessionWS				CreateBDCSessionWS			();
				IExcelBDCSessionRequest		CreateBDCSessionRequest	();
				IExcelBDCSessionResult		CreateBDCSessionResult	();

			#endregion

		}
}