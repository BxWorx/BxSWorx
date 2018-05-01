using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
//.........................................................
using BxS_WorxIPX.NCO;
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
						this.Sessions		= new	Dictionary<int , ISession>()	;
						this._Index			= 0;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	int _Index;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	Count		{ get { return	this.Sessions.Count; } }
				//.................................................
				[DataMember]	public	IUser						User			{ get; set; }
				[DataMember]	public	ISAP_Logon			SAPLogon	{ get; set; }
				[DataMember]	public	IRequest_Config	Config		{ get; set; }
				//...
				[DataMember]	public	Dictionary<int , ISession>	Sessions	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	Set_User		( IUser				user	)				=>	this.User			.CopyPropertiesFrom	( user )		;
				public	void	Set_Logon		( ISAP_Logon	logon	)				=>	this.SAPLogon	.CopyPropertiesFrom	( logon )		;
				public	void	Set_Config	( IRequest_Config	config )	=>	this.Config		.CopyPropertiesFrom	( config )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	Add_Session	( ISession	session )
					{
						Interlocked.Increment( ref	this._Index )		;
						this.Sessions.Add( this._Index , session )	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	Clear()	=>	this.Sessions.Clear();
				public	void	Sync()	=>	Interlocked.Exchange( ref	this._Index	, this.Sessions.Count );

			#endregion

		}
}