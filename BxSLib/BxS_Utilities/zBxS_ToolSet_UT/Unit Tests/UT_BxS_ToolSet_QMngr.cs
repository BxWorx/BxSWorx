using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset.Queue;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_QMngr
		{
			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_QMngr_ProcessQ()
				{
					int	ln_Cnt = 0;
					//...............................................
					ln_Cnt ++;

					const int N = 100000;

					var r = new Random();
					var x = new QueueManager<TestClass>(11);
					int q = x.QCount;

					Parallel.For(
												0, N, i =>	{
																			x.Add(	r.Next(q)	,
																							new TestClass() );
																		}
											);

					int ln_Tot	= x.GetCount();

					Assert.AreEqual(N,	ln_Tot,	$"QM:ProcessQ {ln_Cnt}: Add");
					//...............................................
					ln_Tot = 0;
					Parallel.For(	0, q, i =>	{
																			int xx = 0;

																			do
																				{
																					TestClass zz	= x.Get(i);
																					if (zz != null)	xx ++;
																				} while ( x.GetCount(i) > 0 );

																			Interlocked.Add(ref ln_Tot, xx);
																		}
											);

					Assert.AreEqual(N,	ln_Tot,	$"QM:ProcessQ {ln_Cnt}: Use");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_QMngr_Instantiate()
				{
					int	ln_Cnt = 0;
					//...............................................
					ln_Cnt ++;

					var w = new QueueManager<TestClass>(-1);
					var x = new QueueManager<TestClass>();
					var y = new QueueManager<TestClass>(5);
					var z = new QueueManager<TestClass>(11);

					Assert.AreEqual(02	, w.QCount,	$"QM:Inst {ln_Cnt}: Less");
					Assert.AreEqual(04	, x.QCount,	$"QM:Inst {ln_Cnt}: None");
					Assert.AreEqual(06	, y.QCount,	$"QM:Inst {ln_Cnt}: asis");
					Assert.AreEqual(11	, z.QCount,	$"QM:Inst {ln_Cnt}: bigg");
				}

			//===========================================================================================
			#region "Local"

				//-----------------------------------------------------------------------------------------
				internal class TestClass
					{
						internal	int			Count { get; }
						public		string	Prop1 { get; set; }
					}

			#endregion

		}
}
