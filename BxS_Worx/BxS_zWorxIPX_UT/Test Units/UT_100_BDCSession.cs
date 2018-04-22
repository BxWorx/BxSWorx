using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_BDCSession
		{
			private	const			string	_Nme	=   "xx.xml"									;
			private	const			string	_Path	=  @"C:\ProgramData\BxS_Worx"	;
			private	readonly	string	_Full	;

			private	readonly IPX_Controller	co_Cntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_BDCSession()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this._Full		= $@"{_Path}\{_Nme}"	;

					Assert.IsNotNull	( this.co_Cntlr	, "" )	;
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_IPXCntlr_10_Instantiate()
				{
					Assert.IsNotNull( this.co_Cntlr	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ParseWS_20_FileToRequestFail()
				{
					try
						{
							const string			lc_Path	= "XXXX";
							IExcel_BDCRequest	lo_R0		= this.co_Cntlr.ReadExcelBDCRequest( lc_Path );
							Assert.Fail( "A" );
						}
					catch	{	}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ParseWS_30_FileToRequestPass()
				{
					IExcel_BDCRequest lo_R0	= null;

					try
						{
							lo_R0		= this.co_Cntlr.ReadExcelBDCRequest( this._Full );
						}
					catch
						{
							Assert.Fail( "A" );
						}

					Assert.IsNotNull(	lo_R0							, "" );
					Assert.IsNotNull(	lo_R0.Worksheets	, "" );
				}

			//[TestMethod]
			////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//public void UT_100_ParseWS_20_FileToRequest()
			//	{
			//		string	lc_Nme	=   "xxx.xml"									;
			//		string	lc_Path	=  @"C:\ProgramData\BxS_Worx"	;
			//		string	lc_Full	= $@"{lc_Path}\{lc_Nme}"			;

			//		IExcelBDC_Request lo_R0		= this.co_Cntlr.XMLFiletoExcelBDCRequest( lc_Path );
			//		Assert.IsNotNull( lo_R0	, "" );
			//		Assert.AreEqual	( -1 , lo_R0.RowLB , "" );
			//		//...............................................
			//		lo_WS.WSCells	= new object[ 10, 10 ];

			//		for (int r = 0; r < 10; r++)
			//			{
			//				for (int c = 0; c < 10; c++)
			//					{
			//						if ( c != 0 )
			//							{
			//								lo_WS.WSCells[r,c]	= $"{r.ToString()},{c.ToString()}";
			//							}
			//					}
			//			}

			//		IExcelBDC_Request lo_R1		= this.co_Cntlr.ParseWStoRequest( lo_WS );

			//		Assert.AreEqual	( 90	,	lo_R1.WSData1D.Count , "" );
			//	}

			//.

		}

	//.

	}
