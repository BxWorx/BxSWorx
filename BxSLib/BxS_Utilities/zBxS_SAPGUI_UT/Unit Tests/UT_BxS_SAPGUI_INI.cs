using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using BxS_SAPGUI.API;
using BxS_SAPGUI.COM.DL;
using BxS_Toolset.IO;
using BxS_SAPGUI.INI;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPGUI_UT
{
	[TestClass]
	public class UT_BxS_SAPGUI_INI
		{
			private const string	cz_TestDir	= "Test Resources";
			private const string	cz_TestIni	= "saplogon_Test.ini";
			//...................................................
			private	static readonly string	_Path				= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest		= Path.Combine(_Path,	cz_TestDir);
			private	static readonly string	_FileTest		= Path.Combine(_PathTest,	cz_TestIni);

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiIni_Parser()
				{
					int	ln_Cnt;
					//...............................................
					IReposSAPGui	lo_Rep		= this.CreateRepository();
					var					lo_IO			= new IO();
					var					lo_Parser	= new INIParse2ReposDTO(lo_IO, lo_Rep, _FileTest);
					//...............................................
					ln_Cnt	= 1;
					lo_Parser.Load();
					Assert.IsTrue	(true	,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
				}

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IReposSAPGui CreateRepository(bool emptyOne = false)
					{
						return	emptyOne	? new ReposSAPGui(new DCSapGui()) :	new ReposSAPGui(new DCSapGui());
					}

			#endregion

		}
}
