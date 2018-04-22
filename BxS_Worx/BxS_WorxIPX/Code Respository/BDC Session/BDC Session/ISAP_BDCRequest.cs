using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.SAPBDCSession
{
	public interface ISAP_BDCRequest
		{
			#region "Properties"

				ISAP_Logon				SAPLogon					{ get; set;	}
				IExcel_WSRequest	ExcelBDCRequest		{ get; set;	}

			#endregion
		}
}