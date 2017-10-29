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
			private	static readonly string	_Path				= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest		= Path.Combine(_Path,	cz_TestDir);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Schema()
				{
					int	ln_Cnt;
					var	lo_DS	= new Schema();
					//...............................................
					ln_Cnt	= 1;
					DataSet lo_DSx	= lo_DS.Create();
					Assert.IsNotNull	(lo_DSx										,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreEqual		(lo_DSx.Tables.Count	, 3	,	$"Cntlr: {ln_Cnt}: DS-Schema-Tbl: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_Mapping()
				{
					int	ln_Cnt;
					var	lo_Map	= new Mapping();
					//...............................................
					ln_Cnt	= 1;
					Assert.AreNotEqual	(lo_Map.Service.Count		,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.MsgServer.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.Workspace.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");

					Assert.AreNotEqual	(lo_Map.Service.Count		,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.MsgServer.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
					Assert.AreNotEqual	(lo_Map.Workspace.Count	,	$"Cntlr: {ln_Cnt}: DS-Schema-Nul: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DSStartup()
				{
					int		ln_Cnt;
					bool	lb_Ok;

					var		lo_DSH	= new UsrDataSet(_PathTest);
					//...............................................
					ln_Cnt	= 0;
					string lc_Nme	= Path.Combine(_PathTest,	lo_DSH.SchemaName);

					if (File.Exists(lc_Nme))	File.Delete(lc_Nme);
					Assert.IsFalse	(File.Exists(lc_Nme),	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					//...............................................
					ln_Cnt	= 1;
					UsrDataSet	lo_DSH1 = null;

					try
						{
							lo_DSH1	= new UsrDataSet(_PathTest);
							lb_Ok		= true;
						}
						catch (Exception)	{	lb_Ok	= false; }

					Assert.IsTrue		(lb_Ok,																		$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
					Assert.AreEqual	(3		,	lo_DSH1?.DataSetUsr.Tables.Count,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
					//...............................................
					ln_Cnt	= 2;
					UsrDataSet	lo_DSH2 = null;

					try
						{
							lo_DSH2	= new UsrDataSet("ZZZZ");
							lb_Ok		= false;
						}
						catch (System.IO.DirectoryNotFoundException)	{	lb_Ok	= true; }
						catch (Exception)															{	lb_Ok	= false; }

						Assert.IsTrue(lb_Ok,	$"Cntlr: {ln_Cnt}: DS-NoPath: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_DSService()
				{
					//int	ln_Cnt	= 0;
					var	lo_DSH	= new UsrDataSet(_PathTest);
					//...............................................
					var			lc_Key		= Guid.NewGuid();
					DataRow lo_DSRow	= lo_DSH.NewSrvRow();
					lo_DSRow["UUID"]	= lc_Key;
					lo_DSH.AddUpdateService(lc_Key, lo_DSRow);

					//var	lo_DSH	= new UsrDataSet(cc_PathTest);
					//Assert.IsTrue(lo_DSH.IsReady	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
					////...............................................
					//var lo_DTOSrv = new DTOService	{	UUID = Guid.NewGuid().ToString() };
					//var x = lo_DTOSrv.UUID.Length;
					//ln_Cnt	= 1;
					//lo_DSH.AddUpdateService(lo_DTOSrv);
					//Assert.IsTrue(lo_DSH.AddUpdateService(lo_DTOSrv)	,	$"Cntlr: {ln_Cnt}: DS Ready: Error");
				}
		}
}
