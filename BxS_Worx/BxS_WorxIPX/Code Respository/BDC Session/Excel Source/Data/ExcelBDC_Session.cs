using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal class ExcelBDC_Session : IExcelBDC_Session
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDC_Session( ISAP_Logon	sapLogon )
					{
						this.SAPLogon		= sapLogon;
						//.............................................
						this.Worksheets		= new	Dictionary< Guid , IExcelBDC_WS >();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	ISAP_Logon	SAPLogon		{ get; }
				//.................................................
				public	Dictionary< Guid , IExcelBDC_WS >		Worksheets		{ get; }

			#endregion

		}
}