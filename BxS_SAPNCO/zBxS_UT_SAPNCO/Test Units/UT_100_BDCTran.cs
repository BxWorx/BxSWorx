using System;
using System.Collections.Generic;
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

				private readonly	UT_Destination	co_Dest;
				private readonly	NCOController		co_Cntlr;

			#endregion

			//...................................................
			public UT_100_BDCTran()
				{
					this.co_Dest		= new UT_Destination(2);
					this.co_Cntlr		= new NCOController();
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCCallTransaction	lo_BDCTran0		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);
					IBDCCallTransaction	lo_BDCTran1		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);

					Assert.IsNotNull(	lo_BDCTran0	,	$"SAPNCO:BDCTran:Inst {ln_Cnt}: 1st" );
					Assert.IsNotNull(	lo_BDCTran1	,	$"SAPNCO:BDCTran:Inst {ln_Cnt}: 2nd" );

					lo_BDCTran0.SAPTransaction	= "0";
					lo_BDCTran1.SAPTransaction	= "1";

					Assert.AreEqual( lo_BDCTran0.SAPTransaction	, "0"	,	$"SAPNCO:BDCTran:Indi {ln_Cnt}: 1st" );
					Assert.AreEqual( lo_BDCTran1.SAPTransaction	, "1"	,	$"SAPNCO:BDCTran:Indi {ln_Cnt}: 2nd" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Invoke()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IBDCCallTransaction	lo_BDCTran0		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);

					this.UpdateCTU				(lo_BDCTran0)	;
					this.SetupTestBDCData	(lo_BDCTran0	, "1007084"	, "444" )	;

					lo_BDCTran0.Invoke();

					Assert.AreNotEqual( 0	, lo_BDCTran0.MsgDataCount	, $"SAPNCO:BDCTran:Invoke {ln_Cnt}: Simple" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Multiple()
				{
					int			ln_Cnt	= 0;
					string	lc_Tel	= "000";
					Random	lo_Rnd	= new Random();
					//...............................................
					ln_Cnt	++;

					lc_Tel	= lo_Rnd.Next(100,1000).ToString();

					IList<string>				lt_No				= this.LoadList();
					IBDCCallTransaction	lo_BDCTran0	= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);

					this.UpdateCTU(lo_BDCTran0)	;

					for (int i = 0; i < lt_No.Count; i++)
						{
							lo_BDCTran0.Reset();
							this.SetupTestBDCData	(lo_BDCTran0	, lt_No[i]	, lc_Tel )	;
							lo_BDCTran0.Invoke();
							Assert.AreNotEqual( 0	, lo_BDCTran0.MsgDataCount	, $"SAPNCO:BDCTran:Invoke {ln_Cnt}: Multi {i}" );
						}
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_BDCTran_Many()
				{
					int	ln_Cnt	=  0;
					int ln_Max	= 10;
					//...............................................
					ln_Cnt	++;

					IList<bool>		lt_TF = new List<bool>	(ln_Max);
					IList<string> lt_No = this.LoadList();

					Parallel.Invoke(	() =>	lt_TF.Add( this.Task(lt_No[0])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[1])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[2])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[3])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[4])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[5])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[6])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[7])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[8])	)	,
														() =>	lt_TF.Add( this.Task(lt_No[9])	)		);

					//Assert.AreEqual( ln_Max, lt_No.Count	, $"SAPNCO:BDCTran:Many {ln_Cnt}: <>=" );
				}

			//-------------------------------------------------------------------------------------------
			private IList<string> LoadList()
			{
					int ln_Max	= 10;
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
			private bool Task(string CustNo)
				{
					IBDCCallTransaction	lo_BDCTran0		= this.co_Cntlr.CreateBDCCallTransaction(this.co_Dest.RfcDest);
					this.UpdateCTU(lo_BDCTran0)	;
					this.SetupTestBDCData	(lo_BDCTran0	, CustNo, "888" )	;
					return	lo_BDCTran0.Invoke();
				}

			//-------------------------------------------------------------------------------------------
			private void SetupTestBDCData( IBDCCallTransaction BDCTran, string CustNo, string TelNo )
				{
					BDCTran.SAPTransaction	= "XD02";

					BDCTran.CreateBDCEntry("SAPMF02D"	,	0101	,	true	,""						, ""			);
					BDCTran.CreateBDCEntry(""					,	0			,	false	,"BDC_OKCODE"	, "/00"		);
					BDCTran.CreateBDCEntry(""					,	0			,	false	,"RF02D-KUNNR", CustNo	);
					BDCTran.CreateBDCEntry(""					,	0			,	false	,"RF02D-D0110", "X"			);
					BDCTran.CreateBDCEntry(""					,	0			,	false	,"USE_ZAV"		, "X"			);
					BDCTran.CreateBDCEntry("SAPMF02D"	,	0111	,	true	,""						, ""			);
					BDCTran.CreateBDCEntry(""					,	0			,	false	,"BDC_OKCODE"	, "=UPDA"	);
					BDCTran.CreateBDCEntry(""					,	0			,	false	,"SZA1_D0100-FAX_NUMBER"	, TelNo	);
				}

			//-------------------------------------------------------------------------------------------
			private void UpdateCTU( IBDCCallTransaction BDCTran )
				{
					var lo_CTU	= new CTU_Parameters();

					lo_CTU.DisplayMode		= lo_CTU.DisplayMode_BGrnd	;
					lo_CTU.UpdateMode			= lo_CTU.UpdateMode_ASync		;
					lo_CTU.CATTMode				= lo_CTU.CATTMode_None			;
					lo_CTU.DefaultSize		= lo_CTU.Setas_No						;
					lo_CTU.NoCommit				= lo_CTU.Setas_No						;
					lo_CTU.NoBatchInpFor	= lo_CTU.Setas_No						;
					lo_CTU.NoBatchInpAft	= lo_CTU.Setas_No 					;

					BDCTran.CTUParm	=	lo_CTU.GetImage();

					// or
					//lo_CTU.TransferImage(DTO);
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
			}
}
