using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.SAPBDCSession
{
	public interface ISAP_BDCRequest
		{
			#region "Properties"

				ISAP_Logon	SAPLogon		{ get; set;	}
				//...
				Dictionary< Guid , ISAP_BDCSession >	Sessions		{ get; set;	}

			#endregion
		}
}