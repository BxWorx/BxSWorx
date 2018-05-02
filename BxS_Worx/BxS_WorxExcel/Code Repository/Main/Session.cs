//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal class WSData
		{
			#region "Properties"

				internal	string		WBID				{ get; set; }
				internal	string		WSID				{ get; set;	}
				internal	int				WSNo				{ get; set;	}
				internal	string		UsedAddress	{ get; set;	}
				//...
				internal	object[,]	WSCells			{ get; set;	}

			#endregion

		}
	}
