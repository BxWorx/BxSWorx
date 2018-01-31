using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_30DestCntlr
	{
			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_Cntlr_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var	lo_Cntlr1	= new NCOController();
					var	lo_Cntlr2	= new NCOController();
					var	lo_Cntlr3	= new NCOController(LoadSAPGUIConfig: true, FirstReset: true);

					Assert.IsNotNull(	lo_Cntlr1	,	$"SAPNCO:Cntlr:With {ln_Cnt}: Inst1" );
					Assert.IsNotNull(	lo_Cntlr2	,	$"SAPNCO:Cntlr:With {ln_Cnt}: Inst2" );
					Assert.IsNotNull(	lo_Cntlr3	,	$"SAPNCO:Cntlr:With {ln_Cnt}: Inst3" );

					lo_Cntlr3.LoadSAPGUIConfig(true);

					Assert.AreNotEqual(0,	lo_Cntlr3.GetSAPGUIConfigEntries().Count,	$"SAPNCO:Cntlr:Reload {ln_Cnt}: Count" );
					//...............................................
					ln_Cnt	++;

					IList<string>				lt1	= lo_Cntlr1.GetSAPGUIConfigEntries();
					IList<IDTORefEntry> lt2	= lo_Cntlr2.ConnectionReferenceList();
					IList<IDTORefEntry> lt3	= lo_Cntlr3.ConnectionReferenceList();

					Assert.AreNotEqual(0,	lt1.Count,	$"SAPNCO:Cntlr:List1 {ln_Cnt}: Count"		);
					Assert.AreNotEqual(0,	lt2.Count,	$"SAPNCO:Cntlr:List2 {ln_Cnt}: Count"		);
					Assert.AreNotEqual(0,	lt3.Count,	$"SAPNCO:Cntlr:List3 {ln_Cnt}: Count"		);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_Cntlr_GetDestination()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var	lo_Cntlr1	= new NCOController();
					Assert.IsNotNull(	lo_Cntlr1	,	$"SAPNCO:Cntlr:With {ln_Cnt}: Inst1" );
					//...............................................
					ln_Cnt	++;

					IList<string>		lt1	= lo_Cntlr1.GetSAPGUIConfigEntries();

					string	lc_ID0		= lt1.FirstOrDefault(s => s.Contains("PWD"));
					string	lc_ID1		= lt1.FirstOrDefault(s => s.Contains("SSO"));

					DestinationRfc	lo_Desto	= lo_Cntlr1.GetDestination(lc_ID0);
					DestinationRfc	lo_Destx	= lo_Cntlr1.GetDestination(lc_ID1);

					lo_Desto.Client		= "700";
					lo_Desto.User			= "DERRICKBINGH";
					lo_Desto.Password	= "M@@n1234";

					lo_Destx.Client			= "700";
					lo_Destx.User				= "DERRICKBINGH";
					lo_Destx.SNCLibPath	= "C:\\TEMP\\gx64krb5.DLL";

					Assert.IsTrue(	lo_Desto.Ping(),	$"SAPNCO:Cntlr:Ping {ln_Cnt}: True" );
					Assert.IsTrue(	lo_Destx.Ping(),	$"SAPNCO:Cntlr:Ping {ln_Cnt}: True" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_DestCntlr_NoSAPINi()
				{
					//int	ln_Cnt	= 0;
					////...............................................
					//ln_Cnt	++;

					//var	lo_Cntlr	= new DestinationController(false);
					//Assert.IsNotNull(	lo_Cntlr	,	$"SAPNCO:DestCntlr:Without {ln_Cnt}: Inst"	);
					////...............................................
					//ln_Cnt	++;
					//IList<BxS_SAPNCO.API.DL.IDTORefEntry>	lt_Lst	= lo_Cntlr.ConnectionReferenceList();

					//Assert.AreEqual(0,	lt_Lst.Count,	$"SAPNCO:DestCntlr:Without {ln_Cnt}: List"		);
				}
	}
}
