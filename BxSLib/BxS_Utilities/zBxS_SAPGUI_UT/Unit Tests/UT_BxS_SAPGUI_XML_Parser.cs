using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPGUI.XML;
using System.IO;
using System.Reflection;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_XML_Parser
		{
			private const string	cz_FileName	= "SAPUILandscapeS2A.xml";
			private const string	cz_TestDir	= "Test Resources";
			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Parser()
				{
					int						ln_Cnt;
					DTORepository	lo_Repos;
					//...............................................
					var	lo_Parser		= new ParseXML2Repository();
					//...............................................
					lo_Repos	= lo_Parser.Load("XXXX");
					ln_Cnt		= 1;
					Assert.AreEqual	(00, lo_Repos.MsgServers.Count	, $"Error: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual	(00, lo_Repos.Services.Count		, $"Error: {ln_Cnt}: Services: Error");
					Assert.AreEqual	(00, lo_Repos.WorkSpaces.Count	, $"Error: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_Parser.Load(cc_FullName);
					ln_Cnt = 2;
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_Parser.Load(cc_FullName, true);
					ln_Cnt = 3;
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(36, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(04, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
					//...............................................
					lo_Repos	= lo_Parser.Load(cc_FullName, false);
					ln_Cnt = 4;
					Assert.AreEqual(07, lo_Repos.MsgServers.Count	, $"Base: {ln_Cnt}: MsgSrvs: Error");
					Assert.AreEqual(47, lo_Repos.Services.Count		, $"Base: {ln_Cnt}: Services: Error");
					Assert.AreEqual(05, lo_Repos.WorkSpaces.Count	, $"Base: {ln_Cnt}: Workspaces: Error");
				}

		}
}
