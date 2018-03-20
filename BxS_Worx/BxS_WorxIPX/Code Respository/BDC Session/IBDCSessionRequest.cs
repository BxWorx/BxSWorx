using System;
using System.Collections.Generic;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public interface IBDCSessionRequest
		{
			#region "Properties"

				Guid ID { get; set; }
				//.................................................
				int			IdleTimeout				{ get; set; }
				int			IdleCheckTime			{ get; set; }
				int			MaxPoolWaitTime		{ get; set; }
				int			PeakConnLimit			{ get; set; }
				int			PoolIdleTimeout		{ get; set; }
				int			PoolSize					{ get; set; }
				int			RepoIdleTimeout		{ get; set; }
				bool		DoLogonCheck			{ get; set; }
				//.................................................
				string	SAPSysID					{ get; set; }
				string	Client						{ get; set; }
				string	User							{ get; set; }
				string	Lang							{ get; set; }
				string	Pwrd							{ get; set; }

				SecureString	SecurePwrd	{ get; set; }
				//.................................................
				string	WBID							{ get; set; }
				string	WSID							{ get; set;	}
				string	WSNo							{ get; set;	}
				string	UsedAddress				{ get; set;	}
				bool		IsTest						{ get; set;	}
				//.................................................
				int			RowLB							{ get; set;	}
				int			RowUB							{ get; set;	}
				int			ColLB							{ get; set;	}
				int			ColUB							{ get; set;	}
				//.................................................
				Dictionary< string , string >	WSData1D	{ get; set; }

			#endregion

		}
}
