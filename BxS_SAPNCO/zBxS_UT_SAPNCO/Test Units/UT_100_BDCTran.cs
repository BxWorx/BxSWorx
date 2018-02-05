using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_100_BDCTran
		{
			#region "Declarations"

				private readonly	UT_Destination	co_Dest;
				private readonly	NCOController		co_Cntlr;

			#endregion

			//...................................................
			public UT_100_BDCTran()
				{
					this.co_Dest		= new UT_Destination(1);
					this.co_Cntlr		= new NCOController();
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCCallTransaction	lo_BDCTran0		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);
					IBDCCallTransaction	lo_BDCTran1		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);

					Assert.IsNotNull(	lo_BDCTran0	,	$"SAPNCO:BDCTran:Inst {ln_Cnt}: 1st" );
					Assert.IsNotNull(	lo_BDCTran1	,	$"SAPNCO:BDCTran:Inst {ln_Cnt}: 2nd" );

					lo_BDCTran0.SAPTransaction	= "0";
					lo_BDCTran1.SAPTransaction	= "1";

					Assert.AreEqual( lo_BDCTran0.SAPTransaction	, "0"	,	$"SAPNCO:BDCTran:Indi {ln_Cnt}: 1st" );
					Assert.AreEqual( lo_BDCTran1.SAPTransaction	, "1"	,	$"SAPNCO:BDCTran:Indi {ln_Cnt}: 2nd" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Invoke()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_CTU	= new CTU_Parameters();

					lo_CTU.DisplayMode		= lo_CTU.DisplayMode_All	;
					lo_CTU.UpdateMode			= lo_CTU.UpdateMode_ASync	;
					lo_CTU.CATTMode				= lo_CTU.CATTMode_None		;
					lo_CTU.DefaultSize		= lo_CTU.Setas_Yes				;
					lo_CTU.NoCommit				= lo_CTU.Setas_No					;
					lo_CTU.NoBatchInpFor	= lo_CTU.Setas_No					;
					lo_CTU.NoBatchInpAft	= lo_CTU.Setas_No					;

					IBDCCallTransaction	lo_BDCTran0		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);

					lo_CTU.TransferImage(lo_BDCTran0.CTUParm);

					lo_BDCTran0.SAPTransaction	= "SM13";

					lo_BDCTran0.CreateBDCEntry("SAPMSM13",1000,true,"BDC_OKCODE", "=ENTER", true);
					lo_BDCTran0.CreateBDCEntry("SAPMSSY0",0120,true,"BDC_OKCODE", "=&F03"	, true);
					lo_BDCTran0.CreateBDCEntry("SAPMSM13",1000,true,"BDC_OKCODE", "=BACK"	, true);

					lo_BDCTran0.Invoke();

				}
		}
}
