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
			private	static string	cc_Path;
			private	static string	cc_FullName;

			//-------------------------------------------------------------------------------------------
			[ClassInitialize]
			public static void Setup()
				{
					cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
					cc_FullName	= Path.Combine(cc_Path, cz_FileName);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Loader()
				{
					Loader lo_Loader	= new Loader(cc_FullName);
				}
		}
}
