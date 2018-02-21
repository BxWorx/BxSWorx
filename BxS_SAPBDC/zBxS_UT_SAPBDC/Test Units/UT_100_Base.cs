using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
//.........................................................
using					BxS_SAPBDC.Helpers;
using					BxS_SAPBDC.Parser;
using static	BxS_SAPBDC.Parser.BDC_Constants;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_UT_SAPBDC
{
	[TestClass]
	public class UT_100_Base
		{
			private readonly	ObjSerializer						co_ObjSer				;
			private	readonly	BDC_Processor						co_Proc					;
			private readonly	BDC_Processor_Tokens		co_Proc_Tokens	;
			private readonly	BDC_Processor_Columns		co_Proc_Columns	;
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			public UT_100_Base()
				{
					this.co_ObjSer				= new	ObjSerializer();

					this.co_Proc_Tokens		= new BDC_Processor_Tokens	(		new ObjSerializer()
																															, () => new DTO_TokenReference() );

					this.co_Proc_Columns	= new BDC_Processor_Columns	(	() => new DTO_BDCColumn() );

					//this.co_Proc					= new BDC_Processor					(		this.co_Proc_Tokens
					//																										, this.co_Proc_Columns
					//																										, () => new DTO_BDCSession()
					//																										, () => new DTO_BDCHeaderRowRef()	);
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
					var x	= new DTO_TokenReference();
					Assert.IsNotNull( x , "xxxx" );

					DTO_BDCXMLConfig y = this.CreateXMLConfig();
					Assert.IsNotNull( y , "xxxx" );

					DTO_BDCSession lo_Session	= this.CreateBDCSession();
					Assert.IsNotNull( lo_Session , "xxxx" );
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			[TestMethod]
			public void UT_100_20_ParseForTokens()
				{
					DTO_BDCSession	lo_Session	= this.CreateBDCSession();
					string[,]				lt_Data			= this.CreateData();

					Task t = Task.Run( () => this.co_Proc_Tokens.Process( lo_Session , lt_Data ));
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
					DTO_BDCSession	lo_Session	= this.CreateBDCSession();
					string[,]				lt_Data			= this.CreateData();

					Task t = Task.Run( () => this.co_Proc_Tokens.Process( lo_Session , lt_Data ));
					t.Wait();

					bool lb = this.co_Proc_Columns.Process( lo_Session , lt_Data );

					//Task t = Task.Run( () => this.co_Tokens.ParseForTokens());
					//t.Wait();
					//this.co_Column.ParseForColumns();
					//Assert.AreNotEqual( 0 , this.co_BDCMain.Columns.Count	, ""	);
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ

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
			private DTO_BDCSession CreateBDCSession()
				{
					return	new DTO_BDCSession( new DTO_BDCHeaderRowRef() );
				}

			//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
			private string[,] CreateData()
				{
					string XMLConfig	= this.co_ObjSer.Serialize( this.CreateXMLConfig() );

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
