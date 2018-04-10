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

					this.LoadBDCData( lo_SessDTO , ln_Trn , 'A' );

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
				private void LoadBDCData( DTO_BDC_Session dto , int NoOfTrans = 1, char DispMode = 'N' )
					{
						dto.Header.SAPTCode		= "XD03"	;
						dto.Header.Skip1st		= false		;

						DTO_BDC_CTU lo_CTU	= dto.Header.CTUParms	;

						lo_CTU.DisplayMode	= DispMode	;
						lo_CTU.UpdateMode		= 'A'				;
						//.............................................
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
								lo_Trn.AddBDCData( field: "RF02D-KUNNR" , value: "1000000"	);
								lo_Trn.AddBDCData( field: "RF02D-D0110" , value: "X"				);

								lo_Trn.AddBDCData( "SAPMF02D" ,  110 , true , "BDC_OKCODE" , "=PF03" );
								//.............................................
								dto.Trans.Enqueue( lo_Trn);
							}
					}
		}
}
