using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Main
{
	public class DTO_WBWSManifest
		{
			//#region "Constructors"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal DTO_WBWSManifest()
			//		{
			//		}

			//#endregion

			//===========================================================================================
			#region "Properties"

				public	string	WBID					{ get;	set;	}
				public	string	WSID					{ get;	set;	}
				public	string	UsedAddress		{ get;	set;	}

			#endregion

		}
}
