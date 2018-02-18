using System.Collections.Generic;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.BDCParser
{
	internal class BDCWorksheetParser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCWorksheetParser()
					{
						this._Tokens	= new Dictionary<	string , TokenReference >();
						//.............................................
						this.LoadSearchTokens();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	string[,]	_Data;
				//.................................................
				private	Dictionary<	string , TokenReference >		_Tokens;
				//.................................................
				private int _RowLB;
				private int _RowUB;
				private int _ColLB;
				private int _ColUB;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int TokenCount { get { return	this._Tokens.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<int> ParseWorksheet( string[,] data	)
					{
						this._Data	= data;
						//.............................................
						if ( this.SetupProcessBoundaries( data ) )
							{
								await this.SearchForTokens().ConfigureAwait(false);
								//.........................................
							}
						//.............................................
						return	0;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task<int> SearchForTokens()
					{
						IList< Task	>	lo_Tasks	= new	List< Task >(this._Tokens.Count);
						//.............................................
						foreach ( KeyValuePair<string, TokenReference> ls_kvp in this._Tokens )
							{
								string	lc_ST	= ls_kvp.Key;
								lo_Tasks.Add(	Task.Run(	() =>	this.ProcessToken( lc_ST ) ) );
							}

						await Task.WhenAll(lo_Tasks).ConfigureAwait(false);
						//.............................................
						return	this.TokenCount;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool SetupProcessBoundaries( string[,] data )
					{
						this._RowLB		= data.GetLowerBound(0);
						this._RowUB		= data.GetUpperBound(0)	+ 1;
						//.............................................
						this._ColLB		= data.GetLowerBound(1);
						this._ColUB		= data.GetUpperBound(1) + 1;
						//.............................................
						return	true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSearchTokens()
					{
						this.AddToken(	"<@@PROGRAM>"				, 0, 0 );
						this.AddToken(	"<@@DYNNO>"					, 2, 1 );
						this.AddToken(	"<@@DYNSTART>"			, 3, 1 );
						this.AddToken(	"<@@CURSORBEFORE>"	, 4, 1 );
						this.AddToken(	"<@@SPECIALINST>"		, 5, 1 );
						this.AddToken(	"<@@OKCODE>"				, 6, 1 );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ProcessToken( string token )
					{
						for ( int r = this._RowLB; r < this._RowUB; r++ )
							{
								for ( int c = this._ColLB; c < this._ColUB; c++ )
									{
										if ( this._Data[r,c].Contains( token ) )
											{
												if ( this._Tokens.TryGetValue( token, out TokenReference lo_Token ) )
													{
														lo_Token.Row		= r;
														lo_Token.Col		= c;
														lo_Token.Value	= this._Data[ r , c ];

														return;
													}
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddToken( string token, int row, int col )
					{
						this._Tokens.Add(	token	, new TokenReference() {	Token	= token
																														, Row		= row
																														, Col		= col
																														, Value	=	token } );
					}

			#endregion

			//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
			//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
			internal class TokenReference
				{
					public	string	Token		{	get; set; }
					public	int			Row			{	get; set; }
					public	int			Col			{	get; set; }
					public	string	Value		{	get; set; }
				}
		}
}
