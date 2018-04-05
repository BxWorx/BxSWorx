using System;
using System.Collections.Generic;
using System.Security;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]
	public class ExcelBDCSessionRequest : IExcelBDCSessionRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDCSessionRequest()
					{ }

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	Guid		ID								{ get; set; }
				[DataMember]	public	int			Priority					{ get; set; }
				//.................................................
				[DataMember]	public	int			IdleTimeout				{ get; set; }
				[DataMember]	public	int			IdleCheckTime			{ get; set; }
				[DataMember]	public	int			MaxPoolWaitTime		{ get; set; }
				[DataMember]	public	int			PeakConnLimit			{ get; set; }
				[DataMember]	public	int			PoolIdleTimeout		{ get; set; }
				[DataMember]	public	int			PoolSize					{ get; set; }
				[DataMember]	public	int			RepoIdleTimeout		{ get; set; }
				[DataMember]	public	bool		DoLogonCheck			{ get; set; }
				//.................................................
				[DataMember]	public	string	SAPSysID					{ get; set; }
				[DataMember]	public	string	Client						{ get; set; }
				[DataMember]	public	string	User							{ get; set; }
				[DataMember]	public	string	Lang							{ get; set; }
				[DataMember]	public	string	Pwrd							{ get; set; }

				[DataMember]	public	SecureString	SecurePwrd	{ get; set; }
				//.................................................
				[DataMember]	public	string	WBID							{ get; set; }
				[DataMember]	public	string	WSID							{ get; set;	}
				[DataMember]	public	int			WSNo							{ get; set;	}
				[DataMember]	public	string	UsedAddress				{ get; set;	}
				[DataMember]	public	bool		IsTest						{ get; set;	}
				//.................................................
				[DataMember]	public	int			RowLB							{ get; set;	}
				[DataMember]	public	int			RowUB							{ get; set;	}
				[DataMember]	public	int			ColLB							{ get; set;	}
				[DataMember]	public	int			ColUB							{ get; set;	}
				//.................................................
				[DataMember]	public	Dictionary< string , string >	WSData1D	{ get; set; }
				//.................................................
				[DataMember]	public	bool	IgnoreDestinationConfig		{ get; set; }
				[DataMember]	public	bool	IgnoreSessionConfig				{ get; set; }

			#endregion

		}
}
