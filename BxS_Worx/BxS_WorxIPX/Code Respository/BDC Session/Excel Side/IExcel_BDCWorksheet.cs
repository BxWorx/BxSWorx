using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDCExcel
{
	public interface IExcel_BDCWorksheet
		{
			#region "Properties"

				Guid		WSGuid				{ get; set;	}
				//.................................................
				bool		IsTest				{ get; set;	}
				bool		IsOnline			{ get; set;	}
				//.................................................
				string	WBID					{ get; set;	}
				string	WSID					{ get; set;	}
				int			WSNo					{ get; set;	}
				string	UsedAddress		{ get; set;	}
				//.................................................
				object[,]	WSCells			{ get; set;	}

				bool		IsBDCSession	{ get; set;	}
				bool		IsActive			{ get; set;	}

			#endregion
		}
}