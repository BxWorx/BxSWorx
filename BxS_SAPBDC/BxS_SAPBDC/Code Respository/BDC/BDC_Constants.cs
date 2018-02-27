//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.BDC
{
	internal class BDC_Constants
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Constants()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				internal enum ZDTON_RowNo
					{
						ColumnNo      = 0	,
						ProgName      = 1 ,
						DynProNo      = 2 ,
						DynBegin      = 3 ,
						OKCode        = 4 ,
						Cursor        = 5 ,
						SubScreen     = 6 ,
						FieldName     = 7 ,
						Description   = 8 ,
						Instructions  = 9
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal const	string	cz_Val_True				= "X"			;
				internal const	string	cz_Val_False			= " "			;
				internal const	string	cz_Val_Empty			= ""			;
				internal const	string	cz_Val_Scrn0			= "0000"	;

				internal const	string	cz_Cmd_Prefix			= "<<@>>";

				internal const	string	cz_Cmd_Delim			= ";";
				internal const	string	cz_Cmd_PartDelim	= ":";

				internal const	string	cz_Sub_Token			= "(@@)";
				internal const	string	cz_Sub_ABAPTrue		= "X";

				internal const	string	cz_Sym_ActionCol	= "@@";
				internal const	string	cz_Sym_ClearFld		= "@@[]";

				//.................................................
				internal const	string	cz_SAP_OKCode		= "BDC_OKCODE";
				internal const	string	cz_SAP_Cursor		= "BDC_CURSOR";
				internal const	string	cz_SAP_SubScr		= "BDC_SUBSCR";
				//.................................................
				internal	const	string	cz_Token_Prog			=	"<PROGRAMNAME>"				;
				internal	const	string	cz_Token_Scrn			=	"<SCREENNO>"					;
				internal	const	string	cz_Token_Begn			=	"<SCREENSTART>"				;
				internal	const	string	cz_Token_OKCd			=	"<OKCODE>"						;
				internal	const	string	cz_Token_Crsr			=	"<CURSORBEFORE>"			;
				internal	const	string	cz_Token_Subs			=	"<SUBSCREEN>"					;
				internal	const	string	cz_Token_FNme			=	"<FIELDNAME>"					;
				internal	const	string	cz_Token_Desc			=	"<DESCRIPTION>"				;
				internal	const	string	cz_Token_Inst			=	"<INSTRUCTIONS>"			;

				internal	const	string	cz_Token_IDCol		=	"<TRANID>"						;
				internal	const	string	cz_Token_MsgCol		=	"<MESSAGES>"					;
				internal	const	string	cz_Token_ExeCol		=	"<EXECUTE>"						;
				internal	const	string	cz_Token_DataCol	=	"<DATASTARTCOL>"			;
				internal	const	string	cz_Token_DataRow	=	"<DATASTARTROW>"			;

				internal const	string	cz_Token_XCfg			= "</DTO_BDCXMLConfig>"	;
				//.................................................
				internal	const	string	cz_Instr_Post				=	"@@POST"		;
				internal	const	string	cz_Instr_Exec				=	"@@EXEC"		;
				internal const	string	cz_Instr_DoIf				= "@@DOIF"		;
				internal const	string	cz_Instr_SubFldIdx	= "@@SUBF"		;
				internal const	string	cz_Instr_SubCsrIdx	= "@@SUBC"		;
				internal const	string	cz_Instr_ValFldIdx	= "@@INDEX"		;
				internal const	string	cz_Instr_ValCsrIdx	= "@@CSRIDX"	;

			#endregion

		}
}


