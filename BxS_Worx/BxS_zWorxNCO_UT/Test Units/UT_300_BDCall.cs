using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.Common;
using BxS_WorxNCO.Destination.API.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_300_BDCall
		{
			private	const			string			cz_FNme	= "/ISDFPS/CALL_TRANSACTION";
			private readonly	UT_000_NCO	co_NCO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_300_BDCall()
				{
					this.co_NCO			= new	UT_000_NCO();
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
					BDCCall_Lines		lo_Lines	= lo_Fnc0.CreateBDCCallLines()	;

					Assert.IsNotNull	( lo_Fnc0		, "" );
					Assert.IsNotNull	( lo_Head		, "" );
					Assert.IsNotNull	( lo_Lines	, "" );

					IRfcFncController lo_FCnt	= new RfcFncController( this.co_NCO.GetSAPDest() );
					BDCCall_Function	lo_Fnc1	= lo_FCnt.CreateBDCCallFunction();

					Assert.IsNotNull	( lo_Fnc1 , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_300_BCDCall_20_Basics()
				{
					IRfcFncController lo_FCnt	= new RfcFncController( this.co_NCO.GetSAPDestLoggedOn( true ) );

					BDCCall_Function	lo_Fnc0	= lo_FCnt.CreateBDCCallFunction();
					BDCCall_Function	lo_Fnc1	= lo_FCnt.CreateBDCCallFunction();

					try	{
								lo_Fnc0.Invoke();
								Assert.Fail("");
							}
					catch	{	}

					BDCCall_Header	lo_Head		= lo_Fnc0.CreateBDCCallHeader()	;
					Assert.IsNotNull	( lo_Head.CTUParms	, "" );

					BDCCall_Lines		lo_Lines	= lo_Fnc0.CreateBDCCallLines()	;
					Assert.IsNotNull	( lo_Lines.BDCData	, "" );
					Assert.IsNotNull	( lo_Lines.MSGData	, "" );
					Assert.IsNotNull	( lo_Lines.SPAData	, "" );

					lo_Head.SAPTCode	= "XD03";
					lo_Head.Skip1st		= false;
					lo_Head.CTUParms[ lo_Fnc0.MyProfile.Value.CTUOpt_DspMde ].SetValue( "N" );
					lo_Head.CTUParms[ lo_Fnc0.MyProfile.Value.CTUOpt_DefSze ].SetValue( "X" );

					lo_Fnc0.Config( lo_Head );
					try	{
								lo_Fnc0.Process( lo_Lines );
							}
					catch
							{
								Assert.Fail("NCO Process failed");
							}
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCCall_Profile CreateBDCCallProfile()
					{
						IRfcDestination	lo_DS	= this.co_NCO.GetSAPDestLoggedOn();

						return	new BDCCall_Profile(	cz_FNme
																				, lo_DS
																				, ()=>	new BDCCall_Header()
																				, ()=>	new BDCCall_Lines	() );
					}
		}
}
