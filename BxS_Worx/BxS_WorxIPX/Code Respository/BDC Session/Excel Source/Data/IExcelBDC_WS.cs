//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcelBDC_WS
		{
			#region "Properties"

				string	WBID					{ get; set;	}
				string	WSID					{ get; set;	}
				int			WSNo					{ get; set;	}
				string	UsedAddress		{ get; set;	}
				bool		IsBDCSession	{ get; set;	}
				bool		IsActive			{ get; set;	}
				bool		IsTest				{ get; set;	}
				//.................................................
				object[,]	WSCells			{ get; set;	}

			#endregion
		}
}