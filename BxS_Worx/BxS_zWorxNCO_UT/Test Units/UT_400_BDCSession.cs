using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.BDCSession.API;
using BxS_WorxNCO.Destination.API;
using BxS_WorxIPX.API.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_400_BDCSession
		{
			private readonly	IBDCSessionController	co_Ctlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_400_BDCSession()
				{
					this.co_Ctlr	= new BDCSessionController();
					//...............................................
					Assert.IsNotNull( this.co_Ctlr , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_10_Instantiate()
				{
					IConfigSetupDestination	lo_DestCfg	= this.co_Ctlr.CreateDestinationConfig();
					DTO_BDC_SessionConfig		lo_SessCfg	= this.co_Ctlr.CreateSessionConfig();
					IBDCSession							lo_BDCSess	= this.co_Ctlr.CreateBDCSession( this.GetSAPID() );

					Assert.IsNotNull	( lo_DestCfg , "bbb" );
					Assert.IsNotNull	( lo_SessCfg , "bbb" );
					Assert.IsNotNull	( lo_BDCSess , "bbb" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_400_BCDSess_20_Configure()
				{
					IConfigSetupDestination	lo_DestCfg	= this.co_Ctlr.CreateDestinationConfig();
					DTO_BDC_SessionConfig		lo_SessCfg	= this.co_Ctlr.CreateSessionConfig();
					IBDCSession							lo_BDCSess	= this.co_Ctlr.CreateBDCSession( this.GetSAPID() );
					//...............................................
					this.Configure( lo_DestCfg );

					lo_BDCSess.ConfigureOperation( lo_SessCfg );
					lo_BDCSess.ConfigureDestination( lo_DestCfg );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_400_BCDSess_30_Process()
				{
					IConfigSetupDestination	lo_DestCfg	= this.co_Ctlr.CreateDestinationConfig();
					DTO_BDC_SessionConfig		lo_SessCfg	= this.co_Ctlr.CreateSessionConfig();
					IBDCSession							lo_BDCSess	= this.co_Ctlr.CreateBDCSession( this.GetSAPID() );
					//...............................................
					this.Configure( lo_DestCfg );
					this.Configure( lo_SessCfg );

					lo_BDCSess.ConfigureDestination	( lo_DestCfg );
					lo_BDCSess.ConfigureOperation		( lo_SessCfg );
					//...............................................
					DTO_BDC_Session lo_SessDTO = this.co_Ctlr.CreateSessionDTO();
					this.LoadBDCData( lo_SessDTO , 50 );

					int ln_ConCnt = await lo_BDCSess.Process_SessionAsync( lo_SessDTO ).ConfigureAwait(false);

					while (!ln_ConCnt.Equals(lo_SessDTO.Transactions.Count))
						{
							Thread.Sleep(10);
						}
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Configure( DTO_BDC_SessionConfig	lo_SessCfg )
					{
						lo_SessCfg.ConsumersNo	= 4;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Configure( IConfigSetupDestination	lo_DestCfg )
					{
						lo_DestCfg.Client			= 700						;
						lo_DestCfg.User				= "DERRICKBINGH"	;
						lo_DestCfg.Password		= "M@@n4321"			;

						lo_DestCfg.SetSAPGUIasUsed();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string GetSAPID()
					{
						IList< string > lt_Ini	=	this.co_Ctlr.GetSAPINIList();
						string					lc_ID		= lt_Ini.FirstOrDefault( s => s.Contains("PWD)") );

						Assert.IsNotNull	( lc_ID	, "" );
						return	lc_ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBDCData( DTO_BDC_Session dto , int NoOfTrans = 1 )
					{
						dto.SessionHeader.SAPTCode	= "XD03";
						dto.SessionHeader.Skip1st		= false;

						DTO_BDC_CTU lo_CTU = dto.SessionHeader.CTUParms;

						lo_CTU.DisplayMode	= 'N';
						lo_CTU.UpdateMode		= 'A';
						//.............................................
						for (int i = 0; i < NoOfTrans; i++)
							{
								DTO_BDC_Trans lo_Trn	= dto.CreateTransDTO(i+1);
								DTO_BDC_Data	lo_D1		=	lo_Trn.CreateDataDTO();

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
								dto.Transactions.TryAdd( lo_Trn.TranNo , lo_Trn);
							}
					}
		}
}
