using BxS_WorxIPX.BDC;
using BxS_WorxIPX.SAPBDCSession;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				IBDCRequestManager	Create_BDCRequestManager();

				//IExcelBDCSessionResult	CreateBDCSessionResult	();
				//IExcelBDC_Config				Create_BDCConfig	();
				//.............................................
				//.............................................

				//.............................................
				//ISAP_Logon	Create_SAPLogon()	;
				//.............................................
				//ISAP_BDCRequest		Create_SAPBDCRequest()	;
				//ISAP_BDCSession		Create_SAPBDCSession()	;
				//.............................................
				//IExcel_BDCRequest			Create_ExcelBDCRequest()		;
				//IExcel_BDCWorksheet		Create_ExcelBDCWorksheet()	;
				//.............................................
				//ISAP_BDCRequest		ParseRequest		( IExcel_BDCRequest request );
				//ISAP_BDCRequest		ReadBDCRequest	( string pathName );
				//void							WriteBDCRequest	( ISAP_BDCRequest	request , string pathName );

				//.............................................
				//.............................................
				//.............................................

				//.............................................

		#endregion

		}
}