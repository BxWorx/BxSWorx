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
	public class UT_410_BDCSessionParser
		{
			private readonly	UT_000_NCO						co_NCO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_410_BDCSessionParser()
				{
					this.co_NCO			= new	UT_000_NCO();
					//...............................................
					Assert.IsNotNull	( this.co_NCO			, "A" );
					Assert.IsNotNull	( this.co_SCntlr	, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_410_BCDParser_10_Instantiate()
				{
					
					Assert.IsNotNull( this.co_SCntlr	, "" );
				}

		//.

		}
}
