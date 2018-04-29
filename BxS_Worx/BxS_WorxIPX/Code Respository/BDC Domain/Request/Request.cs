using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//.........................................................
using BxS_WorxIPX.Toolset;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	[DataContract()]

	public class Request : IRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Request(		IUser						user
													,	ISAP_Logon			sapLogon
													, IRequest_Config	config		)
					{
						this.User				= user			;
						this.SAPLogon		= sapLogon	;
						this.Config			= config		;
						//...
						this.Sessions		= new	Dictionary<Guid , ISession>()	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	Count		{ get { return	this.Sessions.Count; } }
				//.................................................
				[DataMember]	public	IUser						User			{ get; set; }
				[DataMember]	public	ISAP_Logon			SAPLogon	{ get; set; }
				[DataMember]	public	IRequest_Config	Config		{ get; set; }
				//...
				[DataMember]	public	Dictionary<Guid , ISession>		Sessions	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	Set_User		( IUser				user	)				=>	this.User			.CopyPropertiesFrom	( user )		;
				public	void	Set_Logon		( ISAP_Logon	logon	)				=>	this.SAPLogon	.CopyPropertiesFrom	( logon )		;
				public	void	Set_Config	( IRequest_Config	config )	=>	this.Config		.CopyPropertiesFrom	( config )	;
				//...
				public	void	Add_Session( ISession	session )	=>	this.Sessions	.Add( session.ID , session );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Clear()	=>	this.Sessions.Clear();

			#endregion

		}
}