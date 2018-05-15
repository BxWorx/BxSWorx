using System;
using System.Collections.Generic;
using System.ComponentModel;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	internal class SAPSessionsVM : VMBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPSessionsVM()
					{
						this.RequestSAPSessionListEventHandler	+= this.OnRequestSAPSessionList	;
						this.ResetSAPSessionListEventHandler		+= this.OnResetSAPSessionList	;
						//.............................................
						this.List		= new	BindingList<IDTO_Session>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				public	event	Action	SuspendLayout	;
				public	event	Action	ResumeLayout	;
				//...
				public	EventHandler	RequestSAPSessionListEventHandler;
				public	EventHandler	ResetSAPSessionListEventHandler;
				//.................................................
				public	string		_user	;
				public	string		_name	;
				public	DateTime	_sdte	;
				public	DateTime	_edte	;

			#endregion

			//===========================================================================================
			#region "Properties"

				// setter injection
				//
				public IBDCx_Controller IPXBDCCntlr { get; set; }
				//.................................................
				public	string		PName_User		{	get	=>	nameof( DTO_Session.UserID				)	; }
				public	string		PName_Session	{	get	=>	nameof( DTO_Session.SessionName		)	; }
				public	string		PName_SAPTCde	{	get	=>	nameof( DTO_Session.SAPTCode			)	; }
				public	string		PName_Date		{	get	=>	nameof( DTO_Session.CreationDate	)	; }
				//...
				public	string		UserID        { get=> this._user	;		set	{ this.SetProperty( ref this._user	, value )	; } }
				public	string		SessionName		{ get=> this._name	;		set	{ this.SetProperty( ref this._name	, value )	; } }
				public	DateTime	StartDate			{ get=> this._sdte	;		set	{ this.SetProperty( ref this._sdte	, value )	; } }
				public	DateTime	EndDate				{ get=> this._edte	;		set	{ this.SetProperty( ref this._edte	, value )	; } }
				//...
				public	BindingList<IDTO_Session>	List	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	DTO_Session CreateNew()	=>	DTO_Session.Create();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Load( IDTO_Session dto )
					{
						this.List.Add( dto );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	LoadList( IList<IDTO_Session> list )
					{
						this.SuspendLayout();
						this.List.Clear();
						//...
						foreach ( IDTO_Session lo_Ssn in list )
							{
								this.List.Add( lo_Ssn	);
							}
						//...
						this.ResumeLayout();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void OnResetSAPSessionList( object sender , EventArgs e )
					{
						this.SuspendLayout();
						this.List.Clear();
						this.ResumeLayout();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void OnRequestSAPSessionList( object sender , EventArgs e )
					{
						if ( this.IPXBDCCntlr != null )
							{
								//DTO_SAPSessionRequest	R = this.IPXBDCCntlr.CreateSAPSessionListRequest();
								//// TO-DO: populate the request
								//this.LoadList( this.IPXBDCCntlr.RequestSAPSessionList( R ) );
							}
					}

			#endregion

		}
}
