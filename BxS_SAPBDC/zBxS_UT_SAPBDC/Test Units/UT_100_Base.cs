using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
//.........................................................
using					BxS_SAPBDC.Parser;
using static	BxS_SAPBDC.Parser.BDC_Constants;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_UT_SAPBDC
{
	[TestClass]
	public class UT_100_Base
		{
			private	readonly	BDCMain							co_BDCMain		;
			private readonly	Parser_BDCTokens		co_Tokens	;
			private readonly	Parser_BDCColumns		co_Column	;

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			public UT_100_Base()
				{
					this.co_BDCMain	= new BDCMain						(		this.CreateData					()
																										,	new DTO_BDCHeaderRowRef	()	);

					this.co_Tokens	= new Parser_BDCTokens	(		this.co_BDCMain
																										, () => new DTO_TokenReference() );

					this.co_Column	= new Parser_BDCColumns	(		this.co_BDCMain
																										, () => new DTO_BDCColumn() );
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_00_Regex()
				{
					const	string	z1	= "asdasda(1)";

					var			r1	= new Regex(@"\((.*?)\)");
					bool		q1  = r1.IsMatch(z1);
					string	a1	= r1.Replace(z1, "<@@>");

					string y = string.Empty;
					string x	= $"{cz_Cmd_Prefix}{cz_Token_Prog};{cz_Token_Crsr};<OKcode>";

					bool by = Regex.IsMatch(y, cz_Cmd_Prefix, RegexOptions.IgnoreCase);

					bool b1 = Regex.IsMatch(x, cz_Cmd_Prefix, RegexOptions.IgnoreCase);
					bool b2 = Regex.IsMatch(x, cz_Token_Crsr, RegexOptions.IgnoreCase);
					bool b3 = Regex.IsMatch(x, cz_Token_OKCd, RegexOptions.IgnoreCase);
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
					Task t = Task.Run( () => this.co_Tokens.ParseForTokens());
					t.Wait();

					this.co_BDCMain.Tokens.TryGetValue( cz_Token_Prog	, out DTO_TokenReference lo_Token );

					Assert.IsNotNull	(			lo_Token			, ""	);
					Assert.AreNotEqual( 0 , lo_Token.Row	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_30_ParseForColumns()
				{
					Task t = Task.Run( () => this.co_Tokens.ParseForTokens());
					t.Wait();
					this.co_Column.ParseForColumns();
					Assert.AreNotEqual( 0 , this.co_BDCMain.Columns.Count	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private string[,] CreateData()
				{
					string[,]	lt_Data	= new string[2,2];

					lt_Data[1,1]	= cz_Cmd_Prefix + cz_Token_Prog;
					lt_Data[0,1]	= cz_Cmd_Prefix + cz_Token_OKCd;
					lt_Data[0,0]	= cz_Cmd_Prefix + "<Headerend>[9];<Execute>[D]";
					lt_Data[1,0]	= cz_Cmd_Prefix + "<messages>[3]";

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
