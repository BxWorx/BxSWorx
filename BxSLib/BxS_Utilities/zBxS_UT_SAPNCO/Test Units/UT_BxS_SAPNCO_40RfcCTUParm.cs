using System.Security;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions;

using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_40RfcCTUParm
		{
			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_CTU_Base()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_CTU	= new BDCCTU_Parameters();

					Assert.AreEqual	( lo_CTU.DisplayMode		,	lo_CTU.DisplayMode_BGrnd,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Disp" );
					Assert.AreEqual	( lo_CTU.UpdateMode			,	lo_CTU.UpdateMode_ASync	,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Updt" );
					Assert.AreEqual	( lo_CTU.CATTMode				, lo_CTU.CATTMode_None		,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: CATT" );
					Assert.AreEqual	( lo_CTU.DefaultSize		, lo_CTU.Setas_Yes				,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Defsze" );
					Assert.AreEqual	( lo_CTU.NoCommit				, lo_CTU.Setas_Yes				,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Commit" );
					Assert.AreEqual	( lo_CTU.NoBatchInpFor	, lo_CTU.Setas_Yes				,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchFor" );
					Assert.AreEqual	( lo_CTU.NoBatchInpAft	, lo_CTU.Setas_Yes				,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchAft" );
				}
		}
}
