using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_WinUtilFRM;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_WinUtil_UT
{
	[TestClass]
	public class UT_BxS_WinUtil_Registry
		{
			private const string	_RootNode	= "Software";
			private const string	_ApplID		= "BxSWorx";
			private const string	_ValuIDOK	= "BxSTest";

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_WinRegBase_CRUD()
				{
					var registry = new WinRegistryCurrentUser(_RootNode, _ApplID);

					if (registry.ExistsPath())	registry.RemovePath();

					Assert.AreEqual(true	, registry.Write(_ValuIDOK, true)	,	"CRUD: Write: Error");
					Assert.AreEqual(true	, registry.Exists(_ValuIDOK)			,	"CRUD: Read:	Error");
					Assert.AreEqual(true	, registry.Remove(_ValuIDOK)			,	"CRUD: Delete1: Error");
					Assert.AreEqual(false	, registry.Remove(_ValuIDOK)			,	"CRUD: Delete2: Error");
					Assert.AreEqual(true	, registry.RemovePath()					,	"CRUD: Delete3: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_WinRegBase_Existing()
				{
					const string 	lc_AppName	= @"Microsoft\Notepad";
					const string	lc_ValueID	= "iWindowPosDX";

					string lc_Path = string.Join(@"\", _RootNode, lc_AppName);

					var registry0 = new WinRegistryCurrentUser();
					var registry2 = new WinRegistryCurrentUser();
					var registry1 = new WinRegistryCurrentUser(_RootNode, lc_AppName);

					registry2.ApplicationName	= lc_AppName;
					registry2.RootNode				= _RootNode;

					int VDwrd0	= registry0.Read(lc_Path,	lc_ValueID, default(int));
					int VDwrd1	= registry1.Read(				lc_ValueID, default(int));
					int VDwrd2	= registry2.Read(				lc_ValueID, default(int));

					Assert.AreNotEqual(default(int)	, VDwrd0	, "Base: DWrd0 error");
					Assert.AreNotEqual(default(int)	, VDwrd1	, "Base: DWrd1 error");
					Assert.AreNotEqual(default(int)	, VDwrd2	, "Base: DWrd2 error");

					Assert.AreEqual(true	, registry0.Exists(lc_Path,	lc_ValueID)	, "Base: Exists0 error");
					Assert.AreEqual(true	, registry1.Exists(				lc_ValueID)	, "Base: Exists1 error");
					Assert.AreEqual(true	, registry2.Exists(				lc_ValueID)	, "Base: Exists2 error");
					Assert.AreEqual(false	, registry2.Exists(				"XXX")	, "Base: Exists3 error");

					Assert.AreEqual(true	, registry0.ExistsPath(lc_Path)	, "Base: ExistsPath0 error");
					Assert.AreEqual(true	, registry1.ExistsPath()			, "Base: ExistsPath1 error");
					Assert.AreEqual(false	, registry2.ExistsPath("XXX")	, "Base: ExistsPath2 error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_WinRegBase_Defaults()
				{
					var registry = new WinRegistryCurrentUser();

					bool	 VBool	= registry.Read(_ValuIDOK, default(bool));
					string VStr		= registry.Read(_ValuIDOK, string.Empty);
					int		 VInt		= registry.Read(_ValuIDOK, default(int));

					Assert.AreEqual(default(bool)	, VBool	, "Base: Bool error");
					Assert.AreEqual(string.Empty	, VStr	, "Base: String error");
					Assert.AreEqual(default(int)	, VInt	, "Base: Int error");
				}
		}
}
