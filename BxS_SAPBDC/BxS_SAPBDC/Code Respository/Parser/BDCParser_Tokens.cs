using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class Parser_Tokens
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	Parser_Tokens(	BDCMain										BDCMain
																,	Func<DTO_TokenReference>	createToken	)
					{
						this._BDCMain				= BDCMain			;
						this._CreateToken		= createToken	;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private readonly	BDCMain										_BDCMain			;
				private readonly	Func<DTO_TokenReference>	_CreateToken	;

			#endregion

			//"@@EXEC"

			private const string t1	= "<@@PROGRAM>"	;
			private const string t2	= "<@@DYNNO>"		;

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

								lo_Tasks.Add(	Task.Run(	() => {
																								for ( int r = this._BDCMain.RowLB; r < this._BDCMain.RowUB; r++ )
																									{
																										for ( int c = this._BDCMain.ColLB; c < this._BDCMain.ColUB; c++ )
																											{
																												if ( this._BDCMain.Data[r, c]?.Contains(lc_ST) == true )
																													{
																														lo_Token.Row		= r;
																														lo_Token.Col		= c;
																														lo_Token.Value	= this._BDCMain.Data[ r , c ];

																														return;
																													}
																											}
																									}
																							}
														) );
							}

						await Task.WhenAll(lo_Tasks).ConfigureAwait(false);
						this.UpdateHeaderRowReference();
						//.............................................
						return	lo_Tasks.Count;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTokens()
					{
						foreach (DTO_TokenReference lo_Token in this._BDCMain.Constants.GetTokenList())
							{
							}


						this._BDCMain.Tokens.Add( t1, this.CreateToken(	t1, 0, 0 ) );
						this._BDCMain.Tokens.Add( t2, this.CreateToken(	t2, 2, 1 ) );
						this._BDCMain.Tokens.Add( t3, this.CreateToken(	t3, 3, 1 ) );
						this._BDCMain.Tokens.Add( t4, this.CreateToken(	t4, 4, 1 ) );
						this._BDCMain.Tokens.Add( t5, this.CreateToken(	t5, 5, 1 ) );
						this._BDCMain.Tokens.Add( t6, this.CreateToken(	t6, 6, 1 ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UpdateHeaderRowReference()
					{
						//.............................................
						if ( this._BDCMain.Tokens.TryGetValue( t1 , out DTO_TokenReference lo_Token ) )
							{
								this._BDCMain.BDCHeaderRowRef.Prog	= lo_Token.Row;
							}
						//.............................................
						if ( this._BDCMain.Tokens.TryGetValue( t2 , out lo_Token ) )
							{
								this._BDCMain.BDCHeaderRowRef.Scrn	= lo_Token.Row;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_TokenReference CreateToken( string token, int row, int col )
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
