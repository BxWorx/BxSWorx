using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Main
{
	public class DTO_ExcelAppManifest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_ExcelAppManifest()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	WBID					{ get; set;	}
				public	string	WSID					{ get; set;	}
				public	string	UsedAddress		{ get; set;	}
				public  bool		IsBDCSession	{ get; set;	}
				public  bool		IsActive			{ get; set;	}

			#endregion

		}
}
