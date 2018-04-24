using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.ExcelBDC
{
	public interface IExcel_BDCRequest
		{
			#region "Properties"

				ISAP_Logon	SAPLogon	{ get; }
				//.................................................
				Dictionary<Guid , IExcel_BDCWorksheet>	Worksheets	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Clear();

			#endregion

		}
}