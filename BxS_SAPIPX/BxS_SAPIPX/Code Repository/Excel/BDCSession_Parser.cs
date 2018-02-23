using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Excel
{
	internal class BDCSession_Parser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession_Parser()
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Parse1Dto2D( DTO_BDCSessionRequest DTO , bool resetSource = true )
					{
						int[]	lt_UB = new int[2];
						int[]	lt_LB = new int[2];

						lt_UB[0] =	DTO.RowUB;
						lt_UB[1] =	DTO.ColUB;
						lt_LB[0] =	DTO.RowLB;
						lt_LB[1] =	DTO.ColLB;

						DTO.WSData	= (string[,])Array.CreateInstance( typeof(string) , lt_UB, lt_LB );
						//.............................................
						int	ln_Row	= 0;
						int ln_Col	= 0;

						foreach ( KeyValuePair<string, string> ls_kvp in DTO.WSData1D )
							{
								string[] lt_Idx = ls_kvp.Key.Split(',');

								ln_Row	= int.Parse(lt_Idx[0]);
								ln_Col	= int.Parse(lt_Idx[1]);

								DTO.WSData[ln_Row,ln_Col]	= ls_kvp.Value;
							}
						//.............................................
						if (resetSource)	DTO.WSData1D.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Parse2Dto1D( DTO_BDCSessionRequest DTO, bool resetSource = true )
					{
						DTO.RowLB	= DTO.WSCells.GetLowerBound(0);
						DTO.RowUB	= DTO.WSCells.GetUpperBound(0);
						DTO.ColLB	= DTO.WSCells.GetLowerBound(1);
						DTO.ColUB	= DTO.WSCells.GetUpperBound(1);

						DTO.WSData1D.Clear();
						//.............................................
						for (int r = DTO.RowLB; r <= DTO.RowUB; r++)
							{
								for (int c = DTO.ColLB; c <= DTO.ColUB; c++)
									{
										if ( DTO.WSCells[r,c] != null )
											{
												string lc_Key	= $"{r.ToString()},{c.ToString()}";

												DTO.WSData1D.Add( lc_Key , DTO.WSCells[r,c].ToString() );
											}
									}
							}
						//.............................................
						if ( resetSource )	DTO.WSCells = null;
					}

			#endregion

		}
}
