using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.TableReader;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_310_TableReader
		{
			private const int cn_Recs	= 100;

			private readonly	UT_000_NCO				co_NCO000			;
			private readonly	IRfcDestination		co_RfcDestOn	;
			private	readonly	IRfcFncController co_FCntlr			;
			private	readonly	TblRdr_Function		co_Fnctn			;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_310_TableReader()
				{
					this.co_NCO000			= new	UT_000_NCO()					;
					this.co_RfcDestOn		= this.co_NCO000.GetSAPDestConfigured( true , true );
					this.co_FCntlr			= new RfcFncController( this.co_RfcDestOn );
					this.co_Fnctn				= this.co_FCntlr.CreateTblRdrFunction();
					//...............................................
					Assert.IsNotNull	( this.co_NCO000		, "" );
					Assert.IsNotNull	( this.co_RfcDestOn , "" );
					Assert.IsNotNull	( this.co_FCntlr		, "" );
					Assert.IsNotNull	( this.co_Fnctn			, "" );
					//...............................................
					Task.Run( async ()=> await	this.co_FCntlr.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_310_TableReader_10_Instantiate()
				{
					TblRdr_Data	lo_Data		=	this.co_Fnctn.MyProfile.Value.CreateTblRdrData();

					Assert.IsNotNull	( lo_Data	, "" );
					//...............................................
					this.LoadSetup( lo_Data );

					Assert.AreEqual	( 2, lo_Data.Fields.Count		, "" );
					Assert.AreEqual	( 1, lo_Data.Options.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_310_TableReader_20_GetData()
				{
					TblRdr_Data	lo_Data		=	this.co_Fnctn.MyProfile.Value.CreateTblRdrData();
					//...............................................
					this.LoadSetup( lo_Data );
					this.co_Fnctn.Process( lo_Data , this.co_RfcDestOn.SMCDestination );

					Assert.AreEqual	( cn_Recs	, lo_Data.OutData	.Count , "" );
					Assert.AreEqual	( 2				, lo_Data.Fields	.Count , "" );
				}

		//.

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void LoadSetup( TblRdr_Data	dto	)
					{
						dto.Delimeter		= '|'			;
						dto.QueryTable	= "KNA1"	;
						dto.ReturnRows	= cn_Recs			;
						//...............................................
						dto.LoadField( "MANDT" )	;
						dto.LoadField( "KUNNR" )	;
						//...............................................
						dto.LoadOption( "LAND1 EQ 'NA' " )	;
					}

		}
}
