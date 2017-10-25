using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.USR.DS;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
	{
	[TestClass]
	public class UT_BxS_SAPGUI_USR
		{
			private const string	cz_SchemaName	= "SAPGUI_USR_Schema.xml";
			private const string	cz_FileName		= "SAPGUI_USR_Repos.xml";
			private const string	cz_DSName			= "SAPGUI_USR";
			private const string	cz_TestDir		= "Test Resources";

			//...................................................
			private	static readonly string	cc_Path				= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_PathTest		= Path.Combine(cc_Path,	cz_TestDir);
			private	static readonly string	cc_SchemaName	= Path.Combine(cc_Path,	cz_TestDir, cz_SchemaName);
			private	static readonly string	cc_FullName		= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DataSetHandler()
				{
					int	ln_Cnt;
					var	lo_DSH	= new DataSetHandler();
					//...............................................
					ln_Cnt	= 1;
					DataSet lo_DSx	= lo_DSH.GetDataSet("ZZZZ");
					Assert.IsNull(lo_DSx,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
					//...............................................
					ln_Cnt	= 2;
					DataSet lo_DSo	= lo_DSH.GetDataSet(cc_PathTest);
					Assert.AreEqual	(cz_DSName					, lo_DSo.DataSetName	,	$"Cntlr: {ln_Cnt}: DS-Name: Error");
					Assert.AreEqual(lo_DSo.Tables.Count	, 3										,	$"Cntlr: {ln_Cnt}: DS-Tabs: Error");
				}

			////-------------------------------------------------------------------------------------------
			//[TestMethod]
			//public void UT_SapGuiUsr_DataSet()
			//	{
			//		int	ln_Cnt;
			//		//...............................................
			//		var	lo_DSx	= new UsrDataSet("ZZZ", "ZZZZ");
			//		var	lo_DSo	= new UsrDataSet(cc_FullName, cc_SchemaName);
			//		////...............................................
			//		//ln_Cnt	= 1;
			//		//var lo_DSTest	= new DataSet();
			//		//lo_DSTest.ReadXmlSchema(cc_FullName);
			//		//Assert.AreEqual	(cz_DSName				, lo_DSTest.DataSetName	,	$"Cntlr: {ln_Cnt}: DS-Name: Error");
			//		//Assert.IsNotNull(lo_DSTest.Tables.Count										,	$"Cntlr: {ln_Cnt}: DS-Tabs: Error");
			//	}
		}
}
