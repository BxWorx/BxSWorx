using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPGUI.XML;
using System.IO;
using System.Reflection;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_XML
		{
			private const string	cz_FileName	= "SAPUILandscapeS2A.xml";
			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Loader_Errors()
				{
					Repository lo_Loader0	= new Loader().Load("XXXX");
					Assert.AreEqual(0, lo_Loader0.WorkSpaces.Count, "Invalid Name: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Loader_Base()
				{
					Repository lo_Loader0	= new Loader().Load(cc_FullName, null, null, false);
					Assert.AreNotEqual(0, lo_Loader0.MsgServers.Count	, "Base: MsgSrvs: Error");

					//...............................................
					Repository lo_Loader1	= new Loader().Load(cc_FullName);
					Assert.AreNotEqual(0, lo_Loader1.MsgServers.Count	, "Base: MsgSrvs: Error");
					Assert.AreNotEqual(0, lo_Loader1.Services.Count		, "Base: Servies: Error");
					Assert.AreNotEqual(0, lo_Loader1.WorkSpaces.Count	, "Base: Workspaces: Error");

					//...............................................
					Repository lo_Loader2	= new Loader().Load(cc_FullName, "LEGACY SYSTEMS", null, false);
					Assert.AreNotEqual(0, lo_Loader1.MsgServers.Count	, "Base: MsgSrvs: Error");
					Assert.AreNotEqual(0, lo_Loader1.Services.Count		, "Base: Servies: Error");
					Assert.AreNotEqual(0, lo_Loader1.WorkSpaces.Count	, "Base: Workspaces: Error");
				}
		}
}
