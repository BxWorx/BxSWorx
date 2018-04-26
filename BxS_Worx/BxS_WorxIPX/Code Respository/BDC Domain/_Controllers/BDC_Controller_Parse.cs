using System;
using System.Collections.Generic;
using System.Text;
//.........................................................
using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public partial class BDC_Controller
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PreTransport( IRequest request )
					{
						//
						foreach (	KeyValuePair<Guid , ISession> ls_kvp in request.Sessions )
							{
								this.WSToSession( ls_kvp.Value );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PostTransport( IRequest request )
					{
						//
						foreach (	KeyValuePair<Guid , ISession> ls_kvp in request.Sessions )
							{
								this.SessionToWS( ls_kvp.Value );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SessionToWS( ISession	session )
					{
						if ( session.WSData.Count.Equals(0) )		return;
						//.............................................
						int[]	lt_UB		= new int[2];
						int[]	lt_LB		= new int[2];

						lt_UB[0]	=	session.RowUB;
						lt_UB[1]	=	session.ColUB;
						lt_LB[0]	=	session.RowLB;
						lt_LB[1]	=	session.ColLB;

						session.WSCells	= ( string[,] ) Array.CreateInstance( typeof( string ) , lt_UB, lt_LB );
						//.............................................
						int	ln_Row	= 0;
						int ln_Col	= 0;

						foreach ( KeyValuePair<string, string> ls_kvp in session.WSData )
							{
								string[] lt_Idx		= ls_kvp.Key.Split( cz_Coma );

								ln_Row	= int.Parse( lt_Idx[0] );
								ln_Col	= int.Parse( lt_Idx[1] );

								session.WSCells[ln_Row , ln_Col]		= ls_kvp.Value;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// process each worksheet object array into dictionary with dictionary key having the 
				// syntax of WSCell[row , col ] == "row,col"
				//
				private void WSToSession( ISession session )
					{
						session.WSData.Clear();
						//.............................................
						if ( session.WSCells == null )
							{
								session.RowLB		= -1	;
								session.RowUB		= -1	;
								session.ColLB		= -1	;
								session.ColUB		= -1	;
							}
						else
							{
								var	lo_SB		= new StringBuilder();
								//...
								session.RowLB		= session.WSCells.GetLowerBound(0);
								session.RowUB		= session.WSCells.GetUpperBound(0);
								session.ColLB		= session.WSCells.GetLowerBound(1);
								session.ColUB		= session.WSCells.GetUpperBound(1);
								//...
								for ( int	r = session.RowLB; r <= session.RowUB; r++ )
									{
										for ( int c = session.ColLB; c <= session.ColUB; c++ )
											{
												if ( session.WSCells[r,c] != null )
													{
														if ( session.WSCells[r,c].ToString().Contains( cz_XmlCfgTag ) )
															{
																session.XMLConfig		=	this.Serializer.DeSerialize<IXMLConfig>(	session.WSCells[r,c].ToString()
																																															, this._CfgTypes.Value						);
																continue;
															}
														//...
														lo_SB.Clear();
														lo_SB.AppendFormat( $"{r.ToString()},{c.ToString()}" );
														//...
														session.WSData.Add( lo_SB.ToString() , session.WSCells[r,c].ToString() );
													}
											}
									}
							}
						//.............................................
						session.WSCells		= null;
					}

			#endregion

		}
}
