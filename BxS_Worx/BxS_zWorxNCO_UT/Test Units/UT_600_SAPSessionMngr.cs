using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.API;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.SAPSession.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_600_SAPSessionMngr
		{
			private readonly	UT_000_NCO						co_NCO000		;
			private readonly	INCO_Controller				co_NCOCntlr	;
			private readonly	IRfcDestination				co_RfcDest	;
			private	readonly	ISAP_Session_Manager	co_SAPMngr	;
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_600_SAPSessionMngr()
				{
					this.co_NCO000		= new	UT_000_NCO()						;
					this.co_NCOCntlr	= NCO_Controller.Instance			;
					this.co_RfcDest		= this.co_NCO000.GetSAPDestConfigured()	;

					this.co_SAPMngr		= this.co_NCOCntlr.CreateSAPSessionManager( this.co_RfcDest )	;
					Task.Run( ()=> this.co_SAPMngr.ReadySessionAsync() ).Wait();
					//...............................................
					Assert.IsNotNull	( this.co_NCO000		, "A" );
					Assert.IsNotNull	( this.co_NCOCntlr	, "B" );
					Assert.IsNotNull	( this.co_RfcDest		, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_SAPSsnMngr_10_Instantiate()
				{
					Assert.IsNotNull(	this.co_SAPMngr	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_SAPSsnMngr_20_SAPSsnList()
				{
					IList<ISAP_Session_Header> lt_L0 = this.co_SAPMngr.SAPSessionList();
					Assert.AreNotEqual(	0 , lt_L0.Count	, "" );
					//...............................................
					IList<ISAP_Session_Header> lt_L1 = this.co_SAPMngr.SAPSessionList( userId: "DER*" );
					Assert.AreNotEqual(	0 , lt_L1.Count	, "" );
					Assert.AreNotEqual(	lt_L0.Count , lt_L1.Count	, "" );
					//...............................................
					IList<ISAP_Session_Header> lt_L2 = this.co_SAPMngr.SAPSessionList( sessionName: "DPB*" );
					Assert.AreNotEqual(	0 , lt_L2.Count	, "" );
					Assert.AreNotEqual(	lt_L0.Count , lt_L2.Count	, "" );
					//...............................................
					var ld = new DateTime(2018, 4, 1);
					IList<ISAP_Session_Header> lt_L3 = this.co_SAPMngr.SAPSessionList( dateFrom: ld );
					Assert.AreNotEqual(	0 , lt_L3.Count	, "" );
					Assert.AreNotEqual(	lt_L0.Count , lt_L3.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_SAPSsnMngr_30_SAPData()
				{
					IList<ISAP_Session_Header> lt_L1 = this.co_SAPMngr.SAPSessionList( userId: "DER*" );
					Assert.AreNotEqual(	0 , lt_L1.Count	, "" );
					//...............................................
					int i = lt_L1.Count - 1;
					ISAP_Session_Profile lo_P1	= this.co_SAPMngr.GetSAPSessionData( lt_L1[i].SessionName , lt_L1[i].QID , true );
					ISAP_Session_Profile lo_P2	= this.co_SAPMngr.GetSAPSessionData( lt_L1[i].SessionName , lt_L1[i].QID , false );

					Assert.AreEqual		(	0 , lo_P1.Count , "" );
					Assert.AreNotEqual(	0 , lo_P2.Count , "" );
				}

		}
}
