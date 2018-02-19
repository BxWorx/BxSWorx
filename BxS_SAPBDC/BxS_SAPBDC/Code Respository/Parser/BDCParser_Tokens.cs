using System.Collections.Generic;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal partial class BDCParser
		{
			//"@@EXEC"

			private const string t1	= "<@@PROGRAM>"	;
			private const string t2	= "<@@DYNNO>"		;

			//===========================================================================================
			#region "Methods: Exposed: Tokens"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task< int > ParseForTokens( BDCMain BDCMain )
					{
						this.LoadTokens( BDCMain );
						IList< Task	>	lo_Tasks	= new	List< Task >( BDCMain.Tokens.Count );
						//.............................................
						foreach ( KeyValuePair< string , DTO_TokenReference > ls_kvp in BDCMain.Tokens )
							{
								string							lc_ST			= ls_kvp.Key		;
								DTO_TokenReference	lo_Token	= ls_kvp.Value	;

								lo_Tasks.Add(	Task.Run(	() => {
																								for ( int r = BDCMain.RowLB; r < BDCMain.RowUB; r++ )
																									{
																										for ( int c = BDCMain.ColLB; c < BDCMain.ColUB; c++ )
																											{
																												if ( BDCMain.Data[r, c]?.Contains(lc_ST) == true )
																													{
																														lo_Token.Row		= r;
																														lo_Token.Col		= c;
																														lo_Token.Value	= BDCMain.Data[ r , c ];

																														return;
																													}
																											}
																									}
																							}
														) );
							}

						await Task.WhenAll(lo_Tasks).ConfigureAwait(false);
						this.UpdateHeaderRowReference( BDCMain );
						//.............................................
						return	lo_Tasks.Count;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTokens( BDCMain BDCMain )
					{
						const string t3	= "<@@DYNSTART>"			;
						const string t4	= "<@@CURSORBEFORE>	"	;
						const string t5	= "<@@SPECIALINST>	"	;
						const string t6	= "<@@OKCODE>"				;

						BDCMain.Tokens.Add( t1, this.CreateToken(	t1, 0, 0 ) );
						BDCMain.Tokens.Add( t2, this.CreateToken(	t2, 2, 1 ) );
						BDCMain.Tokens.Add( t3, this.CreateToken(	t3, 3, 1 ) );
						BDCMain.Tokens.Add( t4, this.CreateToken(	t4, 4, 1 ) );
						BDCMain.Tokens.Add( t5, this.CreateToken(	t5, 5, 1 ) );
						BDCMain.Tokens.Add( t6, this.CreateToken(	t6, 6, 1 ) );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UpdateHeaderRowReference( BDCMain BDCMain )
					{
						//.............................................
						if ( BDCMain.Tokens.TryGetValue( t1 , out DTO_TokenReference lo_Token ) )
							{
								BDCMain.BDCHeaderRowRef.Prog	= lo_Token.Row;
							}
						//.............................................
						if ( BDCMain.Tokens.TryGetValue( t2 , out lo_Token ) )
							{
								BDCMain.BDCHeaderRowRef.Scrn	= lo_Token.Row;
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
