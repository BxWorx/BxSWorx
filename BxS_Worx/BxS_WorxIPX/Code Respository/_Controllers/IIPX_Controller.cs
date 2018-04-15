using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				IExcelBDC_WS				CreateBDCSessionWS			();
				IExcelBDC_Request		CreateBDCSessionRequest	();
				//.............................................
				IExcelBDC_Request		ParseWStoRequest( IExcelBDC_WS ws );

				//.............................................
				//.............................................
				//.............................................
				IExcelBDCSessionResult		CreateBDCSessionResult	();
	
		#endregion

		}
}