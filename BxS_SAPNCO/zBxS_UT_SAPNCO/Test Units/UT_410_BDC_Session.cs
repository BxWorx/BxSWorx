using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_410_BDC_Session
		{
			#region "Declarations"

				private readonly	UT_TestData			co_Data		;
				private readonly	UT_Destination	co_Dest		;
				private readonly	NCOController		co_Cntlr	;

				private readonly	CTUParametersHandler	co_CTUHndlr;

				private readonly	string	cc_ID			;
				private	readonly	Guid		cg_GuidID	;

			#endregion

			//...................................................
			public UT_410_BDC_Session()
				{
					this.co_CTUHndlr	= new CTUParametersHandler();

					this.co_Data		= new UT_TestData		()								;
					this.co_Dest		= new UT_Destination(2, false)				;
					this.co_Cntlr		= new NCOController	( autoLoad: true );

					IList<string> lt	=	this.co_Cntlr.GetSAPGUIConfigEntries();
					this.cc_ID				= lt.FirstOrDefault(s => s.Contains("PWD"));
					this.cg_GuidID		= this.co_Cntlr.Repository.GetAddIDFor	(	this.cc_ID	);
				}

			//...................................................
			[TestMethod]
			public void UT_410_10_BDCSession_Inst()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCSession lo_Ses0	= this.co_Cntlr.CreateBDCSession( this.cg_GuidID );
					IBDCSession lo_Ses1	= this.co_Cntlr.CreateBDCSession( this.cc_ID		 );

					Assert.IsNotNull(	lo_Ses0	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.IsNotNull(	lo_Ses1	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_410_11_BDCSession_AddTran()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCSession lo_Ses0	= this.co_Cntlr.CreateBDCSession( this.cg_GuidID	);

					lo_Ses0.AddTransaction(	lo_Ses0.CreateTran() );
					lo_Ses0.AddTransaction(	lo_Ses0.CreateTran() );
					lo_Ses0.AddTransaction(	lo_Ses0.CreateTran() );
					lo_Ses0.AddTransaction(	lo_Ses0.CreateTran() );

					Assert.AreEqual(4,	lo_Ses0.TransactionCount	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_410_20_BDCSession_Startup()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCSession lo_Ses0	= this.co_Cntlr.CreateBDCSession(this.cg_GuidID	);

					var x = this.co_Dest.UT_Destination_User(2);
					lo_Ses0.ConfigureUser(x);

					lo_Ses0.SessionOptions.Sequential	= true;
					lo_Ses0.SessionHeader.SAPTCode		= "XD02";
					lo_Ses0.SessionHeader.Skip1st			= " ";
					lo_Ses0.SessionHeader.CTUParms.DisplayMode	= this.co_CTUHndlr.DisplayMode_All;

					DTO_SessionTran lo_BdcTran1	= lo_Ses0.CreateTran();
					DTO_SessionTran lo_BdcTran2	= lo_Ses0.CreateTran();

					this.co_Data.SetupTestBDCData( lo_BdcTran1, "1007084", "222" );
					this.co_Data.SetupTestBDCData( lo_BdcTran2, "1800476", "222" );

					lo_Ses0.AddTransaction(	lo_BdcTran1 );
					lo_Ses0.AddTransaction(	lo_BdcTran2 );

					lo_Ses0.ProcessAsync();

					Assert.IsTrue		(			lo_Ses0.IsStarted						,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.AreEqual	( 2,	lo_Ses0.RFCTransactionCount	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public async Task UT_300_PipelineBDC_020_Start()
			//	{
			//					int	ln_Cnt	= 00;
			//					int	ln_Tot	= 00;
			//					int ln_Max	= 00;
			//		const int ln_Con	= 1;
			//					string	lc_Tel	= "000";
			//					var			lo_Rnd	= new Random();
			//		//...............................................
			//		ln_Cnt	++;

			//		//Pipeline<IBDCTranData, BDCProgressInfo> lo_Pipe		=	this.co_Cntlr.CreateBDCPipeline( this.co_Dest.GuidID );
			//		IList<string>	lt_No		= this.co_Data.LoadList();
			//									lc_Tel	= lo_Rnd.Next(100,1000).ToString();
			//									ln_Max	= lt_No.Count;

			//		for (int i = 0; i < lt_No.Count; i++)
			//			{
			//				IBDCTranData lo_BDCData	= this.co_Cntlr.CreateBDCTranData(Guid.NewGuid());
			//				this.co_Data.SetupTestBDCData	(lo_BDCData	, lt_No[i]	, lc_Tel ) ;
			//				//lo_Pipe.Post(lo_BDCData);
			//			}
			//		//lo_Pipe.AddingCompleted();

			//		//int ln_ConCnt = await lo_Pipe.StartAsync(ln_Con).ConfigureAwait(false);

			//		//while (!ln_ConCnt.Equals(ln_Con))
			//		//	{
			//		//		Thread.Sleep(10);
			//		//	}

			//		//foreach (Task<IConsumer<IUT_TranData>> lo_Task in lo_Pipe.TasksCompleted)
			//		//	{
			//		//		ln_Tot	+= lo_Task.Result.Successful.Count ;
			//		//	}

			//		//Assert.AreEqual( ln_Con	, ln_ConCnt								,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
			//		//Assert.AreEqual( ln_Max	, ln_Tot									,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 2nd" );
			//		//Assert.AreEqual( ln_Con	, lo_Pipe.CompletedCount	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 3rd" );
			//	}
		}
}
