using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Excel
{
	[DataContract()]
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

				[DataMember]	public	string	SAPID					{ get; set; }
				[DataMember]	public	string	Client				{ get; set; }
				[DataMember]	public	string	User					{ get; set; }
				[DataMember]	public	string	Lang					{ get; set; }

				[DataMember]	public	string	WBID					{ get; set; }
				[DataMember]	public	string	WSID					{ get; set;	}
				[DataMember]	public	string	WSNo					{ get; set;	}
				[DataMember]	public	string	UsedAddress		{ get; set;	}
				//.................................................
				[DataMember]	public	int			RowLB	{ get; set;	}
				[DataMember]	public	int			RowUB	{ get; set;	}
				[DataMember]	public	int			ColLB	{ get; set;	}
				[DataMember]	public	int			ColUB	{ get; set;	}

				[DataMember]	public	Dictionary< string , string >	WSData	{ get; set; }
				[DataMember]	public	SecureString									Pwrd			{ get; set; }
				//.................................................

			#endregion

		}
}
