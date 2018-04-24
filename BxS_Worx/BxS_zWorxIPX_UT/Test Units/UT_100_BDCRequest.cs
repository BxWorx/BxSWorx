using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.BDCSAP;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_BDCRequest
		{
			private	const	string	_Nme	=  "Test-00"									;
			private	const	string	_Path	=  @"C:\Users\BMA\GitHub\BxSWorx\BxS_Worx\BxS_zWorxIPX_UT\Test Resources";
			private				string	_Full	;

			private	readonly	IPX_Controller			co_Cntlr;
			private	readonly	IBDCRequestManager	co_RM;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_BDCRequest()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
					this.co_RM			= this.co_Cntlr.Create_BDCRequestManager();
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCReqst_10_Instantiate()
				{
					Assert.IsNotNull( this.co_Cntlr	, "" );
					Assert.IsNotNull( this.co_RM		, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCReqst_20_FileToRequestFail()
				{
					try
						{
							this.SetFullPath("XXXX");
							ISAP_BDCRequest	lo_R0		= this.co_RM.Read_BDCRequest( this._Full );
							Assert.Fail( "A" );
						}
					catch	{	}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_BDCReqst_30_FileToRequestPass()
				{
					ISAP_BDCRequest lo_R0	= null;
					//...
					try
						{
							this.SetFullPath(_Nme);
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
			public void UT_100_BDCReqst_40_FileToRequestMany()
				{
					ISAP_BDCRequest lo_R0	= null;
					//...
					try
						{
							this.SetFullPath("DPB");
							lo_R0		= this.co_RM.Read_BDCRequest( this._Full );
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

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void SetFullPath( string name )	=>	this._Full	= $@"{_Path}\{name}.xml" ;

			//.

		}

	//.

	}
