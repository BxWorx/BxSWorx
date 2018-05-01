using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

using BxS_WorxIPX.Toolset;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_010_IPX_Controller
		{
			private	readonly IPX_Controller	co_Cntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_010_IPX_Controller()
				{
					this.co_Cntlr		= IPX_Controller.Instance	;
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_010_IPXCntlr_10_Instantiate()
				{
					Assert.IsNotNull( this.co_Cntlr	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_010_IPXCntlr_20_CreateObjs()
				{
					IBDC_Controller	lo_RM = this.co_Cntlr.Create_BDCController();
					ISAP_Logon			lo_SL	= this.co_Cntlr.Create_SAPLogon();

					Assert.IsNotNull( lo_RM	, "" );
					Assert.IsNotNull( lo_SL	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_010_IPXCntlr_30_ExtMth()
				{
					IUser	l0	= new User();
					IUser l1	= new User();
					//...
					l0.Name	= "A";
					l1.Name	= "B";
					l0.CopyPropertiesFrom( l1 );
					Assert.AreEqual( l0.Name , l1.Name	, "" );
					//...
					l1.Name	="C";
					Assert.AreNotEqual( l0.Name , l1.Name	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_010_IPXCntlr_32_ExtMth_Skip()
				{
					IXMLConfig	lo_X	= new XMLConfig();
					ISession		l0		= new Session( lo_X )	{ WBID	= "Y" }	;
					ISession		l1		= new Session( lo_X	) { WBID	= "X" } ;
					//...
					l0.CopyPropertiesFrom( l1 );

					Assert.AreEqual		( l0.WBID , l1.WBID	, "" );
					Assert.AreEqual		( "X"			, l0.WBID	, "" );
					Assert.AreNotEqual( l0.ID		, l1.ID		, "" );
				}

			//.

		}
	}
