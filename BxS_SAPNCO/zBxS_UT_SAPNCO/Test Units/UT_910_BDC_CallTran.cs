using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
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

				private readonly	BDCCallTranProfile			co_Prof	;
				private readonly	BDCCallTranProcessor		co_Tran	;

				private readonly	ConsumerOpEnv<	DTO_SessionTran
																				, DTO_ProgressInfo >	co_OpEnv	;

			#endregion

			//...................................................
			public UT_910_BDC_CallTran()
				{
					this.co_SapCon	= new SAPFncConstants();
					this.co_UTData	= new UT_TestData();
					this.co_UTPipe	= new UT_Pipeline();
					this.co_UTDest	= new UT_Destination(	1 , true );

					this.co_Prof		= this.CreateBDCTranProfile();
					this.co_Tran		= new BDCCallTranProcessor(this.co_Prof);
					this.co_OpEnv		= this.co_UTPipe.CNOpEnv;
				}

			//...................................................
			[TestMethod]
			public void UT_910_10_CallTranProfile()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					Assert.IsNotNull(	this.co_Prof ,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					if (this.co_Prof.Ready())
						{
							Assert.IsTrue	(	this.co_Prof.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
							bool z = this.co_UTDest.DestRfc.TryGetProfile(this.co_Prof.FunctionName, out BDCCallTranProfile y);
							Assert.IsTrue	(	y.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
						}
					else
						{
							Assert.IsFalse	(	this.co_Prof.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
							bool z = this.co_UTDest.DestRfc.TryGetProfile(this.co_Prof.FunctionName, out BDCCallTranProfile y);
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

					if (!this.co_Prof.Ready())	Assert.Fail( $"SAPNCO:CallTran:910/30 {ln_Cnt}: Not Ready" );

					DTO_RFCHeader lo_HD		= this.CreateRFCHead(this.co_Prof);
					DTO_RFCTran		lo_TR		= this.CreateRFCData(this.co_Prof);
					var lo_Fnc	= new BDCCallTranProcessor(this.co_Prof);

					lo_HD.SAPTCode	=	"X";
					lo_HD.Skip1st		= "X";
					lo_HD.CTUParms.SetValue(0,"X");

					lo_Fnc.Config(lo_HD);

					Assert.AreEqual( "X", lo_Fnc.Header.SAPTCode							,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.AreEqual( "X", lo_Fnc.Header.Skip1st								,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.AreEqual( "X", lo_Fnc.Header.CTUParms.GetString(0)	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_910_30_CallTranInvoke()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					if (!this.co_Prof.Ready())	Assert.Fail( $"SAPNCO:CallTran:910/30 {ln_Cnt}: Not Ready" );

					DTO_RFCHeader		lo_RfcHead		= this.CreateRFCHead(this.co_Prof);
					DTO_SessionTran lo_BDCData;

					var lo_FN	= new BDCCallTranProcessor(this.co_Prof);
					//...............................................
					lo_FN.Config(lo_RfcHead);
					//...............................................
					lo_FN.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1007084", "7777" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_FN.Transaction.BDCData );

					lo_FN.Process();

					Assert.IsTrue	(	lo_FN.Transaction.ProcessedStatus	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsTrue	(	lo_FN.Transaction.SuccesStatus		,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					//...............................................
					lo_FN.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1800476", "7771" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_FN.Transaction.BDCData );

					lo_FN.Process();

					Assert.IsTrue	(	lo_FN.Transaction.ProcessedStatus	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsTrue	(	lo_FN.Transaction.SuccesStatus		,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					//...............................................
					lo_FN.Reset();
					lo_BDCData	= this.co_UTData.SetupTestBDCData( "1802054", "7772" );
					this.co_UTData.PutBDCData( lo_BDCData.BDCData	,	lo_FN.Transaction.BDCData );

					lo_FN.Process();

					Assert.IsTrue	(	lo_FN.Transaction.ProcessedStatus	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsTrue	(	lo_FN.Transaction.SuccesStatus		,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_910_40_CallTranConsumer()
				{
					int	ln_Cnt	= 0;
					const string lz_Tel	= "6666";
					//...............................................
					ln_Cnt	++;

					DTO_SessionHeader lo_HD	= this.co_UTData.CreateSessionHead('N');

					var lo_Psr		= new BDCCallTranParser( this.co_Tran.Indexer );
					var	lo_Con		= new BDCCallTranConsumer< DTO_SessionTran, DTO_ProgressInfo >(	this.co_OpEnv
																																											, lo_HD
																																											, this.co_Tran
																																											, lo_Psr				);

					Assert.IsNotNull(	lo_Con	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					DTO_SessionTran	lo_DT1	= this.co_UTData.SetupTestBDCData( "1007084", lz_Tel );
					this.co_OpEnv.Queue.Add(lo_DT1);

					DTO_SessionTran	lo_DT2	= this.co_UTData.SetupTestBDCData( "1800476", lz_Tel );
					this.co_OpEnv.Queue.Add(lo_DT2);

					DTO_SessionTran	lo_DT3	= this.co_UTData.SetupTestBDCData( "1802054", lz_Tel );
					this.co_OpEnv.Queue.Add(lo_DT3);

					this.co_OpEnv.Queue.CompleteAdding();

					lo_Con.Start();

					Assert.AreEqual( 3 , lo_Con.TotalProcessed	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
			}

			//...................................................
			[TestMethod]
			public async Task UT_910_50_CallTranPipeline()
				{
					int	ln_Cnt	= 0;
					int	ln_Tot	= 0;
					const int			lz_NoCon	= 3;
					const string	lz_Tel		= "5555";
					//...............................................
					ln_Cnt	++;

					var lo_PL = new Pipeline<DTO_SessionTran, DTO_ProgressInfo>(this.co_OpEnv.CT);
					var lo_Psr	= new BDCCallTranParser( this.co_Tran.Indexer );
					DTO_SessionHeader lo_HD	= this.co_UTData.CreateSessionHead('N');
					//...............................................
					IList<string> lt = this.co_UTData.LoadList(true);

					foreach (string lc_Cust in lt)
						{
							this.co_OpEnv.Queue.Add( this.co_UTData.SetupTestBDCData( lc_Cust, lz_Tel ) );
						}
					this.co_OpEnv.Queue.CompleteAdding();
					//...............................................
					if (!this.co_Prof.Ready())	Assert.Fail("Not readied");

					for (int i = 0; i < lz_NoCon; i++)
						{
							var	lo_Tran	= new BDCCallTranProcessor(this.co_Prof);
							if (lo_Tran.Configure())
								{
									var	lo_Con	= new BDCCallTranConsumer<	DTO_SessionTran
																												, DTO_ProgressInfo >(		this.co_OpEnv
																																							, lo_HD
																																							, lo_Tran
																																							, lo_Psr				);
									lo_PL.AddConsumer( lo_Con );
								}
						}

					int ln_ConCnt = await lo_PL.StartAsync().ConfigureAwait(false);

					while (!ln_ConCnt.Equals(lo_PL.ConsumerCount))
						{
							Thread.Sleep(10);
						}

					foreach ( Task<IConsumer<DTO_SessionTran>> lo_Task in lo_PL.TasksCompleted)
						{
							ln_Tot	+= lo_Task.Result.Successful.Count ;
						}

					Assert.AreEqual( lt.Count	, ln_Tot	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 2nd" );
				}

					//Assert.AreEqual( ln_Con	, ln_ConCnt								,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
					//Assert.AreEqual( ln_Max	, ln_Tot									,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 2nd" );
					//Assert.AreEqual( ln_Con	, lo_Pipe.CompletedCount	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 3rd" );

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
					DTO_CTUParms	ls_CTU			= this.co_UTData.UpdateCTU('A');
					var						lo_RfcHead	= new DTO_RFCHeader	{	CTUParms = lo_PF.GetCTUStr };

					lo_RfcHead.SAPTCode	= "XD02";
					lo_RfcHead.Skip1st	= " ";
					this.co_UTData.PutCTUOptions(ls_CTU,lo_RfcHead.CTUParms);

					return	lo_RfcHead;
				}

			//...................................................
			private BDCCallTranProfile CreateBDCTranProfile()
				{
					BDC_OpFnc	lo_OpFnc = new BDC_OpFnc();
					var lo_Indexer	= new BDCCallTranIndex();

					return	new BDCCallTranProfile(		this.co_UTDest.DestRfc
																					, this.co_SapCon.BDCCallTran
																					, lo_Indexer
																					, lo_OpFnc	);

					//return	new BDCCallTranProfile(		this.co_UTDest.DestRfc
					//																, this.co_SapCon.BDCCallTran
					//																, lo_Indexer
					//																, () => new DTO_RFCHeader()
					//																, () => new DTO_RFCTran()
					//																, ( SMC.RfcFunctionMetadata FncMetadata ) => new BDCCallTranIndexSetup(FncMetadata) );
				}
		}
}
