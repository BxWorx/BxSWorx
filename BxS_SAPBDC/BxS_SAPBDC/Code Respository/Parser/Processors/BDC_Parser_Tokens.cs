using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//.........................................................
using static	BxS_SAPBDC.BDC.BDC_Constants;
using					BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Parser_Tokens
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Parser_Tokens( Lazy< BDC_Parser_Factory > factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< BDC_Parser_Factory >		_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_BDCSessionRequest dtoRequest
															,	DTO_ParserProfile			dtoProfile )
					{
						if (dtoRequest.WSData == null)	return;
						//.............................................
						dtoProfile.RowLB		= dtoRequest.WSData.GetLowerBound(0)	;
						dtoProfile.RowUB		= dtoRequest.WSData.GetUpperBound(0)	;
						dtoProfile.ColLB		= dtoRequest.WSData.GetLowerBound(1)	;
						dtoProfile.ColUB		= dtoRequest.WSData.GetUpperBound(1)	;
						//.............................................
						// Search for DATAROWSTART token to lessen remainder of search rows as all token should be
						// in header section
						//
						DTO_ParserToken lo_DataRowToken	= this._Factory.Value.CreateDTOToken( cz_Token_DataRow );
						lo_DataRowToken.Col	= -1;
						lo_DataRowToken.Row	= 10;

						this.UpdateToken( lo_DataRowToken , dtoRequest , dtoProfile.RowLB , dtoProfile.RowUB , dtoProfile.ColLB , dtoProfile.ColUB );
						dtoProfile.RowDataStart	= lo_DataRowToken.Row;
						//.............................................
						this.LoadTokens( dtoProfile );

						foreach ( KeyValuePair< string , DTO_ParserToken > ls_kvp in dtoProfile.Tokens )
							{
								this.UpdateToken(		ls_kvp.Value
																	, dtoRequest
																	, dtoProfile.RowLB
																	, dtoProfile.RowDataStart - 1
																	, dtoProfile.ColLB
																	, dtoProfile.ColUB						);
							}
						//.............................................
						// Add the DATAROWSTART token
						//
						dtoProfile.Tokens.Add( lo_DataRowToken.ID, lo_DataRowToken );
						//.............................................
						if ( this.UpdateHeaderRowReference( dtoProfile ) )
							{
								this.ExtractXMLConfig						( dtoProfile );
								this.ExtractBDCTokenValues			( dtoProfile );
								this.ExtractSpecificTokenValues	( dtoProfile );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UpdateToken(		DTO_ParserToken					token
																	,	DTO_BDCSessionRequest dtoRequest
																	, int fromRow , int toRow
																	, int fromCol , int toCol						)
					{
						bool	lb_Found	= false;
						//.............................................
						for ( int r = fromRow; r < toRow; r++ )
							{
								for ( int c = fromCol; c < toCol; c++ )
									{
										if ( dtoRequest.WSData[r,c] != null )
											{
												if ( Regex.IsMatch( dtoRequest.WSData[r,c] , cz_Cmd_Prefix , RegexOptions.IgnoreCase ) )
													{
														if ( Regex.IsMatch( dtoRequest.WSData[r,c] , token.ID , RegexOptions.IgnoreCase ) )
															{
																token.Row		= r;
																token.Col		= c;
																token.Value	= dtoRequest.WSData[r,c].Replace( cz_Cmd_Prefix , "" );

																lb_Found	= true;
															}
													}
											}

										if (lb_Found)	break;
									}

								if (lb_Found)	break;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	bool UpdateHeaderRowReference( DTO_ParserProfile dtoProfile )
					{
						dtoProfile.BDCHeaderRowRef.Prog	= dtoProfile.Tokens.TryGetValue( cz_Token_Prog , out DTO_ParserToken lo_Token ) ? lo_Token.Row : -1 ;

						dtoProfile.BDCHeaderRowRef.Scrn	= dtoProfile.Tokens.TryGetValue( cz_Token_Scrn , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.Strt	= dtoProfile.Tokens.TryGetValue( cz_Token_Begn , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.OKCd	= dtoProfile.Tokens.TryGetValue( cz_Token_OKCd , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.Curs	= dtoProfile.Tokens.TryGetValue( cz_Token_Crsr , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.Subs	= dtoProfile.Tokens.TryGetValue( cz_Token_Subs , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.FldN	= dtoProfile.Tokens.TryGetValue( cz_Token_FNme , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.Desc	= dtoProfile.Tokens.TryGetValue( cz_Token_Desc , out lo_Token ) ? lo_Token.Row : -1 ;
						dtoProfile.BDCHeaderRowRef.Inst	= dtoProfile.Tokens.TryGetValue( cz_Token_Inst , out lo_Token ) ? lo_Token.Row : -1 ;
						//.............................................
						if (		dtoProfile.BDCHeaderRowRef.Prog.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.Scrn.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.Strt.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.OKCd.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.Curs.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.Subs.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.FldN.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.Desc.Equals(-1)
								||	dtoProfile.BDCHeaderRowRef.Inst.Equals(-1) )

							{	return	false	; }
						else
							{	return	true	; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTokens( DTO_ParserProfile dtoProfile )
					{
						this.AddToken( dtoProfile, cz_Token_Prog	, (int)ZDTON_RowNo.ProgName			);
						this.AddToken( dtoProfile, cz_Token_Scrn	,	(int)ZDTON_RowNo.DynProNo			);
						this.AddToken( dtoProfile, cz_Token_Begn	,	(int)ZDTON_RowNo.DynBegin			);
						this.AddToken( dtoProfile, cz_Token_OKCd	,	(int)ZDTON_RowNo.OKCode				);
						this.AddToken( dtoProfile, cz_Token_Crsr	,	(int)ZDTON_RowNo.Cursor				);
						this.AddToken( dtoProfile, cz_Token_Subs	,	(int)ZDTON_RowNo.SubScreen		);
						this.AddToken( dtoProfile, cz_Token_FNme	,	(int)ZDTON_RowNo.FieldName		);
						this.AddToken( dtoProfile, cz_Token_Desc	,	(int)ZDTON_RowNo.Description	);
						this.AddToken( dtoProfile, cz_Token_Inst	,	(int)ZDTON_RowNo.Instructions	);
						//.............................................
						this.AddToken( dtoProfile, cz_Token_IDCol		,	-1 ,  2 );
						this.AddToken( dtoProfile, cz_Token_MsgCol	,	-1 ,  1 );
						this.AddToken( dtoProfile, cz_Token_ExeCol	,	-1 ,  3 );
						this.AddToken( dtoProfile, cz_Token_DataCol	,	-1 ,  5 );
						this.AddToken( dtoProfile, cz_Token_XCfg		,	-1 , -1 );
						//.............................................
						this.AddToken( dtoProfile, cz_Instr_Post	,	-1 , -1 );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddToken(	DTO_ParserProfile	dtoProfile
															, string						token
															, int								row
															, int								col = -1		)
					{
						DTO_ParserToken lo_Token	= this.CreateToken( token, row, col );
						dtoProfile.Tokens.Add( lo_Token.ID, lo_Token );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_ParserToken CreateToken(	string	token
																						, int			row
																						, int			col = -1 )
					{
						DTO_ParserToken lo_DTO		= this._Factory.Value.CreateDTOToken();

						lo_DTO.ID	= token;
						lo_DTO.Row		= row;
						lo_DTO.Col		= col;
						lo_DTO.Value	=	token;

						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ExtractBDCTokenValues( DTO_ParserProfile dtoProfile )
					{
						this.ExtractTokenValue( dtoProfile , cz_Token_Prog , true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_Scrn	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_Begn	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_OKCd	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_Crsr	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_Subs	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_FNme	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_Desc	,	true	);
						this.ExtractTokenValue( dtoProfile , cz_Token_Inst	,	true	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ExtractSpecificTokenValues( DTO_ParserProfile dtoProfile )
					{
						dtoProfile.ColDataStart	= this.ExtractTokenValue( dtoProfile , cz_Token_DataCol	, false	);
						dtoProfile.ColID				= this.ExtractTokenValue( dtoProfile , cz_Token_IDCol		, false	);
						dtoProfile.ColExec			= this.ExtractTokenValue( dtoProfile , cz_Token_ExeCol	, false	);
						dtoProfile.ColMsgs			= this.ExtractTokenValue( dtoProfile , cz_Token_MsgCol	, false	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int ExtractTokenValue(	DTO_ParserProfile	dtoProfile
																			, string						tokenID
																			, bool							setRow = true )
					{
						if ( !dtoProfile.Tokens.TryGetValue(	tokenID
																								, out DTO_ParserToken token ) )
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
				private int ExtractInstructionInt( DTO_ParserToken token )
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
				private string ExtractInstructionString( DTO_ParserToken token )
					{
						Match lo_Srch = Regex.Match( token.Value , $"{token.ID}.*?([^{cz_Cmd_Delim}]*)" , RegexOptions.IgnoreCase );
						if (lo_Srch.Success)
							{
								string	lc_Val = lo_Srch.Groups[1].Value.Replace("[","");
								return	lc_Val.Replace("]","").ToUpper();
							}
						//.............................................
						return	string.Empty;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ExtractXMLConfig( DTO_ParserProfile dtoProfile )
					{
						if ( dtoProfile.Tokens.TryGetValue( cz_Token_XCfg , out DTO_ParserToken token ) )
							{
								try
									{
										dtoProfile.XMLConfig	= this._Factory.Value.IPXController
																							.DeSerialize< DTO_ParserXMLConfig >(	token.Value );
										return;
									}
								catch
									{
										// NO-OP
									}
							}
						//.............................................
						DTO_ParserXMLConfig lo_Cfg	= this._Factory.Value.CreateDTOXMLCfg();

						lo_Cfg.Col_Active			= "2";
						lo_Cfg.Col_Msg				= "1";
						lo_Cfg.Col_ID					= "2";
						lo_Cfg.Col_Exec				= "2";
						lo_Cfg.Col_Active			= "2";
						lo_Cfg.Col_Msg				= "2";
						lo_Cfg.Col_DataStart	= "2";
						lo_Cfg.Row_DataStart	= "2";

						lo_Cfg.CTU_DefSize		= cz_Val_True;
						lo_Cfg.CTU_DisMode		= "N";
						lo_Cfg.CTU_UpdMode		= "A";
						lo_Cfg.GUID						= Guid.NewGuid().ToString();
						lo_Cfg.IsActive				= cz_Val_True;
						lo_Cfg.IsProtected		= cz_Val_True;
						lo_Cfg.Password				= "";
						lo_Cfg.PauseTime			= "0";
						lo_Cfg.SAPTCode				= "";
						lo_Cfg.SAPBDCSessionID	= "";
						lo_Cfg.Skip1st					= cz_Val_False;

						dtoProfile.XMLConfig	= lo_Cfg;
					}

			#endregion

		}
}



						////.............................................
						//// Calculate Excel to array offsets
						////
						//string[]	lt_Addr = DTO.UsedAddress.Split(':');
						//string[]	lt_TLft = lt_Addr[0].Split('$');
						//int				ln_Col	= 0;

						//foreach (char lc_Char in lt_TLft[1])
						//	{
						//	ln_Col += (lc_Char - 64);
						//	}
						//DTO.OffsetCol	= ln_Col - 1;

						//if ( int.TryParse( lt_TLft[2], out int ln_Row ) )
						//	{
						//		ln_Row --;
						//	}
						//DTO.OffsetRow	= ln_Row;
