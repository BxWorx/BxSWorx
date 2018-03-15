using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API.Destination;
using BxS_WorxNCO.RfcFunction.Common;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_200_RfcFnc
		{
			private	const string cz_FNme	= "/ISDFPS/CALL_TRANSACTION";
			private readonly	UT_000_NCO	co_NCO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_200_RfcFnc()
				{
					this.co_NCO			= new	UT_000_NCO();
					//...............................................
					Assert.IsNotNull	( this.co_NCO									, "" );
					Assert.IsNotNull	( this.co_NCO.DestController	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_200_RfcFnc_10_Instantiate()
				{
					IRfcDestination	lo_D	= this.co_NCO.GetSAPDest();
					IRfcFncManager	lo_M	= new RfcFncManager( lo_D );

					Assert.IsNotNull	( lo_D , "" );
					Assert.IsNotNull	( lo_M , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_200_RfcFnc_20_MetaData()
				{
					IRfcDestination	lo_DS	= this.co_NCO.GetSAPDestLoggedOn();
					IRfcFncManager	lo_FM = new RfcFncManager( lo_DS );
					//...............................................
					BDCCall_Profile lo_PR0	= this.CreateBDCCallProfile();
					lo_FM.RegisterProfile	( lo_PR0 );
					//...............................................
					BDCCall_Profile lo_PR1	= lo_FM.GetProfile<BDCCall_Profile>( cz_FNme );
					BDCCall_Profile lo_PR2	= lo_FM.GetProfile<BDCCall_Profile>( cz_FNme );

					Assert.AreEqual	( lo_PR1 , lo_PR2 ,	"" );
					//...............................................
					Assert.AreEqual	(	0	, lo_PR1.ParIdx_TabSPA	,	"" );
					Assert.AreEqual	(	0	, lo_PR1.CTUOpt_NoBtcE	,	"" );
					Assert.AreEqual	(	0	, lo_PR1.SPADat_Val			,	"" );
					Assert.AreEqual	(	0	, lo_PR1.BDCDat_Val			,	"" );
					Assert.AreEqual	(	0	, lo_PR1.TabMsg_Fldnm		,	"" );

					Assert.IsTrue		(	lo_FM.UpdateProfiles()	, "" );

					Assert.AreNotEqual	(	0	, lo_PR1.ParIdx_TabSPA	,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.CTUOpt_NoBtcE	,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.SPADat_Val			,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.BDCDat_Val			,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.TabMsg_Fldnm		,	"" );
					//...............................................
					var lo_FN1	= new MyRfcFnc( lo_PR1 );
					var lo_FN2	= new MyRfcFnc( lo_PR2 );

					//var x = lo_FN1.GetNCORfcFunction();

					Assert.IsNotNull	( lo_FN1.NCORfcFunction , "" );
					Assert.IsNotNull	( lo_FN2.NCORfcFunction , "" );

					Assert.AreNotEqual	( lo_FN1.NCORfcFunction	, lo_FN2.NCORfcFunction	,	"" );

					SMC.IRfcStructure x1 = lo_FN1.NCORfcFunction.GetStructure("IS_OPTIONS");
					SMC.IRfcStructure x2 = lo_FN1.NCORfcFunction.GetStructure(lo_PR2.ParIdx_CTUOpt);
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

		//

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		private class MyRfcFnc : RfcFncBase
			{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MyRfcFnc( BDCCall_Profile profile ) : base( profile )
					{
					}
			}

		//

		}
}
