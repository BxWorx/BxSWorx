using System;
using System.Security;
//.........................................................
using BxS_SAPIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.BDC
{
	public class DTO_SessionHeader
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionHeader( DTO_CTUParms dtoParms )
					{
						this.CTUParms	= dtoParms;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid					ID				{ get;	set;	}

				public	string				Name			{ get;	set;	}
				public	string				SAPTCode	{ get;	set;	}
				public	string				Skip1st		{ get;	set;	}
				public	DTO_CTUParms	CTUParms	{ get;	set;	}

				public	string				SAPSysID	{ get; set; }
				public	string				Client		{ get; set; }
				public	string				User			{ get; set; }
				public	string				Lang			{ get; set; }

				public	SecureString	Pwrd			{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	Reset()
						{
							this.CTUParms.SetToDefaults();
						}

			#endregion

		}
}
