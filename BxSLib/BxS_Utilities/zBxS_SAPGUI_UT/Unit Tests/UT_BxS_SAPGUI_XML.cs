using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//.........................................................
using BxS_SAPGUI.XML;
using BxS_SAPGUI.API;
using BxS_SAPGUI.COM.CNTLR;
using BxS_SAPGUI.COM.DL;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_XML
		{
			private const string	cz_FileName		= "SAPUILandscapeS2A.xml";
			private const string	cz_TestDir		= "Test Resources";
			private				Guid		cz_TestConnID	= Guid.Parse("dbb1aab6-c82f-4762-bf2b-c525dc55191b");
			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Parser()
				{
					int					ln_Cnt;
					IRepository	lo_Repos;
					var					lo_Parser	= new XMLParse2ReposDTO();
					//...............................................
					ln_Cnt	= 1;
					lo_Repos	= this.CreateRepository();
					lo_Parser.Load(lo_Repos, "XXXX");
					Assert.AreEqual	(00, lo_Repos.MsgServerCount	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual	(00, lo_Repos.ServiceCount		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual	(00, lo_Repos.WorkspaceCount	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					ln_Cnt = 2;
					lo_Repos	= this.CreateRepository();
					lo_Parser.Load(lo_Repos, cc_FullName);
					Assert.AreEqual(07, lo_Repos.MsgServerCount	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.ServiceCount		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkspaceCount	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					ln_Cnt = 3;
					lo_Repos	= this.CreateRepository();
					lo_Parser.Load(lo_Repos, cc_FullName, true);
					Assert.AreEqual(07, lo_Repos.MsgServerCount	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.ServiceCount		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkspaceCount	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					ln_Cnt = 4;
					lo_Repos	= this.CreateRepository();
					lo_Parser.Load(lo_Repos, cc_FullName, false);
					Assert.AreEqual(07, lo_Repos.MsgServerCount	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(47, lo_Repos.ServiceCount		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(05, lo_Repos.WorkspaceCount	, $"Base: {ln_Cnt}: Workspaces: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Controller()
				{
					int								ln_Cnt;
					var								lo_Parser	= new XMLParse2ReposDTO();
					IRepository				lo_Repos	= this.CreateRepository();
					IControllerSource	lo_Cntlr	= new XMLController(lo_Repos) ;
					//...............................................
					lo_Parser.Load(lo_Repos, cc_FullName);
					//...............................................
					ln_Cnt			= 1;
					Assert.AreNotEqual(0,	lo_Cntlr.Repository.MsgServerCount,	$"Cntlr: {ln_Cnt}: Loaded: Error");

					//IDTOConnection		lo_DTOConn;
					//lo_DTOConn	= new DTOConnection	{	ID = default(Guid) };
					//lo_Cntlr.GetConnection(lo_DTOConn);
					//Assert.IsFalse	(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
					////...............................................
					//ln_Cnt			= 2;
					//lo_DTOConn	= new DTOConnection	{	ID = cz_TestConnID };
					//lo_Cntlr.GetConnection(lo_DTOConn);
					//Assert.AreEqual	(cz_TestConnID			, lo_DTOConn.ID	,	$"Cntlr: {ln_Cnt}: Conn: Error");
					//Assert.IsTrue		(lo_DTOConn.IsValid	,									$"Cntlr: {ln_Cnt}: Valid: Error");
				}

			//-------------------------------------------------------------------------------------------
			private IRepository	CreateRepository()
				{
					var	lo_DC			= new DataContainer();
					var	lo_Repos	= new Repository(lo_DC);
					return	lo_Repos;
				}

		}
}
