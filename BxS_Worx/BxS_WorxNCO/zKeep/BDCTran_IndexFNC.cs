using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCTran_IndexFNC : RfcFunctionIndex
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
				internal BDCTran_IndexFNC()
					{
						this._TCode			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TCODE"				) );
						this._Skip1			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "SKIP_SCREEN"	) );
						this._Mode			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MODE_VAL"		) );
						this._Update		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "UPDATE_VAL"	) );
						//.............................................
						this._TabBDC		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_BDCTran	) );
						this._TabMSG		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_MSGTran	) );
						this._TabSPA		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_SPATran	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

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

				internal	int	TCode			{ get { return	this.IsLoaded	?	this._TCode	.Value	: cz_Neg	; } }
				internal	int	Skip1			{ get { return	this.IsLoaded	?	this._Skip1	.Value	: cz_Neg	; } }
				internal	int	Mode			{ get { return	this.IsLoaded	?	this._Mode	.Value	: cz_Neg	; } }
				internal	int	Update		{ get { return	this.IsLoaded	?	this._Update.Value	: cz_Neg	; } }
				internal	int	TabBDC		{ get { return	this.IsLoaded	?	this._TabBDC.Value	: cz_Neg	; } }
				internal	int	TabMSG		{ get { return	this.IsLoaded	?	this._TabMSG.Value	: cz_Neg	; } }
				internal	int	TabSPA		{ get { return	this.IsLoaded	?	this._TabSPA.Value	: cz_Neg	; } }

			#endregion

		}
}
