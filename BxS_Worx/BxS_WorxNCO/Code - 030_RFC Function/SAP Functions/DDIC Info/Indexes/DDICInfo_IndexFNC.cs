using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	internal class DDICInfo_IndexFNC : RfcFunctionIndex
		{
			#region "Documentation"

				//	function ddif_fieldinfo_get.
				//	*"----------------------------------------------------------------------
				//	*"*"Lokale Schnittstelle:
				//	*"  IMPORTING
				//	*"     VALUE(TABNAME)				TYPE  DDOBJNAME
				//	*"     VALUE(FIELDNAME)			TYPE  DFIES-FIELDNAME		DEFAULT ' '
				//	*"     VALUE(LANGU)					TYPE  SY-LANGU					DEFAULT SY-LANGU
				//	*"     VALUE(LFIELDNAME)		TYPE  DFIES-LFIELDNAME	DEFAULT ' '
				//	*"     VALUE(ALL_TYPES)			TYPE  DDBOOL_D					DEFAULT ' '
				//	*"     VALUE(GROUP_NAMES)		TYPE  DDBOOL_D					DEFAULT ' '
				//	*"     VALUE(UCLEN)					TYPE  UNICODELG					OPTIONAL
				//	*"     VALUE(DO_NOT_WRITE)	TYPE  DDBOOL_D					DEFAULT ' '
				//	*"  EXPORTING
				//	*"     VALUE(X030L_WA)			TYPE  X030L
				//	*"     VALUE(DDOBJTYPE)			TYPE  DD02V-TABCLASS
				//	*"     VALUE(DFIES_WA)			TYPE  DFIES
				//	*"     VALUE(LINES_DESCR)		TYPE  DDTYPELIST
				//	*"  TABLES
				//	*"      DFIES_TAB						STRUCTURE DFIES				OPTIONAL
				//	*"      FIXED_VALUES				TYPE			DDFIXVALUES OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      NOT_FOUND
				//	*"      INTERNAL_ERROR
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_IndexFNC()
					{
						this._TblNm		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TABNAME"				) );
						this._FldNm		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIELDNAME"			) );
						this._Langu		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "LANGU"					) );
						this._LFldN		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "LFIELDNAME"		) );
						this._AllTp		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "ALL_TYPES"			) );
						this._GrpNm		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "GROUP_NAMES"		) );
						this._UCLen		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "UCLEN"					) );
						this._NoWrt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DO_NOT_WRITE"	) );
						this._X03wa		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "X030L_WA"			) );
						this._DDObj		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DDOBJTYPE"			) );
						this._DFSWa		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DFIES_WA"			) );
						this._LnDes		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "LINES_DESCR"		) );
						this._DFIES		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DFIES_TAB"			) );
						this._Fixed		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIXED_VALUES"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_TblNm	;
				private	readonly	Lazy<int>		_FldNm	;
				private	readonly	Lazy<int>		_Langu	;
				private	readonly	Lazy<int>		_LFldN	;
				private	readonly	Lazy<int>		_AllTp	;
				private	readonly	Lazy<int>		_GrpNm	;
				private	readonly	Lazy<int>		_UCLen	;
				private	readonly	Lazy<int>		_NoWrt	;
				private	readonly	Lazy<int>		_X03wa	;
				private	readonly	Lazy<int>		_DDObj	;
				private	readonly	Lazy<int>		_DFSWa	;
				private	readonly	Lazy<int>		_LnDes	;
				private	readonly	Lazy<int>		_DFIES	;
				private	readonly	Lazy<int>		_Fixed	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		TblNm		{ get { return	this.IsLoaded	?	this._TblNm.Value : cz_Neg	; } }
				internal	int		FldNm		{ get { return	this.IsLoaded	?	this._FldNm.Value : cz_Neg	; } }
				internal	int		Langu		{ get { return	this.IsLoaded	?	this._Langu.Value : cz_Neg	; } }
				internal	int		LFldN		{ get { return	this.IsLoaded	?	this._LFldN.Value : cz_Neg	; } }
				internal	int		AllTp		{ get { return	this.IsLoaded	?	this._AllTp.Value : cz_Neg	; } }
				internal	int		GrpNm		{ get { return	this.IsLoaded	?	this._GrpNm.Value : cz_Neg	; } }
				internal	int		UCLen		{ get { return	this.IsLoaded	?	this._UCLen.Value : cz_Neg	; } }
				internal	int		NoWrt		{ get { return	this.IsLoaded	?	this._NoWrt.Value : cz_Neg	; } }
				internal	int		X03wa		{ get { return	this.IsLoaded	?	this._X03wa.Value : cz_Neg	; } }
				internal	int		DDObj		{ get { return	this.IsLoaded	?	this._DDObj.Value : cz_Neg	; } }
				internal	int		DFsWa		{ get { return	this.IsLoaded	?	this._DFSWa.Value : cz_Neg	; } }
				internal	int		LnDes		{ get { return	this.IsLoaded	?	this._LnDes.Value : cz_Neg	; } }
				internal	int		DFIES		{ get { return	this.IsLoaded	?	this._DFIES.Value : cz_Neg	; } }
				internal	int		Fixed		{ get { return	this.IsLoaded	?	this._Fixed.Value : cz_Neg	; } }

			#endregion

		}
}
