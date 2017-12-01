using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
//.........................................................
using BxS_SAPGUI.COM.DL;
using BxS_Toolset.IO;
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

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_SapGuiIni_Parser()
				{
					int	ln_Cnt;
					//...............................................
					IReposSAPGui	lo_Rep		= this.CreateRepository();
					var						lo_IO			= new IO();
					var						lo_Ser		= new ObjSerializer();
					var						lo_Parser	= new INIParse2ReposDTO(lo_IO, lo_Ser, lo_Rep, _FileTestINI, _FileTestLNK);
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
