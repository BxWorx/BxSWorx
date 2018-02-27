using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
//.........................................................
using					BxS_SAPBDC.Main;
using					BxS_SAPBDC.Parser;
using static	BxS_SAPBDC.BDC.BDC_Constants;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_UT_SAPBDC
{
	[TestClass]
	public class UT_100_Base
		{
			private readonly	IBDC_Controller			co_Cntlr;
			private	readonly	BDC_Processor_Cfg		co_ProcCfg			;
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			public UT_100_Base()
				{
					this.co_Cntlr		= new BDC_Controller();
					this.co_ProcCfg	= BDC_Processor_Cfg.Instance;
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
					DTO_TokenReference x	= this.co_ProcCfg.CreateDTOToken();
					Assert.IsNotNull( x , "xxxx" );

					DTO_BDCXMLConfig y = this.co_ProcCfg.CreateDTOXMLCfg();
					Assert.IsNotNull( y , "xxxx" );

					DTO_BDCSession z	= this.co_ProcCfg.CreateDTOSession();
					Assert.IsNotNull( z , "xxxx" );
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_20_ParseForTokens()
				{
					DTO_BDCSession	lo_Session	= this.co_ProcCfg.CreateDTOSession();
					string[,]				lt_Data			= this.CreateData();

					Task t = Task.Run( () => this.co_ProcCfg.TokenProcessor.Process( lo_Session , lt_Data ));
					t.Wait();

					Assert.IsNotNull(			lo_Session.XMLConfig		, ""	);
					Assert.AreEqual	(10	,	lo_Session.RowDataStart	, ""	);
					Assert.AreEqual	( 3	,	lo_Session.ColDataMsgs	, ""	);
					Assert.AreEqual	( 9	,	lo_Session.ColDataStart	, ""	);
					Assert.AreEqual	(	4	,	lo_Session.ColDataExec	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_30_ParseForColumns()
				{
					DTO_BDCSession	lo_Session	= this.co_ProcCfg.CreateDTOSession();
					string[,]				lt_Data			= this.CreateData();

					Task t = Task.Run( () => this.co_ProcCfg.ColumnProcessor.Process( lo_Session , lt_Data ));
					t.Wait();

					//bool lb = this.co_Proc_Columns.Process( lo_Session , lt_Data );

					//Task t = Task.Run( () => this.co_Tokens.ParseForTokens());
					//t.Wait();
					//this.co_Column.ParseForColumns();
					//Assert.AreNotEqual( 0 , this.co_BDCMain.Columns.Count	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_40_ParseXML()
				{
					//DTO_ExcelWorksheet lo_WS	= this.GetWSDTO();

					//Task t = Task.Run( () => this.co_Proc.Process( lo_WS ));
					//t.Wait();

					//Task t = Task.Run( () => this.co_Tokens.ParseForTokens());
					//t.Wait();
					//this.co_Column.ParseForColumns();
					//Assert.AreNotEqual( 0 , this.co_BDCMain.Columns.Count	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ

			////จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//private DTO_ExcelWorksheet GetWSDTO()
			//	{
			//		DTO_ExcelWorksheet	lo_WS;
			//		IO									lo_IO	= IPC_Controller.CreateIO();
			//		ObjSerializer				lo_SR	= IPC_Controller.CreateSerialiser();
			//		WSDTOParser					lo_PS	= IPC_Controller.CreateWSDTOParser();

			//		string x = lo_IO.ReadFile(@"C:\Temp\BxSWorx\xx.xml");
			//		lo_WS = lo_SR.DeSerialize<DTO_ExcelWorksheet>( x );
			//		lo_PS.Parse1Dto2D(lo_WS);
			//		return	lo_WS;
			//	}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private DTO_BDCXMLConfig CreateXMLConfig()
				{
					return	new DTO_BDCXMLConfig
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
			private string[,] CreateData()
				{
					string XMLConfig	= this.co_ProcCfg.IPXController.Serialize( this.CreateXMLConfig() );

					string[,]	lt_Data	= new string[10,10];

					lt_Data[0,0]	= cz_Cmd_Prefix + "<DATASTARTCOL>[9];<Execute>[D]";
					lt_Data[0,1]	= cz_Cmd_Prefix + cz_Token_OKCd[4];
					lt_Data[1,0]	= cz_Cmd_Prefix + "<messages>[3]";
					lt_Data[1,1]	= cz_Cmd_Prefix + cz_Token_Prog;
					lt_Data[2,0]	= cz_Cmd_Prefix + XMLConfig;

					return	lt_Data;
				}
		}
}
