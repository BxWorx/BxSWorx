using BxS_WorxIPX.BDC;
using BxS_WorxIPX.SAPBDCSession;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				IExcelBDCSessionResult		CreateBDCSessionResult	();

				//.............................................
				//.............................................

				//.............................................
				IExcelBDC_Config		Create_BDCConfig	();
				ISAP_Logon					Create_Logon			();

				ISAP_BDCSessionRequest	Create_SAPBDCSessionRequest();

				IExcelBDC_WS				Create_ExcelBDCWS				();
				IExcelBDC_Request		Create_ExcelBDCRequest	();
				//.............................................
				IExcelBDC_Request		ParseWStoRequest						( IExcelBDC_WS worksheet );
				bool								ExcelBDCWStoRequestXMLFile	( IExcelBDC_WS worksheet , string pathName );
				IExcelBDC_Request		XMLFiletoExcelBDCRequest		( string pathName );

		#endregion

		}
}