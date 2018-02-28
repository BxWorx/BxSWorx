using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
//.........................................................
using static	BxS_SAPBDC.BDC.BDC_Constants;
using					BxS_SAPBDC.Main;
using					BxS_SAPBDC.Parser;
using					BxS_SAPIPX.Main;
using					BxS_SAPIPX.Excel;
using					BxS_SAPBDC.BDC;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_UT_SAPBDC
{
	[TestClass]
	public class UT_100_Base
		{
			private	readonly	IIPX_Controller		co_IPX;
			private readonly	IBDC_Controller		co_Cntlr;
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			public UT_100_Base()
				{
					this.co_IPX			= IPX_Controller.Instance;
					this.co_Cntlr		= new BDC_Controller();
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_00_Regex()
				{
					const string	t0	= "<Execute>";
					const string	t7	= "<Col>";
					const string	t8	= "<datastartCol>";
					const string	t9	= ";";
					const	string	t1	= "<ExEcute>[d];<datastartCol>[F]";

					Match m = Regex.Match(t1, $"{t0}(.*){t9}?",RegexOptions.IgnoreCase);
					Match n = Regex.Match(t1, $"{t8}(.*){t9}?",RegexOptions.IgnoreCase);
					Match o = Regex.Match(t1, $"{t7}(.*){t9}?",RegexOptions.IgnoreCase);

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
					DTO_ParserToken x	= BDC_Parser_Factory.Instance.CreateDTOToken();
					Assert.IsNotNull( x , "xxxx" );

					DTO_ParserXMLConfig y = BDC_Parser_Factory.Instance.CreateDTOXMLCfg();
					Assert.IsNotNull( y , "xxxx" );

					DTO_ParserProfile z	= BDC_Parser_Factory.Instance.CreateDTOProfile();
					Assert.IsNotNull( z , "xxxx" );

					BDC_Parser c = this.co_Cntlr.CreateBDCParser();
					Assert.IsNotNull( c , "xxxx" );
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_20_ParseForTokens()
				{
					DTO_ParserProfile						lo_Session	= BDC_Parser_Factory.Instance.CreateDTOProfile();
					Lazy< BDC_Parser_Tokens > lo_Token		= BDC_Parser_Factory.Instance.GetTokenParser();
					//...............................................
					DTO_BDCSessionRequest	lo_Req0		= this.co_IPX.CreateBDCSessionRequest();
					lo_Token.Value.Process( lo_Req0 , lo_Session );
					//...............................................
					DTO_BDCSessionRequest	lo_Req			= this.CreateRequest();
					lo_Token.Value.Process( lo_Req , lo_Session );

					Assert.IsNotNull(			lo_Session.XMLConfig		, ""	);
					Assert.AreEqual	(10	,	lo_Session.RowDataStart	, ""	);
					Assert.AreEqual	( 3	,	lo_Session.ColMsgs			, ""	);
					Assert.AreEqual	( 0	,	lo_Session.ColDataStart	, ""	);
					Assert.AreEqual	(	4	,	lo_Session.ColExec			, ""	);

					DTO_ParserProfile				lo_Session1	= BDC_Parser_Factory.Instance.CreateDTOProfile();
					DTO_BDCSessionRequest	lo_Req1			= this.CreateRequest();

					BDC_Parser_Tokens	lo_Token1 = BDC_Parser_Factory.Instance.GetTokenParser().Value;
					lo_Token1.Process( lo_Req1	, lo_Session1 );

					Assert.IsNotNull(			lo_Session1.XMLConfig			, ""	);
					Assert.AreEqual	(10	,	lo_Session1.RowDataStart	, ""	);
					Assert.AreEqual	( 3	,	lo_Session1.ColMsgs				, ""	);
					Assert.AreEqual	( 0	,	lo_Session1.ColDataStart	, ""	);
					Assert.AreEqual	(	4	,	lo_Session1.ColExec				, ""	);

				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_30_ParseForColumns()
				{
					DTO_ParserProfile				lo_Session	= BDC_Parser_Factory.Instance.CreateDTOProfile();
					DTO_BDCSessionRequest	lo_Req			= this.CreateRequest();

					BDC_Parser_Tokens		lo_Token	= BDC_Parser_Factory.Instance.GetTokenParser().Value;
					BDC_Parser_Columns	lo_Cols		= BDC_Parser_Factory.Instance.GetColumnParser().Value;

					lo_Token.Process( lo_Req , lo_Session );
					lo_Cols.Process	( lo_Req , lo_Session );

					Assert.AreNotEqual	(0	,	lo_Session.Columns.Count	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_40_Parser()
				{
					DTO_BDCSessionRequest lo_BDC = GetRequestFromFile();
					//...............................................
					BDC_Parser	lo_Psr	= this.co_Cntlr.CreateBDCParser();
					BDC_Session	lo_Ssn	= lo_Psr.Process( lo_BDC );

					Assert.AreEqual( 5 , lo_Ssn.TransactionCount	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private DTO_BDCSessionRequest GetRequestFromFile()
				{
					String	lc_FleNme	= $@"C:\ProgramData\BxS_Worx\xx.xml";
					string	lc_XML		= this.co_IPX.ReadFile( lc_FleNme );

					DTO_BDCSessionRequest	lo_BDC	=	this.co_IPX.DeSerialize<DTO_BDCSessionRequest>( lc_XML );
					this.co_IPX.Parse1Dto2D( lo_BDC );
					return	lo_BDC;
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private DTO_ParserXMLConfig CreateXMLConfig()
				{
					return	new DTO_ParserXMLConfig
						{
								GUID				= "76b37787-47c1-45b2-a9a2-72f548c6191d"
							,	SAPTCode		= "XD02"
							,	PauseTime		= "1"
							, CTU_UpdMode	= "A"
							, CTU_DefSize	= "X"
							, CTU_DisMode	= "A"
						};
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private DTO_BDCSessionRequest CreateRequest()
				{
					DTO_BDCSessionRequest lo_Req	= this.co_IPX.CreateBDCSessionRequest();
					lo_Req.WSData	= this.CreateData();
					return	lo_Req;
				}


			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private string[,] CreateData()
				{
					string XMLConfig	= BDC_Parser_Factory.Instance.IPXController.Serialize( this.CreateXMLConfig() );

					string[,]	lt_Data	= new string[10,10];

					lt_Data[0,0]	= cz_Cmd_Prefix + "<DATASTARTCOL>[0];<Execute>[D]";
					lt_Data[0,1]	= cz_Cmd_Prefix + cz_Token_OKCd[4];
					lt_Data[1,0]	= cz_Cmd_Prefix + "<messages>[3]";
					lt_Data[1,1]	= cz_Cmd_Prefix + cz_Token_Prog;
					lt_Data[2,0]	= cz_Cmd_Prefix + XMLConfig;

					return	lt_Data;
				}
		}
}
