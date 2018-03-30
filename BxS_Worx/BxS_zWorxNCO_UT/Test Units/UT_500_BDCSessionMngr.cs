using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxIPX.Main;
using BxS_WorxNCO.BDCSession.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_500_BDCSessionMngr
		{
			private readonly	UT_000_NCO	co_NCO;
			private	readonly	IBDCSessionController	co_SCntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_500_BDCSessionMngr()
				{
					this.co_NCO			= new	UT_000_NCO();
					this.co_SCntlr	= new	BDCSessionController();
					//...............................................
					Assert.IsNotNull	( this.co_NCO									, "A" );
					Assert.IsNotNull	( this.co_NCO.DestController	, "B" );
					Assert.IsNotNull	( this.co_SCntlr							, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_BCDSMngr_10_Instantiate()
				{
					BDC_SessionManager	lo_SM		= this.GetBDCSMngr();
					//...............................................
					Assert.IsNotNull	( lo_SM	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_500_BCDSMngr_20_Instantiate()
				{
					BDC_SessionManager	lo_SM		= this.GetBDCSMngr();
					//...............................................







					//BxS_WorxIPX.BDC.IExcelBDCSessionRequest x = IPXController.Instance.CreateBDCSessionRequest();

					//await	lo_SM.Process(x);

					//Assert.IsNotNull	( lo_SM	, "" );
				}

		//.

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private BDC_SessionManager GetBDCSMngr()
				{
					IList< string >		lt_Ini	=	this.co_SCntlr.GetSAPINIList();
					string						lc_ID		= lt_Ini.FirstOrDefault( s => s.Contains("05.01") );
					BDC_SessionManager	lo_SM		= this.co_SCntlr.CreateBDCSessionManager( lc_ID );
					//...............................................
					return	lo_SM;
				}

		}
}
