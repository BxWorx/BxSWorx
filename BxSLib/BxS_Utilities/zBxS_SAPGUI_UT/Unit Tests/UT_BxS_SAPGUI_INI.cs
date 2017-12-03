using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
//.........................................................
using BxS_SAPGUI.API;
using BxS_SAPGUI.COM.DL;
using BxS_Toolset.IODisk;
using BxS_SAPGUI.INI;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_INI
		{
			private const string	cz_TestDir	= "Test Resources";
			private const string	cz_TestINI	= "saplogon_Test.ini";
			private const string	cz_TestLNK	= "saplogon_Link.xml";
			//...................................................
			private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest			= Path.Combine(_Path,	cz_TestDir);
			private	static readonly string	_FileTestINI	= Path.Combine(_PathTest,	cz_TestINI);
			private	static readonly string	_FileTestLNK	= Path.Combine(_PathTest,	cz_TestLNK);
			//...................................................
			private readonly ControllerFactory					_Fac			= new ControllerFactory();
			private readonly UT_BxS_DataContainerFiller	_DCFiller	= new UT_BxS_DataContainerFiller();

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiIni_Parser()
				{
					int	ln_Cnt;
					//...............................................
					IReposSAPGui			lo_Rep		= this._DCFiller.CreateRepository();
					IReposSAPGui			lo_Repx		= this._DCFiller.CreateRepository(true);
					INIParse2ReposDTO lo_Parser	= this._Fac.CreateINIParser(lo_Rep	,	_FileTestINI, _FileTestLNK);
					INIParse2ReposDTO lo_Parsex	= this._Fac.CreateINIParser(lo_Repx	,	_FileTestINI, _FileTestLNK);
					//...............................................
					ln_Cnt	= 1;
					lo_Parser.Load();
					lo_Parsex.Load();

					IList<IDTOItem> lt	= lo_Rep.ItemList();
					IList<IDTOItem> ltx	= lo_Rep.ItemList();

					Assert.AreEqual(43								,	lo_Rep	.ItemCount,	$"DLCntlr: {ln_Cnt}: Rep count");
					Assert.AreEqual(43								,	lo_Repx	.ItemCount,	$"DLCntlr: {ln_Cnt}: Repx count");
					Assert.AreEqual(lo_Rep.ItemCount	,	lo_Repx	.ItemCount,	$"DLCntlr: {ln_Cnt}: Rep vs Repx count");
					Assert.AreEqual(lt[1].UUID				,	ltx[1].UUID				,	$"DLCntlr: {ln_Cnt}: Rep vs Repx ID");
				}

		}
}
