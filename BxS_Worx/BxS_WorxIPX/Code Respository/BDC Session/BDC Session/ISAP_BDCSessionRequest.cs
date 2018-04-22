using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.SAPBDCSession
{
	public interface ISAP_BDCSessionRequest
		{
			#region "Properties"

				ISAP_Logon				SAPLogon					{ get; set;	}
				IExcelBDC_Request	ExcelBDCRequest		{ get; set;	}

			#endregion
		}
}