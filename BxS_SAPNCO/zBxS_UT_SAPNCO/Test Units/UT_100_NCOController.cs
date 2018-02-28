using System.Collections.Generic;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_100_NCOController
		{
			#region "Declarations"

				private	readonly NCOController		_NCOCntlr ;

			#endregion

			//...................................................
			public UT_100_NCOController()
				{
					this._NCOCntlr	= new NCOController(true,false,true);
				}

			//...................................................
			[TestMethod]
			public void UT_100_10_Base()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					Assert.IsNotNull(	this._NCOCntlr	,	$"SAPNCO:Cntlr:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_100_20_Destination()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IList<BxS_SAPNCO.Destination.IDTO_SAPSystemReference> x = this._NCOCntlr.ConnectionReferenceList();

					Assert.AreNotEqual( 0, x	,	$"SAPNCO:Cntlr:Inst {ln_Cnt}: 1st" );
				}
		}
}
