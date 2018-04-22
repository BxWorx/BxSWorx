using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal interface IExcelBDC_Session
		{
			#region "Properties"

				ISAP_Logon		SAPLogon		{ get; }
				//.................................................
				Dictionary< Guid , IExcelBDC_WS >		Worksheets		{ get; }

			#endregion
		}
}