using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.BDCSession.Main;

using BxS_WorxUtil.Progress;
using BxS_WorxUtil.ObjectPool;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_400_BDCSession
		{
			private readonly	UT_000_NCO					co_NCO000				;
			private readonly	INCO_Controller			co_NCOCntlr			;
			private	readonly	BDC_SessionFactory	co_SessFact			;
			private	readonly	BDC_SessionFactory	co_SessFactGUI	;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_400_BDCSession()
				{
					this.co_NCO000				= new	UT_000_NCO()					;
					this.co_NCOCntlr			= this.co_NCO000._NCO_Cntlr	;

					this.co_SessFact			=	new	BDC_SessionFactory( this.co_NCO000.GetSAPDestConfigured() )	;
					this.co_SessFactGUI		=	new	BDC_SessionFactory( this.co_NCO000.GetSAPDestConfigured( showSAPGui: true ) )	;
					//...............................................
					Assert.IsNotNull( this.co_NCOCntlr , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_10_Instantiate()
				{
				  BDC_Session						lo_BDCSess	= this.GetBDCSession();
					DTO_BDC_SessionConfig	lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

					Assert.IsNotNull	( this.co_SessFact	, "a" );
					Assert.IsNotNull	( lo_BDCSess				, "b" );
					Assert.IsNotNull	( lo_SessCfg				, "c" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_20_Configure()
				{
				  BDC_Session						lo_BDCSess	= this.GetBDCSession();
					DTO_BDC_SessionConfig	lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

					lo_SessCfg.ConsumersNo	= 2	;
					lo_SessCfg.ConsumersMax	= 4	;

					lo_BDCSess.ConfigureSession( lo_SessCfg );

					Assert.AreEqual	( lo_SessCfg.ConsumersMax	,	lo_BDCSess.Config.ConsumersMax	, "a" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_400_BCDSess_30_ProcessSimple()
				{
					const	int ln_Trn	= 1;

					var CTS		= new CancellationTokenSource();

					ProgressHandler<DTO_BDC_Progress> lo_PH		= this.co_SessFact.CreateProgressHandler();
					ObjectPool<BDC_SessionConsumer>		lo_Pool	= this.co_SessFact.CreateBDCConsumerPool();

				  BDC_Session			lo_BDCSess	= this.GetConfiguredBDCSession();
					DTO_BDC_Session	lo_SessDTO	=	this.co_SessFact.CreateSessionDTO()	;

					this.LoadBDCData( lo_SessDTO , ln_Trn , 'N' , true );

					int ln_ConCnt = await lo_BDCSess.Process_SessionAsync(	lo_SessDTO
																																,	CTS.Token
																																, lo_PH
																																,	lo_Pool
																																,	this.co_SessFact.SMCDestination ).ConfigureAwait(false);

					while (!ln_ConCnt.Equals(lo_SessDTO.Trans.Count))
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual( ln_Trn , lo_BDCSess.TransactionsProcessed , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_400_BCDSess_40_ProcessSAPGUI()
				{
					const	int ln_Trn	= 01;

					var CTS		= new CancellationTokenSource();

					ProgressHandler<DTO_BDC_Progress> lo_PH		= this.co_SessFactGUI.CreateProgressHandler();
					ObjectPool<BDC_SessionConsumer>		lo_Pool	= this.co_SessFactGUI.CreateBDCConsumerPool();

				  BDC_Session			lo_BDCSess	= this.GetConfiguredBDCSession( true , 1 , 1 , true );
					DTO_BDC_Session	lo_SessDTO	=	this.co_SessFactGUI.CreateSessionDTO()	;

					this.LoadBDCData( lo_SessDTO , ln_Trn , 'A' );

					int ln_ConCnt = await lo_BDCSess.Process_SessionAsync(	lo_SessDTO
																																,	CTS.Token
																																, lo_PH
																																,	lo_Pool
																																,	this.co_SessFactGUI.SMCDestination ).ConfigureAwait(false);

					while (!ln_ConCnt.Equals(lo_SessDTO.Trans.Count))
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual( ln_Trn , lo_BDCSess.TransactionsProcessed , "" );
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session GetConfiguredBDCSession( bool Seq = false, int No = 5, int Max = 5 , bool GUI = false )
					{
						BDC_Session						lo_BDCSess	= this.GetBDCSession( GUI );
						DTO_BDC_SessionConfig	lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

						lo_SessCfg.IsSequential	=	Seq	;
						lo_SessCfg.ConsumersNo	= No	;
						lo_SessCfg.ConsumersMax	= Max	;

						lo_BDCSess.ConfigureSession( lo_SessCfg );
						return	lo_BDCSess;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session GetBDCSession( bool GUI = false )
					{
						this.ReadyFactory( GUI );
						return	GUI ?	this.co_SessFactGUI.CreateBDCSession() : this.co_SessFact.CreateBDCSession() ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ReadyFactory( bool GUI = false )
					{
						if (GUI)
							{
								Task.Run( ()=>	this.co_SessFactGUI	.ReadyEnvironmentAsync()).Wait();
							}
						else
							{
								Task.Run( ()=>	this.co_SessFact		.ReadyEnvironmentAsync()).Wait();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBDCData( DTO_BDC_Session dto , int NoOfTrans = 1 , char DispMode = 'N' , bool ChgMode = false )
					{
						dto.Header.SAPTCode		=	ChgMode	? "XD02" : "XD03"	;
						dto.Header.Skip1st		= false		;

						DTO_BDC_CTU lo_CTU	= dto.Header.CTUParms	;

						lo_CTU.DisplayMode	= DispMode	;
						lo_CTU.UpdateMode		= 'A'				;
						//.............................................
						IList<string>	lt_CustNo		= this.LoadCustNo( NoOfTrans );

						for (int i = 0; i < NoOfTrans; i++)
							{
								DTO_BDC_Transaction lo_Trn	= dto.CreateTransDTO(i+1);
								DTO_BDC_Data				lo_D1		=	lo_Trn.CreateDataDTO();

								lo_D1.ProgramName	= "SAPMF02D";
								lo_D1.Dynpro			= "0101";
								lo_D1.Begin				= "X";
								lo_D1.FieldName		= "BDC_OKCODE";
								lo_D1.FieldValue	=	"/00";

								lo_Trn.BDCData.Add( lo_D1 );
								//.............................................
								lo_Trn.AddBDCData( field: "RF02D-KUNNR" , value: lt_CustNo[i]	);
								lo_Trn.AddBDCData( field: "RF02D-D0110" , value: "X"				);

								if ( ChgMode )
									{
										string x = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss:fffzzz");

										lo_Trn.AddBDCData( "SAPMF02D" ,  110 , true , "BDC_OKCODE" , "=UPDA" );
										lo_Trn.AddBDCData(	field: "KNA1-NAME2" , value: x );
									}
								else
									{
										lo_Trn.AddBDCData( "SAPMF02D" ,  110 , true , "BDC_OKCODE" , "=PF03" );
									}
								//.............................................
								dto.Trans.Enqueue( lo_Trn);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> LoadCustNo( int NoOfTrans = 1 )
					{
						IList<string>	lt_List		= new	List<string>( NoOfTrans );
						string[]			lt_No			= this.LoadList();

						for (int i = 0; i < NoOfTrans; i++)
							{
								lt_List.Add( lt_No[i] );
							}

						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string[] LoadList()
					{
						string[] lt_No	=
							{
								"0001000000"	,
								"0001000001"	,
								"0001000005"	,
								"0001000006"	,
								"0001000007"	,
								"0001000008"	,
								"0001000009"	,
								"0001000010"	,
								"0001000011"	,
								"0001000012"	,
								"0001000013"	,
								"0001000014"	,
								"0001000015"	,
								"0001000016"	,
								"0001000017"	,
								"0001000018"	,
								"0001000019"	,
								"0001000020"	,
								"0001000021"	,
								"0001000022"	,
								"0001000023"	,
								"0001000024"	,
								"0001000025"	,
								"0001000026"	,
								"0001000027"	,
								"0001000028"	,
								"0001000029"	,
								"0001000030"	,
								"0001000031"	,
								"0001000032"	,
								"0001000033"	,
								"0001000034"	,
								"0001000035"	,
								"0001000036"	,
								"0001000037"	,
								"0001000038"	,
								"0001000039"	,
								"0001000040"	,
								"0001000041"	,
								"0001000045"	,
								"0001000046"	,
								"0001000047"	,
								"0001000048"	,
								"0001000049"	,
								"0001000050"	,
								"0001000051"	,
								"0001000052"	,
								"0001000053"	,
								"0001000054"	,
								"0001000055"	,
								"0001000056"	,
								"0001000057"	,
								"0001000058"	,
								"0001000059"	,
								"0001000060"	,
								"0001000061"	,
								"0001000062"	,
								"0001000063"	,
								"0001000064"	,
								"0001000065"	,
								"0001000066"	,
								"0001000067"	,
								"0001000068"	,
								"0001000069"	,
								"0001000070"	,
								"0001000071"	,
								"0001000072"	,
								"0001000073"	,
								"0001000074"	,
								"0001000075"	,
								"0001000076"	,
								"0001000077"	,
								"0001000078"	,
								"0001000079"	,
								"0001000080"	,
								"0001000081"	,
								"0001000082"	,
								"0001000083"	,
								"0001000084"	,
								"0001000085"	,
								"0001000086"	,
								"0001000087"	,
								"0001000088"	,
								"0001000089"	,
								"0001000090"	,
								"0001000091"	,
								"0001000092"	,
								"0001000093"	,
								"0001000094"	,
								"0001000095"	,
								"0001000096"	,
								"0001000097"	,
								"0001000098"	,
								"0001000099"	,
								"0001000100"	,
								"0001000101"	,
								"0001000102"	,
								"0001000103"	,
								"0001000104"	,
								"0001000105"	,
								"0001000106"	,
								"0001000107"	,
								"0001000108"	,
								"0001000109"	,
								"0001000110"	,
								"0001000111"	,
								"0001000112"	,
								"0001000113"	,
								"0001000114"	,
								"0001000115"	,
								"0001000116"	,
								"0001000117"	,
								"0001000118"	,
								"0001000119"	,
								"0001000120"	,
								"0001000121"	,
								"0001000122"	,
								"0001000123"	,
								"0001000124"	,
								"0001000125"	,
								"0001000126"	,
								"0001000127"	,
								"0001000128"	,
								"0001000129"	,
								"0001000130"	,
								"0001000131"	,
								"0001000132"	,
								"0001000133"	,
								"0001000134"	,
								"0001000135"	,
								"0001000136"	,
								"0001000137"	,
								"0001000138"	,
								"0001000139"	,
								"0001000140"	,
								"0001000141"	,
								"0001000142"	,
								"0001000143"	,
								"0001000144"	,
								"0001000145"	,
								"0001000146"	,
								"0001000147"	,
								"0001000148"	,
								"0001000149"	,
								"0001000150"	,
								"0001000151"	,
								"0001000152"	,
								"0001000153"	,
								"0001000154"	,
								"0001000155"	,
								"0001000156"	,
								"0001000157"	,
								"0001000158"	,
								"0001000159"	,
								"0001000160"	,
								"0001000161"	,
								"0001000162"	,
								"0001000163"	,
								"0001000164"	,
								"0001000165"	,
								"0001000166"	,
								"0001000167"	,
								"0001000168"	,
								"0001000169"	,
								"0001000170"	,
								"0001000171"	,
								"0001000172"	,
								"0001000173"	,
								"0001000174"	,
								"0001000175"	,
								"0001000176"	,
								"0001000177"	,
								"0001000178"	,
								"0001000179"	,
								"0001000180"	,
								"0001000181"	,
								"0001000182"	,
								"0001000183"	,
								"0001000184"	,
								"0001000185"	,
								"0001000186"	,
								"0001000187"	,
								"0001000188"	,
								"0001000189"	,
								"0001000190"	,
								"0001000191"	,
								"0001000192"	,
								"0001000193"	,
								"0001000194"	,
								"0001000195"	,
								"0001000196"	,
								"0001000197"	,
								"0001000198"	,
								"0001000199"	,
								"0001000200"	,
								"0001000201"	,
								"0001000202"	,
								"0001000203"	,
								"0001000204"	,
								"0001000205"
							};

						return	lt_No;
					}

		//.

		}
}

