using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.ObjectPool;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_Pool
		{
			private const			int			_N	= 200;

			private readonly	ToolSet	_TS	= new ToolSet();
			//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_Pool_Use()
				{
					int ln_Max	= 10;
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					ObjectPool<TestClass> lo_OP	= this._TS.CreateObjectPool<TestClass>(() => new TestClass(), ln_Max);
					Assert.IsNotNull	(lo_OP,	$"Pool: {ln_Cnt}: Instantiate");
					//...............................................
					ln_Cnt	++;

					Parallel.For(	0, _N,
						i =>	{
										TestClass x = lo_OP.GetObject();
										if (x != null)
											{
												x.Run();
												lo_OP.PutObject(x);
											}
									}
											);

					Assert.AreEqual	(ln_Max	,	lo_OP.MaxEntries	,	$"Pool:Use {ln_Cnt}: Max"		);
					Assert.AreEqual	(ln_Max	,	lo_OP.Count				,	$"Pool:Use {ln_Cnt}: Count"	);
					Assert.AreEqual	(ln_Max	,	lo_OP.ObjectCount	,	$"Pool:Use {ln_Cnt}: Objs"	);

					int ln_Tot = 0;
					for (int i = 0; i < lo_OP.ObjectCount; i++)
						{
							TestClass x= lo_OP.GetObject();
							ln_Tot	+= x.Count;
							Console.WriteLine(x.Count.ToString());
						}
						Console.WriteLine(ln_Tot.ToString());
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_Pool_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					ObjectPool<TestClass> lo_OP	= this._TS.CreateObjectPool<TestClass>(() => new TestClass(), 5);

					Assert.IsNotNull	(			lo_OP							,	$"Pool: {ln_Cnt}: Instantiate");
					Assert.AreEqual		(5	,	lo_OP.MaxEntries	,	$"Pool: {ln_Cnt}: Max");
				}

			//===========================================================================================
			#region "Local"

				//-----------------------------------------------------------------------------------------
				private class TestClass
					{
						internal 	int			Index { get; }
						internal	int			Count { get; private set; }
						public		string	Prop1 { get; set; }

						public void Run()	{ this.Count ++;	Thread.Sleep(1000); }
					}

			#endregion

		}
}
