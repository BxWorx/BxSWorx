using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.SAPMsg;
using BxS_WorxNCO.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_200_RfcFnc
		{
			private	const string cz_FNme	= "/ISDFPS/CALL_TRANSACTION";

			private readonly	UT_000_NCO				co_NCO000		;
			private readonly	INCO_Controller		co_NCOCntlr	;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_200_RfcFnc()
				{
					this.co_NCO000		= new	UT_000_NCO()					;
					this.co_NCOCntlr	= this.co_NCO000._NCO_Cntlr	;
					//...............................................
					Assert.IsNotNull	( this.co_NCO000 , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_200_RfcFnc_10_Instantiate()
				{
					IRfcDestination	lo_D	= this.co_NCO000.GetSAPDest();
					IRfcFncManager	lo_M	= new RfcFncManager( lo_D );

					Assert.IsNotNull	( lo_D , "" );
					Assert.IsNotNull	( lo_M , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_200_RfcFnc_20_MetaData()
				{
					IRfcDestination	lo_DS	= this.co_NCO000.GetSAPDestConfigured();
					IRfcFncManager	lo_FM = new RfcFncManager( lo_DS );
					//...............................................
					BDCCall_Profile lo_PR0	= this.CreateBDCCallProfile();
					lo_FM.RegisterProfile	( lo_PR0 );
					//...............................................
					BDCCall_Profile lo_PR1	= lo_FM.GetProfile<BDCCall_Profile>( cz_FNme );
					BDCCall_Profile lo_PR2	= lo_FM.GetProfile<BDCCall_Profile>( cz_FNme );

					Assert.AreEqual	( lo_PR0 , lo_PR1 ,	"" );
					Assert.AreEqual	( lo_PR1 , lo_PR2 ,	"" );
					Assert.AreEqual	( lo_PR2 , lo_PR0 ,	"" );
					//...............................................
					Assert.AreEqual	(	0	, lo_PR1.FNCIndex.TabSPA	,	"" );
					Assert.AreEqual	(	0	, lo_PR1.CTUIndex.NoBtcE	,	"" );
					Assert.AreEqual	(	0	, lo_PR1.SPAIndex.Val			,	"" );
					Assert.AreEqual	(	0	, lo_PR1.BDCIndex.Val			,	"" );
					Assert.AreEqual	(	0	, lo_PR1.MSGIndex.Fldnm		,	"" );

				  await	lo_FM.UpdateProfilesAsync().ConfigureAwait(false);

					Assert.AreNotEqual	(	0	, lo_PR1.FNCIndex.TabSPA	,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.CTUIndex.NoBtcE	,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.SPAIndex.Val			,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.BDCIndex.Val			,	"" );
					Assert.AreNotEqual	(	0	, lo_PR1.MSGIndex.Fldnm		,	"" );
					//...............................................
					var lo_FN1	= new MyRfcFnc( lo_PR1 );
					var lo_FN2	= new MyRfcFnc( lo_PR2 );

					Assert.IsNotNull	( lo_FN1.NCORfcFunction , "" );
					Assert.IsNotNull	( lo_FN2.NCORfcFunction , "" );

					Assert.AreNotEqual	( lo_FN1.NCORfcFunction	, lo_FN2.NCORfcFunction	,	"" );

					SMC.IRfcStructure x1 = lo_FN1.NCORfcFunction.GetStructure("IS_OPTIONS");
					SMC.IRfcStructure x2 = lo_FN1.NCORfcFunction.GetStructure(lo_PR2.FNCIndex.CTUOpt);

					Assert.AreNotEqual	(	0	, x1.Count	,	"" );
					Assert.AreNotEqual	(	0	, x2.Count	,	"" );
			}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_200_RfcFnc_30_MetaDataMany()
				{
					IRfcDestination		lo_DS	= this.co_NCO000.GetSAPDestConfigured();
					IRfcFncController	lo_FC = new RfcFncController( lo_DS );

					lo_FC.RegisterBDCCallProfile();
					lo_FC.RegisterSAPMsgProfile	();
					await	lo_FC.ActivateProfilesAsync().ConfigureAwait(false);
					//...............................................
					BDCCall_Function	lo_FN1	=	lo_FC.CreateBDCCallFunction	();
					SAPMsg_Function		lo_FN2	=	lo_FC.CreateSAPMsgFunction	();

					Assert.AreNotEqual	(	0	, lo_FN1.MyProfile.Value.FNCIndex.Skip1	,	"" );
					Assert.AreNotEqual	(	0	, lo_FN2.MyProfile.Value.FNCIndex.MsgID	,	"" );
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCCall_Profile CreateBDCCallProfile()
					{
						IRfcDestination	lo_DS	= this.co_NCO000.GetSAPDestConfigured();
						BDCCall_Factory lo_FC	= BDCCall_Factory.Instance;

						return	new BDCCall_Profile( cz_FNme , lo_FC );
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
