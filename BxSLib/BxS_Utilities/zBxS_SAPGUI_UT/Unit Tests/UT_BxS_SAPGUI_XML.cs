using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//.........................................................
using SAPGUI.XML;
using SAPGUI.API;
using SAPGUI.COM.CNTLR;
using SAPGUI.COM.DL;
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
					DataContainer	lo_Repos;
					var					lo_Parser	= new XMLParse2ReposDTO();
					//...............................................
					ln_Cnt		= 1;
					lo_Repos	= new DataContainer();
					lo_Parser.Load(lo_Repos, "XXXX");
					Assert.AreEqual	(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual	(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual	(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					ln_Cnt = 2;
					lo_Repos	= new DataContainer();
					lo_Parser.Load(lo_Repos, cc_FullName);
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					ln_Cnt = 3;
					lo_Repos	= new DataContainer();
					lo_Parser.Load(lo_Repos, cc_FullName, true);
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					ln_Cnt = 4;
					lo_Repos	= new DataContainer();
					lo_Parser.Load(lo_Repos, cc_FullName, false);
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(47, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(05, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Controller()
				{
					int								ln_Cnt;
					IDTOConnection		lo_DTOConn;
					var								lo_Parser	= new XMLParse2ReposDTO();
					var								lo_Repos	= new DataContainer();
					IControllerSource	lo_Cntlr	= new XMLController(lo_Repos) ;
					//...............................................
					lo_Parser.Load(lo_Repos, cc_FullName);
					//...............................................
					ln_Cnt			= 1;
					lo_DTOConn	= new DTOConnection	{	ID = default(Guid) };
					lo_Cntlr.GetConnection(lo_DTOConn);
					Assert.IsFalse	(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
					//...............................................
					ln_Cnt			= 2;
					lo_DTOConn	= new DTOConnection	{	ID = cz_TestConnID };
					lo_Cntlr.GetConnection(lo_DTOConn);
					Assert.AreEqual	(cz_TestConnID			, lo_DTOConn.ID	,	$"Cntlr: {ln_Cnt}: Conn: Error");
					Assert.IsTrue		(lo_DTOConn.IsValid	,									$"Cntlr: {ln_Cnt}: Valid: Error");
				}

		}
}
