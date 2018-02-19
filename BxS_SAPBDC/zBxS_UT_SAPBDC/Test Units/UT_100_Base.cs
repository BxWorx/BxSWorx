using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPBDC.Parser;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_UT_SAPBDC
{
	[TestClass]
	public class UT_100_Base
		{
			private	const string lz_ID1	= "<@@PROGRAM>";

			private	readonly	BDCMain			co_BDCMain		;
			private readonly	BDCParser		co_BDCParser	;

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			public UT_100_Base()
				{
					this.co_BDCMain			= new BDCMain(	this.CreateData					()
																						,	new DTO_BDCHeaderRowRef	()	);

					this.co_BDCParser		= new BDCParser(	() => new DTO_TokenReference()
																							, () => new DTO_BDCColumn			()	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_10_Base()
				{
					var x	= new DTO_TokenReference();
					Assert.IsNotNull( x , "xxxx" );
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_20_ParseForTokens()
				{
					Task t = Task.Run( () => this.co_BDCParser.ParseForTokens( this.co_BDCMain ));
					t.Wait();

					this.co_BDCMain.Tokens.TryGetValue( lz_ID1	, out DTO_TokenReference lo_Token );
					Assert.IsNotNull	(			lo_Token			, ""	);
					Assert.AreNotEqual( 0 , lo_Token.Row	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_30_ParseForColumns()
				{
					Task t = Task.Run( () => this.co_BDCParser.ParseForTokens( this.co_BDCMain ));
					t.Wait();
					this.co_BDCParser.ParseForColumns( this.co_BDCMain );
					Assert.AreNotEqual( 0 , this.co_BDCMain.Columns.Count	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private string[,] CreateData()
				{
					string[,]	lt_Data	= new string[2,2];

					lt_Data[1,1]	= lz_ID1;
					lt_Data[0,1]	= "<@@OKCODE>	";

					return	lt_Data;
				}
		}
}

//		Program Name: 		SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0	SAPMILO0
//"<?xml version=""1.0"" encoding=""utf-16""?>
//<BDCXMLConfig>
//  <GUID>76b37787-47c1-45b2-a9a2-72f548c6191d</GUID>
//  <SessionID>XX1</SessionID>
//  <IsActive>X</IsActive>
//  <SAPTCode>IL02</SAPTCode>
//  <PauseTime>0</PauseTime>
//  <Active_Column>$D$9</Active_Column>
//  <Msg_Column>$B$9</Msg_Column>
//  <CTU_DisMode>N</CTU_DisMode>
//  <CTU_UpdMode>A</CTU_UpdMode>
//  <CTU_DefSize>X</CTU_DefSize>
//  <IsProtected>X</IsProtected>
//  <Password>xSAPtor</Password>
//</BDCXMLConfig>"		Screen Number: 		1110	1110	1110	2100	2100	2100	2100	2100	2100	2100	2100	2100	2100
//		Screen Start: 		X			X			X		X				
//		BDC OK CODE: 		/00			=PA			=T\03		=BU				
//		BDC CURSOR: 		IFLO-TPLNR			IFLO-PLTXT					SCRN_GSOA_FLOC-ZZCSITE				
//		BDC SUBSCREEN:						SAPLITO0                                1052SUB_0102A	SAPLITO0                                1062SUB_0102B	SAPLITO0                                1052SUB_0102A	SAPLITO0                                1062SUB_0102B	SAPLXTOB                                4000CNTRL_FLOC				
//		Field Name: 		IFLO-TPLNR	RILO0-ALKEY	RILO0-TPLKZ	IFLO-PLTXT	ITOB-BUKRS	ITOB-IWERK	ITOB-BUKRS	ITOB-IWERK	SCRN_GSOA_FLOC-ZZCSITE	SCRN_GSOA_FLOC-ZZSLDIS	SCRN_GSOA_FLOC-ZZDIST	SCRN_GSOA_FLOC-ZZTTIME_DAY	SCRN_GSOA_FLOC-ZZTTIME_TIME
//		Description: 														
//Messages		Special Instructions														
//		X		300000000082	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 46	5	KM	22	00:00:00
//		x		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
//		X		300000000083	1	DD000	test	ZA20	ZA10	ZA20	ZA10	TEST STORE 47	5	KM	22	00:00:00
