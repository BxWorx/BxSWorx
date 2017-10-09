using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPGUI.XML;
using System.Reflection;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_XML
		{
			private const string	cz_FullName = "SAPUILandscapeS2A.xml";

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiXML_Loader()
				{
					string xx = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
					Loader lo_Loader	= new Loader(cz_FullName);
				}
		}
}
