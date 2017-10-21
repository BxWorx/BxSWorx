using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using SAPGUI.USR.DS;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
	{
	[TestClass]
	public class UT_BxS_SAPGUI_USR
		{
			private const string	cz_FileName		= "SAPGUI_USR.xml";
			private const string	cz_TestDir		= "Test Resources";

			//...................................................
			private	static readonly string	cc_Path			= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	cc_FullName	= Path.Combine(cc_Path,	cz_TestDir, cz_FileName);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiUsr_XMLSchema()
				{
					int	ln_Cnt;
					//...............................................
					var	lo_DSHndlr = new  DataSetCreate(cc_FullName);
					//...............................................
					ln_Cnt			= 1;
					//lo_DTOConn	= new DTOConnection	{	ID = "ZZZZZ" };
					//lo_DSHndlr.GetConnection(lo_DTOConn);
					//Assert.IsFalse	(lo_DTOConn.IsValid	,	$"Cntlr: {ln_Cnt}: DTO b: Error");
					////...............................................
					//ln_Cnt			= 2;
					//lo_DTOConn	= new DTOConnection	{	ID = cz_TestConnID };
					//lo_DSHndlr.GetConnection(lo_DTOConn);
					//Assert.AreEqual	(cz_TestConnID			, lo_DTOConn.ID	,	$"Cntlr: {ln_Cnt}: Conn: Error");
					//Assert.IsTrue		(lo_DTOConn.IsValid	,									$"Cntlr: {ln_Cnt}: Valid: Error");
				}
		}
}
