using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO

{
	public interface ISAP_Logon
		{
			#region "Properties"

				bool    IsSAPINI	{ get; set; }
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
