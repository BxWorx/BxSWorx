using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class Excel_WSSource : IExcel_WSSource
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Excel_WSSource()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid		WSGuid				{ get; set;	}
				//.................................................
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
