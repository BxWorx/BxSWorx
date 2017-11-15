using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_IO
		{
			private const string	cz_TestDir			= "Test Resources";
			private const string	cz_TestFileName	= "Test Data.txt";
			//...................................................
			private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest			= Path.Combine(_Path,	cz_TestDir);
			private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestFileName	);
			//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_IO_Write()
				{
					if (File.Exists(_TestFullNme))	File.Delete(_TestFullNme);
					//...............................................
					int	ln_Cnt;
					//...............................................
					ln_Cnt	= 1;

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
