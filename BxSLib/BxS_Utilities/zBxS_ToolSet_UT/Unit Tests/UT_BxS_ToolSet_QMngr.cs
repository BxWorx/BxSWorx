using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.Queue;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_ToolSet_UT
{
	[TestClass]
	public class UT_BxS_ToolSet_QMngr
		{
			private const int	_N	= 1000;

			private	ToolSet		_TS	= new ToolSet();

			//...................................................

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_QMngr_Priority()
				{
					int	ln_Ass	= 0;
					int	ln_Cnt	= 0;

					QueueManager<TestClass> lo_QM		=	this._TS.CreateQueueManager<TestClass>();
					//...............................................
					ln_Ass ++;

					for (int i = 0; i < lo_QM.QCount; i++)
						{
							for (int j = 1; j <= 1; j++)
								{
									lo_QM.Add( i, new TestClass(i) );
									ln_Cnt ++;
								}
						}

					Assert.AreEqual(ln_Cnt,	lo_QM.Count(),	$"QM:ProcessQ {ln_Ass}: Add");
					//...............................................
					var				lt_Ls		= new List<TestClass>();
					bool			lb_Ok		= true;
					TestClass	lo_Obj	= null;

					do
						{
							lo_Obj = lo_QM.GetNext();

							if (lo_Obj == null)	lb_Ok = false	;
							else						lt_Ls.Add(lo_Obj)	;

						} while (lb_Ok);

					Assert.AreEqual(ln_Cnt,	lt_Ls.Count,	$"QM:ProcessQ {ln_Ass}: Add");

					Assert.AreEqual(3	,	lt_Ls[0].Index	,	$"QM:ProcessQ {ln_Ass}: Add");
					Assert.AreEqual(2	,	lt_Ls[1].Index	,	$"QM:ProcessQ {ln_Ass}: Add");
					Assert.AreEqual(1	,	lt_Ls[2].Index	,	$"QM:ProcessQ {ln_Ass}: Add");
					Assert.AreEqual(0	,	lt_Ls[3].Index	,	$"QM:ProcessQ {ln_Ass}: Add");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_QMngr_GetNext()
				{
					int	ln_Cnt = 0;
					//...............................................
					ln_Cnt ++;

					QueueManager<TestClass> lo_QM		=	this._TS.CreateQueueManager<TestClass>();

					var r = new Random();
					int q = lo_QM.QCount;

					Parallel.For(
												0, _N, i =>	{
																			int	t	= r.Next(q);
																			lo_QM.Add( t, new TestClass(t) );
																		}
											);

					int ln_Tot	= lo_QM.Count();
					Assert.AreEqual(_N,	ln_Tot,	$"QM:ProcessQ {ln_Cnt}: Add");
					//...............................................
					ln_Tot = 0;
					TestClass lo;

					Parallel.For(	0, q, i =>	{
																			bool lb = true;
																			do
																				{
																					lo = lo_QM.GetNext();
																					if (lo == null) { lb = false;											}
																					else						{ Interlocked.Add(ref ln_Tot, 1); }
																				} while (lb);
																		}
												);

					Assert.AreEqual(_N,	ln_Tot,	$"QM:ProcessQ {ln_Cnt}: Use");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_QMngr_ProcessQ()
				{
					int	ln_Cnt = 0;
					//...............................................
					ln_Cnt ++;

					QueueManager<TestClass> lo_QM		=	this._TS.CreateQueueManager<TestClass>(11);

					var r = new Random();
					int q = lo_QM.QCount;

					Parallel.For(
												0, _N, i =>	{
																			int	t	= r.Next(q);
																			lo_QM.Add( t, new TestClass(t) );
																		}
											);

					int ln_Tot	= lo_QM.Count();

					Assert.AreEqual(_N,	ln_Tot,	$"QM:ProcessQ {ln_Cnt}: Add");
					//...............................................
					ln_Tot = 0;
					Parallel.For(	0, q, i =>	{
																			int xx = 0;

																			do
																				{
																					TestClass zz	= lo_QM.Get(i);
																					if (zz != null)	xx ++;
																				} while ( lo_QM.Count(i) > 0 );

																			Interlocked.Add(ref ln_Tot, xx);
																		}
											);

					Assert.AreEqual(_N,	ln_Tot,	$"QM:ProcessQ {ln_Cnt}: Use");
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_ToolSet_QMngr_Instantiate()
				{
					int	ln_Cnt = 0;
					//...............................................
					ln_Cnt ++;

					QueueManager<TestClass> w		=	this._TS.CreateQueueManager<TestClass>(-1);
					QueueManager<TestClass> x		=	this._TS.CreateQueueManager<TestClass>();
					QueueManager<TestClass> y		=	this._TS.CreateQueueManager<TestClass>(5);
					QueueManager<TestClass> z		=	this._TS.CreateQueueManager<TestClass>(11);

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
						internal TestClass(int x) { this.Index	= x; }
						//.............................................
						internal 	int			Index { get; }
						internal	int			Count { get; }
						public		string	Prop1 { get; set; }
					}

			#endregion

		}
}
