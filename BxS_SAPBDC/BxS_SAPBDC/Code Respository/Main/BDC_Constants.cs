using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Constants
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Constants( Func<DTO_TokenReference>	createToken	)
					{
						this._CreateToken		= createToken	;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private readonly	Func<DTO_TokenReference>	_CreateToken	;

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal const	string	cz_Cmd_Prefix				= "<@@>";

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

			//===========================================================================================
			#region "Properties"

				internal	string	FldName_OKCode			{ get { return cz_Cmd_OKCode; } }
				internal	string	FldName_Cursor			{ get { return cz_Cmd_Cursor; } }
				internal	string	FldName_Subscreen		{ get { return cz_Cmd_SubScr; } }

				internal	string	FldValue_Psuedo			{ get { return cz_Sym_PsuedoAction	; } }
				internal	string	FldValue_Clear			{ get { return cz_Sym_ClearFld			;	} }

				internal	string	IndexSubstitute			{ get { return cz_Sub_Token	;	} }

				//.................................................
				internal	string	Token_Prog	{ get { return	"<PROGRAMNAME>"		;	} }
				internal	string	Token_Scrn	{ get { return	"<SCREENNO>"			;	} }
				internal	string	Token_Begn	{ get { return	"<SCREENSTART>"		;	} }
				internal	string	Token_OKCd	{ get { return	"<OKCODE>"				;	} }
				internal	string	Token_Crsr	{ get { return	"<CURSORBEFORE>"	;	} }
				internal	string	Token_Subs	{ get { return	"<SUBSCREEN>"			;	} }
				internal	string	Token_FNme	{ get { return	"<FIELDNAME>"			;	} }
				internal	string	Token_Desc	{ get { return	"<DESCRIPTION>"		;	} }
				internal	string	Token_Inst	{ get { return	"<INSTRUCTIONS>"	;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<DTO_TokenReference>	GetTokenList()
					{
						IList<DTO_TokenReference> lt_List = new List<DTO_TokenReference>();
						//.............................................
						lt_List	.Add(	this.CreateToken(	this.Token_Prog	,	BDCOld_RowNo.ProgName			) );
						lt_List	.Add(	this.CreateToken(	this.Token_Scrn	,	BDCOld_RowNo.DynProNo			) );
						lt_List	.Add(	this.CreateToken(	this.Token_Begn	,	BDCOld_RowNo.DynBegin			) );
						lt_List	.Add(	this.CreateToken(	this.Token_OKCd	,	BDCOld_RowNo.OKCode				) );
						lt_List	.Add(	this.CreateToken(	this.Token_Crsr	,	BDCOld_RowNo.Cursor				) );
						lt_List	.Add(	this.CreateToken(	this.Token_Subs	,	BDCOld_RowNo.SubScreen		) );
						lt_List	.Add(	this.CreateToken(	this.Token_FNme	,	BDCOld_RowNo.FieldName		) );
						lt_List	.Add(	this.CreateToken(	this.Token_Desc	,	BDCOld_RowNo.Description	) );
						lt_List	.Add(	this.CreateToken(	this.Token_Inst	,	BDCOld_RowNo.Instructions	) );
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_TokenReference CreateToken( string token, BDCOld_RowNo row )
					{
						DTO_TokenReference x	= this._CreateToken();
						x.Token	= token				;
						x.Row		= (int)row		;
						return	x;
					}

			#endregion

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
		}
}


