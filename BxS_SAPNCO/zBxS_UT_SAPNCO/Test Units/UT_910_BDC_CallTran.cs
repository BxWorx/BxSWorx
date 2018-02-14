using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.Common;
using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_910_BDC_CallTran
		{
			#region "Declarations"

				private	readonly	UT_Destination		co_UTDest	;
				private	readonly	UT_TestData				co_UTData	;
				private readonly	SAPFncConstants		co_SapCon ;

			#endregion

			//...................................................
			public UT_910_BDC_CallTran()
				{
					this.co_SapCon	= new SAPFncConstants();
					this.co_UTData	= new UT_TestData();
					this.co_UTDest	= new UT_Destination(	2 , true );
				}

			//...................................................
			[TestMethod]
			public void UT_910_10_CallTranProfile()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var x = new BDCCallTranProfile( this.co_SapCon.BDCCallTran );
					Assert.IsNotNull(	x					,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					x.Ready();
					Assert.IsFalse	(	x.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					this.co_UTDest.DestRfc.RegisterProfile(x);

					if (x.Ready())
						{
							Assert.IsTrue	(	x.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
							bool z = this.co_UTDest.DestRfc.TryGetProfile(x.FunctionName, out BDCCallTranProfile y);
							Assert.IsTrue	(	y.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
						}
					else
						{
							Assert.IsFalse	(	x.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
							bool z = this.co_UTDest.DestRfc.TryGetProfile(x.FunctionName, out BDCCallTranProfile y);
							Assert.IsFalse	(	y.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
						}
				}

			//...................................................
			[TestMethod]
			public void UT_910_20_CallTranSetup()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_PF	= new BDCCallTranProfile( this.co_SapCon.BDCCallTran );
					var lo_HD	= new DTO_RFCHeader	();
					var lo_TR	= new DTO_RFCTran		();

					this.co_UTDest.DestRfc.RegisterProfile(lo_PF);
					var lo_Fnc	= new BDCCallTranProcessor(lo_PF);

					lo_Fnc.Config(lo_HD);
					lo_Fnc.Process(lo_TR);
				}

			//...................................................
			[TestMethod]
			public void UT_910_30_CallTranInvoke()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_PF	= new BDCCallTranProfile( this.co_SapCon.BDCCallTran );
					var lo_FN	= new BDCCallTranProcessor(lo_PF);
					DTO_SessionTran lo_BDCData;

					this.co_UTDest.DestRfc.RegisterProfile(lo_PF);
					if (!lo_PF.Ready())	Assert.Fail( $"SAPNCO:CallTran:910/30 {ln_Cnt}: Not Ready" );
					//...............................................
					DTO_CTUParms	ls_CTU			= this.co_UTData.UpdateCTU('A');
					var						lo_RfcHead	= new DTO_RFCHeader	{	CTUParms = lo_PF.GetCTUStr };
					lo_RfcHead.SAPTCode	= "XD02";
					lo_RfcHead.Skip1st	= " ";
					this.co_UTData.PutCTUOptions(ls_CTU,lo_RfcHead.CTUParms);
					//...............................................
					var lo_RfcData	= new DTO_RFCTran	{		BDCData = lo_PF.GetBDCTbl
																							,	SPAData = lo_PF.GetSPATbl
																							,	MSGData = lo_PF.GetMSGTbl	};
					//...............................................
					lo_RfcData.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "8888" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_RfcData.BDCData );
					//...............................................
					lo_FN.Config(lo_RfcHead);

					lo_FN.Process(lo_RfcData);

					Assert.IsTrue	(	lo_RfcData.ProcessedStatus	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsTrue	(	lo_RfcData.SuccesStatus			,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					//...............................................
					lo_RfcData.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "8881" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_RfcData.BDCData );

					lo_FN.Process(lo_RfcData);

					Assert.IsTrue	(	lo_RfcData.ProcessedStatus	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsTrue	(	lo_RfcData.SuccesStatus			,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					//...............................................
					lo_RfcData.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "8882" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_RfcData.BDCData );

					lo_FN.Process(lo_RfcData);

					Assert.IsTrue	(	lo_RfcData.ProcessedStatus	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsTrue	(	lo_RfcData.SuccesStatus			,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}
		}
}
