using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using zBxS_WinUtil_UT;

namespace zBxS_WinUtil_UT
{
	[TestClass]
	public class UT_BxS_WinUtil_Registry
	{
		private const string	RootNode	= "Software";
		private const string	AppID			= "BxSWorx";
		private const string	ValIDOK		= "BxSTest";

		//-------------------------------------------------------------------------------------------
		[TestMethod]
		public void UT_WinRegistryReadWriteRemove()
			{
				BxS_WinUtilFRM.WinRegistryCurrentUser registry = new BxS_WinUtilFRM.WinRegistryCurrentUser(RootNode, AppID);

				if (registry.ExistsPath())	registry.RemovePath();

				Assert.AreEqual(true	, registry.Write(ValIDOK, true)	,	"CRUD: Write: Error");
				Assert.AreEqual(true	, registry.Exists(ValIDOK)			,	"CRUD: Read:	Error");
				Assert.AreEqual(true	, registry.Remove(ValIDOK)			,	"CRUD: Delete1: Error");
				Assert.AreEqual(false	, registry.Remove(ValIDOK)			,	"CRUD: Delete2: Error");
				Assert.AreEqual(true	, registry.RemovePath()					,	"CRUD: Delete3: Error");
			}

		//-------------------------------------------------------------------------------------------
		[TestMethod]
		public void UT_WinRegistryBase_Existing()
			{
				const string 	AppName		= @"Microsoft\Notepad";
				const string	ValID			= "iWindowPosDX";

				var Path = string.Join(@"\", RootNode, AppName);

				BxS_WinUtilFRM.WinRegistryCurrentUser registry0 = new BxS_WinUtilFRM.WinRegistryCurrentUser();
				BxS_WinUtilFRM.WinRegistryCurrentUser registry2 = new BxS_WinUtilFRM.WinRegistryCurrentUser();
				BxS_WinUtilFRM.WinRegistryCurrentUser registry1 = new BxS_WinUtilFRM.WinRegistryCurrentUser(RootNode, AppName);

				registry2.ApplicationName	= AppName;
				registry2.RootNode				= RootNode;

				var VDwrd0	= registry0.Read(Path,	ValID, default(int));
				var VDwrd1	= registry1.Read(				ValID, default(int));
				var VDwrd2	= registry2.Read(				ValID, default(int));

				Assert.AreNotEqual(default(int)	, VDwrd0	, "Base: DWrd0 error");
				Assert.AreNotEqual(default(int)	, VDwrd1	, "Base: DWrd1 error");
				Assert.AreNotEqual(default(int)	, VDwrd2	, "Base: DWrd1 error");

				Assert.AreEqual(true	, registry0.Exists(Path,	ValID)	, "Base: Exists0 error");
				Assert.AreEqual(true	, registry1.Exists(				ValID)	, "Base: Exists1 error");
				Assert.AreEqual(true	, registry2.Exists(				ValID)	, "Base: Exists2 error");
				Assert.AreEqual(false	, registry2.Exists(				"XXX")	, "Base: Exists3 error");

				Assert.AreEqual(true	, registry0.ExistsPath(Path)	, "Base: ExistsPath0 error");
				Assert.AreEqual(true	, registry1.ExistsPath()			, "Base: ExistsPath1 error");
				Assert.AreEqual(false	, registry2.ExistsPath("XXX")	, "Base: ExistsPath2 error");
			}

		//-------------------------------------------------------------------------------------------
		[TestMethod]
		public void UT_WinRegistryBase_Defaults()
			{
				BxS_WinUtilFRM.WinRegistryCurrentUser registry = new BxS_WinUtilFRM.WinRegistryCurrentUser();

				var VBool	= registry.Read(ValIDOK, default(bool));
				var VStr	= registry.Read(ValIDOK, string.Empty);
				var VInt	= registry.Read(ValIDOK,	default(int));

				Assert.AreEqual(default(bool)	, VBool	, "Base: Bool error");
				Assert.AreEqual(string.Empty	, VStr	, "Base: String error");
				Assert.AreEqual(default(int)	, VInt	, "Base: Int error");
			}
	}
}
