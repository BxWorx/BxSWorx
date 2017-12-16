using System;
using System.Collections.Generic;
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
			private const			int			_N	= 100;
			private const			int			_O	= 100;

			private readonly	ToolSet	_TS	= new ToolSet();
			//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_Pool_Use()
				{
					const int ln_Max	= 20;
								int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					ObjectPoolx<TestClass> lo_OP	= this._TS.CreateObjectPool<TestClass>(	() => new TestClass(), ln_Max, ln_Max );
					Assert.IsNotNull	(lo_OP,	$"Pool: {ln_Cnt}: Instantiate");
					//...............................................
					ln_Cnt	++;

					int ln_Lop	= 0;
					int ln_Tot	= 0;
					int ln_Skp	= 0;
					int ln_Err	= 0;

					Task[] lt_Tasks	= new	Task[_N];

					for (int i = 0; i < _N; i++)
						{
							int Idx	= i;

							lt_Tasks[Idx]	= Task.Run(
																				async () =>
																					{
																						for (int j = 0; j < _O; j++)
																							{
																								Interlocked.Add(ref ln_Lop, 1);
																								TestClass x = lo_OP.GetObject();
																								if (x == null)
																									{	Interlocked.Add(ref ln_Skp, 1); }
																								else
																									{
																										x.Run(Idx*j);
																										bool	xx	= await	lo_OP.PutObjectAsync(x).ConfigureAwait(false);
																										if (!xx)	Interlocked.Add(ref ln_Err, 1);
																									}
																							}
																					}
																			);
						}

					Task.WaitAll(lt_Tasks);

					Assert.AreEqual	(ln_Max	,	lo_OP.Max			,	$"Pool:Use {ln_Cnt}: Max"		);
					Assert.AreEqual	(lo_OP.Count,	lo_OP.ObjectCount	,	$"Pool:Use {ln_Cnt}: Count"	);
					//...............................................
					ln_Cnt	++;

					Console.WriteLine($"MaxOb: {lo_OP.Max.ToString()}"		);
					Console.WriteLine($"Objts: {lo_OP.ObjectCount.ToString()}"	);
					Console.WriteLine($"Count: {lo_OP.Count.ToString()}"				);
					Console.WriteLine($"Loops: {ln_Lop.ToString()}"							);
					Console.WriteLine($"Skips: {ln_Skp.ToString()}"							);
					Console.WriteLine($"Error: {ln_Err.ToString()}"							);

					Console.WriteLine("====");

					for (int i = 0; i < lo_OP.Count; i++)
						{
							TestClass x	= lo_OP.GetObject();
							ln_Tot	+= x.LCount;
							Console.WriteLine( $"{x.LCount.ToString()}/{x.LCount.ToString()}");
						}
						Console.WriteLine("-----");
						Console.WriteLine($"Tot: {ln_Tot.ToString()}");

					Assert.AreEqual	(_N * _O,	ln_Lop					,	$"Pool:Use {ln_Cnt}: Loop"	);
					Assert.AreEqual	(_N * _O,	ln_Skp + ln_Tot	,	$"Pool:Use {ln_Cnt}: Tot"		);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_Pool_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					ObjectPoolx<TestClass> lo_OP	= this._TS.CreateObjectPool<TestClass>( () => new TestClass(), 5);

					Assert.IsNotNull	(			lo_OP							,	$"Pool: {ln_Cnt}: Instantiate");
					Assert.AreEqual		(5	,	lo_OP.Max	,	$"Pool: {ln_Cnt}: Max");
				}

			//===========================================================================================
			#region "Local"

				//-----------------------------------------------------------------------------------------
				private class TestClass : IPoolObject
					{
						internal 	Boolean	CheckedIn	{ get; set; }
						public		string	Prop1			{ get; set; }
						internal	int			Count			{ get; private set; }
						internal  int     LCount		{ get { return this._lt.Count; } }

						private readonly	IList<int>	_lt		= new List<int>();

						public void Run(int I)	{ this.Count ++; this._lt.Add(I);	Thread.Sleep(10); }

						public async Task<bool> ResetAsync()
							{
								bool	lb_Ret	=	await Task<bool>.Run(() => {	this.CheckedIn	= false;
																															this.Prop1			= string.Empty;
																															this.Count			= 0;
																															return	true;										}	).ConfigureAwait(false);
								return	lb_Ret;
							}
			}

			#endregion

		}
}
