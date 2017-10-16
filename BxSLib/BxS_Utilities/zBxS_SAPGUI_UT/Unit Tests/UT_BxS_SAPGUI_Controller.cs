using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using SAPGUI.XML;
using SAPGUI.API;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
	{
	[TestClass]
	public class UT_BxS_SAPGUI_Controller
		{
			private const string	cz_FileName		= "SAPUILandscapeS2A.xml";
			private const string	cz_TestDir		= "Test Resources";
			private const string	cz_TestConnID	=	"dbb1aab6-c82f-4762-bf2b-c525dc55191b";

			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_Controller()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					//...............................................
					lo_Cntlr	= ControllerFactory.CreateControllerForSAPXML(cc_FullName, true);
					//...............................................
					ln_Cnt			= 1;
					lo_DTOConn	= lo_Cntlr.CreateConnection();
					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
					Assert.IsFalse		(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
					////...............................................
					//ln_Cnt			= 2;
					//lo_DTOConn	= lo_Cntlr.GetConnection(cz_TestConnID);
					//Assert.AreEqual	(cz_TestConnID			, lo_DTOConn.ID	, $"Cntlr: {ln_Cnt}: Conn: Error");
					//Assert.IsTrue		(lo_DTOConn.IsValid	,									$"Cntlr: {ln_Cnt}: Valid: Error");
					////...............................................
					//ln_Cnt				= 3;
					//lo_DTOConn		= lo_Cntlr.CreateConnection(cz_TestConnID);
					//lo_Cntlr.GetConnection(lo_DTOConn);
					//Assert.AreEqual	(cz_TestConnID			, lo_DTOConn.ID	,	$"Cntlr: {ln_Cnt}: Conn: Error");
					//Assert.IsTrue		(lo_DTOConn.IsValid	,									$"Cntlr: {ln_Cnt}: Valid: Error");
				}

		}
}
