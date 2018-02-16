using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.CTU;
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
				private readonly	CTUParametersHandler	co_CTUHndlr;

				private readonly	ConsumerOpEnv<	DTO_SessionTran
																				, DTO_ProgressInfo >	co_OpEnv	;

				private readonly	string	cc_ID			;
				private	readonly	Guid		cg_GuidID	;

			#endregion


			//...................................................
			public UT_410_BDC_Session()
				{
					this.co_CTUHndlr	= new CTUParametersHandler();

					this.co_SapCon	= new SAPFncConstants()								;
					this.co_UTPipe	= new UT_Pipeline()										;
					this.co_Data		= new UT_TestData		()								;
					this.co_Dest		= new UT_Destination( 1, true)				;
					this.co_OpFnc		= new BDC_OpFnc()											;

					this.co_OpEnv		= this.co_UTPipe.CNOpEnv			;
					this.co_Prof		= this.CreateBDCTranProfile()	;
				}

			//...................................................
			[TestMethod]
			public void UT_410_00_Session_Base()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					DTO_RFCHeader DTO = this.co_OpFnc.CreateRfcHead();
					Assert.IsNotNull(	DTO	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_410_10_BDCSession_Inst()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					DTO_SessionHeader lo_HD	= this.co_Data.CreateSessionHead('N');

					var lo_SessOp	= new DTO_SessionOptions();
					var lo_Parser	= new BDCCallTranParser();

					IBDCSession lo_Ses0	= new BDCSession( this.co_OpFnc, lo_SessOp, lo_HD, lo_Parser, this.co_Prof, this.co_OpEnv );

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
					//lo_Ses0.ConfigureUser(x);

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

					//Assert.IsTrue		(			lo_Ses0.IsStarted						,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					//Assert.AreEqual	( 2,	lo_Ses0.RFCTransactionCount	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			private BDCCallTranProfile CreateBDCTranProfile()
				{
					var lo_Indexer	= new BDCCallTranIndex();

					return	new BDCCallTranProfile(		this.co_Dest.DestRfc
																					, this.co_SapCon.BDCCallTran
																					, lo_Indexer
																					, this.co_OpFnc	);
				}
		}
}
