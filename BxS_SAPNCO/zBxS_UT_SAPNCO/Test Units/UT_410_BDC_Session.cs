using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.Common;
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_410_BDC_Session
		{
			#region "Declarations"

				private readonly	SAPFncConstants		co_SapCon ;
				private readonly	UT_TestData				co_Data		;
				private readonly	UT_Destination		co_Dest		;
				private readonly	BDC_OpFnc					co_OpFnc;
				private	readonly	UT_Pipeline				co_UTPipe	;

				private readonly	BDCCallTranProfile		co_Prof	;

				private readonly	ConsumerOpEnv<	DTO_SessionTran
																				, DTO_ProgressInfo >	co_OpEnv	;

			#endregion

			//...................................................
			public UT_410_BDC_Session()
				{
					this.co_SapCon	= new SAPFncConstants()								;
					this.co_UTPipe	= new UT_Pipeline()										;
					this.co_Data		= new UT_TestData		()								;
					this.co_Dest		= new UT_Destination( 2, true)				;
					this.co_OpFnc		= new BDC_OpFnc()											;

					this.co_OpEnv		= this.co_UTPipe.CNOpEnv			;
					this.co_Prof		= this.CreateBDCTranProfile()	;
				}

			//...................................................
			[TestMethod]
			public void UT_410_10_BDCSession_Inst()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCSession				lo_Ses0		= this.CreateBDCSession();

					Assert.IsNotNull(	lo_Ses0	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_410_20_BDCSession_Configure()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCSession	lo_Ses0		= this.CreateBDCSession();

					lo_Ses0.SessionHeader.CTUParms.DisplayMode	= 'N';
					lo_Ses0.SessionOptions.NoOfConsumers				= 2;
				}

			//...................................................
			[TestMethod]
			public void UT_410_30_BDCSession_AddTran()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCSession	lo_Ses0		= this.CreateBDCSession();

					IList<DTO_SessionTran> lt = new List<DTO_SessionTran>
							{	lo_Ses0.CreateTran(),
								lo_Ses0.CreateTran(),
								lo_Ses0.CreateTran()	};

					lo_Ses0.AddTransaction(	lo_Ses0.CreateTran() );
					lo_Ses0.AddTransaction(	lt );

					Assert.AreEqual(4,	lo_Ses0.TransactionCount	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					lo_Ses0.Reset();
					Assert.AreEqual(0,	lo_Ses0.TransactionCount	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public async Task UT_410_40_BDCSession_Process()
				{
					int	ln_Cnt		= 0;
					int	ln_Tally	= 0;
					const int			lz_NoCon	= 3;
					const int     lz_MaxTr	= 100;
					const string	lz_Tel		= "5555";
					//...............................................
					ln_Cnt	++;

					IBDCSession	lo_Ses0		= this.CreateBDCSession('N');
					lo_Ses0.SessionOptions.NoOfConsumers	= lz_NoCon;
					//...............................................
					IList<string> lt = this.co_Data.LoadList(true);

					foreach (string lc_Cust in lt)
						{
							lo_Ses0.AddTransaction( this.co_Data.SetupTestBDCData( lc_Cust, lz_Tel ) );
							ln_Tally ++;
							if (ln_Tally.Equals(lz_MaxTr))	break;
						}

					Assert.AreEqual(ln_Tally,	lo_Ses0.TransactionCount	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					//...............................................
					int ln_ConCnt = await lo_Ses0.ProcessAsync().ConfigureAwait(false);

					while ( !ln_ConCnt.Equals(lo_Ses0.SessionOptions.NoOfConsumers) )
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual(	ln_ConCnt	,	lo_Ses0.SessionOptions.NoOfConsumers	,	$"SAPNCO:Session:Process	{ln_Cnt}: 1st" );
					Assert.AreEqual(	lt.Count	, lo_Ses0.CompletedSuccessfulCount			,	$"SAPNCO:Session:Process	{ln_Cnt}: 2nd" );
				}

			//...................................................
			private BDCSession CreateBDCSession(char DispMode = 'A')
				{
					DTO_SessionHeader lo_HD			= this.co_Data.CreateSessionHead(DispMode);
					var								lo_SessOp	= new DTO_SessionOptions();

					return	new BDCSession( this.co_OpFnc, lo_SessOp, lo_HD, this.co_Prof, this.co_OpEnv );
				}

			//...................................................
			private BDCCallTranProfile CreateBDCTranProfile()
				{
					var lo_Indexer	= new BDCCallTranIndex();
					var lo_Parser		= new BDCCallTranParser( lo_Indexer );

					return	new BDCCallTranProfile(		this.co_Dest.DestRfc
																					, this.co_SapCon.BDCCallTran
																					, lo_Indexer
																					, lo_Parser
																					, this.co_OpFnc								);
				}
		}
}
