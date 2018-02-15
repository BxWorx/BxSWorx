using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.Common;
using BxS_SAPNCO.CTU;
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_910_BDC_CallTran
		{
			#region "Declarations"

				private	readonly	UT_Destination		co_UTDest	;
				private	readonly	UT_TestData				co_UTData	;
				private	readonly	UT_Pipeline				co_UTPipe	;
				private readonly	SAPFncConstants		co_SapCon ;

				private readonly BDCCallTranProfile			co_Prof	;
				private readonly BDCCallTranProcessor		co_Tran	;
				private readonly DTO_RFCHeader					co_Head	;

				private readonly ConsumerOpEnv<DTO_RFCTran, DTO_ProgressInfo> co_OpEnv	;

			#endregion

			//...................................................
			public UT_910_BDC_CallTran()
				{
					this.co_SapCon	= new SAPFncConstants();
					this.co_UTData	= new UT_TestData();
					this.co_UTPipe	= new UT_Pipeline();
					this.co_UTDest	= new UT_Destination(	2 , true );

					this.co_Prof		= this.CreateBDCTranProfile();
					this.co_Tran		= new BDCCallTranProcessor(this.co_Prof);
					this.co_OpEnv		= this.co_UTPipe.CNOpEnv;
					this.co_Head		= this.CreateRFCHead(this.co_Prof);
				}

			//...................................................
			[TestMethod]
			public void UT_910_10_CallTranProfile()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					BDCCallTranProfile	x	= this.CreateBDCTranProfile();
					Assert.IsNotNull(	x					,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

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

					BDCCallTranProfile	lo_PF	= this.CreateBDCTranProfile();
					var lo_HD		= new DTO_RFCHeader	();
					var lo_TR		= new DTO_RFCTran		();
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

					BDCCallTranProfile	lo_PF	= this.CreateBDCTranProfile();
					var lo_FN	= new BDCCallTranProcessor(lo_PF);
					DTO_SessionTran lo_BDCData;

					if (!lo_PF.Ready())	Assert.Fail( $"SAPNCO:CallTran:910/30 {ln_Cnt}: Not Ready" );
					//...............................................
					DTO_RFCHeader lo_RfcHead	= this.CreateRFCHead(lo_PF);
					DTO_RFCTran		lo_RfcData	= this.CreateRFCData(lo_PF);

					lo_FN.Config(lo_RfcHead);
					//...............................................
					lo_RfcData.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "8888" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_RfcData.BDCData );
					//...............................................

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

			//...................................................
			[TestMethod]
			public void UT_910_40_CallTranConsumer()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					DTO_SessionTran lo_BDCData	;

					var	lo_Con		= new BDCConsumer<DTO_RFCTran, DTO_ProgressInfo>(this.co_OpEnv, this.co_Tran);

					Assert.IsNotNull(	lo_Con	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					DTO_RFCHeader lo_HD = this.CreateRFCHead(this.co_Prof);

					lo_BDCData					= this.co_UTData.SetupTestBDCData( "1007084", "8888" );
					DTO_RFCTran	lo_DT1	= this.CreateRFCData(this.co_Prof);
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_DT1.BDCData );
					this.co_OpEnv.Queue.Add(lo_DT1);

					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "8881" );
					DTO_RFCTran	lo_DT2	= this.CreateRFCData(this.co_Prof);
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_DT2.BDCData );
					this.co_OpEnv.Queue.Add(lo_DT2);

					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "8882" );
					DTO_RFCTran	lo_DT3	= this.CreateRFCData(this.co_Prof);
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_DT3.BDCData );
					this.co_OpEnv.Queue.Add(lo_DT3);

					this.co_OpEnv.Queue.CompleteAdding();




			}

			//...................................................
			//...................................................
			//...................................................
			//...................................................

			//...................................................
			private	DTO_RFCTran CreateRFCData(BDCCallTranProfile	lo_PF)
				{
					return	 new DTO_RFCTran	{		BDCData = lo_PF.GetBDCTbl
																			,	SPAData = lo_PF.GetSPATbl
																			,	MSGData = lo_PF.GetMSGTbl	};
				}

			//...................................................
			private	DTO_RFCHeader CreateRFCHead(BDCCallTranProfile	lo_PF)
				{
					DTO_CTUParms	ls_CTU			= this.co_UTData.UpdateCTU('N');
					var						lo_RfcHead	= new DTO_RFCHeader	{	CTUParms = lo_PF.GetCTUStr };

					lo_RfcHead.SAPTCode	= "XD02";
					lo_RfcHead.Skip1st	= " ";
					this.co_UTData.PutCTUOptions(ls_CTU,lo_RfcHead.CTUParms);

					return	lo_RfcHead;
				}

			//...................................................
			private BDCCallTranProfile CreateBDCTranProfile()
				{
					return	new BDCCallTranProfile( this.co_UTDest.DestRfc , this.co_SapCon.BDCCallTran );
				}
		}
}
