using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_IndexFNC : RfcFunctionIndex
		{
			#region "Documentation"

				//	FUNCTION /isdfps/call_transaction.
				//	*"----------------------------------------------------------------------
				//	*"  IMPORTING
				//	*"     VALUE(IF_TCODE)							TYPE	TCODE
				//	*"     VALUE(IF_SKIP_FIRST_SCREEN)	TYPE	FLAG DEFAULT SPACE
				//	*"     VALUE(IT_BDCDATA)						TYPE	BDCDATA_TAB OPTIONAL
				//	*"     VALUE(IS_OPTIONS)						TYPE	CTU_PARAMS OPTIONAL
				//	*"  EXPORTING
				//	*"     VALUE(ET_MSG)								TYPE	ETTCD_MSG_TABTYPE
				//	*"  TABLES
				//	*"      CT_SETGET_PARAMETER					STRUCTURE	RFC_SPAGPA OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      IMPORT_PARA_ERROR
				//	*"      TCODE_ERROR
				//	*"      AUTH_ERROR
				//	*"      TRANS_ERROR
				//	*"----------------------------------------------------------------------

				// ****************************************************************************************
				// ****************************************************************************************

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
				internal BDC_IndexFNC( bool useTranVersion )
					{
						this._TrnVers	= useTranVersion;
						//.............................................
						this._TCode		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex(	this._TrnVers	?	cz_TCdTran : cz_TCdCall ) );
						this._Skip1		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex(	this._TrnVers	?	cz_SkpTran : cz_SkpCall ) );

						this._TabBDC	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( this._TrnVers	?	cz_BDCTran : cz_BDCCall	) );
						this._TabMSG	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( this._TrnVers	?	cz_MSGTran : cz_MSGCall	) );
						this._TabSPA	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( this._TrnVers	?	cz_SPATran : cz_SPACall	) );
						//.............................................
						this._Mode			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_MdeTran ) );
						this._Update		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_UpdTran ) );
						this._CTUOpt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_CTUCall ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	bool	_TrnVers	;
				//.................................................
				private	readonly	Lazy<int>		_TCode	;
				private	readonly	Lazy<int>		_Skip1	;
				private	readonly	Lazy<int>		_TabBDC	;
				private	readonly	Lazy<int>		_TabMSG	;
				private	readonly	Lazy<int>		_TabSPA	;

				private	readonly	Lazy<int>		_CTUOpt	;
				private	readonly	Lazy<int>		_Mode		;
				private	readonly	Lazy<int>		_Update	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TCode			{ get { return	this.IsLoaded	?	this._TCode	.Value	: cz_Neg	; } }
				internal	int	Skip1			{ get { return	this.IsLoaded	?	this._Skip1	.Value	: cz_Neg	; } }
				internal	int	TabBDC		{ get { return	this.IsLoaded	?	this._TabBDC.Value	: cz_Neg	; } }
				internal	int	TabMSG		{ get { return	this.IsLoaded	?	this._TabMSG.Value	: cz_Neg	; } }
				internal	int	TabSPA		{ get { return	this.IsLoaded	?	this._TabSPA.Value	: cz_Neg	; } }

				internal	int	CTUOpt		{ get { return	this.IsLoaded	?	this._CTUOpt.Value	: cz_Neg	; } }
				internal	int	Mode			{ get { return	this.IsLoaded	?	this._Mode	.Value	: cz_Neg	; } }
				internal	int	Update		{ get { return	this.IsLoaded	?	this._Update.Value	: cz_Neg	; } }

			#endregion

		}
}
