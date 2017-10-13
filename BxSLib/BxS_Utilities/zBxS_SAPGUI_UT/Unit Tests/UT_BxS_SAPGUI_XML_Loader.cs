using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPGUI.XML;
using System.IO;
using System.Reflection;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_XML_Loader
		{
			private const string	cz_FileName	= "SAPUILandscapeS2A.xml";
			private const string	cz_TestDir	= "Test Resources";
			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Loader_Errors()
				{
					DTORepository	lo_Repos;
					var	lo_LoaderOK		= new Loader();
					int	ln_Cnt			= 0;
					//...............................................
					lo_Repos	= lo_LoaderOK.Load("XXXX");
					ln_Cnt = 1;
					Assert.AreEqual	(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual	(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual	(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderOK.Load("XXXX", null, null);
					ln_Cnt = 2;
					Assert.AreEqual(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderOK.Load(cc_FullName, "LEGACY SYSTEMS", "XX");
					ln_Cnt = 3;
					Assert.AreEqual(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderOK.Load(cc_FullName, "XXXX", "ZA");
					ln_Cnt = 4;
					Assert.AreEqual(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderOK.Load(cc_FullName, "DDNEW SYSTEMS", "ZA");
					ln_Cnt = 5;
					Assert.AreEqual(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Loader_Base()
				{
					DTORepository	lo_Repos;
					int	ln_Cnt			= 0;
					var	lo_LoaderT	= new Loader();
					//...............................................
					lo_Repos	= lo_LoaderT.Load(cc_FullName, null, null, false);
					ln_Cnt = 1;
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(47, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(05, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderT.Load(cc_FullName);
					ln_Cnt = 2;
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderT.Load(cc_FullName, "LEGACY SYSTEMS");
					ln_Cnt = 3;
					Assert.AreEqual(03, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(05, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(01, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderT.Load(cc_FullName, "LEGACY SYSTEMS", "ZA");
					ln_Cnt = 4;
					Assert.AreEqual(01, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(02, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(01, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_LoaderT.Load(cc_FullName, "DDNEW SYSTEMS");
					ln_Cnt = 5;
					Assert.AreEqual(02, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(25, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(01, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
				}
		}
}
