using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Main
{
	public class DTO_ExcelAppManifest
		{
			#region "Properties"

				public	string	WBID					{ get; set;	}
				public	string	WSID					{ get; set;	}
				public	string	UsedAddress		{ get; set;	}
				public  bool		IsBDCSession	{ get; set;	}
				public  bool		IsActive			{ get; set;	}

			#endregion

		}
}
