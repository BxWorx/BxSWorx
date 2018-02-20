using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//.............................................
using static BxS_SAPBDC.Parser.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class Parser_BDCTokens
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	Parser_BDCTokens(		BDCMain										BDCMain
																		,	Func<DTO_TokenReference>	createToken	)
					{
						this._BDCMain				= BDCMain			;
						this._CreateToken		= createToken	;
						//.............................................
						this.IsReady	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDCMain										_BDCMain			;
				private readonly	Func<DTO_TokenReference>	_CreateToken	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool	IsReady	{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Tokens"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task< int > ParseForTokens()
					{
						this.LoadTokens();
						IList< Task	>	lo_Tasks	= new	List< Task >( this._BDCMain.Tokens.Count );
						//.............................................
						foreach ( KeyValuePair< string , DTO_TokenReference > ls_kvp in this._BDCMain.Tokens )
							{
								string							lc_ST			= ls_kvp.Key		;
								DTO_TokenReference	lo_Token	= ls_kvp.Value	;

								lo_Tasks.Add(	Task.Run(	() => this.SearchTask( lc_ST , lo_Token )	)	);
							}

						await Task.WhenAll(lo_Tasks).ConfigureAwait(false);
						//.............................................
						this.UpdateHeaderRowReference();
						this.CheckStatus();
						//.............................................
						return	lo_Tasks.Count;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SearchTask( string searchFor , DTO_TokenReference updateToken )
					{
						for ( int r = this._BDCMain.RowLB; r < this._BDCMain.RowUB; r++ )
							{
								for ( int c = this._BDCMain.ColLB; c < this._BDCMain.ColUB; c++ )
									{
										if ( this._BDCMain.Data[r,c] != null )
											{
												if ( Regex.IsMatch( this._BDCMain.Data[r,c] , cz_Cmd_Prefix, RegexOptions.IgnoreCase ) )
													{
														if ( Regex.IsMatch( this._BDCMain.Data[r,c] , searchFor, RegexOptions.IgnoreCase ) )
															{
																updateToken.Row		= r;
																updateToken.Col		= c;
																updateToken.Value	= this._BDCMain.Data[r,c];

																return;
															}
													}
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CheckStatus()
					{
						this.IsReady	= true;
						//.............................................
						if (		this._BDCMain.BDCHeaderRowRef.Prog.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.Scrn.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.Strt.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.OKCd.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.Curs.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.Subs.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.FldN.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.Desc.Equals(-1)
								||	this._BDCMain.BDCHeaderRowRef.Inst.Equals(-1) )
							{
								this.IsReady	= false;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTokens()
					{
						this.AddZDTONTokenDefault( cz_Token_Prog	, ZDTON_RowNo.ProgName			);
						this.AddZDTONTokenDefault( cz_Token_Scrn	,	ZDTON_RowNo.DynProNo			);
						this.AddZDTONTokenDefault( cz_Token_Begn	,	ZDTON_RowNo.DynBegin			);
						this.AddZDTONTokenDefault( cz_Token_OKCd	,	ZDTON_RowNo.OKCode				);
						this.AddZDTONTokenDefault( cz_Token_Crsr	,	ZDTON_RowNo.Cursor				);
						this.AddZDTONTokenDefault( cz_Token_Subs	,	ZDTON_RowNo.SubScreen			);
						this.AddZDTONTokenDefault( cz_Token_FNme	,	ZDTON_RowNo.FieldName			);
						this.AddZDTONTokenDefault( cz_Token_Desc	,	ZDTON_RowNo.Description		);
						this.AddZDTONTokenDefault( cz_Token_Inst	,	ZDTON_RowNo.Instructions	);
						//.............................................
						this._BDCMain.Tokens.Add(	cz_Token_Msgs , this.CreateToken( cz_Token_Msgs ,	-1 ,  2 ) );
						this._BDCMain.Tokens.Add(	cz_Token_Exec , this.CreateToken( cz_Token_Exec ,	-1 ,  4 ) );
						this._BDCMain.Tokens.Add(	cz_Token_Data , this.CreateToken( cz_Token_Data ,	-1 ,  6 ) );
						this._BDCMain.Tokens.Add(	cz_Token_HdrE , this.CreateToken( cz_Token_HdrE ,	 9 , -1 ) );
						this._BDCMain.Tokens.Add(	cz_Token_XCfg	, this.CreateToken( cz_Token_XCfg	,	-1 , -1 ) );
						//.............................................
						this._BDCMain.Tokens.Add(	cz_Instr_Post , this.CreateToken( cz_Instr_Post ,	-1 , -1 ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UpdateHeaderRowReference()
					{
						this._BDCMain.BDCHeaderRowRef.Prog	= this.SetRowReference( cz_Token_Prog	);
						this._BDCMain.BDCHeaderRowRef.Scrn	= this.SetRowReference( cz_Token_Scrn	);
						this._BDCMain.BDCHeaderRowRef.Strt	= this.SetRowReference( cz_Token_Begn	);
						this._BDCMain.BDCHeaderRowRef.OKCd	= this.SetRowReference( cz_Token_OKCd	);
						this._BDCMain.BDCHeaderRowRef.Curs	= this.SetRowReference( cz_Token_Crsr	);
						this._BDCMain.BDCHeaderRowRef.Subs	= this.SetRowReference( cz_Token_Subs	);
						this._BDCMain.BDCHeaderRowRef.FldN	= this.SetRowReference( cz_Token_FNme	);
						this._BDCMain.BDCHeaderRowRef.Desc	= this.SetRowReference( cz_Token_Desc	);
						this._BDCMain.BDCHeaderRowRef.Inst	= this.SetRowReference( cz_Token_Inst	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int SetRowReference( string token )
					{
						if ( this._BDCMain.Tokens.TryGetValue( token , out DTO_TokenReference lo_Token ) )
							{
								return	lo_Token.Row;
							}
						else
							{
								return	-1;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddZDTONTokenDefault( string token, ZDTON_RowNo row )
					{
						this._BDCMain.Tokens.Add(	token ,	this.CreateToken(	token	,	(int)row ) );
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
