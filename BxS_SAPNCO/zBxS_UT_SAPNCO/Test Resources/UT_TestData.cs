using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	//***************************************************************************
	public class UT_TestData
		{
			//-------------------------------------------------------------------------------------------
			public DTO_SessionHeader CreateSessionHead(char DispMode	= 'A' )
				{
					var lo = new DTO_SessionHeader	{		SAPTCode	= "XD02"
																						,	Skip1st		= " "			};
					var lo_CTU	= new CTUParametersHandler	{	DisplayMode	= DispMode };
					lo.CTUParms	= lo_CTU.GetImage();
					return	lo;
				}

			//-------------------------------------------------------------------------------------------
			public DTO_CTUParms UpdateCTU(char DispMode	= 'A')
				{
					var						lo_CTU	= new CTUParametersHandler();
					DTO_CTUParms	ls_CTU	=	lo_CTU.GetImage();
			    this.UpdateCTU(ls_CTU, DispMode);
					return	ls_CTU;
				}

			//-------------------------------------------------------------------------------------------
			public void UpdateCTU( DTO_CTUParms CTUParms, char DispMode	= 'A' )
				{
					var lo_CTU	= new CTUParametersHandler	{	DisplayMode	= DispMode };
					lo_CTU.TransferImage(CTUParms);
				}

			//-------------------------------------------------------------------------------------------
			public IList<string> LoadList(bool big = false)
			{
					if (big)
						{
							return	 new List<string>()	{		"1007084"	,	"1800476"	,	"1802054"	,	"1802201"	,	"1810161"
																						,	"1810184"	,	"2012050"	,	"2035959"	,	"2800242"	,	"1800238"
																						,	"2810415"	,	"2812552"	,	"2812860"	,	"2814664"	,	"2814665"
																						,	"2815127"	,	"2815563"	,	"2815938"
																					};
						}
					else
						{
							return	 new List<string>()	{		"1007084"	,	"1800476"	,	"1802054"	,	"1802201"	,	"1810161"
																						,	"1810184"	,	"2012050"	,	"2035959"	,	"2800242"	,	"1800238"
																					};
						}
			}

			//-------------------------------------------------------------------------------------------
			public DTO_SessionTran SetupTestBDCData( string CustNo, string TelNo )
				{
					var X = new DTO_SessionTran();
					this.SetupTestBDCData(X,CustNo,TelNo);
					return	X;
				}

			//-------------------------------------------------------------------------------------------
			public void SetupTestBDCData( DTO_SessionTran BDCTran, string CustNo, string TelNo )
				{
					BDCTran.Reset();
					//...............................................
					BDCTran.AddBDCData(	"SAPMF02D"	,	0101	,	true	,""						, ""			);
					BDCTran.AddBDCData(	""					,	0			,	false	,"BDC_OKCODE"	, "/00"		);
					BDCTran.AddBDCData(	""					,	0			,	false	,"RF02D-KUNNR", CustNo	);
					BDCTran.AddBDCData(	""					,	0			,	false	,"RF02D-D0110", "X"			);
					BDCTran.AddBDCData(	""					,	0			,	false	,"USE_ZAV"		, "X"			);
					BDCTran.AddBDCData(	"SAPMF02D"	,	0111	,	true	,""						, ""			);
					BDCTran.AddBDCData(	""					,	0			,	false	,"BDC_OKCODE"	, "=UPDA"	);

					BDCTran.AddBDCData(	""					,	0			,	false	,"SZA1_D0100-FAX_NUMBER"	, TelNo	);
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal void PutCTUOptions( DTO_CTUParms ctuParms , SMC.IRfcStructure ctuParmsRFC )
				{
					ctuParmsRFC.SetValue(	0	,	ctuParms.DisplayMode		);
					ctuParmsRFC.SetValue(	1	,	ctuParms.UpdateMode			);
					ctuParmsRFC.SetValue(	2	,	ctuParms.CATTMode				);
					ctuParmsRFC.SetValue(	3	,	ctuParms.DefaultSize		);
					ctuParmsRFC.SetValue(	4	,	ctuParms.NoCommit				);
					ctuParmsRFC.SetValue(	5	,	ctuParms.NoBatchInpFor	);
					ctuParmsRFC.SetValue(	6	,	ctuParms.NoBatchInpAft	);
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal void PutBDCData(	IList<DTO_SessionTranData> bdcData	, SMC.IRfcTable lt_Data	)
				{
					lt_Data.Append(	bdcData.Count	);
					//.............................................
					for (	int i = 0; i < bdcData.Count; i++	)
						{
							lt_Data.CurrentIndex	= i;

							lt_Data.SetValue(	0	, bdcData[i].ProgramName );
							lt_Data.SetValue(	1	, bdcData[i].Dynpro			 );
							lt_Data.SetValue(	2	, bdcData[i].Begin			 );
							lt_Data.SetValue(	3	, bdcData[i].FieldName	 );
							lt_Data.SetValue(	4	, bdcData[i].FieldValue	 );
						}
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

