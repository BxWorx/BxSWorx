using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPBDC.BDCParser;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_UT_SAPBDC
{
	[TestClass]
	public class UT_100_Base
		{
			private	BDCWorksheetParser co_BDCParser;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_Base()
				{
					this.co_BDCParser	= new BDCWorksheetParser();
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			[TestMethod]
			public void TestMethod1()
				{
					string[,] lo_Data	=  new string[2,2];

					lo_Data[1,1]	= "<@@PROGRAM> asdasdasdasdasd";
					lo_Data[0,1]	= "<@@YY> xxxx";
					lo_Data[1,0]	= "xxxx";
					lo_Data[0,0]	= "xxxx";

					var T = Task.Run( () => this.co_BDCParser.ParseWorksheet(lo_Data) );

					T.Wait();


					int x =	this.co_BDCParser.TokenCount;

					//Assert.AreEqual(2, x , "xxxx");
				}
		}
}
