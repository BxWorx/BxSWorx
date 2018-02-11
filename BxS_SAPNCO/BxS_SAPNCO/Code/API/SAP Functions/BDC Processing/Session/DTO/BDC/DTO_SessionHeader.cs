using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class DTO_SessionHeader
		{
			#region "Properties"

				public	string				SAPTCode	{ get;	set;	}
				public	string				Skip1st		{ get;	set;	}
				public	DTO_CTUParms	CTUParms	{ get;	set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void	Reset()
						{
							this.CTUParms.SetToDefaults();
						}

			#endregion

		}
}
