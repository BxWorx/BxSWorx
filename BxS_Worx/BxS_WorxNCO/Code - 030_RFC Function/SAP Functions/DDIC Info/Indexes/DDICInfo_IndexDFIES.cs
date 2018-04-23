using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main							.NCO_Constants			;
using	static	BxS_WorxNCO.RfcFunction.DDIC	.DDICInfo_Constants	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	internal class DDICInfo_IndexDFIES : RfcStructureIndex
		{
			#region "Documentation"

				//	*"----------------------------------------------------------------------
				//	DFIES                          Active
				//	DD Interface: Table Fields for DDIF_FIELDINFO_GET
				//	
				//	TABNAME					Types	TABNAME				CHAR	030			Table Name
				//	FIELDNAME				Types	FIELDNAME			CHAR	030			Field Name
				//	LANGU						Types	DDLANGUAGE		LANG	001			Language Key
				//	POSITION				Types	TABFDPOS			NUMC	004			Position of the field in the table
				//	OFFSET					Types	DOFFSET				NUMC	006			Offset of a field
				//	DOMNAME					Types	DOMNAME				CHAR	030			Domain name
				//	ROLLNAME				Types	ROLLNAME			CHAR	030			Data element (semantic domain)
				//	CHECKTABLE			Types	TABNAME				CHAR	030			Table Name
				//	LENG						Types	DDLENG				NUMC	006			Length (No. of Characters)
				//	INTLEN					Types	INTLEN				NUMC	006			Internal Length in Bytes
				//	OUTPUTLEN				Types	OUTPUTLEN			NUMC	006			Output Length
				//	DECIMALS				Types	DECIMALS			NUMC	006			Number of Decimal Places
				//	DATATYPE				Types	DYNPTYPE			CHAR	004			ABAP/4 Dictionary: Screen data type for Screen Painter
				//	INTTYPE					Types	INTTYPE				CHAR	001			ABAP data type (C,D,N,...)
				//	REFTABLE				Types	REFTABLE			CHAR	030			Table for reference field
				//	REFFIELD				Types	REFFIELD			CHAR	030			Reference field for currency and qty fields
				//	PRECFIELD				Types	PRECFIELD			CHAR	030			Name of included table
				//	AUTHORID				Types	AUTHORID			CHAR	003			Authorization class
				//	MEMORYID				Types	MEMORYID			CHAR	020			Set/Get parameter ID
				//	LOGFLAG					Types	LOGFLAG				CHAR	001			Indicator for writing change documents
				//	MASK						Types	AS4MASK				CHAR	020			Template (not used)
				//	MASKLEN					Types	MASKLEN				NUMC	004			Template length (not used)
				//	CONVEXIT				Types	CONVEXIT			CHAR	005			Conversion Routine
				//	HEADLEN					Types	HEADLEN				NUMC	002			Maximum length of heading
				//	SCRLEN1					Types	SCRLEN_S			NUMC	002			Max. length for short field label
				//	SCRLEN2					Types	SCRLEN_M			NUMC	002			Max. length for medium field label
				//	SCRLEN3					Types	SCRLEN_L			NUMC	002			Max. length for long field label
				//	FIELDTEXT				Types	AS4TEXT				CHAR	060			Short Description of Repository Objects
				//	REPTEXT					Types	REPTEXT				CHAR	055			Heading
				//	SCRTEXT_S				Types	SCRTEXT_S			CHAR	010			Short Field Label
				//	SCRTEXT_M				Types	SCRTEXT_M			CHAR	020			Medium Field Label
				//	SCRTEXT_L				Types	SCRTEXT_L			CHAR	040			Long Field Label
				//	KEYFLAG					Types	KEYFLAG				CHAR	001			Identifies a key field of a table
				//	LOWERCASE				Types	LOWERCASE			CHAR	001			Lowercase letters allowed/not allowed
				//	MAC							Types	DDSHATTACH		CHAR	001			Flag if search help is attached to the field
				//	GENKEY					Types	AS4FLAG				CHAR	001			Flag (X or Blank)
				//	NOFORKEY				Types	AS4FLAG				CHAR	001			Flag (X or Blank)
				//	VALEXI					Types	VALEXI				CHAR	001			Existence of fixed values
				//	NOAUTHCH				Types	AS4FLAG				CHAR	001			Flag (X or Blank)
				//	SIGN						Types	SIGNFLAG			CHAR	001			Flag for sign in numerical fields
				//	DYNPFLD					Types	DYNPROFLD			CHAR	001			Flag: field to be displayed on the screen
				//	F4AVAILABL			Types	DDF4AVAIL			CHAR	001			Does the field have an input help
				//	COMPTYPE				Types	COMPTYPE			CHAR	001			DD: Component Type
				//	LFIELDNAME			Types	FNAM_____4		CHAR	132			Field name
				//	LTRFLDDIS				Types	DDLTRFLDDI		CHAR	001			Basic write direction has been defined LTR (left-to-right)
				//	BIDICTRLC				Types	DDBIDICTRL		CHAR	001			DD: No Filtering of BIDI Formatting Characters
				//	OUTPUTSTYLE			Types	OUTPUTSTYLE		NUMC	002			DD: Output Style (Output Style) for Decfloat Types
				//	NOHISTORY				Types	DDNOHISTORY		CHAR	001			DD: Flag for Deactivating Input History in Screen Field
				//	AMPMFORMAT			Types	DDAMPMFORMAT	CHAR	001			DD: Indicator whether AM/PM time format is required
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DDICInfo_IndexDFIES()
					{
						this.Name		=	cz_DFies;
						//.............................................
						this._Tbl		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TABNAME"		) );
						this._Fld		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIELDNAME"	) );
						this._Txt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIELDTEXT"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Tbl;
				private	readonly	Lazy<int>		_Fld;
				private	readonly	Lazy<int>		_Txt;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	Tbl		{ get { return	this.IsLoaded	?	this._Tbl.Value	:	cz_Neg1	; } }
				internal	int	Fld		{ get { return	this.IsLoaded	?	this._Fld.Value	:	cz_Neg1	; } }
				internal	int	Txt		{ get { return	this.IsLoaded	?	this._Txt.Value	:	cz_Neg1	; } }

			#endregion

		}
}
