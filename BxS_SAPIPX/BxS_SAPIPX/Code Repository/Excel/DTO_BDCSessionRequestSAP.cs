using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Excel
{
	public class DTO_BDCSessionRequestSAP
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionRequestSAP()
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	SAPID					{ get; set; }			// SAP ID as per SAPLogonINI
				public	string	Client				{ get; set; }
				public	string	User					{ get; set; }
				public	string	Lang					{ get; set; }
				//.................................................
				public	SecureString	Pwrd		{ get; set; }

			#endregion

		}
}
