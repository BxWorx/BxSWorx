using System.Collections.Generic;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public interface IBDCSessionResult
		{
			#region "Properties"

				string	SAPSysID			{ get; set; }
				string	Client				{ get; set; }
				string	User					{ get; set; }
				string	Lang					{ get; set; }

				string	WBID					{ get; set; }
				string	WSID					{ get; set;	}
				string	WSNo					{ get; set;	}
				string	UsedAddress		{ get; set;	}
				bool		IsTest				{ get; set;	}
				//.................................................
				int			RowLB	{ get; set;	}
				int			RowUB	{ get; set;	}
				int			ColLB	{ get; set;	}
				int			ColUB	{ get; set;	}

				Dictionary< string , string >	WSData1D	{ get; set; }
				SecureString									Pwrd			{ get; set; }

			#endregion

		}
}
