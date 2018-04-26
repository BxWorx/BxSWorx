using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface ISession
		{
			#region "Properties"

				Guid		ID							{ get; set; }
				int			Priority				{ get; set; }
				//.................................................
				bool		IsTest					{ get; set;	}
				bool		IsOnline				{ get; set;	}
				//.................................................
				string	WBID						{ get; set; }
				string	WSID						{ get; set;	}
				int			WSNo						{ get; set;	}
				string	UsedAddress			{ get; set;	}
				//.................................................
				bool		IsActive				{ get; set;	}
				bool		IsBDCSession		{ get; set;	}
				//.................................................
				int			RowLB						{ get; set;	}
				int			RowUB						{ get; set;	}
				int			ColLB						{ get; set;	}
				int			ColUB						{ get; set;	}
				//.................................................
				IXMLConfig										XMLConfig		{ get; set;	}
				Dictionary< string , string >	WSStore			{ get; set; }
				object[,]											WSCells			{ get; set;	}
				string[,]											WSData			{ get; set;	}

			#endregion

		}
}
