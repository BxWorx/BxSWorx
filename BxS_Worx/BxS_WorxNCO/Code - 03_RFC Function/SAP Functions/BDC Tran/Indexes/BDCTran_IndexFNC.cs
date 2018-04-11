using System;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCTran_IndexFNC
		{
			#region "Documentation"

				//	FUNCTION abap4_call_transaction.
				//	*"----------------------------------------------------------------------
				//	*"*"Lokale Schnittstelle:
				//	*"  IMPORTING
				//	*"     VALUE(TCODE)				LIKE  SY-TCODE
				//	*"     VALUE(SKIP_SCREEN)	LIKE  SY-FTYPE DEFAULT SPACE
				//	*"     VALUE(MODE_VAL)		LIKE  SY-FTYPE DEFAULT 'A'
				//	*"     VALUE(UPDATE_VAL)	LIKE  SY-FTYPE DEFAULT 'A'
				//	*"  EXPORTING
				//	*"     VALUE(SUBRC)				LIKE  SY-SUBRC
				//	*"  TABLES
				//	*"      USING_TAB					STRUCTURE  BDCDATA		OPTIONAL
				//	*"      SPAGPA_TAB				STRUCTURE  RFC_SPAGPA OPTIONAL
				//	*"      MESS_TAB					STRUCTURE  BDCMSGCOLL OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      CALL_TRANSACTION_DENIED
				//	*"      TCODE_INVALID
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTran_IndexFNC( BDCCall_Profile profile )
					{
						this._Profile		= profile;
						//.............................................
						this._TCode			= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "TCODE				"	) );
						this._Skip1			= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "SKIP_SCREEN	"	) );
						this._Mode			= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MODE_VAL		"	) );
						this._Update		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "UPDATE_VAL	"	) );
						//.............................................
						this._TabBDC		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "USING_TAB"	) );
						this._TabMSG		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESS_TAB"		) );
						this._TabSPA		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "SPAGPA_TAB"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCall_Profile		_Profile;
				//.................................................
				private	readonly	Lazy<int>		_TCode	;
				private	readonly	Lazy<int>		_Skip1	;
				private	readonly	Lazy<int>		_Mode		;
				private	readonly	Lazy<int>		_Update	;
				private	readonly	Lazy<int>		_TabBDC	;
				private	readonly	Lazy<int>		_TabMSG	;
				private	readonly	Lazy<int>		_TabSPA	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	Name			{ get { return	cz_BDCCall; } }
				//.................................................
				internal	int			TCode			{ get { return	this._Profile.IsReady ?	this._TCode	.Value	: 0	; } }
				internal	int			Skip1			{ get { return	this._Profile.IsReady ?	this._Skip1	.Value	: 0	; } }
				internal	int			Mode			{ get { return	this._Profile.IsReady ?	this._Mode.Value		: 0	; } }
				internal	int			Update		{ get { return	this._Profile.IsReady ?	this._Update.Value	: 0	; } }
				internal	int			TabBDC		{ get { return	this._Profile.IsReady ?	this._TabBDC.Value	: 0	; } }
				internal	int			TabMSG		{ get { return	this._Profile.IsReady ?	this._TabMSG.Value	: 0	; } }
				internal	int			TabSPA		{ get { return	this._Profile.IsReady ?	this._TabSPA.Value	: 0	; } }

			#endregion

		}
}
