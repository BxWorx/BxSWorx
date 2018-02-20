using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//.........................................................
using static	BxS_SAPBDC.Parser.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public partial class BDC_Processor
		{
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
				private async Task< bool > ParseForTokens( DTO_BDCSession dto , string[,] data )
					{
						this.LoadTokens( dto );

						IList< Task	>	lo_Tasks	= new	List< Task >( dto.Tokens.Count );
						//.............................................
						foreach ( KeyValuePair< string , DTO_TokenReference > ls_kvp in dto.Tokens )
							{
								string							lc_ST			= ls_kvp.Key		;
								DTO_TokenReference	lo_Token	= ls_kvp.Value	;

								lo_Tasks.Add(	Task.Run(	() =>
									{
										for ( int r = dto.RowLB; r < dto.RowUB; r++ )
											{
												for ( int c = dto.ColLB; c < dto.ColUB; c++ )
													{
														if ( data[r,c] != null )
															{
																if ( Regex.IsMatch( data[r,c] , cz_Cmd_Prefix , RegexOptions.IgnoreCase ) )
																	{
																		if ( Regex.IsMatch( data[r,c] , lc_ST , RegexOptions.IgnoreCase ) )
																			{
																				lo_Token.Row		= r;
																				lo_Token.Col		= c;
																				lo_Token.Value	= data[r,c];

																				return;
																			}
																	}
															}
													}
											}
									} )	);
							}

						await Task.WhenAll(lo_Tasks).ConfigureAwait(false);
						//.............................................
						return	this.UpdateHeaderRowReference( dto );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	bool UpdateHeaderRowReference( DTO_BDCSession dto )
					{
						dto.BDCHeaderRowRef.Prog	= dto.Tokens.TryGetValue( cz_Token_Prog , out DTO_TokenReference lo_Token ) ? lo_Token.Row : -1 ;

						dto.BDCHeaderRowRef.Scrn	= dto.Tokens.TryGetValue( cz_Token_Scrn , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.Strt	= dto.Tokens.TryGetValue( cz_Token_Begn , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.OKCd	= dto.Tokens.TryGetValue( cz_Token_OKCd , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.Curs	= dto.Tokens.TryGetValue( cz_Token_Crsr , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.Subs	= dto.Tokens.TryGetValue( cz_Token_Subs , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.FldN	= dto.Tokens.TryGetValue( cz_Token_FNme , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.Desc	= dto.Tokens.TryGetValue( cz_Token_Desc , out lo_Token ) ? lo_Token.Row : -1 ;
						dto.BDCHeaderRowRef.Inst	= dto.Tokens.TryGetValue( cz_Token_Inst , out lo_Token ) ? lo_Token.Row : -1 ;
						//.............................................
						if (		dto.BDCHeaderRowRef.Prog.Equals(-1)
								||	dto.BDCHeaderRowRef.Scrn.Equals(-1)
								||	dto.BDCHeaderRowRef.Strt.Equals(-1)
								||	dto.BDCHeaderRowRef.OKCd.Equals(-1)
								||	dto.BDCHeaderRowRef.Curs.Equals(-1)
								||	dto.BDCHeaderRowRef.Subs.Equals(-1)
								||	dto.BDCHeaderRowRef.FldN.Equals(-1)
								||	dto.BDCHeaderRowRef.Desc.Equals(-1)
								||	dto.BDCHeaderRowRef.Inst.Equals(-1) )

							{	return	false	; }
						else
							{	return	true	; }
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
