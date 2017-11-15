using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using Toolset.Serialize;
using Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_SerializerDC
		{
			private const string	cz_TestDir			= "Test Resources";
			private const string	cz_TestFileName	= "Test Data.txt";
			private const string	cz_TestFNameXML	= "Test Data.xml";
			private const	string	cz_Tst					= "TestString";
			//...................................................
			private	static readonly string	_Path					= Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.FullName;
			private	static readonly string	_PathTest			= Path.Combine(_Path,	cz_TestDir);
			private	static readonly string	_TestFullNme	= Path.Combine(_PathTest,	cz_TestFileName	);
			private	static readonly string	_TestXMLFNme	= Path.Combine(_PathTest,	cz_TestFNameXML	);
			//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_SerializerDC_Obj2XML()
				{
					int	ln_Cnt;
					//...............................................
					var				lo_IO		= new IO();
					var				lo_Ser	= new SerializerViaDataContract();
					TestClass lo_Tst	= this.CreateTestClass();
					string		lc_Cls;
					TestClass lo_Res;

					ln_Cnt	= 1;
					lc_Cls	= lo_Ser.Serialize2XML(lo_Tst);
					lo_IO.WriteFile(_TestXMLFNme, lc_Cls);
					lo_Res	= lo_Ser.DeSerializeObject<TestClass>(lo_IO.ReadFile(_TestXMLFNme));

					Assert.IsTrue		(	File.Exists(_TestXMLFNme)		,	$"IO: {ln_Cnt}: Write: Error");

					Assert.IsNotNull(					lo_Res							, $"Serializer(DC): {ln_Cnt}: Obj-Deserialise: Error");
					Assert.AreEqual	(cz_Tst	, lo_Res.Prop1				, $"Serializer(DC): {ln_Cnt}: Obj-Complete-001: Error");
					Assert.AreEqual	(cz_Tst	, lo_Res.Prop2				, $"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
					Assert.AreEqual	(cz_Tst	, lo_Res.Prop3				, $"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
					Assert.AreEqual	(1			, lo_Res._Dict.Count	, $"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_SerializerDC_Obj2File()
				{
					int	ln_Cnt;
					//...............................................
					var				lo_IO		= new IO();
					var				lo_Ser	= new SerializerViaDataContract();
					TestClass lo_Tst	= this.CreateTestClass();
					string		lc_Cls;
					TestClass lo_Res;

					ln_Cnt	= 1;
					lc_Cls	= lo_Ser.SerializeObject(lo_Tst);
					lo_IO.WriteFile(_TestFullNme, lc_Cls);
					lo_Res	= lo_Ser.DeSerializeObject<TestClass>(lo_IO.ReadFile(_TestFullNme));

					Assert.IsTrue		(	File.Exists(_TestFullNme)		,	$"IO: {ln_Cnt}: Write: Error");

					Assert.IsNotNull(					lo_Res							, $"Serializer(DC): {ln_Cnt}: Obj-Deserialise: Error");
					Assert.AreEqual	(cz_Tst	, lo_Res.Prop1				, $"Serializer(DC): {ln_Cnt}: Obj-Complete-001: Error");
					Assert.AreEqual	(cz_Tst	, lo_Res.Prop2				, $"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
					Assert.AreEqual	(cz_Tst	, lo_Res.Prop3				, $"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
					Assert.AreEqual	(1			, lo_Res._Dict.Count	, $"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
			}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_SerializerDC_Obj()
				{
					int	ln_Cnt;
					//...............................................
					ln_Cnt	= 1;

					var				lo_Ser	= new SerializerViaDataContract();
					TestClass lo_Tst	= this.CreateTestClass();
					string		lc_Cls;
					TestClass lo_Res;

					lc_Cls	= lo_Ser.SerializeObject(lo_Tst);
					lo_Res	= lo_Ser.DeSerializeObject<TestClass>(lc_Cls);

					Assert.IsNotNull	(					lc_Cls							,	$"Serializer(DC): {ln_Cnt}: Obj-Serialise: Error");
					Assert.IsNotNull	(					lo_Res							,	$"Serializer(DC): {ln_Cnt}: Obj-Deserialise: Error");
					Assert.AreEqual		(cz_Tst	,	lo_Res.Prop1				,	$"Serializer(DC): {ln_Cnt}: Obj-Complete-001: Error");
					Assert.AreEqual		(cz_Tst	,	lo_Res.Prop2				,	$"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
					Assert.AreEqual		(cz_Tst	,	lo_Res.Prop3				,	$"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
					Assert.AreEqual		(1			,	lo_Res._Dict.Count	,	$"Serializer(DC): {ln_Cnt}: Obj-Complete-002: Error");
				}

			//===========================================================================================
			#region "Local"

				//-----------------------------------------------------------------------------------------
				private TestClass CreateTestClass()
					{
						var						lo_Tst	= new TestClass();

						lo_Tst.Prop1	= cz_Tst;
						lo_Tst.Prop2	= cz_Tst;

						lo_Tst.SetTest(cz_Tst);
						lo_Tst.AddDict(cz_Tst);

						return	lo_Tst;
					}

				//-----------------------------------------------------------------------------------------
				[DataContract()]
				internal class TestClass
					{
						internal TestClass()
							{
								this._Dict	= new	Dictionary<string, string>();
							}

						[DataMember]	public		string Prop1 { get; set; }
						[DataMember]	internal	string Prop2 { get; set; }
						[DataMember]	internal	string Prop3 { get; private set; }

						[DataMember]	internal Dictionary<string, string>  _Dict;


						internal void SetTest(string Str)
							{
								this.Prop3 = Str;
							}

						internal void AddDict(string Data)
							{
								this._Dict.Add(Data, Data);
							}
					}

			#endregion

		}
}
