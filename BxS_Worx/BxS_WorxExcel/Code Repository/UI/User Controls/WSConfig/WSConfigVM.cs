using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	public class WSConfigVM : VMBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public WSConfigVM()
					{
						this.CTUDspList		= new	List<CTU>();
						this.CTUUpdList		= new List<CTU>();
						//...
						this.LoadCTU();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	Guid		_guid				;
				private	bool		_active			;
				private	bool		_protected	;

				private	string	_saptcode		;
				private	string	_sessionid	;
				private	int			_pausetime	;
				private	bool		_skip1st		;

				private	string	_disMode		;
				private	char		_updMode		;
				private	bool		_defSize		;

				private	string	_colid			;
				private	string	_colactive	;
				private	string	_colexec		;
				private	string	_colmsg			;

				private	SecureString	_password	;
				//...
				private readonly DataSourceUpdateMode _DSMode	= DataSourceUpdateMode.OnPropertyChanged;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//.................................................
				// ID Section
				//
				public	Guid					GUID				{ get	=> this._guid				;		set	{ this.SetProperty( ref this._guid			, value ); } }
				public	bool					Active			{ get	=> this._active			;		set	{ this.SetProperty( ref this._active		, value ); } }

				//.................................................
				// SAP Section
				//
				public	string				SessionID		{ get	=> this._sessionid	;		set	{ this.SetProperty( ref this._sessionid	, value ); } }
				public	string				SAPTCode		{ get	=> this._saptcode		;		set	{ this.SetProperty( ref this._saptcode	, value ); } }
				public	int						PauseTime		{ get	=> this._pausetime	;		set	{ this.SetProperty( ref this._pausetime	, value ); } }
				public	bool					Skip1st			{ get	=> this._skip1st		;		set	{ this.SetProperty( ref this._skip1st		, value ); } }
				//...
				public	string				DisMode			{ get	=> this._disMode		;		set	{ this.SetProperty( ref this._disMode		, value ); } }
				public	char					UpdMode			{ get	=> this._updMode		;		set	{ this.SetProperty( ref this._updMode		, value ); } }
				public	bool					DefSize			{ get	=> this._defSize		;		set	{ this.SetProperty( ref this._defSize		, value ); } }

				//.................................................
				// WS Section
				//
				public	bool					Protected		{ get	=> this._protected	;		set	{ this.SetProperty( ref this._protected	, value ); } }
				public	SecureString	Password		{ get	=> this._password		;		set	{ this.SetProperty( ref this._password	, value ); } }
				//...
				public	string				Col_ID			{ get	=> this._colid			;		set	{ this.SetProperty( ref this._colid			, value ); } }
				public	string				Col_Active	{ get	=> this._colactive	;		set	{ this.SetProperty( ref this._colactive	, value ); } }
				public	string				Col_Exec		{ get	=> this._colexec		;		set	{ this.SetProperty( ref this._colexec		, value ); } }
				public	string				Col_Msg			{ get	=> this._colmsg			;		set	{ this.SetProperty( ref this._colmsg		, value ); } }
				//...
				public	int						DataRow			{ get	=> this._pausetime	;		set	{ this.SetProperty( ref this._pausetime	, value ); } }
				public	string				DataCol			{ get	=> this._saptcode		;		set	{ this.SetProperty( ref this._saptcode	, value ); } }
				//.................................................
				// UI Section
				//
				public	List<CTU>		CTUDspList	{ get; }
				public	List<CTU>		CTUUpdList	{ get; }
				//...
				public	string			DisplayMem	{ get	=>	nameof(CTU.Desc)				; }
				public	string			ValueMem		{ get	=>	nameof(CTU.ReturnValue)	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//... ID Section
				public Binding	BindGUID			( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.GUID		) );
				public Binding	BindIsActive	( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.Active	) );

				//... SAP Section
				public Binding	BindSessionID		( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.SessionID	) );
				public Binding	BindSAPTCode		( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.SAPTCode	) );
				public Binding	BindPauseTime		( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.PauseTime	) );
				public Binding	BindSkip1st			( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.Skip1st		) );

				public Binding	BindCTUDispList	( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.DisMode ) );
				public Binding	BindCTUUpdtList	( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.UpdMode ) );
				public Binding	BindDefltSize		( string vwCntlrProperty )	=> this.CreateBinding( vwCntlrProperty , nameof( this.DefSize ) );

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	Binding CreateBinding( string vwName , string vmName )
					{
						return	new	Binding(	vwName , this	, vmName , true , this._DSMode );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadCTU()
					{
						//	CTU_MODE:	A	Display all screens
						//						E	Display Errors
						//						N	Background processing
						//						P	Background processing; debugging possible
						//...
						//	CTU_UPDATE:	L	Local
						//							S	Synchronous
						//							A	Asynchronous
						//.............................................
						//...
						this.CTUDspList.Add( CTU.Create( "Display all screens"							, "A" ) );
						this.CTUDspList.Add( CTU.Create( "Display Errors"										, "E" ) );
						this.CTUDspList.Add( CTU.Create( "Background processing"						, "N" ) );
						this.CTUDspList.Add( CTU.Create( "Background processing; debugging"	, "P" ) );
						//.............................................
						this.CTUUpdList.Add( CTU.Create( "Local"				, "L"	)	);
						this.CTUUpdList.Add( CTU.Create( "Synchronous"	, "S" ) );
						this.CTUUpdList.Add( CTU.Create( "Asynchronous"	, "A" ) );
					}

			#endregion

			//===========================================================================================
			#region "Private Models"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public struct CTU
					{
						public	string	Desc					{ get; set; }
						public	string	ReturnValue		{ get; set; }

						public static CTU Create( string desc , string retVal )	=>	new	CTU { Desc	= desc , ReturnValue = retVal };
					}

			#endregion

		}
}
