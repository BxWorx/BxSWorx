using System;
using System.Text;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxExcel.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal class BDC_Handler
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Handler( Lazy<IBDCx_Controller> ipxBDCCntlr )
					{
						this._IPXBDCCntlr	= ipxBDCCntlr;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy<IBDCx_Controller>	_IPXBDCCntlr;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IXMLConfig CreateXMLConfig()	=> this._IPXBDCCntlr.Value.Create_XMLConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void TransferWSDatatoSession(	DTO_WSData	wsData
																							,	ISession		session	)
					{
						session.WBID				= wsData.WBID	;
						session.WSID				= wsData.WSID	;
						session.UsedAddress	= wsData.UsedAddress	;
						//...
						this.WSToSession( wsData , session );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// process each worksheet object array into dictionary with dictionary key having the 
				// syntax of WSCell[row , col ] == "row,col"
				//
				private void WSToSession(		DTO_WSData	wsData
																	,	ISession		session	)
					{
						session.WSData.Clear();

						if ( wsData.WSCells == null )
							{
								session.RowLB		= -1;
								session.RowUB		= -1;
								session.ColLB		= -1;
								session.ColUB		= -1;
							}
						else
							{
								var	lo_SB		= new StringBuilder();
								//...
								session.RowLB		= wsData.WSCells.GetLowerBound(0);
								session.RowUB		= wsData.WSCells.GetUpperBound(0);
								session.ColLB		= wsData.WSCells.GetLowerBound(1);
								session.ColUB		= wsData.WSCells.GetUpperBound(1);
								//...
								for ( int	r = session.RowLB; r <= session.RowUB; r++ )
									{
										for ( int c = session.ColLB; c <= session.ColUB; c++ )
											{
												if ( wsData.WSCells[r,c] != null )
													{
														if ( wsData.WSCells[r,c].ToString().Contains( this._IPXBDCCntlr.Value.XmlConfigTag ) )
															{
																session.XMLConfig		=	this._IPXBDCCntlr.Value.DeserializeXMLConfig( wsData.WSCells[r,c].ToString() );
																continue;
															}
														//...
														lo_SB.Clear();
														lo_SB.AppendFormat( $"{r.ToString()},{c.ToString()}" );
														//...
														session.WSData.Add( lo_SB.ToString() , wsData.WSCells[r,c].ToString() );
													}
											}
									}
							}
					}

			#endregion

		}
}
