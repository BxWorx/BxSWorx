using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcelBDCSessionResult
		{
			#region "Properties"

				Guid		ID		{ get; set; }
				//.................................................
				string	WBID	{ get; set; }
				string	WSID	{ get; set;	}
				string	WSNo	{ get; set;	}

			#endregion

		}
}
