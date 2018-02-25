using System.Collections.Generic;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Excel
{
	public class DTO_SessionRequestRaw
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_SessionRequestRaw()
					{
						this.WSData	= new	Dictionary< string , string >();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	SAPID					{ get; set; }
				public	string	Client				{ get; set; }
				public	string	User					{ get; set; }
				public	string	Lang					{ get; set; }

				public	string	WBID					{ get; set; }
				public	string	WSID					{ get; set;	}
				public	string	WSNo					{ get; set;	}
				public	string	UsedAddress		{ get; set;	}
				//.................................................
				public	int			RowLB	{ get; set;	}
				public	int			RowUB	{ get; set;	}
				public	int			ColLB	{ get; set;	}
				public	int			ColUB	{ get; set;	}

				public	Dictionary< string , string >	WSData	{ get; set; }
				public	SecureString									Pwrd		{ get; set; }

			#endregion

		}
}
