//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
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
			#region "Properties"

				internal enum BDCOld_RowNo
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

				internal const	string	cz_Cmd_Prefix				= "@@";

				internal const	string	cz_Cmd_Delim        = ";";
				internal const	string	cz_Cmd_PartDelim		= ":";

				internal const	string	cz_Sub_Token        = "<<@>>";
				internal const	string	cz_Sub_ABAPTrue     = "X";

				internal const	string	cz_Cmd_OKCode       = "BDC_OKCODE";
				internal const	string	cz_Cmd_Cursor       = "BDC_CURSOR";
				internal const	string	cz_Cmd_SubScr       = "BDC_SUBSCR";

				internal const	string	cz_Sym_PsuedoAction = "@@";
				internal const	string	cz_Sym_ClearFld     = "@@[]";

				internal const	string	cz_Cmd_DoIf         = "@@DOIF";
				internal const	string	cz_Cmd_SubFldIdx    = "@@SUBF";
				internal const	string	cz_Cmd_SubCsrIdx    = "@@SUBC";
				internal const	string	cz_Cmd_ValFldIdx    = "@@INDEX";
				internal const	string	cz_Cmd_ValCsrIdx    = "@@CSRIDX";

			#endregion

		}
}


