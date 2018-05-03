//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.DTO
{
	internal class DTO_Favourites
		{
			#region "Properties"

						this.List		= new	DataTable<int , ISAP_Logon>( create );
				


				internal	string		WBID				{ get; set; }
				internal	string		WSID				{ get; set;	}
				internal	int				WSNo				{ get; set;	}
				internal	string		UsedAddress	{ get; set;	}
				//...
				internal	object[,]	WSCells			{ get; set;	}

			#endregion

		}
	}
