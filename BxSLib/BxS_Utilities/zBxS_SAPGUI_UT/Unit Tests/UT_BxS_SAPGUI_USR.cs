using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.USR.DS;
using SAPGUI.API.DTO;
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
			public void UT_SapGuiUsr_Schema()
				{
					int	ln_Cnt;
					var	lo_DS	= new Schema();
					//...............................................
					ln_Cnt	= 1;
					DataSet lo_DSx	= lo_DS.Create();
					Assert.IsNotNull	(lo_DSx								,												$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreEqual		(lo_DSx.Tables.Count	, 3										,	$"Cntlr: {ln_Cnt}: DS-Schema-Tbl: Error");
				}

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

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DataSet()
				{
					int	ln_Cnt	= 0;
					var	lo_DSH	= new UsrDataSet(cc_PathTest);
					Assert.IsTrue(lo_DSH.IsReady	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					//...............................................
					var lo_DTOSrv = new DTOService	{	UUID = Guid.NewGuid().ToString() };
					var x = lo_DTOSrv.UUID.Length;
					ln_Cnt	= 1;
					lo_DSH.AddUpdateService(lo_DTOSrv);
					Assert.IsTrue(lo_DSH.AddUpdateService(lo_DTOSrv)	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
				}
		}
}
