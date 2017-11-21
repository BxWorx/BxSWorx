using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
//.........................................................
using BxS_SAPGUI.API;
using BxS_SAPGUI.COM.DL;
using BxS_SAPGUI.USR;
using BxS_Toolset.IO;
using BxS_Toolset.Serialize;
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
					IRepository	lo_Rep		= this.CreateRepository();
					var					lo_IO			= new IO();
					var					lo_Parser	= new INIParse2ReposDTO(lo_IO);
					//...............................................
					ln_Cnt	= 1;
					lo_Parser.Load(lo_Rep, _FileTest);
					//Assert.IsFalse	(lo_UsrCntlr.FileExists()	,	$"DLCntlr: {ln_Cnt}: Del:Dataset: Error");
				}

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IRepository CreateRepository(bool emptyOne = false)
					{
						return	emptyOne	? new Repository(new DataContainer()) :	new Repository(new DataContainer());
					}

			#endregion

		}
}
