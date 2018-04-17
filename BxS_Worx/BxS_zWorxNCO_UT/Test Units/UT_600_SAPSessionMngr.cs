using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.API;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.SAPSession.API;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_600_SAPSessionMngr
		{
			private readonly	UT_000_NCO						co_NCO000		;
			private readonly	INCO_Controller				co_NCOCntlr	;
			private readonly	IRfcDestination				co_RfcDest	;
			private	readonly	ISAP_Session_Manager	co_SAPMngr	;
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_600_SAPSessionMngr()
				{
					this.co_NCO000		= new	UT_000_NCO()						;
					this.co_NCOCntlr	= NCO_Controller.Instance			;
					this.co_RfcDest		= this.co_NCO000.GetSAPDest()	;

					this.co_SAPMngr		= this.co_NCOCntlr.CreateSAPSessionManager( this.co_RfcDest )	;
					
					//...............................................
					Assert.IsNotNull	( this.co_NCO000		, "A" );
					Assert.IsNotNull	( this.co_NCOCntlr	, "B" );
					Assert.IsNotNull	( this.co_RfcDest		, "C" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_SAPSsnMngr_10_Instantiate()
				{
					Assert.IsNotNull(	this.co_SAPMngr	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_500_SAPSsnMngr_20_SAPSsnList()
				{
					Task.Run( async ()=> await this.co_RfcDest.FetchMetadataAsync().ConfigureAwait(false)).Wait();
					var x = this.co_SAPMngr.SAPSessionList();


					Assert.IsNotNull(	this.co_SAPMngr	, "" );
				}

		}
}
