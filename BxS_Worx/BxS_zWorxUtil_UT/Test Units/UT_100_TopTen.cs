using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxUtil.General;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_TopTen
		{
			private	readonly TopTenList<string>		co_TT;
			private	readonly TopTenList<MyClass>	co_TO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_TopTen()
				{
					this.co_TT	= new TopTenList<string>();
					this.co_TO	= new TopTenList<MyClass>();
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_TopTen_10_Instantiate()
				{
					Assert.IsNotNull	( this.co_TT	, "" );
					Assert.IsNotNull	( this.co_TO	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_TopTen_20_Add()
				{
					this.co_TT.Add("A");
					Assert.AreEqual( 1 , this.co_TT.Count	, "" );
					this.co_TT.Add("B");
					Assert.AreEqual( 2 , this.co_TT.Count	, "" );
					Assert.IsTrue( this.co_TT.List[0].Equals("B") , "");
					this.co_TT.Add("C");
					this.co_TT.Add("B");
					Assert.AreEqual	( 3 , this.co_TT.Count								, ""	);
					Assert.IsTrue		(			this.co_TT.List[0].Equals("B")	, ""	);

					for ( int i = 0; i < 10; i++ )
						{
							this.co_TT.Add(i.ToString());
						}
					Assert.AreEqual	( this.co_TT.Size , this.co_TT.Count	, "" );
					Assert.IsTrue		(	this.co_TT.List[0].Equals("9")	, ""	);
					this.co_TT.Add("4");
					Assert.AreEqual	( this.co_TT.Size , this.co_TT.Count	, "" );
					Assert.IsTrue		(	this.co_TT.List[0].Equals("4")	, ""	);
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_TopTen_30_Remove()
				{
					for ( int i = 0; i < 10; i++ )
						{
							this.co_TT.Add(i.ToString());
						}
					Assert.AreEqual	( this.co_TT.Size		, this.co_TT.Count	, "" );
					this.co_TT.Remove("4");
					Assert.AreEqual	( this.co_TT.Size-1 , this.co_TT.Count	, "" );
					this.co_TT.Remove("$");
					Assert.AreEqual	( this.co_TT.Size-1 , this.co_TT.Count	, "" );
					this.co_TT.Clear();
					Assert.AreEqual	( 0									, this.co_TT.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_TopTen_40_Resize()
				{
					for ( int i = 0; i < 10; i++ )
						{
							this.co_TT.Add(i.ToString());
						}
					Assert.AreEqual	( this.co_TT.Size , this.co_TT.Count	, "" );
					//...
					this.co_TT.Size = 5;
					Assert.AreEqual	( this.co_TT.Size , this.co_TT.Count	, "" );
					Assert.AreEqual	( 5								, this.co_TT.Count	, "" );
					//...
					this.co_TT.Size = 10;
					for ( int i = 0; i < 10; i++ )
						{
							this.co_TT.Add(i.ToString());
						}
					Assert.AreEqual	( this.co_TT.Size , this.co_TT.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_TopTen_50_Object()
				{
					var x = new MyClass();
					this.co_TO.Add( x );
					Assert.AreEqual	( 1	, this.co_TO.Count	, "" );
					this.co_TO.Add( x );
					Assert.AreEqual	( 1	, this.co_TO.Count	, "" );
					var y = new MyClass();
				}

		//.

		private class MyClass
			{
				private MyCl
				public string ID { get; set;}
			}

		}

	//.

	}
