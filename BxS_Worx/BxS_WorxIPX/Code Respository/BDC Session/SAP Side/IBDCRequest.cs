using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IBDCRequest
		{
			#region "Properties"

				IBDCUser		User			{ get; set;	}
				ISAP_Logon	SAPLogon	{ get; set;	}
				//...
				Dictionary< Guid , IBDCSession >	Sessions		{ get; set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Clear();

			#endregion
		}
}