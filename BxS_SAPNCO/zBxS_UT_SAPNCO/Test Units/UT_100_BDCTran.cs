using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_100_BDCTran
		{
			#region "Declarations"

				private readonly	UT_Destination					co_Dest;
				private readonly	NCOController						co_Cntlr;
				private readonly	IBDCProfile							co_Profile;
				private readonly	BDC2RfcParser						co_Parser;
				private readonly	BDCProfileConfigurator	co_PrfCnfg;

			#endregion

			//...................................................
			public UT_100_BDCTran()
				{
					this.co_Dest		= new UT_Destination(2);
					this.co_Cntlr		= new NCOController();
					this.co_Profile	= this.co_Cntlr.GetAddBDCTranProcessorProfile(this.co_Dest.RfcDest);
					this.co_Parser	= this.co_Cntlr.CreateBDC2RfcParser(this.co_Profile);
					this.co_PrfCnfg	= this.co_Cntlr.CreateProfileConfigurator();
					this.co_PrfCnfg.Configure(this.co_Profile);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCTranProcessor	lo_BDCTran0		= this.co_Cntlr.CreateBDCTransactionProcessor(this.co_Profile);
					IBDCTranProcessor	lo_BDCTran1		= this.co_Cntlr.CreateBDCTransactionProcessor(this.co_Profile);

					Assert.IsNotNull(	lo_BDCTran0	,	$"SAPNCO:BDCTran:Inst {ln_Cnt}: 1st" );
					Assert.IsNotNull(	lo_BDCTran1	,	$"SAPNCO:BDCTran:Inst {ln_Cnt}: 2nd" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Invoke()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					DTO_RFCData				lo_RfcData		= this.co_Cntlr.CreateRFCTranData(this.co_Profile);
					IBDCTranData			lo_TranData		= this.co_Cntlr.CreateBDCTranData(Guid.NewGuid());
					IBDCTranProcessor	lo_BDCTran0		= this.co_Cntlr.CreateBDCTransactionProcessor(this.co_Profile);

					this.UpdateCTU						(lo_TranData.CTUOptions)	;
					this.SetupTestBDCData			(lo_TranData	, "1007084"	, "666" )	;

					this.co_Parser.ParseFrom	(lo_TranData	,	lo_RfcData);
					lo_BDCTran0.Process(lo_RfcData);
					this.co_Parser.ParseTo(lo_RfcData,lo_TranData);

					Assert.AreNotEqual( 0	, lo_TranData.MSGCount	, $"SAPNCO:BDCTran:Invoke {ln_Cnt}: Simple" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Multiple()
				{
					int			ln_Cnt	= 0;
					string	lc_Tel	= "000";
					var			lo_Rnd	= new Random();
					//...............................................
					ln_Cnt	++;

												lc_Tel	= lo_Rnd.Next(100,1000).ToString();
					IList<string>	lt_No		= this.LoadList();

					DTO_RFCData				lo_RfcData		= this.co_Cntlr.CreateRFCTranData(this.co_Profile);
					IBDCTranData			lo_TranData		= this.co_Cntlr.CreateBDCTranData(Guid.NewGuid());
					IBDCTranProcessor	lo_BDCTran0		= this.co_Cntlr.CreateBDCTransactionProcessor(this.co_Profile);

					this.UpdateCTU(lo_TranData.CTUOptions)	;

					for (int i = 0; i < lt_No.Count; i++)
						{
							this.SetupTestBDCData			(lo_TranData	, lt_No[i]	, lc_Tel )	;
							this.co_Parser.ParseFrom	(lo_TranData	,	lo_RfcData);

							lo_BDCTran0.Process(lo_RfcData);

							this.co_Parser.ParseTo(lo_RfcData,lo_TranData);

							Assert.AreNotEqual( 0	, lo_TranData.MSGCount	, $"SAPNCO:BDCTran:Invoke {ln_Cnt}: Multi {i}" );
						}
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Many()
				{
					int ln_Cnt = 0;
					int ln_Tot = 0;
					const int ln_Max = 10;
					string	lc_Tel	= "000";
					var			lo_Rnd	= new Random();
					//...............................................
					ln_Cnt++;

												lc_Tel	= lo_Rnd.Next(100,1000).ToString();
					IList<string> lt_No		= this.LoadList();

					Parallel.Invoke(	() => { if (this.Task(lt_No[0]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[1]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[2]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[3]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[4]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[5]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[6]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[7]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[8]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); },
														() => { if (this.Task(lt_No[9]	,	lc_Tel)) Interlocked.Increment(ref ln_Tot); });

					Assert.AreEqual(ln_Max, ln_Tot, $"SAPNCO:BDCTran:Many {ln_Cnt}: <>=");
			}

			//-------------------------------------------------------------------------------------------
			private bool Task(string CustNo, string TelNo)
				{
					DTO_RFCData				lo_RfcData		= this.co_Cntlr.CreateRFCTranData(this.co_Profile);
					IBDCTranData			lo_TranData		= this.co_Cntlr.CreateBDCTranData(Guid.NewGuid());
					IBDCTranProcessor	lo_BDCTran0		= this.co_Cntlr.CreateBDCTransactionProcessor(this.co_Profile);
					this.UpdateCTU(lo_TranData.CTUOptions);

					this.SetupTestBDCData			(lo_TranData	, CustNo	, TelNo )	;
					this.co_Parser.ParseFrom	(lo_TranData	,	lo_RfcData);

					lo_BDCTran0.Process(lo_RfcData);

					this.co_Parser.ParseTo(lo_RfcData,lo_TranData);
					return	lo_TranData.SuccesStatus;
				}

			//-------------------------------------------------------------------------------------------
			private IList<string> LoadList()
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
			private void SetupTestBDCData( IBDCTranData BDCTran, string CustNo, string TelNo )
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

			//-------------------------------------------------------------------------------------------
			private void UpdateCTU( DTO_CTUOptions CTUOptions )
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

				// Sony GUI Path
				//C:\ProgramData\SAP\SAPUILandscapeS2A.xml

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

			}
}
