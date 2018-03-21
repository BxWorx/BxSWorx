using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class ExcelBDCSessionResult : IExcelBDCSessionResult
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDCSessionResult()
					{ }

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid		ID		{ get; set; }
				//.................................................
				public	string	WBID	{ get; set; }
				public	string	WSID	{ get; set;	}
				public	string	WSNo	{ get; set;	}

			#endregion

		}
}
