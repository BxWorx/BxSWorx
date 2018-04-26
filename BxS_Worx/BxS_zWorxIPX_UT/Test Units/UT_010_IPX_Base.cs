using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;

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
		}

	//.

	}
