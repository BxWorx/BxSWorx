using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]

	public class Request : IRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Request(		IUser				user
													,	ISAP_Logon	sapLogon	)
					{
						this.User				= user			;
						this.SAPLogon		= sapLogon	;
						//...
						this.Sessions		= new	Dictionary<Guid , ISession>()	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	Count		{ get { return	this.Sessions.Count; } }
				//.................................................
				[DataMember]	public	IUser				User			{ get; }
				[DataMember]	public	ISAP_Logon	SAPLogon	{ get; }
				//...
				[DataMember]	public	Dictionary<Guid , ISession>		Sessions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	Set_User	( IUser				user	)	=>	this.User			.Transfer	( user	)	;
				public	void	Set_Logon	( ISAP_Logon	logon	)	=>	this.SAPLogon	.Transfer	( logon )	;
				//...
				public	void	Add_Session( ISession	session )	=>	this.Sessions	.Add( session.ID , session );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Clear()	=>	this.Sessions.Clear();

			#endregion

		}
}