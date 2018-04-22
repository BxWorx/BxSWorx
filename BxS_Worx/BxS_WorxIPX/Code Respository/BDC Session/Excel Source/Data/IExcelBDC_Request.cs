using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IExcelBDC_Request
		{
			#region "Properties"

				Guid		ID								{ get; set; }
				int			Priority					{ get; set; }
				//.................................................
				bool		IsTest						{ get; set;	}
				bool		IsOnline					{ get; set;	}
				//.................................................
				string	WBID							{ get; set; }
				string	WSID							{ get; set;	}
				int			WSNo							{ get; set;	}
				string	UsedAddress				{ get; set;	}
				//.................................................
				bool		IsActive					{ get; set;	}
				bool		IsBDCSession			{ get; set;	}
				//.................................................
				string	XMLConfig					{ get; set; }
				int			RowLB							{ get; set;	}
				int			RowUB							{ get; set;	}
				int			ColLB							{ get; set;	}
				int			ColUB							{ get; set;	}
				//.................................................
				Dictionary< string , string >	WSData1D	{ get; set; }

			#endregion

		}
}
