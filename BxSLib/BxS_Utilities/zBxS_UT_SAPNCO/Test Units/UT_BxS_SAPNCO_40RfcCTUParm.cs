using System.Security;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC

using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_BxS_SAPNCO_40RfcCTUParm
		{
			private BDCCTU_Parameters	co_CTUParms	= new	BDCCTU_Parameters();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SAPNCO_CTU_DTO()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var lo_CTU0	= new DTO_CTUParams();

					Assert.AreEqual	( lo_CTU0.DisplayMode		, this.co_CTUParms.DisplayMode_BGrnd	,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Disp"			);
					Assert.AreEqual	( lo_CTU0.UpdateMode		, this.co_CTUParms.UpdateMode_ASync		,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Updt"			);
					Assert.AreEqual	( lo_CTU0.CATTMode			, this.co_CTUParms.CATTMode_None			,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: CATT"			);
					Assert.AreEqual	( lo_CTU0.DefaultSize		, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Defsze"		);
					Assert.AreEqual	( lo_CTU0.NoCommit			, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Commit"		);
					Assert.AreEqual	( lo_CTU0.NoBatchInpFor	, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchFor"	);
					Assert.AreEqual	( lo_CTU0.NoBatchInpAft	, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchAft"	);
					//...............................................
					ln_Cnt	++;

					var lo_CTU1	= new DTO_CTUParams(	this.co_CTUParms.DisplayMode_All	,
																						this.co_CTUParms.UpdateMode_Local	,
																						this.co_CTUParms.CATTMode_Cntrl		,
																						this.co_CTUParms.Setas_No					,
																						this.co_CTUParms.Setas_No					,
																						this.co_CTUParms.Setas_No					,
																						this.co_CTUParms.Setas_No						);

					Assert.AreEqual	( lo_CTU1.DisplayMode		,	this.co_CTUParms.DisplayMode_BGrnd	,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Disp"			);
					Assert.AreEqual	( lo_CTU1.UpdateMode		,	this.co_CTUParms.UpdateMode_ASync		,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Updt"			);
					Assert.AreEqual	( lo_CTU1.CATTMode			, this.co_CTUParms.CATTMode_None			,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: CATT"			);
					Assert.AreEqual	( lo_CTU1.DefaultSize		, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Defsze"		);
					Assert.AreEqual	( lo_CTU1.NoCommit			, this.co_CTUParms.Setas_No						,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Commit"		);
					Assert.AreEqual	( lo_CTU1.NoBatchInpFor	, this.co_CTUParms.Setas_No						,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchFor"	);
					Assert.AreEqual	( lo_CTU1.NoBatchInpAft	, this.co_CTUParms.Setas_No						,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchAft"	);
				}
		}
}
