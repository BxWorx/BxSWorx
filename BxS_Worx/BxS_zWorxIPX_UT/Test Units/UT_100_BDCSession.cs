using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_BDCSession
		{
			private	readonly IPX_Controller	co_Cntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_BDCSession()
				{
					this.co_Cntlr		= IPX_Controller.Instance;
					Assert.IsNotNull	( this.co_Cntlr	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ParseWS_10_Base()
				{
					IExcelBDC_WS			lo_WS		= this.co_Cntlr.CreateBDCSessionWS()			;
					IExcelBDC_Request lo_R0		= this.co_Cntlr.ParseWStoRequest( lo_WS )	;

					Assert.IsNotNull( lo_WS	, "" );
					Assert.IsNotNull( lo_R0	, "" );

					Assert.AreEqual	( -1 , lo_R0.RowLB , "" );
					//...............................................
					lo_WS.WSCells	= new object[ 10, 10 ];

					for (int r = 0; r < 10; r++)
						{
							for (int c = 0; c < 10; c++)
								{
									if ( c != 0 )
										{
											lo_WS.WSCells[r,c]	= $"{r.ToString()},{c.ToString()}";
										}
								}
						}

					IExcelBDC_Request lo_R1		= this.co_Cntlr.ParseWStoRequest( lo_WS );

					Assert.AreEqual	( 90	,	lo_R1.WSData1D.Count , "" );
				}

			//.

		}

	//.

	}
