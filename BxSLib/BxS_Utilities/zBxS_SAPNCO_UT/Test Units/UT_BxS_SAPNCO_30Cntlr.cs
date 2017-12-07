using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using BxS_SAPNCO.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_30DestCntlr
	{
			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_DestCntlr_Get()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var	lo_Cntlr	= new DestinationController(true);
					Assert.IsNotNull(	lo_Cntlr	,	$"SAPNCO:DestCntlr:With {ln_Cnt}: Inst"	);
					//...............................................
					ln_Cnt	++;
					IList<BxS_SAPNCO.API.DL.IDTORefEntry> lt_Lst	= lo_Cntlr.ConnectionReferenceList();
					Assert.AreNotEqual(0,	lt_Lst.Count,	$"SAPNCO:DestCntlr:With {ln_Cnt}: List"		);
					//...............................................
					ln_Cnt	++;

					SMC.RfcCustomDestination lo_Dest0 = lo_Cntlr.GetRfcDestination(lt_Lst[9].ID);
					SMC.RfcCustomDestination lo_Dest1	= lo_Cntlr.GetRfcDestination(lt_Lst[9].ID);

					Assert.IsNotNull(	lo_Dest0	,	$"SAPNCO:DestCntlr:With {ln_Cnt}: Inst"	);
					Assert.IsNotNull(	lo_Dest1	,	$"SAPNCO:DestCntlr:With {ln_Cnt}: Inst"	);

					lo_Dest0.Client		= "700";
					lo_Dest0.User			= "DERRICKBINGH";
					lo_Dest0.Password	= "M@@n0987";

					lo_Dest1.Client		= "700";
					lo_Dest1.User			= "DERRICKBINGH";
					lo_Dest1.Password	= "M@@n0987";

					try
						{
							lo_Dest0.Ping();
							lo_Dest1.Ping();
						}
					catch (System.Exception)
						{
							int xx = 0; xx++;
						}
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_DestCntlr_LoadSAPINi()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var	lo_Cntlr	= new DestinationController(true);
					Assert.IsNotNull(	lo_Cntlr	,	$"SAPNCO:DestCntlr:With {ln_Cnt}: Inst"	);
					//...............................................
					ln_Cnt	++;
					IList<BxS_SAPNCO.API.DL.IDTORefEntry> lt_Lst	= lo_Cntlr.ConnectionReferenceList();

					Assert.AreNotEqual(0,	lt_Lst.Count,	$"SAPNCO:DestCntlr:With {ln_Cnt}: List"		);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_DestCntlr_NoSAPINi()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var	lo_Cntlr	= new DestinationController(false);
					Assert.IsNotNull(	lo_Cntlr	,	$"SAPNCO:DestCntlr:Without {ln_Cnt}: Inst"	);
					//...............................................
					ln_Cnt	++;
					IList<BxS_SAPNCO.API.DL.IDTORefEntry>	lt_Lst	= lo_Cntlr.ConnectionReferenceList();

					Assert.AreEqual(0,	lt_Lst.Count,	$"SAPNCO:DestCntlr:Without {ln_Cnt}: List"		);
				}
	}
}
