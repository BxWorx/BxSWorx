using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset.IODisk;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_QMngr
		{
			private const string	cz_TestDir			= "Test Resources";
			private const string	cz_TestFileName	= "Test Data.txt";
			private const string	cz_TestIniName	= "saplogon_Test.ini";
			//...................................................
			private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest			= Path.Combine(_Path		,	cz_TestDir			);
			private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestFileName	);
			//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_IO_Read()
				{
					int	ln_Cnt;
					//...............................................
					string				lc_Tst;
					IList<string> lt_List;

					string	lc_IniFullNme	= Path.Combine(_PathTest,	cz_TestIniName);
					var			lo_IO					= new IO();

					ln_Cnt	= 1;
					lc_Tst	= lo_IO.ReadFile(lc_IniFullNme);
					lt_List = lo_IO.ReadTextFile(lc_IniFullNme);

					Assert.AreNotEqual(string.Empty, lc_Tst,	$"IO: {ln_Cnt}: Write: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_IO_Write()
				{
					if (File.Exists(_TestFullNme))	File.Delete(_TestFullNme);
					//...............................................
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					const string lz_Tst	= "TestString";

					var			lo_IO		= new IO();
					string	lc_Tst;

					lo_IO.WriteFile(_TestFullNme, lz_Tst);
					lc_Tst	= lo_IO.ReadFile(_TestFullNme);

					Assert.IsTrue		(					File.Exists(_TestFullNme)	,	$"IO: {ln_Cnt}: Write: Error");
					Assert.AreEqual	(lz_Tst	,	lc_Tst										,	$"IO: {ln_Cnt}: Read: Error");
				}

			//===========================================================================================
			#region "Local"
			#endregion

		}
}
