//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.DTO
{
	internal class DTO_WSData
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
