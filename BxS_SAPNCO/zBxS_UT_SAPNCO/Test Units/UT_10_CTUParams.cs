using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_10_CTUParams
		{
			private readonly BDCCTU_Parameters	co_CTUParms	= new	BDCCTU_Parameters();

			private	int	ln_Cnt	= 0;

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_CTU_DTO()
				{
					var lo_CTU0	= new DTO_CTUParams();
					//...............................................
					this.ln_Cnt	= 1;

					Assert.AreEqual	( lo_CTU0.DisplayMode		, this.co_CTUParms.DisplayMode_BGrnd	,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Disp"			);
					Assert.AreEqual	( lo_CTU0.UpdateMode		, this.co_CTUParms.UpdateMode_ASync		,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Updt"			);
					Assert.AreEqual	( lo_CTU0.CATTMode			, this.co_CTUParms.CATTMode_None			,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: CATT"			);
					Assert.AreEqual	( lo_CTU0.DefaultSize		, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Defsze"		);
					Assert.AreEqual	( lo_CTU0.NoCommit			, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: Commit"		);
					Assert.AreEqual	( lo_CTU0.NoBatchInpFor	, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchFor"	);
					Assert.AreEqual	( lo_CTU0.NoBatchInpAft	, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {ln_Cnt}: BtchAft"	);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_CTU_Image()
				{
					var	lo_CTU1	= new DTO_CTUParams();
					//...............................................
					this.ln_Cnt	= 2;

					this.co_CTUParms.DisplayMode		= this.co_CTUParms.DisplayMode_All	;
					this.co_CTUParms.UpdateMode			= this.co_CTUParms.UpdateMode_Local	;
					this.co_CTUParms.CATTMode				= this.co_CTUParms.CATTMode_Cntrl		;
					this.co_CTUParms.DefaultSize		= this.co_CTUParms.Setas_No					;
					this.co_CTUParms.NoCommit				= this.co_CTUParms.Setas_No					;
					this.co_CTUParms.NoBatchInpFor	= this.co_CTUParms.Setas_No					;
					this.co_CTUParms.NoBatchInpAft	= this.co_CTUParms.Setas_No					;

					this.co_CTUParms.TransferImage(lo_CTU1);

					Assert.AreEqual	( lo_CTU1.DisplayMode		, this.co_CTUParms.DisplayMode_All	,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: Disp"		);
					Assert.AreEqual	( lo_CTU1.UpdateMode		, this.co_CTUParms.UpdateMode_Local	,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: Updt"		);
					Assert.AreEqual	( lo_CTU1.CATTMode			, this.co_CTUParms.CATTMode_Cntrl		,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: CATT"		);
					Assert.AreEqual	( lo_CTU1.DefaultSize		, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: Defsze"	);
					Assert.AreEqual	( lo_CTU1.NoCommit			, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: Commit"	);
					Assert.AreEqual	( lo_CTU1.NoBatchInpFor	, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: BtchFor"	);
					Assert.AreEqual	( lo_CTU1.NoBatchInpAft	, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {ln_Cnt}: BtchAft"	);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_CTU_IsValid()
				{
					var	lo_CTU1	= new DTO_CTUParams()	;

					BDCCTU_Parameters.ce_Validate le_Err	;
					BDCCTU_Parameters.ce_Validate le_OK		;
					BDCCTU_Parameters.ce_Validate le_All	;
					//...............................................
					this.ln_Cnt	= 3;

					lo_CTU1.DisplayMode		= '1'	;
					lo_CTU1.UpdateMode		= '1'	;
					lo_CTU1.CATTMode			= '1'	;
					lo_CTU1.DefaultSize		= '1'	;
					lo_CTU1.NoCommit			= '1'	;
					lo_CTU1.NoBatchInpFor	= '1'	;
					lo_CTU1.NoBatchInpAft	= '1'	;

					le_All		= BDCCTU_Parameters.ce_Validate.BIA | BDCCTU_Parameters.ce_Validate.BIF | BDCCTU_Parameters.ce_Validate.Cat |
											BDCCTU_Parameters.ce_Validate.Com	| BDCCTU_Parameters.ce_Validate.Dsp | BDCCTU_Parameters.ce_Validate.Sze |
											BDCCTU_Parameters.ce_Validate.Upd;

					le_Err	=	this.co_CTUParms.IsValid(lo_CTU1, false);
					le_OK		=	this.co_CTUParms.IsValid(lo_CTU1);

					Assert.AreEqual( le_All															, le_Err	,	$"SAPNCO: CTUParams: Val {ln_Cnt}: Error"		);
					Assert.AreEqual( BDCCTU_Parameters.ce_Validate.Non	, le_OK		,	$"SAPNCO: CTUParams: Val {ln_Cnt}: OK"		);
				}
		}
}
