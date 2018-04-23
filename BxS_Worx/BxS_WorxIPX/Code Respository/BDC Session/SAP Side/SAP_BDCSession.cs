using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]

	public class SAP_BDCSession : ISAP_BDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAP_BDCSession()
					{
						this.WSData1D	= new	Dictionary<string, string>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	Guid		ID								{ get; set; }
				[DataMember]	public	int			Priority					{ get; set; }
				//.................................................
				[DataMember]	public	bool		IsTest						{ get; set;	}
				[DataMember]	public	bool		IsOnline					{ get; set;	}
				//.................................................
				[DataMember]	public	string	WBID							{ get; set; }
				[DataMember]	public	string	WSID							{ get; set;	}
				[DataMember]	public	int			WSNo							{ get; set;	}
				[DataMember]	public	string	UsedAddress				{ get; set;	}
				//.................................................
				[DataMember]	public	bool		IsActive					{ get; set;	}
				[DataMember]	public	bool		IsBDCSession			{ get; set;	}
				//.................................................
				[DataMember]	public	int			RowLB							{ get; set;	}
				[DataMember]	public	int			RowUB							{ get; set;	}
				[DataMember]	public	int			ColLB							{ get; set;	}
				[DataMember]	public	int			ColUB							{ get; set;	}
				//.................................................
				[DataMember]	public	Dictionary< string , string >	WSData1D	{ get; set; }

			#endregion

		}
}
