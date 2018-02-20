using System;
//.........................................................
using static	BxS_SAPBDC.Parser.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal partial class BDC_Processor
		{

			//===========================================================================================
			#region "Declarations"

				private readonly	Func<DTO_TokenReference>	_CreateToken	;

			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"
			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupProcessBoundaries()
					{
						this.RowLB		= this.Data.GetLowerBound(0);
						this.RowUB		= this.Data.GetUpperBound(0)	+ 1;
						//.............................................
						this.ColLB		= this.Data.GetLowerBound(1);
						this.ColUB		= this.Data.GetUpperBound(1)	+ 1;
					}
				



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTokens( DTO_BDCSession dto )
					{
						dto.AddToken( this.CreateToken( cz_Token_Prog	, (int)ZDTON_RowNo.ProgName			)	);
						dto.AddToken( this.CreateToken( cz_Token_Scrn	,	(int)ZDTON_RowNo.DynProNo			)	);
						dto.AddToken( this.CreateToken( cz_Token_Begn	,	(int)ZDTON_RowNo.DynBegin			)	);
						dto.AddToken( this.CreateToken( cz_Token_OKCd	,	(int)ZDTON_RowNo.OKCode				)	);
						dto.AddToken( this.CreateToken( cz_Token_Crsr	,	(int)ZDTON_RowNo.Cursor				)	);
						dto.AddToken( this.CreateToken( cz_Token_Subs	,	(int)ZDTON_RowNo.SubScreen		)	);
						dto.AddToken( this.CreateToken( cz_Token_FNme	,	(int)ZDTON_RowNo.FieldName		)	);
						dto.AddToken( this.CreateToken( cz_Token_Desc	,	(int)ZDTON_RowNo.Description	)	);
						dto.AddToken( this.CreateToken( cz_Token_Inst	,	(int)ZDTON_RowNo.Instructions	)	);
						//.............................................
						dto.AddToken( this.CreateToken( cz_Token_MsgCol		,	-1 ,  2 ) );
						dto.AddToken( this.CreateToken( cz_Token_ExeCol		,	-1 ,  4 ) );
						dto.AddToken( this.CreateToken( cz_Token_DataCol	,	-1 ,  6 ) );
						dto.AddToken( this.CreateToken( cz_Token_DataRow	,	10 , -1 ) );
						dto.AddToken( this.CreateToken( cz_Token_XCfg			,	-1 , -1 ) );
						//.............................................
						dto.AddToken( this.CreateToken( cz_Instr_Post	,	-1 , -1 ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_TokenReference CreateToken( string token, int row, int col = -1 )
					{
						DTO_TokenReference lo_DTO		= this._CreateToken();

						lo_DTO.Token	= token;
						lo_DTO.Row		= row;
						lo_DTO.Col		= col;
						lo_DTO.Value	=	token;

						return	lo_DTO;
					}

			#endregion

		}
}
