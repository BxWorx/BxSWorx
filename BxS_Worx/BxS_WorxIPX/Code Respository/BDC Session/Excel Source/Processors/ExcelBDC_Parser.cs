using System.Text;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	internal class ExcelBDC_Parser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ExcelBDC_Parser()
					{	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseWStoRequest( IExcelBDC_WS ws , IExcelBDC_Request request )
					{
						var	lo_SB	= new StringBuilder();
						//.............................................
						request.WBID					= ws.WBID					;
						request.WSID					= ws.WSID					;
						request.WSNo					= ws.WSNo					;
						request.UsedAddress		= ws.UsedAddress	;
						request.IsTest				= ws.IsTest				;
						request.IsActive			= ws.IsActive			;
						//.............................................
						request.WSData1D.Clear();

						if ( ws.WSCells == null )
							{
								request.RowLB		= -1	;
								request.RowUB		= -1	;
								request.ColLB		= -1	;
								request.ColUB		= -1	;
							}
						else
							{
								request.RowLB		= ws.WSCells.GetLowerBound(0);
								request.RowUB		= ws.WSCells.GetUpperBound(0);
								request.ColLB		= ws.WSCells.GetLowerBound(1);
								request.ColUB		= ws.WSCells.GetUpperBound(1);
								//.............................................
								for ( int	r = request.RowLB; r <= request.RowUB; r++ )
									{
										for ( int c = request.ColLB; c <= request.ColUB; c++ )
											{
												if ( ws.WSCells[r,c] != null )
													{
														lo_SB.Clear();
														lo_SB.AppendFormat( $"{r.ToString()},{c.ToString()}" );
														request.WSData1D.Add( lo_SB.ToString() , ws.WSCells[r,c].ToString() );
													}
											}
									}
							}
					}

			#endregion

		}
}
