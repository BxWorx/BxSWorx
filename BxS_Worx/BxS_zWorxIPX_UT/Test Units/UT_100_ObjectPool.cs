using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.Helpers.ObjectPool;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_ObjectPool
		{
			private	readonly IIPXController	co_Cntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_ObjectPool()
				{
					this.co_Cntlr		= IPXController.Instance;
					Assert.IsNotNull	( this.co_Cntlr	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ObjPool_10_Instantiate()
				{
					ObjectPool<UT_Cls>	lo_OP		= this.co_Cntlr.CreateObjectPool<UT_Cls>( func: ()=> new UT_Cls() );

					Assert.IsNotNull	( lo_OP , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ObjPool_20_PopPush()
				{
					Guid myguid;
					ObjectPool<UT_Cls>	lo_OP		= this.co_Cntlr.CreateObjectPool<UT_Cls>( func: ()=> new UT_Cls() , diagnostics: true );

					using (	UT_Cls x0 = lo_OP.Acquire() )
						{
							x0.State	= false;
							myguid	= x0.Position;
						}

					Thread.Sleep(100);
					Assert.AreEqual	( 1	, lo_OP.Count	, "A" );
					//...............................................
					UT_Cls x1 = lo_OP.Acquire();
					UT_Cls x2 = lo_OP.Acquire();

					Assert.IsTrue		(						x1.State														, "C" );
					Assert.AreEqual	( myguid	, x1.Position													, "0" );
					Assert.AreEqual	( 2				, lo_OP.Diagnostics.InstancesCreated	, "B" );

					Assert.AreNotEqual	( x1.Position	, x2.Position	, "1" );
				}

		//.

		}

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class UT_Cls : PooledObject
		{

			public UT_Cls()
				{
					this.Position = Guid.NewGuid();
				}

			public Guid	Position	{ get; set; }
			public bool State			{ get; set; }

			public bool Reset()
				{
					return	true;
				}

			protected override void OnResetState()
				{
					this.State	= true;
					//base.OnResetState();
				}
		}

	//.

	}
