using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal class Excel_BDCRequest : IExcel_BDCRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Excel_BDCRequest( ISAP_Logon	sapLogon )
					{
						this.SAPLogon		= sapLogon;
						//.............................................
						this.Worksheets		= new	Dictionary< Guid , IExcel_BDCWorksheet >();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	ISAP_Logon	SAPLogon		{ get; }
				//.................................................
				public	Dictionary< Guid , IExcel_BDCWorksheet >		Worksheets		{ get; }

			#endregion

		}
}