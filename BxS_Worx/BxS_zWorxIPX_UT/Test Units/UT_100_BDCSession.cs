using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.SAPBDCSession;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_BDCSession
		{
			private	const			string	_Nme	=   "Test-00.xml"									;
			private	const			string	_Path	=  @"C:\ProgramData\BxS_Worx"	;
			private	readonly	string	_Full	;

			private	readonly	IPX_Controller			co_Cntlr;
			private	readonly	IBDCRequestManager	co_RM;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_BDCSession()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_RM			= this.co_Cntlr.Create_BDCRequestManager();

					this._Full		= $@"{_Path}\{_Nme}"	;
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_IPXCntlr_10_Instantiate()
				{
					Assert.IsNotNull( this.co_Cntlr	, "" );
					Assert.IsNotNull( this.co_RM		, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ParseWS_20_FileToRequestFail()
				{
					try
						{
							const string			lc_Path	= "XXXX";
							ISAP_BDCRequest	lo_R0		= this.co_RM.Read_BDCRequest( lc_Path );
							Assert.Fail( "A" );
						}
					catch	{	}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ParseWS_30_FileToRequestPass()
				{
					ISAP_BDCRequest lo_R0	= null;
					try
						{
							lo_R0		= this.co_RM.Read_BDCRequest( this._Full );
						}
					catch
						{
							Assert.Fail( "A" );
						}

					Assert.IsNotNull(	lo_R0									, "" );
					Assert.IsNotNull(	lo_R0.Sessions.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ParseWS_40_FileToRequestMany()
				{
					ISAP_BDCRequest lo_R0	= null;
					try
						{
							const string			lc_Path	= @"C:\ProgramData\BxS_Worx\DPB.xml";
							lo_R0		= this.co_RM.Read_BDCRequest( lc_Path );
						}
					catch
						{
							Assert.Fail( "A" );
						}

					Assert.IsNotNull	(			lo_R0									, "" );
					Assert.IsNotNull	(			lo_R0.Sessions.Count	, "" );
					Assert.AreNotEqual( 0 , lo_R0.Sessions.Count	, "" );
				}

			//.

		}

	//.

	}
