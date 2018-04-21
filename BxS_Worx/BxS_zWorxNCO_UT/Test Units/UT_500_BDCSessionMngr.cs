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
			private readonly	UT_000_NCO						co_NCO;
			private	readonly	IBDC_Session_Manager	co_SCntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_500_BDCSessionMngr()
				{
					this.co_NCO			= new	UT_000_NCO();
					this.co_SCntlr	= new	BDC_Session_Manager( this.co_NCO.GetSAPDestConfigured() );
					//...............................................
					Assert.IsNotNull	( this.co_NCO			, "A" );
					Assert.IsNotNull	( this.co_SCntlr	, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_BCDSMngr_10_Instantiate()
				{
					Assert.IsNotNull( this.co_SCntlr	, "" );
				}

		//.

		}
}
