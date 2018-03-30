using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.Destination.API;

using static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDCCall_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_300_BDCCall
		{
			private readonly	UT_000_NCO			co_NCO;
			private readonly	IRfcDestination	co_RfcDest;
			private readonly	IRfcDestination	co_RfcDestOn;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_300_BDCCall()
				{
					this.co_NCO					= new	UT_000_NCO();

					this.co_RfcDest			= this.co_NCO.GetSAPDest();
					this.co_RfcDestOn		= this.co_NCO.GetSAPDestLoggedOn( true );
					//...............................................
					Assert.IsNotNull	( this.co_NCO									, "" );
					Assert.IsNotNull	( this.co_NCO.DestController	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_10_Instantiate()
				{
					BDCCall_Profile lo_Prof	= this.CreateBDCCallProfile();
					var							lo_Fnc0	= new BDCCall_Function( lo_Prof );

					BDCCall_Header	lo_Head		= lo_Fnc0.CreateBDCCallHeader()	;
					BDCCall_Data		lo_Lines	= lo_Fnc0.CreateBDCCallLines()	;

					Assert.IsNotNull	( lo_Fnc0		, "" );
					Assert.IsNotNull	( lo_Head		, "" );
					Assert.IsNotNull	( lo_Lines	, "" );

					IRfcFncController lo_FCnt	= new RfcFncController( this.co_RfcDestOn );
					BDCCall_Function	lo_Fnc1	= lo_FCnt.CreateBDCCallFunction();

					Assert.IsNotNull	( lo_Fnc1 , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_20_Basics()
				{
					IRfcFncController lo_FCnt	= new RfcFncController( this.co_RfcDestOn );

					BDCCall_Function	lo_Fnc0	= lo_FCnt.CreateBDCCallFunction();
					//...............................................
					BDCCall_Header	lo_Head	= lo_Fnc0.CreateBDCCallHeader( true )	;
					Assert.IsNotNull	( lo_Head.CTUParms	, "" );
					//...............................................
					BDCCall_Data		lo_Lines	= lo_Fnc0.CreateBDCCallLines()	;
					Assert.IsNotNull	( lo_Lines.BDCData	, "" );
					Assert.IsNotNull	( lo_Lines.MSGData	, "" );
					Assert.IsNotNull	( lo_Lines.SPAData	, "" );
					//...............................................
					try	{
								lo_Fnc0.Invoke( this.co_RfcDestOn.NCODestination );
								Assert.Fail("");
							}
					catch
							{	}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_30_Process()
				{
					IRfcFncController lo_FCnt		= new RfcFncController( this.co_RfcDestOn );

					BDCCall_Function	lo_Fnc0		= lo_FCnt.CreateBDCCallFunction();
					BDCCall_Profile		lo_Prof		= lo_FCnt.GetAddBDCCallProfile();
					BDCCall_Header		lo_Head		= lo_Prof.CreateBDCCallHeader( true )	;
					BDCCall_Data			lo_Lines	= lo_Prof.CreateBDCCallLines()	;
					//...............................................
					this.co_RfcDestOn.ShowSAPGui	= true;
					//...............................................
					lo_Head.SAPTCode		= "XD03";
					lo_Head.CTUParms[ lo_Fnc0.CTUIndex.CTUOpt_DspMde ].SetValue( cz_CTU_A );

					this.LoadBDCData( lo_Lines	, lo_Fnc0.MyProfile.Value );
					//...............................................
					try	{
								lo_Fnc0.Config	( lo_Head );
								lo_Fnc0.Process	( lo_Lines , this.co_RfcDestOn.NCODestination );

								Assert.IsTrue ( lo_Lines.ProcessedStatus	, "a" );
								Assert.IsTrue ( lo_Lines.SuccesStatus			, "b" );
							}
					catch
							{
								Assert.Fail("NCO Process failed");
							}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_40_Many()
				{
					IRfcFncController lo_FCnt		= new RfcFncController( this.co_RfcDestOn );

					BDCCall_Function	lo_Fnc0		= lo_FCnt.CreateBDCCallFunction();
					BDCCall_Header		lo_Head		= lo_Fnc0.CreateBDCCallHeader( true )	;
					BDCCall_Data			lo_Lines	= lo_Fnc0.CreateBDCCallLines()	;

					lo_Head.SAPTCode	= "XD03";
					lo_Head.CTUParms[ lo_Fnc0.CTUIndex.CTUOpt_NoBtcI ].SetValue( cz_False );
					lo_Head.CTUParms[ lo_Fnc0.CTUIndex.CTUOpt_DspMde ].SetValue( cz_CTU_N );

					this.LoadBDCData( lo_Lines	, lo_Fnc0.MyProfile.Value );
					lo_Fnc0.Config	( lo_Head );
					//...............................................
								int ln_Cnt	= 00;
					const int ln_No		= 05;

					for (int i = 0; i < ln_No; i++)
						{
							try	{
										lo_Fnc0.Process	( lo_Lines , this.co_RfcDestOn.NCODestination );
										if (lo_Lines.SuccesStatus) ln_Cnt ++;
									}
							catch
									{	}
						}
					Assert.AreEqual	( ln_No	, ln_Cnt	, "a" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_50_Multiple()
				{
					IRfcFncController lo_FCnt		= new RfcFncController( this.co_RfcDestOn );
					BDCCall_Profile		lo_Prof		= lo_FCnt.GetAddBDCCallProfile();
					BDCCall_Header		lo_Head		= lo_Prof.CreateBDCCallHeader( true )	;
					//...............................................
					lo_Head.SAPTCode	= "XD03";
					lo_Head.CTUParms[ lo_Prof.CTUIndex.CTUOpt_NoBtcI ].SetValue( cz_False );
					lo_Head.CTUParms[ lo_Prof.CTUIndex.CTUOpt_DspMde ].SetValue( cz_CTU_N );
					//...............................................
					const int ln_Trn	= 100;
					const	int ln_Tsk	= 05;

					int ln_Tal	= 00;
					var lo_BC		= new BlockingCollection<BDCCall_Data>( ln_Trn );

					for (int i = 0; i < ln_Trn; i++)
						{
							BDCCall_Data	lo_Lines	= lo_Prof.CreateBDCCallLines();
							this.LoadBDCData( lo_Lines	, lo_Prof );
							lo_BC.Add( lo_Lines );
						}

					lo_BC.CompleteAdding();
					//...............................................
					var myTasks	= new Task[ln_Tsk];

					for (int i = 0; i < ln_Tsk; i++)
						{
							myTasks[i]	= Task.Factory.StartNew
								(	()=>	{
													BDCCall_Function	lo_Fnc	= lo_FCnt.CreateBDCCallFunction();
													lo_Fnc.Config( lo_Head );
													foreach (BDCCall_Data lo_WorkItem in lo_BC.GetConsumingEnumerable() )
														{
															lo_Fnc.Process( lo_WorkItem	, this.co_RfcDestOn.NCODestination );
															if ( lo_WorkItem.SuccesStatus )	Interlocked.Increment( ref ln_Tal );
														}
												}
								);
						}

					Task.WaitAll( myTasks );

					Assert.AreEqual	( ln_Trn	, ln_Tal	, "a" );
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBDCData( BDCCall_Data dtoLines, BDCCall_Profile bdcProf )
					{
						dtoLines.BDCData.Append(4);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 0;

						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Prg	, "SAPMF02D"		);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Dyn	, "0101"				);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Bgn	, "X"						);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Fld	, "BDC_OKCODE"	);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Val	, "/00"   			);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 1;

						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Fld	, "RF02D-KUNNR"	);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Val	, "1000000"			);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 2;

						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Fld	, "RF02D-D0110"	);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Val	, "X"						);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 3;

						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Prg	, "SAPMF02D"		);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Dyn	, "0110"				);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Bgn	, "X"						);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Fld	, "BDC_OKCODE"	);
						dtoLines.BDCData.SetValue( bdcProf.BDCIndex.BDCDat_Val	, "=PF03"				);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCCall_Profile CreateBDCCallProfile()
					{
						IRfcDestination		lo_DS			= this.co_NCO.GetSAPDestLoggedOn();
						IRfcFncController	lo_FCntlr	= new RfcFncController( lo_DS );

						return	lo_FCntlr.GetAddBDCCallProfile();
					}
		}
}
