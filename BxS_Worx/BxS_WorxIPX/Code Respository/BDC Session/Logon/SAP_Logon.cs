using System.Security;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]
	public class SAP_Logon : ISAP_Logon
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAP_Logon()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	string	SAPSysID					{ get; set; }
				[DataMember]	public	string	Client						{ get; set; }
				[DataMember]	public	string	User							{ get; set; }
				[DataMember]	public	string	Lang							{ get; set; }
				[DataMember]	public	string	Pwrd							{ get; set; }

				[DataMember]	public	SecureString	SecurePwrd	{ get; set; }

			#endregion

		}
}
