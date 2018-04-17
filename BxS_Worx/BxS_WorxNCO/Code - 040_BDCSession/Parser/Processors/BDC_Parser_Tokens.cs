using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//.........................................................
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants		;
using static	BxS_WorxNCO.BDCSession.Parser		.BDC_Parser_Constants	;
using static	BxS_WorxNCO.Main								.NCO_Constants				;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser_Tokens : BDC_Parser_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Parser_Tokens( Lazy< BDC_Parser_Factory > factory ) : base( factory )
					{
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_ParserRequest	dtoRequest
															,	DTO_ParserProfile	dtoProfile )
					{
						if (dtoRequest.WSData == null)	return;
						this.Prepare( dtoRequest , dtoProfile );
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
				private	void Prepare(		DTO_ParserRequest	dtoRequest
															,	DTO_ParserProfile	dtoProfile )
					{
						//.............................................
						// Calculate Excel to array offsets
						//
						string[]	lt_Addr = dtoRequest.UsedAddress.Split(':');
						string[]	lt_TLft = lt_Addr[0].Split('$');
						int				ln_Col	= 0;

						foreach (char lc_Char in lt_TLft[1])
							{
							ln_Col += (lc_Char - 64);
							}
						dtoProfile.OffsetCol	= ln_Col - 1;

						if ( int.TryParse( lt_TLft[2], out int ln_Row ) )
							{
								ln_Row --;
							}
						dtoProfile.OffsetRow	= ln_Row;
						//.............................................
						// Calculate data array bounds
						//
						dtoProfile.RowLB		= dtoRequest.WSData.GetLowerBound(0)	;
						dtoProfile.RowUB		= dtoRequest.WSData.GetUpperBound(0)	;
						dtoProfile.ColLB		= dtoRequest.WSData.GetLowerBound(1)	;
						dtoProfile.ColUB		= dtoRequest.WSData.GetUpperBound(1)	;
						//.............................................
						// Search for DATAROWSTART token to lessen remainder of search rows as all token should be
						// in header section
						//
						DTO_ParserToken lo_DataRowToken	=	this.CreateToken( cz_Token_DataRow , 10 , -1 , cz_Token_xInst );

						this.UpdateToken( lo_DataRowToken , dtoRequest , dtoProfile.RowLB , dtoProfile.RowUB , dtoProfile.ColLB , dtoProfile.ColUB );
						dtoProfile.RowDataStart	= lo_DataRowToken.Row;
						if (lo_DataRowToken.FoundAlt)	dtoProfile.RowDataStart ++;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UpdateToken(		DTO_ParserToken		token
																	,	DTO_ParserRequest	dtoRequest
																	, int fromRow , int toRow
																	, int fromCol , int toCol				)
					{
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
																token.Found	= true;
															}
													}
											}

										if (token.Found)	break;
									}

								if (token.Found)	break;
							}

						if (token.Found)	return;
						//.............................................
						// Search for alternate token if supplied and original not found
						//
						if ( !token.AltID.Equals(string.Empty) )
							{
								for ( int r = fromRow; r < toRow; r++ )
									{
										for ( int c = fromCol; c < toCol; c++ )
											{
												if ( dtoRequest.WSData[r,c] != null )
													{
														if ( Regex.IsMatch( dtoRequest.WSData[r,c] , token.AltID , RegexOptions.IgnoreCase ) )
															{
																token.Row				= r;
																token.Col				= c;
																token.Value			= dtoRequest.WSData[r,c].Replace( cz_Cmd_Prefix , "" );
																token.Found			= true;
																token.FoundAlt	= true;
															}
													}

												if (token.Found)	break;
											}

										if (token.Found)	break;
									}
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
						return			!dtoProfile.BDCHeaderRowRef.Prog.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.Scrn.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.Strt.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.OKCd.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.Curs.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.Subs.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.FldN.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.Desc.Equals(-1)
										&&	!dtoProfile.BDCHeaderRowRef.Inst.Equals(-1)	;
						}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTokens( DTO_ParserProfile dtoProfile )
					{
						this.AddToken( dtoProfile, cz_Token_Prog	, (int)ZDTON_RowNo.ProgName			, -1	, cz_Token_xProg	);
						this.AddToken( dtoProfile, cz_Token_Scrn	,	(int)ZDTON_RowNo.DynProNo			, -1	, cz_Token_xScrn	);
						this.AddToken( dtoProfile, cz_Token_Begn	,	(int)ZDTON_RowNo.DynBegin			, -1	, cz_Token_xBegn	);
						this.AddToken( dtoProfile, cz_Token_OKCd	,	(int)ZDTON_RowNo.OKCode				, -1	, cz_Token_xOKCd	);
						this.AddToken( dtoProfile, cz_Token_Crsr	,	(int)ZDTON_RowNo.Cursor				, -1	, cz_Token_xCrsr	);
						this.AddToken( dtoProfile, cz_Token_Subs	,	(int)ZDTON_RowNo.SubScreen		, -1	, cz_Token_xSubs	);
						this.AddToken( dtoProfile, cz_Token_FNme	,	(int)ZDTON_RowNo.FieldName		, -1	, cz_Token_xFNme	);
						this.AddToken( dtoProfile, cz_Token_Desc	,	(int)ZDTON_RowNo.Description	, -1	, cz_Token_xDesc	);
						this.AddToken( dtoProfile, cz_Token_Inst	,	(int)ZDTON_RowNo.Instructions	, -1	, cz_Token_xInst	);
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
															, int								col = -1
															, string						AltID	= cz_Null )
					{
						DTO_ParserToken lo_DTO	= this.CreateToken( token , row , col , AltID );
						dtoProfile.Tokens.Add( lo_DTO.ID, lo_DTO );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_ParserToken CreateToken(	string	token
																						, int			row
																						, int			col = -1
																						, string	AltID	= cz_Null )
					{
						DTO_ParserToken lo_DTO	= this._PFactory.Value.CreateDTOToken( token );
						//.............................................
						lo_DTO.Row		= row		;
						lo_DTO.Col		= col		;
						lo_DTO.Value	=	token	;
						lo_DTO.AltID	= AltID	;
						//.............................................
						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ExtractBDCTokenValues( DTO_ParserProfile dtoProfile )
					{
						this.ExtractTokenValue( dtoProfile , cz_Token_Prog	, true	);
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
										dtoProfile.XMLConfig	= this._PFactory.Value.Serializer
																							.DeSerialize< DTO_ParserXMLConfig >(	token.Value );
										return;
									}
								catch
									{
										// NO-OP
									}
							}
						//.............................................
						const string lz_2	= "2";

						DTO_ParserXMLConfig lo_Cfg	= this._PFactory.Value.CreateDTOXMLCfg();

						lo_Cfg.Col_Msg				= "1";
						lo_Cfg.Col_Active			= lz_2;
						lo_Cfg.Col_ID					= lz_2;
						lo_Cfg.Col_Exec				= lz_2;
						lo_Cfg.Col_Active			= lz_2;
						lo_Cfg.Col_DataStart	= lz_2;
						lo_Cfg.Row_DataStart	= lz_2;

						lo_Cfg.GUID							= Guid.NewGuid().ToString();

						lo_Cfg.CTU_DefSize			= cz_True;
						lo_Cfg.IsActive					= cz_True;
						lo_Cfg.IsProtected			= cz_True;
						lo_Cfg.Skip1st					= cz_False;
						lo_Cfg.SAPTCode					= cz_Null;
						lo_Cfg.SAPBDCSessionID	= cz_Null;
						lo_Cfg.Password					= cz_Null;

						lo_Cfg.CTU_DisMode			= cz_CTU_N.ToString();
						lo_Cfg.CTU_UpdMode			= cz_CTU_N.ToString();

						lo_Cfg.PauseTime				= "0";

						dtoProfile.XMLConfig		= lo_Cfg;
					}

			#endregion

		}
}
