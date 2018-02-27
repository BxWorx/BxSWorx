using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//.........................................................
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Processor_Tokens
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor_Tokens(	BDC_Processor_Cfg	BDCConfig )
					{
						this._BDCCnfg	= BDCConfig;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDC_Processor_Cfg		_BDCCnfg;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task< bool > Process( DTO_BDCSession dto , string[,] data )
					{
						dto.RowLB		= data.GetLowerBound(0)			;
						dto.RowUB		= data.GetUpperBound(0)	+ 1	;
						dto.ColLB		= data.GetLowerBound(1)			;
						dto.ColUB		= data.GetUpperBound(1)	+ 1	;
						//.............................................
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
																				lo_Token.Value	= data[r,c].Replace( cz_Cmd_Prefix , ""	);
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
						if ( this.UpdateHeaderRowReference( dto ) )
							{
								if ( !this.ExtractXMLConfig( dto ) )
									{
										dto.XMLConfig	= this._BDCCnfg.CreateDTOXMLCfg();
									}
									this.ExtractBDCTokenValues( dto );
									this.ExtractSpecificTokenValues( dto );

									return	true;
							}
						//.............................................
						return	false;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

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
						dto.AddToken( this.CreateToken( cz_Token_IDCol		,	-1 ,  0 ) );
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
						DTO_TokenReference lo_DTO		= this._BDCCnfg.CreateDTOToken();

						lo_DTO.Token	= token;
						lo_DTO.Row		= row;
						lo_DTO.Col		= col;
						lo_DTO.Value	=	token;

						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ExtractBDCTokenValues( DTO_BDCSession dto )
					{
						this.ExtractTokenValue( dto , cz_Token_Prog , true	);
						this.ExtractTokenValue( dto , cz_Token_Scrn	,	true	);
						this.ExtractTokenValue( dto , cz_Token_Begn	,	true	);
						this.ExtractTokenValue( dto , cz_Token_OKCd	,	true	);
						this.ExtractTokenValue( dto , cz_Token_Crsr	,	true	);
						this.ExtractTokenValue( dto , cz_Token_Subs	,	true	);
						this.ExtractTokenValue( dto , cz_Token_FNme	,	true	);
						this.ExtractTokenValue( dto , cz_Token_Desc	,	true	);
						this.ExtractTokenValue( dto , cz_Token_Inst	,	true	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ExtractSpecificTokenValues( DTO_BDCSession dto )
					{
						dto.RowDataStart	= this.ExtractTokenValue( dto , cz_Token_IDCol		, false	);
						dto.ColDataStart	= this.ExtractTokenValue( dto , cz_Token_DataCol	, false	);
						dto.ColDataExec		= this.ExtractTokenValue( dto , cz_Token_ExeCol		, false	);
						dto.ColDataMsgs		= this.ExtractTokenValue( dto , cz_Token_MsgCol		, false	);

						dto.RowDataStart	= this.ExtractTokenValue( dto , cz_Token_DataRow	, true	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int ExtractTokenValue( DTO_BDCSession dto , string tokenID , bool setRow = true )
					{
						if ( !dto.Tokens.TryGetValue( tokenID , out DTO_TokenReference token ) )
							{ return	0; }
						//.............................................
						int ln_NewVal  = this.ExtractInstructionInt( token );

						if ( !ln_NewVal.Equals(0) )
							{
								if ( setRow )
									{	token.Row	= ln_NewVal; }
								else
									{	token.Col	= ln_NewVal; }
							}
						//.............................................
						if ( setRow )
							{	return	token.Row; }
						else
							{	return	token.Col; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int ExtractInstructionInt( DTO_TokenReference token )
					{
						string lc_Val = this.ExtractInstructionString( token );
						if ( string.IsNullOrEmpty( lc_Val ) )		return	0;

						if ( !int.TryParse( lc_Val, out int ln_Val ) )
							{
								ln_Val	= this.TranslateToInt( lc_Val );
							}
						//.............................................
						return	ln_Val;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int TranslateToInt( string value )
					{
						int ln_Val = 0;

						foreach (char lc_Char in value)
							{
								ln_Val += ( lc_Char - 64 );
							}
						//.............................................
						return	ln_Val;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string ExtractInstructionString( DTO_TokenReference token )
					{
						Match lo_Srch = Regex.Match( token.Value , $"{token.Token}.*?([^{cz_Cmd_Delim}]*)" , RegexOptions.IgnoreCase );
						if (lo_Srch.Success)
							{
								string	lc_Val = lo_Srch.Groups[1].Value.Replace("[","");
								return	lc_Val.Replace("]","").ToUpper();
							}
						//.............................................
						return	string.Empty;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool ExtractXMLConfig( DTO_BDCSession dto )
					{
						if ( dto.Tokens.TryGetValue( cz_Token_XCfg , out DTO_TokenReference token ) )
							{
								try
									{
										dto.XMLConfig	= this._BDCCnfg.IPXController
																			.DeSerialize< DTO_BDCXMLConfig >(	token.Value );
										return	true;
									}
								catch (Exception)
									{
										return	false;
									}
							}
						//.............................................
						return	false;
					}

			#endregion

		}
}
