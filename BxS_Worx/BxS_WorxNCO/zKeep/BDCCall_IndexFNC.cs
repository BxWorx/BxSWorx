using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexFNC : RfcFunctionIndex
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

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexFNC()
					{
						this._TCode			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IF_TCODE"							) );
						this._Skip1			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	) );
						this._CTUOpt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IS_OPTIONS"						) );

						this._TabBDC		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_BDCCall	) );
						this._TabMSG		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_MSGCall	) );
						this._TabSPA		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_SPACall	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_TCode	;
				private	readonly	Lazy<int>		_Skip1	;
				private	readonly	Lazy<int>		_CTUOpt	;
				private	readonly	Lazy<int>		_TabBDC	;
				private	readonly	Lazy<int>		_TabMSG	;
				private	readonly	Lazy<int>		_TabSPA	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int			TCode			{ get { return	this.IsLoaded	?	this._TCode	.Value : cz_Neg	; } }
				internal	int			Skip1			{ get { return	this.IsLoaded	?	this._Skip1	.Value : cz_Neg	; } }
				internal	int			CTUOpt		{ get { return	this.IsLoaded	?	this._CTUOpt.Value : cz_Neg	; } }
				internal	int			TabBDC		{ get { return	this.IsLoaded	?	this._TabBDC.Value : cz_Neg	; } }
				internal	int			TabMSG		{ get { return	this.IsLoaded	?	this._TabMSG.Value : cz_Neg	; } }
				internal	int			TabSPA		{ get { return	this.IsLoaded	?	this._TabSPA.Value : cz_Neg	; } }

			#endregion

		}
}
