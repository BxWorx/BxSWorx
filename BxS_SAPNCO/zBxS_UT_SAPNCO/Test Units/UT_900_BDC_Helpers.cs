using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.BDCProcess;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_900_BDC_Helpers
		{
			#region "Declarations"

				private readonly	NCOController_BDC	lo_Cntlr	;
				private	readonly	UT_Destination		lo_UTDest	;
				private readonly	BDC_OpFnc					co_OpFnc;

			#endregion

			//...................................................
			public UT_900_BDC_Helpers()
				{
					this.lo_Cntlr		= new NCOController_BDC();
					this.lo_UTDest	= new UT_Destination(	2 , true );

					this.co_OpFnc		= new BDC_OpFnc()											;
				}

			//...................................................
			[TestMethod]
			public void UT_900_10_BDCOpFnc()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					DTO_RFCHeader DTO = this.co_OpFnc.CreateRfcHead();
					Assert.IsNotNull(	DTO	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			////...................................................
			//[TestMethod]
			//public void UT_900_20_BDCOpEnv()
			//	{
			//		int	ln_Cnt	= 0;
			//		//...............................................
			//		ln_Cnt	++;
			//	}
		}
}
