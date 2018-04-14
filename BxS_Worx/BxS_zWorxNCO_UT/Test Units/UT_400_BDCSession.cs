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
			private readonly	UT_000_NCO					co_NCO000		;
			private readonly	INCO_Controller			co_NCOCntlr	;
			private	BDC_Session_Factory	co_SessFact	;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_400_BDCSession()
				{
					this.co_NCO000		= new	UT_000_NCO()					;
					this.co_NCOCntlr	= this.co_NCO000._NCO_Cntlr	;
					//...............................................
					Assert.IsNotNull( this.co_NCOCntlr , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_10_Instantiate()
				{
					this.co_SessFact	=	new	BDC_Session_Factory( this.co_NCO000.GetSAPDestConfigured() )	;

					Assert.IsNotNull	( this.co_SessFact	, "a" );
					//...............................................
				  BDC_Session_TranProcessor	lo_BDCSess	= this.GetBDCSession();
					DTO_BDC_SessionConfig		lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

					Assert.IsNotNull	( lo_BDCSess				, "b" );
					Assert.IsNotNull	( lo_SessCfg				, "c" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_20_Configure()
				{
					this.co_SessFact	=	new	BDC_Session_Factory( this.co_NCO000.GetSAPDestConfigured() )	;

				  BDC_Session_TranProcessor	lo_BDCSess	= this.GetBDCSession();
					DTO_BDC_SessionConfig		lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

					lo_SessCfg.ConsumersNo	= 2	;
					lo_SessCfg.ConsumersMax	= 4	;

					lo_BDCSess.ConfigureSession( lo_SessCfg );

					Assert.AreEqual	( lo_SessCfg.ConsumersMax	,	lo_BDCSess.Config.ConsumersMax	, "a" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_30_ProcessCall()
				{
					Task.Run( async ()=> await this.BCDSess_Process( 200 , false ).ConfigureAwait(false)).Wait();
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_32_ProcessTran()
				{
					Task.Run( async ()=> await this.BCDSess_Process( 200 , true ).ConfigureAwait(false)).Wait();
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private async Task BCDSess_Process( int ln_Trn = 200 , bool UseTrnVersn = false )
				{
					var CTS		= new CancellationTokenSource();

					this.co_SessFact	=	new	BDC_Session_Factory( this.co_NCO000.GetSAPDestConfigured() , UseTrnVersn )	;

					ProgressHandler<DTO_BDC_Progress>				lo_PH				= this.co_SessFact.CreateProgressHandler			();
					ObjectPool<BDC_Session_TranConsumer>		lo_PoolTrn	= this.co_SessFact.CreateBDCTransConsumerPool	();
					ObjectPool<BDC_Session_SAPMsgConsumer	>	lo_PoolMsg	= this.co_SessFact.CreateBDCSAPMsgConsumerPool();

					Task.Run( ()=>	this.co_SessFact.ReadyEnvironmentAsync( true )).Wait();

				  BDC_Session_SAPMsgProcessor	lo_SAPMsg		= this.GetConfiguredSAPMsgProcessor	( false , 5 , 5	);
				  BDC_Session_TranProcessor			lo_BDCSess	= this.GetConfiguredBDCSession			( false , 5 , 5 );
					DTO_BDC_Session							lo_SessDTO	=	this.co_SessFact.CreateSessionDTO	()												;

					this.LoadBDCData( lo_SessDTO , ln_Trn , 'N' , true );
					//...............................................
					int ln_TrnCnt		= await lo_BDCSess.Process_SessionAsync(	lo_SessDTO
																																	,	CTS.Token
																																	, lo_PH
																																	,	lo_PoolTrn
																																	,	this.co_SessFact.SMCDestination ).ConfigureAwait(false);

					while ( ln_TrnCnt < lo_SessDTO.Trans.Count )
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual( ln_Trn , lo_BDCSess.TransactionsProcessed , "" );
					//...............................................
					int ln_MsgCnt		= await lo_SAPMsg.Process_SessionAsync(		lo_SessDTO
																																	,	CTS.Token
																																	, lo_PH
																																	,	lo_PoolMsg
																																	,	this.co_SessFact.SMCDestination ).ConfigureAwait(false);

					while ( ln_MsgCnt < lo_SessDTO.Trans.Count )
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual( ln_Trn , lo_SAPMsg.TransactionsProcessed , "" );
				}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session_SAPMsgProcessor GetConfiguredSAPMsgProcessor( bool Seq = false, int No = 5, int Max = 5 )
					{
						BDC_Session_SAPMsgProcessor		lo_MsgProc	= this.co_SessFact.CreateSAPMsgsProcessor();
						DTO_BDC_SessionConfig					lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

						lo_SessCfg.IsSequential	=	Seq	;
						lo_SessCfg.ConsumersNo	= No	;
						lo_SessCfg.ConsumersMax	= Max	;

						lo_MsgProc.ConfigureSession( lo_SessCfg );

						return	lo_MsgProc;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session_TranProcessor GetConfiguredBDCSession( bool Seq = false, int No = 5, int Max = 5 )
					{
						BDC_Session_TranProcessor	lo_BDCSess	= this.GetBDCSession();
						DTO_BDC_SessionConfig		lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

						lo_SessCfg.IsSequential	=	Seq	;
						lo_SessCfg.ConsumersNo	= No	;
						lo_SessCfg.ConsumersMax	= Max	;

						lo_BDCSess.ConfigureSession( lo_SessCfg );

						return	lo_BDCSess;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session_TranProcessor GetBDCSession()
					{
						return	this.co_SessFact.CreateBDCSession() ;
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
										string x = DateTime.Now.ToString("yyyy-MM-dd [HH:mm:ss:fff]");

										lo_Trn.AddBDCData( "SAPMF02D" , 0110 , true , "BDC_OKCODE" , "=UPDA" );
										lo_Trn.AddBDCData(	field: "KNA1-NAME2" , value: x );
									}
								else
									{
										lo_Trn.AddBDCData( "SAPMF02D" , 0110 , true , "BDC_OKCODE" , "=PF03" );
									}
								//.............................................
								dto.Trans.Enqueue( lo_Trn);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> LoadCustNo( int NoOfTrans = 1 )
					{
						IList<string>	lt_List		= new	List<string>( NoOfTrans );

						for (int i = 0; i < NoOfTrans; i++)
							{
								string x = (i + 1000000).ToString("D" + 7);
								lt_List.Add( x );
							}

						return	lt_List;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private string[] LoadNoList()
				//	{
				//		string[] lt_No	=
				//			{
				//				"0001000000"	,	// 001
				//				"0001000001"	,	// 002
				//				"0001000005"	,	// 003
				//				"0001000006"	,	// 004
				//				"0001000007"	,	// 005
				//				"0001000008"	,	// 006
				//				"0001000009"	,	// 007
				//				"0001000010"	,	// 008
				//				"0001000011"	,	// 009
				//				"0001000012"	,	// 010
				//				"0001000013"	,	// 011
				//				"0001000014"	,	// 012
				//				"0001000015"	,	// 013
				//				"0001000016"	,	// 014
				//				"0001000017"	,	// 015
				//				"0001000018"	,	// 016
				//				"0001000019"	,	// 017
				//				"0001000020"	,	// 018
				//				"0001000021"	,	// 019
				//				"0001000022"	,	// 020
				//				"0001000023"	,	// 001
				//				"0001000024"	,	// 001
				//				"0001000025"	,	// 001
				//				"0001000026"	,	// 001
				//				"0001000027"	,	// 001
				//				"0001000028"	,	// 001
				//				"0001000029"	,	// 001
				//				"0001000030"	,	// 001
				//				"0001000031"	,	// 001
				//				"0001000032"	,	// 001
				//				"0001000033"	,	// 001
				//				"0001000034"	,	// 001
				//				"0001000035"	,	// 001
				//				"0001000036"	,	// 001
				//				"0001000037"	,	// 001
				//				"0001000038"	,	// 001
				//				"0001000039"	,	// 001
				//				"0001000040"	,	// 001
				//				"0001000041"	,	// 001
				//				"0001000045"	,	// 001
				//				"0001000046"	,	// 001
				//				"0001000047"	,	// 001
				//				"0001000048"	,	// 001
				//				"0001000049"	,	// 001
				//				"0001000050"	,	// 001
				//				"0001000051"	,	// 001
				//				"0001000052"	,	// 001
				//				"0001000053"	,	// 001
				//				"0001000054"	,	// 001
				//				"0001000055"	,	// 001
				//				"0001000056"	,	// 001
				//				"0001000057"	,	// 001
				//				"0001000058"	,	// 001
				//				"0001000059"	,	// 001
				//				"0001000060"	,	// 001
				//				"0001000061"	,	// 001
				//				"0001000062"	,	// 001
				//				"0001000063"	,	// 001
				//				"0001000064"	,	// 001
				//				"0001000065"	,	// 001
				//				"0001000066"	,	// 001
				//				"0001000067"	,	// 001
				//				"0001000068"	,	// 001
				//				"0001000069"	,	// 001
				//				"0001000070"	,	// 001
				//				"0001000071"	,	// 001
				//				"0001000072"	,	// 001
				//				"0001000073"	,	// 001
				//				"0001000074"	,	// 001
				//				"0001000075"	,	// 001
				//				"0001000076"	,	// 001
				//				"0001000077"	,	// 001
				//				"0001000078"	,	// 001
				//				"0001000079"	,	// 001
				//				"0001000080"	,	// 001
				//				"0001000081"	,	// 001
				//				"0001000082"	,	// 001
				//				"0001000083"	,	// 001
				//				"0001000084"	,	// 001
				//				"0001000085"	,	// 001
				//				"0001000086"	,	// 001
				//				"0001000087"	,	// 001
				//				"0001000088"	,	// 001
				//				"0001000089"	,	// 001
				//				"0001000090"	,	// 001
				//				"0001000091"	,	// 001
				//				"0001000092"	,	// 001
				//				"0001000093"	,	// 001
				//				"0001000094"	,	// 001
				//				"0001000095"	,	// 001
				//				"0001000096"	,	// 001
				//				"0001000097"	,	// 001
				//				"0001000098"	,	// 001
				//				"0001000099"	,	// 001
				//				"0001000100"	,	// 001
				//				"0001000101"	,	// 001
				//				"0001000102"	,	// 001
				//				"0001000103"	,	// 001
				//				"0001000104"	,	// 001
				//				"0001000105"	,	// 001
				//				"0001000106"	,	// 001
				//				"0001000107"	,	// 001
				//				"0001000108"	,	// 001
				//				"0001000109"	,	// 001
				//				"0001000110"	,	// 001
				//				"0001000111"	,	// 001
				//				"0001000112"	,	// 001
				//				"0001000113"	,	// 001
				//				"0001000114"	,	// 001
				//				"0001000115"	,	// 001
				//				"0001000116"	,	// 001
				//				"0001000117"	,	// 001
				//				"0001000118"	,	// 001
				//				"0001000119"	,	// 001
				//				"0001000120"	,	// 001
				//				"0001000121"	,	// 001
				//				"0001000122"	,	// 001
				//				"0001000123"	,	// 001
				//				"0001000124"	,	// 001
				//				"0001000125"	,	// 001
				//				"0001000126"	,	// 001
				//				"0001000127"	,	// 001
				//				"0001000128"	,	// 001
				//				"0001000129"	,	// 001
				//				"0001000130"	,	// 001
				//				"0001000131"	,	// 001
				//				"0001000132"	,	// 001
				//				"0001000133"	,	// 001
				//				"0001000134"	,	// 001
				//				"0001000135"	,	// 001
				//				"0001000136"	,	// 001
				//				"0001000137"	,	// 001
				//				"0001000138"	,	// 001
				//				"0001000139"	,	// 001
				//				"0001000140"	,	// 001
				//				"0001000141"	,	// 001
				//				"0001000142"	,	// 001
				//				"0001000143"	,	// 001
				//				"0001000144"	,	// 001
				//				"0001000145"	,	// 001
				//				"0001000146"	,	// 001
				//				"0001000147"	,	// 001
				//				"0001000148"	,	// 001
				//				"0001000149"	,	// 001
				//				"0001000150"	,	// 001
				//				"0001000151"	,	// 001
				//				"0001000152"	,	// 001
				//				"0001000153"	,	// 001
				//				"0001000154"	,	// 001
				//				"0001000155"	,	// 001
				//				"0001000156"	,	// 001
				//				"0001000157"	,	// 001
				//				"0001000158"	,	// 001
				//				"0001000159"	,	// 001
				//				"0001000160"	,	// 001
				//				"0001000161"	,	// 001
				//				"0001000162"	,	// 001
				//				"0001000163"	,	// 001
				//				"0001000164"	,	// 001
				//				"0001000165"	,	// 001
				//				"0001000166"	,	// 001
				//				"0001000167"	,	// 001
				//				"0001000168"	,	// 001
				//				"0001000169"	,	// 001
				//				"0001000170"	,	// 001
				//				"0001000171"	,	// 001
				//				"0001000172"	,	// 001
				//				"0001000173"	,	// 001
				//				"0001000174"	,	// 001
				//				"0001000175"	,	// 001
				//				"0001000176"	,	// 001
				//				"0001000177"	,	// 001
				//				"0001000178"	,	// 001
				//				"0001000179"	,	// 001
				//				"0001000180"	,	// 001
				//				"0001000181"	,	// 001
				//				"0001000182"	,	// 001
				//				"0001000183"	,	// 001
				//				"0001000184"	,	// 001
				//				"0001000185"	,	// 001
				//				"0001000186"	,	// 001
				//				"0001000187"	,	// 001
				//				"0001000188"	,	// 001
				//				"0001000189"	,	// 001
				//				"0001000190"	,	// 001
				//				"0001000191"	,	// 001
				//				"0001000192"	,	// 001
				//				"0001000193"	,	// 001
				//				"0001000194"	,	// 001
				//				"0001000195"	,	// 001
				//				"0001000196"	,	// 001
				//				"0001000197"	,	// 001
				//				"0001000198"	,	// 001
				//				"0001000199"	,	// 001
				//				"0001000200"	,	// 001
				//				"0001000201"	,	// 001
				//				"0001000202"	,	// 001
				//				"0001000203"	,	// 001
				//				"0001000204"	,	// 001
				//				"0001000205"	 	// 001
				//			};

				//		return	lt_No;
				//	}

		//.

		}
}

