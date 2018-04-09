using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_400_BDCSession
		{
			private readonly	UT_000_NCO					co_NCO000		;
			private readonly	INCO_Controller			co_NCOCntlr	;
			private	readonly	BDC_SessionFactory	co_SessFact	;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_400_BDCSession()
				{
					this.co_NCO000		= new	UT_000_NCO()					;
					this.co_NCOCntlr	= this.co_NCO000._NCO_Cntlr	;
					this.co_SessFact	=	new	BDC_SessionFactory( this.co_NCO000.GetSAPDestConfigured() )	;
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
			public async Task UT_400_BCDSess_30_Process()
				{
					const	int ln_Trn	= 10;

					var CTS			= new CancellationTokenSource();
					var lo_PH		= this.co_SessFact.CreateProgressHandler();
					var lo_Pool	= this.co_SessFact.CreateBDCConsumerPool();

				  BDC_Session			lo_BDCSess	= this.GetConfiguredBDCSession();
					DTO_BDC_Session	lo_SessDTO	=	this.co_SessFact.CreateSessionDTO()	;

					this.LoadBDCData( lo_SessDTO , ln_Trn );

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

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session GetConfiguredBDCSession()
					{
						BDC_Session						lo_BDCSess	= this.GetBDCSession();
						DTO_BDC_SessionConfig	lo_SessCfg	= this.co_SessFact.CreateBDCSessionConfig();

						lo_SessCfg.IsSequential	=	true	;
						lo_SessCfg.ConsumersNo	= 1			;
						lo_SessCfg.ConsumersMax	= 1			;

						lo_BDCSess.ConfigureSession( lo_SessCfg );
						return	lo_BDCSess;
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Session GetBDCSession()
					{
						this.ReadyFactory();
						return	this.co_SessFact.CreateBDCSession();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ReadyFactory()
					{
						Task.Run( ()=>	this.co_SessFact.ReadyEnvironmentAsync()).Wait();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Configure( DTO_BDC_SessionConfig	lo_SessCfg )
					{
						lo_SessCfg.IsSequential	= true;
						lo_SessCfg.ConsumersNo	= 1		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Configure( IConfigDestination	lo_DestCfg )
					{
						//lo_DestCfg.SetSAPGUIasHidden();

						//lo_DestCfg.RepoIdleTimeout	= 10;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBDCData( DTO_BDC_Session dto , int NoOfTrans = 1 )
					{
						dto.Header.SAPTCode	= "XD03";
						dto.Header.Skip1st		= false;

						DTO_BDC_CTU lo_CTU = dto.Header.CTUParms;

						lo_CTU.DisplayMode	= 'N';
						lo_CTU.UpdateMode		= 'A';
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
