//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class ExcelBDC_WS : IExcelBDC_WS
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDC_WS()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public  bool		IsTest				{ get; set;	}
				public  bool		IsOnline			{ get; set;	}
				//.................................................
				public	string	WBID					{ get; set;	}
				public	string	WSID					{ get; set;	}
				public	int			WSNo					{ get; set;	}
				public	string	UsedAddress		{ get; set;	}
				//.................................................
				public	object[,]		WSCells		{ get; set;	}

				public  bool		IsBDCSession	{ get; set;	}
				public  bool		IsActive			{ get; set;	}

			#endregion

		}
}
