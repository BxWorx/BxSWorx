using BxS_WorxIPX.BDC;
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
				IExcelBDC_Config		Create_BDCConfig();
				IExcelBDC_Logon			Create_Logon();

				IExcelBDC_WS				Create_BDCWS			();
				IExcelBDC_Request		Create_BDCRequest	();
				//.............................................
				IExcelBDC_Request		ParseWStoRequest	( IExcelBDC_WS worksheet );
				bool								ExcelWStoXMLFile	( IExcelBDC_WS worksheet , string PathName );

		#endregion

		}
}