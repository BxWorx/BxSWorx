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
			private const string	cz_FileName		= "SAPGUI_USR.xml";
			private const string	cz_DSName			= "SAPGUI_USR";
			private const string	cz_TestDir		= "Test Resources";

			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_XMLSchema()
				{
					int	ln_Cnt;
					//...............................................
					var	lo_DSHndlr	= new DataSetCreate(cc_FullName, cz_DSName);
					//...............................................
					ln_Cnt	= 1;
					var lo_DSTest	= new DataSet();
					lo_DSTest.ReadXmlSchema(cc_FullName);
					Assert.AreEqual	(cz_DSName				, lo_DSTest.DataSetName	,	$"Cntlr: {ln_Cnt}: DS-Name: Error");
					Assert.IsNotNull(lo_DSTest.Tables.Count										,	$"Cntlr: {ln_Cnt}: DS-Tabs: Error");
				}
		}
}
