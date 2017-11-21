using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using BxS_SAPGUI.API;
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
			private	static readonly string	cc_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullPath			= Path.Combine(cc_Path,	cz_TestDir);
			private	static readonly string	cc_XMLFullName	= Path.Combine(cc_FullPath, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_ControllerXML()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					//...............................................
					lo_Cntlr	= ControllerFactory.CreateControllerForSAPXML(cc_XMLFullName, true);
					//...............................................
					ln_Cnt			= 1;
					lo_DTOConn	= lo_Cntlr.CreateConnection();
					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
					Assert.IsFalse		(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_ControllerINI()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					//...............................................
					lo_Cntlr	= ControllerFactory.CreateControllerForSAPINI(cc_XMLFullName);
					//...............................................
					ln_Cnt			= 1;
					lo_DTOConn	= lo_Cntlr.CreateConnection();
					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
					Assert.IsFalse		(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_ControllerUsr()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					//...............................................
					lo_Cntlr	= ControllerFactory.CreateControllerForSAPUSR(cc_FullPath);
					//...............................................
					ln_Cnt			= 1;
					lo_DTOConn	= lo_Cntlr.CreateConnection();
					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
					Assert.IsFalse		(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
				}
		}
}
