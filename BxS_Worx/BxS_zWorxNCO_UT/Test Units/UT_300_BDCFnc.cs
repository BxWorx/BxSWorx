using System;
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
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_300_BDCFnc
		{
			private readonly	UT_000_NCO				co_NCO000		;
			private readonly	IRfcDestination		co_RfcDestOn;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_300_BDCFnc()
				{
					this.co_NCO000			= new	UT_000_NCO();
					this.co_RfcDestOn		= this.co_NCO000.GetSAPDestConfigured( true , true );
					//...............................................
					Assert.IsNotNull	( this.co_NCO000		, "" );
					Assert.IsNotNull	( this.co_RfcDestOn	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_10_Instantiate()
				{
					IRfcFncController lo_FCnt	= new RfcFncController( this.co_RfcDestOn );
					BDC_Function			lo_Fnc1	= lo_FCnt.CreateBDCFunction();

					Task.Run( async ()=> await lo_FCnt.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
					//...............................................
					BDC_Header	lo_Head		= lo_Fnc1.CreateBDCHeader()	;
					BDC_Data		lo_Lines	= lo_Fnc1.CreateBDCData()	;

					Assert.IsNotNull	( lo_Fnc1		, "" );
					Assert.IsNotNull	( lo_Head		, "" );
					Assert.IsNotNull	( lo_Lines	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_20_Basics()
				{
					IRfcFncController lo_FCnt	= new RfcFncController( this.co_RfcDestOn );
					BDC_Function			lo_Fnc0	= lo_FCnt.CreateBDCFunction();

					Task.Run( async ()=> await lo_FCnt.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
					//...............................................
					BDC_Header	lo_Head		= lo_Fnc0.CreateBDCHeader( true )	;
					BDC_Data		lo_Lines	= lo_Fnc0.CreateBDCData()	;
					//...............................................
					Assert.IsNotNull	( lo_Head.CTUParms	, "" );
					Assert.IsNotNull	( lo_Lines.BDCData	, "" );
					Assert.IsNotNull	( lo_Lines.MSGData	, "" );
					Assert.IsNotNull	( lo_Lines.SPAData	, "" );
					//...............................................
					try	{
								lo_Fnc0.Invoke( this.co_RfcDestOn.SMCDestination );
								Assert.Fail("");
							}
					catch
							{	}
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_30_ProcessDspCall()
				{
					this.BCDCall_Process( false , false , cz_CTU_A );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_32_ProcessDspTrn()
				{
					this.BCDCall_Process( true , false , cz_CTU_A );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_36_ProcessChgCall()
				{
					this.BCDCall_Process( false , true , cz_CTU_A );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_38_ProcessChgTrn()
				{
					this.BCDCall_Process( true , true , cz_CTU_A );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_40_Many()
				{
					IRfcFncController lo_FCnt		= new RfcFncController( this.co_RfcDestOn );
					BDC_Function	lo_Fnc0		= lo_FCnt.CreateBDCFunction();

					Task.Run( async ()=> await lo_FCnt.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
					//...............................................
					BDC_Header	lo_Head		= lo_Fnc0.CreateBDCHeader( true )	;
					BDC_Data				lo_Lines	= lo_Fnc0.CreateBDCData()	;

					lo_Head.SAPTCode	= "XD03";
					lo_Head.CTUParms[ lo_Fnc0.MyProfile.Value._CTUIndex.Value.NoBtcI ].SetValue( cz_False );
					lo_Head.CTUParms[ lo_Fnc0.MyProfile.Value._CTUIndex.Value.DspMde ].SetValue( cz_CTU_N );

					this.LoadBDCData( lo_Lines	, lo_Fnc0.MyProfile.Value );
					lo_Fnc0.Config	( lo_Head );
					//...............................................
								int ln_Cnt	= 00;
					const int ln_No		= 05;

					for (int i = 0; i < ln_No; i++)
						{
							try	{
										lo_Fnc0.Process	( lo_Lines , this.co_RfcDestOn.SMCDestination );
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
					BDC_Profile		lo_Prof		= lo_FCnt.GetAddBDCProfile();

					Task.Run( async ()=> await lo_FCnt.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
					//...............................................
					BDC_Header		lo_Head		= lo_Prof.CreateBDCHeader( true )	;
					//...............................................
					lo_Head.SAPTCode	= "XD03";
					lo_Head.CTUParms[ lo_Prof._CTUIndex.Value.NoBtcI ].SetValue( cz_False );
					lo_Head.CTUParms[ lo_Prof._CTUIndex.Value.DspMde ].SetValue( cz_CTU_N );
					//...............................................
					const int ln_Trn	= 100;
					const	int ln_Tsk	= 05;

					int ln_Tal	= 00;
					var lo_BC		= new BlockingCollection<BDC_Data>( ln_Trn );

					for (int i = 0; i < ln_Trn; i++)
						{
							BDC_Data	lo_Lines	= lo_Prof.CreateBDCData();
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
													BDC_Function	lo_Fnc	= lo_FCnt.CreateBDCFunction();
													lo_Fnc.Config( lo_Head );
													foreach (BDC_Data lo_WorkItem in lo_BC.GetConsumingEnumerable() )
														{
															lo_Fnc.Process( lo_WorkItem	, this.co_RfcDestOn.SMCDestination );
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BCDCall_Process( bool TrnVers = false , bool ChgMode = false , char DispMode = cz_CTU_N )
					{
						IRfcFncController lo_FCnt		= new RfcFncController( this.co_RfcDestOn );
						BDC_Function			lo_Fnc0		= lo_FCnt.CreateBDCFunction( TrnVers );

						Task.Run( async ()=> await lo_FCnt.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
						//...............................................
						BDC_Header	lo_Head		= lo_Fnc0.CreateBDCHeader( true )	;
						BDC_Data		lo_Lines	= lo_Fnc0.CreateBDCData()	;
						//...............................................
						lo_Head.SAPTCode	= ChgMode	? "XD02" : "XD03" ;

						if ( TrnVers )
							{
								lo_Head.DispMode	= DispMode.ToString();
							}
						else
							{
								lo_Head.CTUParms[ lo_Fnc0.MyProfile.Value._CTUIndex.Value.DspMde ].SetValue( DispMode );
							}

						this.LoadBDCData( lo_Lines	, lo_Fnc0.MyProfile.Value , ChgMode );
						//...............................................
						try	{
									lo_Fnc0.Config	( lo_Head );
									lo_Fnc0.Process	( lo_Lines , this.co_RfcDestOn.SMCDestination );

									Assert.IsTrue ( lo_Lines.ProcessedStatus	, "a" );
									Assert.IsTrue ( lo_Lines.SuccesStatus			, "b" );
								}
						catch
								{
									Assert.Fail("NCO Process failed");
								}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBDCData( BDC_Data dtoLines, BDC_Profile bdcProf , bool ChgMode = false )
					{
						BDC_IndexBDC lo_Idx = bdcProf._BDCIndex.Value	;
						int	ln_Rows	= ChgMode ? 5 : 4;

						dtoLines.BDCData.Append(ln_Rows);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 0;

						dtoLines.BDCData.SetValue( lo_Idx.Prg	, "SAPMF02D"		);
						dtoLines.BDCData.SetValue( lo_Idx.Dyn	, "0101"				);
						dtoLines.BDCData.SetValue( lo_Idx.Bgn	, "X"						);
						dtoLines.BDCData.SetValue( lo_Idx.Fld	, "BDC_OKCODE"	);
						dtoLines.BDCData.SetValue( lo_Idx.Val	, "/00"   			);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 1;

						dtoLines.BDCData.SetValue( lo_Idx.Fld	, "RF02D-KUNNR"	);
						dtoLines.BDCData.SetValue( lo_Idx.Val	, "1000000"			);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 2;

						dtoLines.BDCData.SetValue( lo_Idx.Fld	, "RF02D-D0110"	);
						dtoLines.BDCData.SetValue( lo_Idx.Val	, "X"						);
						//.............................................
						dtoLines.BDCData.CurrentIndex	= 3;

						if ( ChgMode )
							{
								dtoLines.BDCData.SetValue( lo_Idx.Prg	, "SAPMF02D"		);
								dtoLines.BDCData.SetValue( lo_Idx.Dyn	, "0110"				);
								dtoLines.BDCData.SetValue( lo_Idx.Bgn	, "X"						);
								dtoLines.BDCData.SetValue( lo_Idx.Fld	, "BDC_OKCODE"	);
								dtoLines.BDCData.SetValue( lo_Idx.Val	, "=UPDA"				);
								//.........................................
								dtoLines.BDCData.CurrentIndex	= 4;

								string x = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszzz");

								dtoLines.BDCData.SetValue( lo_Idx.Fld	, "KNA1-NAME2"	);
								dtoLines.BDCData.SetValue( lo_Idx.Val	, x							);
							}
						else
							{
								dtoLines.BDCData.SetValue( lo_Idx.Prg	, "SAPMF02D"		);
								dtoLines.BDCData.SetValue( lo_Idx.Dyn	, "0110"				);
								dtoLines.BDCData.SetValue( lo_Idx.Bgn	, "X"						);
								dtoLines.BDCData.SetValue( lo_Idx.Fld	, "BDC_OKCODE"	);
								dtoLines.BDCData.SetValue( lo_Idx.Val	, "=PF03"				);
							}
					}

		//.

		}
}
