//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal class ExcelBDCSession_Parser
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseWStoRequest( IExcelBDCSessionWS ws , IExcelBDCSessionRequest request )
					{
						request.WBID					= ws.WBID					;
						request.WSID					= ws.WSID					;
						request.WSNo					= ws.WSNo					;
						request.UsedAddress		= ws.UsedAddress	;
						request.IsTest				= ws.IsTest				;
						//.............................................
						request.RowLB	= ws.WSCells.GetLowerBound(0);
						request.RowUB	= ws.WSCells.GetUpperBound(0);
						request.ColLB	= ws.WSCells.GetLowerBound(1);
						request.ColUB	= ws.WSCells.GetUpperBound(1);
						//.............................................
						request.WSData1D.Clear();

						for (int r = request.RowLB; r <= request.RowUB; r++)
							{
								for (int c = request.ColLB; c <= request.ColUB; c++)
									{
										if ( ws.WSCells[r,c] != null )
											{
												string lc_Key	= $"{r.ToString()},{c.ToString()}";

												request.WSData1D.Add( lc_Key , ws.WSCells[r,c].ToString() );
											}
									}
							}
					}

			#endregion

		}
}
