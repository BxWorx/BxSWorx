using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface ISAP_Logon
		{
			#region "Properties"

				string	SAPSysID	{ get; set; }
				string	Client		{ get; set; }
				string	User			{ get; set; }
				string	Lang			{ get; set; }
				string	Pwrd			{ get; set; }
				//...
				SecureString	SecurePwrd	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Transfer( ISAP_Logon sapLogon );

			#endregion

		}
}
