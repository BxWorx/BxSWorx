using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Helpers;
using BxS_SAPNCO.API;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_300_Pipeline_BDC
		{
			#region "Declarations"

				private readonly	UT_TestData			co_Data;
				private readonly	UT_Destination	co_Dest;
				private readonly	NCOController		co_Cntlr;

			#endregion

			//...................................................
			public UT_300_Pipeline_BDC()
				{
					this.co_Data		= new UT_TestData()			;
					this.co_Dest		= new UT_Destination(2)	;
					this.co_Cntlr		= new NCOController()		;
				}

			//...................................................
			[TestMethod]
			public void UT_300_PipelineBDC_010_Inst()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					Pipeline<IBDCTranData, BDCProgressInfo> lo_Pipe		=	this.co_Cntlr.CreateBDCPipeline( this.co_Dest.GuidID );
					Assert.IsNotNull(	lo_Pipe	,	$"SAPNCO:OpEnv:Inst {ln_Cnt}: 1st" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public async Task UT_300_PipelineBDC_020_Start()
				{
								int	ln_Cnt	= 00;
								int	ln_Tot	= 00;
					const int ln_Max	= 2000;
					const int ln_Con	= 20;
								string	lc_Tel	= "000";
								var			lo_Rnd	= new Random();
					//...............................................
					ln_Cnt	++;

					Pipeline<IBDCTranData, BDCProgressInfo> lo_Pipe		=	this.co_Cntlr.CreateBDCPipeline( this.co_Dest.GuidID );
					IList<string>	lt_No		= this.co_Data.LoadList();

					for (int i = 0; i < lt_No.Count; i++)
						{

							this.co_Data.SetupTestBDCData	(lo_TranData	, lt_No[i]	, lc_Tel )	;
							this.co_Parser.ParseFrom			(lo_TranData	,	lo_RfcData);

							lo_BDCTran0.Process(lo_RfcData);

							this.co_Parser.ParseTo(lo_RfcData,lo_TranData);

							Assert.AreNotEqual( 0	, lo_TranData.MSGCount	, $"SAPNCO:BDCTran:Invoke {ln_Cnt}: Multi {i}" );
						}




					for (int i = 0; i < ln_Max; i++)
						{
							IUT_TranData o = new UT_TranData();
							bool b	=	lo_Pipe.Post(o);
						}
					lo_Pipe.AddingCompleted();

					int y = await lo_Pipe.StartAsync(ln_Con).ConfigureAwait(false);

					while (!y.Equals(ln_Con))
						{
							Thread.Sleep(10);
						}

						foreach (Task<IConsumer<IUT_TranData>> lo_Task in lo_Pipe.TasksCompleted)
							{
								ln_Tot	+= lo_Task.Result.Successful.Count ;
							}

					Assert.AreEqual( ln_Con	, y												,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
					Assert.AreEqual( ln_Max	, ln_Tot									,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 2nd" );
					Assert.AreEqual( ln_Con	, lo_Pipe.CompletedCount	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 3rd" );
				}
		}
}
