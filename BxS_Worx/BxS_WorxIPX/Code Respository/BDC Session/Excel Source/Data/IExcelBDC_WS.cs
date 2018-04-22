//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcelBDC_WS
		{
			#region "Properties"

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