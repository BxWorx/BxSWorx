using System.Collections.Generic;
using System.Threading.Tasks;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.DDIC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_320_DDICInfo
		{
			private readonly	UT_000_NCO				co_NCO000			;
			private readonly	IRfcDestination		co_RfcDestOn	;
			private	readonly	IRfcFncController co_FCntlr			;
			private	readonly	DDICInfo_Function	co_Fnctn			;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_320_DDICInfo()
				{
					this.co_NCO000			= new	UT_000_NCO()	;
					this.co_RfcDestOn		= this.co_NCO000.GetSAPDestConfigured( true , true )	;
					this.co_FCntlr			= new RfcFncController( this.co_RfcDestOn )						;
					this.co_Fnctn				= this.co_FCntlr.CreateDDICInfoFunction()							;
					//...............................................
					Assert.IsNotNull	( this.co_NCO000		, "" )	;
					Assert.IsNotNull	( this.co_RfcDestOn , "" )	;
					Assert.IsNotNull	( this.co_FCntlr		, "" )	;
					Assert.IsNotNull	( this.co_Fnctn			, "" )	;
					//...............................................
					Task.Run( async ()=> await	this.co_FCntlr.ActivateProfilesAsync().ConfigureAwait(false)).Wait();
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_320_DDICInfo_10_Instantiate()
				{
					DDICInfo_FieldCollection	lo_Data		=	this.co_Fnctn.CreateFieldCollection();

					Assert.IsNotNull	( this.co_Fnctn.MyProfile	, "" );
					Assert.IsNotNull	( lo_Data	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_320_DDICInfo_20_Collection()
				{
					DDICInfo_FieldCollection	lo_Data		=	this.co_Fnctn.CreateFieldCollection();

					lo_Data.AddUpdateText( "KNA1" , "KUNNR" );
					Assert.AreEqual	( 1	, lo_Data.TableCount				, "" );
					Assert.AreEqual	( 1	, lo_Data.TableNames.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_320_DDICInfo_30_GetText()
				{
					DDICInfo_FieldCollection	lo_Data		=	this.co_Fnctn.CreateFieldCollection();

					lo_Data.AddUpdateText( "KNA1"		, "MANDT" );
					lo_Data.AddUpdateText( "KNA1"		, "KUNNR" );
					lo_Data.AddUpdateText( "KNA1 "	, "NAME2" );
					lo_Data.AddUpdateText( "RF02D"	, "KUNNR" );
					lo_Data.AddUpdateText( "RF02D"	, "D0110" );
				//...............................................
					this.co_Fnctn.Process( lo_Data , this.co_RfcDestOn.SMCDestination );
					IList<DTO_DDICInfo_Field> x = lo_Data.GetFields();

					Assert.AreNotEqual( 0 , x.Count , "" )	;
				}
		}
}
