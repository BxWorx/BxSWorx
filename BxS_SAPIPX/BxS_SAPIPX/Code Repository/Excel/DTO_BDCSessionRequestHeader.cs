using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Excel
{
	public class DTO_BDCSessionRequestHeader
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionRequestHeader()
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	WBID					{ get; set; }
				public	string	WSID					{ get; set;	}
				public	string	WSNo					{ get; set;	}
				public	string	RangeAddress	{ get; set;	}
				//.................................................
				public	SecureString	Pwrd		{ get; set; }
				//.................................................
				public	string	SAPID					{ get; set; }
				public	string	Client				{ get; set; }
				public	string	User					{ get; set; }
				public	string	Lang					{ get; set; }

			#endregion

		}
}
