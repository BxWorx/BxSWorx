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
					IRfcFncController	lo_M	= new RfcFncController( lo_D );

					Assert.IsNotNull	( lo_D , "" );
					Assert.IsNotNull	( lo_M , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_200_RfcFnc_20_MetaData()
				{
					IRfcDestination		lo_DS		= this.co_NCO000.GetSAPDestConfigured();
					IRfcFncController	lo_FC		= new RfcFncController( lo_DS );
					//...............................................
					lo_FC.RegisterBDCAlt();
					lo_FC.RegisterBDCStd();
					lo_FC.RegisterTblRdr();

					Assert.AreEqual	( 3 , lo_FC.ProfileCount ,	"1" );
					//...............................................
					BDC_Function lo_FN1	= lo_FC.CreateBDCFunctionStd();
					BDC_Function lo_FN2	= lo_FC.CreateBDCFunctionAlt();

					Assert.AreNotEqual	( lo_FN1.MyProfile.Value	, lo_FN2.MyProfile.Value	,	"2" );
					//...............................................
					Assert.AreEqual	(	-1	, lo_FN1.MyProfile.Value._FNCIndex.Value.TabSPA	,	"4" );
					Assert.AreEqual	(	-1	, lo_FN1.MyProfile.Value._CTUIndex.Value.NoBtcE	,	"5" );
					Assert.AreEqual	(	-1	, lo_FN1.MyProfile.Value._SPAIndex.Value.Val		,	"6" );
					Assert.AreEqual	(	-1	, lo_FN1.MyProfile.Value._BDCIndex.Value.Val		,	"7" );
					Assert.AreEqual	(	-1	, lo_FN1.MyProfile.Value._MSGIndex.Value.Fldnm	,	"8" );

				  await	lo_FC.UpdateProfilesAsync().ConfigureAwait(false);

					Assert.AreNotEqual	(	-1	, lo_FN2.MyProfile.Value._CTUIndex.Value.NoBtcE	,	"09" );

					Assert.AreNotEqual	(	-1	, lo_FN1.MyProfile.Value._FNCIndex.Value.TabSPA	,	"10" );
					Assert.AreNotEqual	(	-1	, lo_FN1.MyProfile.Value._SPAIndex.Value.Val		,	"11" );
					Assert.AreNotEqual	(	-1	, lo_FN1.MyProfile.Value._BDCIndex.Value.Val		,	"12" );
					Assert.AreNotEqual	(	-1	, lo_FN1.MyProfile.Value._MSGIndex.Value.Fldnm	,	"13" );
					//...............................................
					Assert.AreNotEqual( lo_FN1.NCORfcFunction		, lo_FN2.NCORfcFunction		,	"16" );
					Assert.IsNotNull	( lo_FN1.NCORfcFunction , "14" );
					Assert.IsNotNull	( lo_FN2.NCORfcFunction , "15" );

					SMC.IRfcStructure x1 = lo_FN2.NCORfcFunction.GetStructure("IS_OPTIONS");
					SMC.IRfcStructure x2 = lo_FN2.NCORfcFunction.GetStructure(lo_FN2.MyProfile.Value._FNCIndex.Value.CTUOpt);

					Assert.AreNotEqual	(	0	, x1.Count	,	"17" );
					Assert.AreNotEqual	(	0	, x2.Count	,	"18" );
					Assert.AreEqual			(	x1.Count	, x2.Count	,	"19" );
			}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public async Task UT_200_RfcFnc_30_MetaDataMany()
				{
					IRfcDestination		lo_DS	= this.co_NCO000.GetSAPDestConfigured();
					IRfcFncController	lo_FC = new RfcFncController( lo_DS );
					//...............................................
					BDC_Function			lo_FN1	=	lo_FC.CreateBDCFunctionAlt	();
					SAPMsg_Function		lo_FN2	=	lo_FC.CreateSAPMsgFunction	();

					await	lo_FC.UpdateProfilesAsync().ConfigureAwait(false);

					Assert.AreNotEqual	(	-1	, lo_FN1.MyProfile.Value._FNCIndex.Value.Skip1	,	"" );
					Assert.AreNotEqual	(	-1	, lo_FN2.MyProfile.Value._FNCIndex.Value.MsgID	,	"" );
				}

		//

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		private class MyRfcFnc : RfcFncBase
			{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MyRfcFnc( BDC_Profile profile ) : base( profile )
					{
					}
			}

		//

		}
}
