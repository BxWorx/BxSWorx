using System;
using System.Collections.Generic;
using System.ComponentModel;
using BxS_WorxExcel.DTO;
//.........................................................
using BxS_WorxExcel.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	public class SAPSessionsVM : VMBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SAPSessionsVM()
					{
						this.List		= new	BindingList<DTO_Session>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				public	string		_user	;
				public	string		_name	;
				public	DateTime	_sdte	;
				public	DateTime	_edte	;

			#endregion

			//===========================================================================================
			#region "Properties"

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
				public	BindingList<DTO_Session>	List	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	DTO_Session CreateNew()	=>	DTO_Session.Create();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Load( DTO_Session dto )
					{
						this.List.Add( dto );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	LoadList( List<DTO_Session> list )
					{
						foreach ( DTO_Session lo_Ssn in list )
							{
								this.List.Add( lo_Ssn	);
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
