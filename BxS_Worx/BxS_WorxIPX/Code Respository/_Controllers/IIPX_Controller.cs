using BxS_WorxIPX.BDC;
using BxS_WorxIPX.SAPBDCSession;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				IExcelBDCSessionResult	CreateBDCSessionResult	();
				IExcelBDC_Config		Create_BDCConfig	();

				//.............................................
				ISAP_Logon					Create_SAPLogon				();
				//.............................................
				ISAP_BDCRequest			Create_SAPBDCRequest();
				//.............................................
				IExcel_WSSource			Create_ExcelWSSource	();
				IExcel_WSRequest		Create_ExcelWSRequest	();
				//...
				IExcel_BDCRequest		Create_ExcelBDCRequest();
				//.............................................
				IExcel_WSRequest	ParseWStoRequest			( IExcel_WSSource worksheet );
				IExcel_BDCRequest	ReadExcelBDCRequest		( string pathName );
				void							WriteExcelBDCRequest	( IExcel_BDCRequest request , string pathName );

		#endregion

		}
}