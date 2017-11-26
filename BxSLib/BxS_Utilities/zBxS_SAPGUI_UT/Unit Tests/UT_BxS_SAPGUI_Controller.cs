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
			private const string	cz_XMLFileName	= "SAPUILandscapeS2A.xml";
			private const string	cz_INIFileName	= "saplogon_Test.ini";
			private const	string	cz_UsrFileName	= "SAPGUI_USR_DC.xml"	;
			private const string	cz_TestDir			= "Test Resources";
			//private const string	cz_TestConnID		=	"dbb1aab6-c82f-4762-bf2b-c525dc55191b";
			//...................................................
			private	static readonly string	cc_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullPath			= Path.Combine(cc_Path,	cz_TestDir);
			//...................................................
			private readonly ControllerFactory _CntlrFact	= new ControllerFactory();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_ControllerXML()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					string						lc_FullName	= Path.Combine(cc_FullPath, cz_XMLFileName);
					//...............................................
					ln_Cnt			= 1;
					lo_Cntlr	= this._CntlrFact.CreateControllerForSAPXML(lc_FullName, true);

					Assert.AreNotEqual(0, lo_Cntlr.ServiceCount,	$"Cntlr-XML: {ln_Cnt}: Instantiate: Error");
					//...............................................
					ln_Cnt			= 2;
					lo_DTOConn	= lo_Cntlr.CreateConnection();

					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_ControllerINI()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					string						lc_FullName	= Path.Combine(cc_FullPath, cz_INIFileName);
					//...............................................
					ln_Cnt			= 1;
					lo_Cntlr	= this._CntlrFact.CreateControllerForSAPINI(lc_FullName);

					Assert.AreNotEqual(0, lo_Cntlr.ServiceCount,	$"Cntlr-INI: {ln_Cnt}: Instantiate: Error");
					//...............................................
					ln_Cnt			= 2;
					lo_DTOConn	= lo_Cntlr.CreateConnection();
					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGui_ControllerUsr()
				{
					int								ln_Cnt;
					IController				lo_Cntlr;
					IDTOConnection		lo_DTOConn;
					string						lc_FullName	= Path.Combine(cc_FullPath, cz_UsrFileName);
					//...............................................
					ln_Cnt			= 1;
					lo_Cntlr	= this._CntlrFact.CreateControllerForSAPUSR(lc_FullName);
					//Assert.AreNotEqual(0, lo_Cntlr.ServiceCount,	$"Cntlr-INI: {ln_Cnt}: Instantiate: Error");
					//...............................................
					ln_Cnt			= 2;
					lo_DTOConn	= lo_Cntlr.CreateConnection();
					Assert.IsNotNull	(lo_DTOConn					,	$"Cntlr: {ln_Cnt}: DTO a: Error");
				}
		}
}
