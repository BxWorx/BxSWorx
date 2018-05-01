using System.Security;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
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

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Transfer( ISAP_Logon sapLogon )
					{
						this.SAPSysID		= sapLogon.SAPSysID		;
						this.Client			= sapLogon.Client			;
						this.User				= sapLogon.User				;
						this.Lang				= sapLogon.Lang				;
						this.Pwrd				= sapLogon.Pwrd				;
						this.SecurePwrd	= sapLogon.SecurePwrd	;
					}

			#endregion

		}
}
