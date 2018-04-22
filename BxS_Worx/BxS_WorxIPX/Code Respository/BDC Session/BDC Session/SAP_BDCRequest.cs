using System.Runtime.Serialization;
//.........................................................
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.SAPBDCSession
{
	[DataContract()]
	[KnownType( typeof(SAP_Logon)					)]
	[KnownType( typeof(Excel_WSRequest)	)]

	public class SAP_BDCRequest : ISAP_BDCRequest
		{
			#region "Properties"

				[DataMember]	public	ISAP_Logon					SAPLogon					{ get; set;	}
				[DataMember]	public	IExcel_WSRequest		ExcelBDCRequest		{ get; set;	}

			#endregion
		}
}