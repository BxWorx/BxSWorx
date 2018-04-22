using System.Runtime.Serialization;
//.........................................................
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.SAPBDCSession
{
	[DataContract()]
	[KnownType( typeof(SAP_Logon)					)]
	[KnownType( typeof(ExcelBDC_Request)	)]

	public class SAP_BDCSessionRequest
		{
			#region "Properties"

				[DataMember]	public	ISAP_Logon					SAPLogon					{ get; set;	}
				[DataMember]	public	IExcelBDC_Request		ExcelBDCRequest		{ get; set;	}

			#endregion
		}
}