using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_10_CTUParams
		{
			private readonly CTU_Parameters	co_CTUParms	= new	CTU_Parameters();
			//...................................................
			private	int	cn_Cnt	= 0;

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_CTU_DTO()
				{
					var lo_CTU0	= new DTO_CTUParameters();
					//...............................................
					this.cn_Cnt	= 1;

					Assert.AreEqual	( lo_CTU0.DisplayMode		, this.co_CTUParms.DisplayMode_BGrnd	,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: Disp"		);
					Assert.AreEqual	( lo_CTU0.UpdateMode		, this.co_CTUParms.UpdateMode_ASync		,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: Updt"		);
					Assert.AreEqual	( lo_CTU0.CATTMode			, this.co_CTUParms.CATTMode_None			,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: CATT"		);
					Assert.AreEqual	( lo_CTU0.DefaultSize		, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: Defsze"	);
					Assert.AreEqual	( lo_CTU0.NoCommit			, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: Commit"	);
					Assert.AreEqual	( lo_CTU0.NoBatchInpFor	, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: BtchFor"	);
					Assert.AreEqual	( lo_CTU0.NoBatchInpAft	, this.co_CTUParms.Setas_Yes					,	$"SAPNCO: CTUParams: Inst {this.cn_Cnt}: BtchAft"	);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_CTU_Image()
				{
					var	lo_CTU1	= new DTO_CTUParameters();
					//...............................................
					this.cn_Cnt	= 2;

					this.co_CTUParms.DisplayMode		= this.co_CTUParms.DisplayMode_All	;
					this.co_CTUParms.UpdateMode			= this.co_CTUParms.UpdateMode_Local	;
					this.co_CTUParms.CATTMode				= this.co_CTUParms.CATTMode_Cntrl		;
					this.co_CTUParms.DefaultSize		= this.co_CTUParms.Setas_No					;
					this.co_CTUParms.NoCommit				= this.co_CTUParms.Setas_No					;
					this.co_CTUParms.NoBatchInpFor	= this.co_CTUParms.Setas_No					;
					this.co_CTUParms.NoBatchInpAft	= this.co_CTUParms.Setas_No					;

					this.co_CTUParms.TransferImage(lo_CTU1);

					Assert.AreEqual	( lo_CTU1.DisplayMode		, this.co_CTUParms.DisplayMode_All	,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: Disp"			);
					Assert.AreEqual	( lo_CTU1.UpdateMode		, this.co_CTUParms.UpdateMode_Local	,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: Updt"			);
					Assert.AreEqual	( lo_CTU1.CATTMode			, this.co_CTUParms.CATTMode_Cntrl		,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: CATT"			);
					Assert.AreEqual	( lo_CTU1.DefaultSize		, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: Defsze"		);
					Assert.AreEqual	( lo_CTU1.NoCommit			, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: Commit"		);
					Assert.AreEqual	( lo_CTU1.NoBatchInpFor	, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: BtchFor"	);
					Assert.AreEqual	( lo_CTU1.NoBatchInpAft	, this.co_CTUParms.Setas_No					,	$"SAPNCO: CTUParams: Upd {this.cn_Cnt}: BtchAft"	);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_CTU_IsValid()
				{
					var	lo_CTU1	= new DTO_CTUParameters()	;

					CTU_Parameters.Ce_Validate le_Err	;
					CTU_Parameters.Ce_Validate le_OK		;
					CTU_Parameters.Ce_Validate le_All	;
					//...............................................
					this.cn_Cnt	= 3;

					lo_CTU1.DisplayMode		= '1'	;
					lo_CTU1.UpdateMode		= '1'	;
					lo_CTU1.CATTMode			= '1'	;
					lo_CTU1.DefaultSize		= '1'	;
					lo_CTU1.NoCommit			= '1'	;
					lo_CTU1.NoBatchInpFor	= '1'	;
					lo_CTU1.NoBatchInpAft	= '1'	;

					le_All	=		CTU_Parameters.Ce_Validate.BIA | CTU_Parameters.Ce_Validate.BIF | CTU_Parameters.Ce_Validate.Cat
										| CTU_Parameters.Ce_Validate.Com | CTU_Parameters.Ce_Validate.Dsp | CTU_Parameters.Ce_Validate.Sze
										| CTU_Parameters.Ce_Validate.Upd;

					le_Err	=	this.co_CTUParms.IsValid(lo_CTU1, false);
					le_OK		=	this.co_CTUParms.IsValid(lo_CTU1);

					Assert.AreEqual( le_All															, le_Err	,	$"SAPNCO: CTUParams: Val {this.cn_Cnt}: Error"	);
					Assert.AreEqual( CTU_Parameters.Ce_Validate.Non	, le_OK		,	$"SAPNCO: CTUParams: Val {this.cn_Cnt}: OK"			);
				}
		}
}
