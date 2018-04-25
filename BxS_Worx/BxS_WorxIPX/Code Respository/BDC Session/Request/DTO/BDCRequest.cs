using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]

	public class BDCRequest : IBDCRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCRequest(	IBDCUser		user
														,	ISAP_Logon	sapLogon )
					{
						this.User				= user			;
						this.SAPLogon		= sapLogon	;
						//...
						this.Sessions		= new	Dictionary<Guid , IBDCSession>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	IBDCUser			User			{ get; }
				[DataMember]	public	ISAP_Logon		SAPLogon	{ get; }
				//...
				[DataMember]	public	Dictionary<Guid , IBDCSession>		Sessions { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Set_User	( IBDCUser		user	)	=>	this.User			.Transfer	( user	)	;
				public void Set_Logon	( ISAP_Logon	logon	)	=>	this.SAPLogon	.Transfer	( logon )	;
				//...
				public void Add_BDCSession( IBDCSession	session )	=>	this.Sessions	.Add( session.ID , session );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Clear()	=>	this.Sessions.Clear();

			#endregion
		}
}