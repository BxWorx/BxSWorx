using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
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
				//	*"      DFIES_TAB						STRUCTURE  DFIES OPTIONAL
				//	*"      FIXED_VALUES				TYPE  DDFIXVALUES OPTIONAL
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
						this._DFsWa		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DFIES_WA"			) );
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
				private	readonly	Lazy<int>		_DFsWa	;
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
				internal	int		DFsWa		{ get { return	this.IsLoaded	?	this._DFsWa.Value : cz_Neg	; } }
				internal	int		LnDes		{ get { return	this.IsLoaded	?	this._LnDes.Value : cz_Neg	; } }
				internal	int		DFIES		{ get { return	this.IsLoaded	?	this._DFIES.Value : cz_Neg	; } }
				internal	int		Fixed		{ get { return	this.IsLoaded	?	this._Fixed.Value : cz_Neg	; } }

			#endregion

		}
}


DFIES                          Active
DD Interface: Table Fields for DDIF_FIELDINFO_GET


TABNAME	1 Types	TABNAME	CHAR	30	0	Table Name
FIELDNAME	1 Types	FIELDNAME	CHAR	30	0	Field Name
LANGU	1 Types	DDLANGUAGE	LANG	1	0	Language Key
POSITION	1 Types	TABFDPOS	NUMC	4	0	Position of the field in the table
OFFSET	1 Types	DOFFSET	NUMC	6	0	Offset of a field
DOMNAME	1 Types	DOMNAME	CHAR	30	0	Domain name
ROLLNAME	1 Types	ROLLNAME	CHAR	30	0	Data element (semantic domain)
CHECKTABLE	1 Types	TABNAME	CHAR	30	0	Table Name
LENG	1 Types	DDLENG	NUMC	6	0	Length (No. of Characters)
INTLEN	1 Types	INTLEN	NUMC	6	0	Internal Length in Bytes
OUTPUTLEN	1 Types	OUTPUTLEN	NUMC	6	0	Output Length
DECIMALS	1 Types	DECIMALS	NUMC	6	0	Number of Decimal Places
DATATYPE	1 Types	DYNPTYPE	CHAR	4	0	ABAP/4 Dictionary: Screen data type for Screen Painter
INTTYPE	1 Types	INTTYPE	CHAR	1	0	ABAP data type (C,D,N,...)
REFTABLE	1 Types	REFTABLE	CHAR	30	0	Table for reference field
REFFIELD	1 Types	REFFIELD	CHAR	30	0	Reference field for currency and qty fields
PRECFIELD	1 Types	PRECFIELD	CHAR	30	0	Name of included table
AUTHORID	1 Types	AUTHORID	CHAR	3	0	Authorization class
MEMORYID	1 Types	MEMORYID	CHAR	20	0	Set/Get parameter ID
LOGFLAG	1 Types	LOGFLAG	CHAR	1	0	Indicator for writing change documents
MASK	1 Types	AS4MASK	CHAR	20	0	Template (not used)
MASKLEN	1 Types	MASKLEN	NUMC	4	0	Template length (not used)
CONVEXIT	1 Types	CONVEXIT	CHAR	5	0	Conversion Routine
HEADLEN	1 Types	HEADLEN	NUMC	2	0	Maximum length of heading
SCRLEN1	1 Types	SCRLEN_S	NUMC	2	0	Max. length for short field label
SCRLEN2	1 Types	SCRLEN_M	NUMC	2	0	Max. length for medium field label
SCRLEN3	1 Types	SCRLEN_L	NUMC	2	0	Max. length for long field label
FIELDTEXT	1 Types	AS4TEXT	CHAR	60	0	Short Description of Repository Objects
REPTEXT	1 Types	REPTEXT	CHAR	55	0	Heading
SCRTEXT_S	1 Types	SCRTEXT_S	CHAR	10	0	Short Field Label
SCRTEXT_M	1 Types	SCRTEXT_M	CHAR	20	0	Medium Field Label
SCRTEXT_L	1 Types	SCRTEXT_L	CHAR	40	0	Long Field Label
KEYFLAG	1 Types	KEYFLAG	CHAR	1	0	Identifies a key field of a table
LOWERCASE	1 Types	LOWERCASE	CHAR	1	0	Lowercase letters allowed/not allowed
MAC	1 Types	DDSHATTACH	CHAR	1	0	Flag if search help is attached to the field
GENKEY	1 Types	AS4FLAG	CHAR	1	0	Flag (X or Blank)
NOFORKEY	1 Types	AS4FLAG	CHAR	1	0	Flag (X or Blank)
VALEXI	1 Types	VALEXI	CHAR	1	0	Existence of fixed values
NOAUTHCH	1 Types	AS4FLAG	CHAR	1	0	Flag (X or Blank)
SIGN	1 Types	SIGNFLAG	CHAR	1	0	Flag for sign in numerical fields
DYNPFLD	1 Types	DYNPROFLD	CHAR	1	0	Flag: field to be displayed on the screen
F4AVAILABL	1 Types	DDF4AVAIL	CHAR	1	0	Does the field have an input help
COMPTYPE	1 Types	COMPTYPE	CHAR	1	0	DD: Component Type
LFIELDNAME	1 Types	FNAM_____4	CHAR	132	0	Field name
LTRFLDDIS	1 Types	DDLTRFLDDI	CHAR	1	0	Basic write direction has been defined LTR (left-to-right)
BIDICTRLC	1 Types	DDBIDICTRL	CHAR	1	0	DD: No Filtering of BIDI Formatting Characters
OUTPUTSTYLE	1 Types	OUTPUTSTYLE	NUMC	2	0	DD: Output Style (Output Style) for Decfloat Types
NOHISTORY	1 Types	DDNOHISTORY	CHAR	1	0	DD: Flag for Deactivating Input History in Screen Field
AMPMFORMAT	1 Types	DDAMPMFORMAT	CHAR	1	0	DD: Indicator whether AM/PM time format is required
