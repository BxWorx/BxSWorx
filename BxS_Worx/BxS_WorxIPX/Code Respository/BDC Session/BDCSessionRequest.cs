using System;
using System.Collections.Generic;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public class BDCSessionRequest : IBDCSessionRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionRequest()
					{ }

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid ID { get; set; }
				//.................................................
				public	int			IdleTimeout				{ get; set; }
				public	int			IdleCheckTime			{ get; set; }
				public	int			MaxPoolWaitTime		{ get; set; }
				public	int			PeakConnLimit			{ get; set; }
				public	int			PoolIdleTimeout		{ get; set; }
				public	int			PoolSize					{ get; set; }
				public	int			RepoIdleTimeout		{ get; set; }
				public	bool		DoLogonCheck			{ get; set; }
				//.................................................
				public	string	SAPSysID					{ get; set; }
				public	string	Client						{ get; set; }
				public	string	User							{ get; set; }
				public	string	Lang							{ get; set; }
				public	string	Pwrd							{ get; set; }

				public	SecureString	SecurePwrd	{ get; set; }
				//.................................................
				public	string	WBID							{ get; set; }
				public	string	WSID							{ get; set;	}
				public	string	WSNo							{ get; set;	}
				public	string	UsedAddress				{ get; set;	}
				public	bool		IsTest						{ get; set;	}
				//.................................................
				public	int			RowLB							{ get; set;	}
				public	int			RowUB							{ get; set;	}
				public	int			ColLB							{ get; set;	}
				public	int			ColUB							{ get; set;	}
				//.................................................
				public	Dictionary< string , string >	WSData1D	{ get; set; }

			#endregion

		}
}
