using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.BDCSession.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxIPX.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_500_BDCSessionMngr
		{
			private readonly	UT_000_NCO	co_NCO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_500_BDCSessionMngr()
				{
					this.co_NCO	= new	UT_000_NCO();
					//...............................................
					Assert.IsNotNull	( this.co_NCO									, "" );
					Assert.IsNotNull	( this.co_NCO.DestController	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_BCDSMngr_10_Instantiate()
				{
					IRfcDestination lo_RfcDest	= this.co_NCO.GetSAPDest();
					var	lo_SM			= new BDCSessionManager( lo_RfcDest );
					//...............................................
					Assert.IsNotNull	( lo_SM	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_500_BCDSMngr_20_Instantiate()
				{
					IRfcDestination lo_RfcDest	= this.co_NCO.GetSAPDest();
					var	lo_SM			= new BDCSessionManager( lo_RfcDest );
					//...............................................
					BxS_WorxIPX.BDC.IExcelBDCSessionRequest x = IPXController.Instance.CreateBDCSessionRequest();

					await	lo_SM.Process(x);

					Assert.IsNotNull	( lo_SM	, "" );
				}
		}
}
