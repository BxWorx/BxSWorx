using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcel_BDCRequest
		{
			#region "Properties"

				ISAP_Logon	SAPLogon	{ get; }
				//.................................................
				Dictionary<Guid , IExcel_BDCWorksheet>	Worksheets	{ get; }

			#endregion
		}
}