using System;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	public class WSConfigVM : VMBase
		{
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

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Guid					GUID				{ get	=> this._guid				;		set	{ this.SetProperty( ref this._guid			, value ); } }
				public	bool					Active			{ get	=> this._active			;		set	{ this.SetProperty( ref this._active		, value ); } }
				public	bool					Protected		{ get	=> this._protected	;		set	{ this.SetProperty( ref this._protected	, value ); } }

				public	string				SAPTCode		{ get	=> this._saptcode		;		set	{ this.SetProperty( ref this._saptcode	, value ); } }
				public	string				SessionID		{ get	=> this._sessionid	;		set	{ this.SetProperty( ref this._sessionid	, value ); } }
				public	int						PauseTime		{ get	=> this._pausetime	;		set	{ this.SetProperty( ref this._pausetime	, value ); } }
				public	bool					Skip1st			{ get	=> this._skip1st		;		set	{ this.SetProperty( ref this._skip1st		, value ); } }

				public	string				DisMode			{ get	=> this._disMode		;		set	{ this.SetProperty( ref this._disMode		, value ); } }
				public	char					UpdMode			{ get	=> this._updMode		;		set	{ this.SetProperty( ref this._updMode		, value ); } }
				public	bool					DefSize			{ get	=> this._defSize		;		set	{ this.SetProperty( ref this._defSize		, value ); } }

				public	SecureString	Password		{ get	=> this._password		;		set	{ this.SetProperty( ref this._password	, value ); } }

				public	int						DataRow			{ get	=> this._pausetime	;		set	{ this.SetProperty( ref this._pausetime	, value ); } }
				public	string				DataCol			{ get	=> this._saptcode		;		set	{ this.SetProperty( ref this._saptcode	, value ); } }

				public	string				Col_ID			{ get	=> this._colid			;		set	{ this.SetProperty( ref this._colid			, value ); } }
				public	string				Col_Active	{ get	=> this._colactive	;		set	{ this.SetProperty( ref this._colactive	, value ); } }
				public	string				Col_Exec		{ get	=> this._colexec		;		set	{ this.SetProperty( ref this._colexec		, value ); } }
				public	string				Col_Msg			{ get	=> this._colmsg			;		set	{ this.SetProperty( ref this._colmsg		, value ); } }

			#endregion

		}
	}
