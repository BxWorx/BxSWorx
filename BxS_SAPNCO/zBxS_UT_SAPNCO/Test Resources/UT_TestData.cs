using System.Collections.Generic;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
using BxS_SAPNCO.API.SAPFunctions.BDC.Session;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	//***************************************************************************
	public class UT_TestData
		{
			//-------------------------------------------------------------------------------------------
			public void UpdateCTU( DTO_CTUParameters CTUOptions )
				{
					var lo_CTU	= new CTU_Parameters();

					lo_CTU.DisplayMode		= lo_CTU.DisplayMode_BGrnd	;
					lo_CTU.UpdateMode			= lo_CTU.UpdateMode_ASync		;
					lo_CTU.CATTMode				= lo_CTU.CATTMode_None			;
					lo_CTU.DefaultSize		= lo_CTU.Setas_No						;
					lo_CTU.NoCommit				= lo_CTU.Setas_No						;
					lo_CTU.NoBatchInpFor	= lo_CTU.Setas_No						;
					lo_CTU.NoBatchInpAft	= lo_CTU.Setas_No 					;

					//CTUOptions	=	lo_CTU.GetImage();
					lo_CTU.TransferImage(CTUOptions);
				}

			//-------------------------------------------------------------------------------------------
			public IList<string> LoadList()
			{
					const int ln_Max	= 10;
					return	 new List<string>(ln_Max)	{	"1007084"	,
																							"1800476"	,
																							"1802054"	,
																							"1802201"	,
																							"1810161"	,
																							"1810184"	,
																							"2012050"	,
																							"2035959"	,
																							"2800242"	,
																							"1800238"		};
			}
		

			//-------------------------------------------------------------------------------------------
			public void SetupTestBDCData( SessionTran BDCTran, string CustNo, string TelNo )
				{
					BDCTran.Reset();
					//...............................................
					BDCTran.AddBDCData("SAPMF02D"	,	0101	,	true	,""						, ""			);
					BDCTran.AddBDCData(""					,	0			,	false	,"BDC_OKCODE"	, "/00"		);
					BDCTran.AddBDCData(""					,	0			,	false	,"RF02D-KUNNR", CustNo	);
					BDCTran.AddBDCData(""					,	0			,	false	,"RF02D-D0110", "X"			);
					BDCTran.AddBDCData(""					,	0			,	false	,"USE_ZAV"		, "X"			);
					BDCTran.AddBDCData("SAPMF02D"	,	0111	,	true	,""						, ""			);
					BDCTran.AddBDCData(""					,	0			,	false	,"BDC_OKCODE"	, "=UPDA"	);
					BDCTran.AddBDCData(""					,	0			,	false	,"SZA1_D0100-FAX_NUMBER"	, TelNo	);
				}

			//-------------------------------------------------------------------------------------------
			public void SetupTestBDCData( IBDCTranData BDCTran, string CustNo, string TelNo )
				{
					BDCTran.Reset();
					//...............................................
					BDCTran.SAPTCode	= "XD02";

					BDCTran.AddBDCData("SAPMF02D"	,	0101	,	true	,""						, ""			);
					BDCTran.AddBDCData(""					,	0			,	false	,"BDC_OKCODE"	, "/00"		);
					BDCTran.AddBDCData(""					,	0			,	false	,"RF02D-KUNNR", CustNo	);
					BDCTran.AddBDCData(""					,	0			,	false	,"RF02D-D0110", "X"			);
					BDCTran.AddBDCData(""					,	0			,	false	,"USE_ZAV"		, "X"			);
					BDCTran.AddBDCData("SAPMF02D"	,	0111	,	true	,""						, ""			);
					BDCTran.AddBDCData(""					,	0			,	false	,"BDC_OKCODE"	, "=UPDA"	);
					BDCTran.AddBDCData(""					,	0			,	false	,"SZA1_D0100-FAX_NUMBER"	, TelNo	);
				}
		}
}

//2810415
//2812552
//2812860
//2814664
//2814665
//2815127
//2815563
//2815938

//1007084
//1800476
//1802054
//1802201
//1810161
//1810184
//2012050
//2035959
//2800242
//1800238

// Sony GUI Path
//C:\ProgramData\SAP\SAPUILandscapeS2A.xml

