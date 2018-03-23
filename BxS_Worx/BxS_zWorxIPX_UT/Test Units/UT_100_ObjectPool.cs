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
					ObjectPool<Xxx>	lo_OP		= this.co_Cntlr.CreateObjectPool<Xxx>( func: ()=> new Xxx() );

					Assert.IsNotNull	( lo_OP , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ObjPool_20_PopPush()
				{
					ObjectPool<Xxx>	lo_OP		= this.co_Cntlr.CreateObjectPool<Xxx>( func: ()=> new Xxx() );

					Xxx		x = lo_OP.Acquire();
					Xxx		y = lo_OP.Acquire();

					Assert.AreEqual	( 2 , lo_OP.Count , "X" );
					Assert.AreSame	( x , y						, "Y" );
				}

		//.

		}

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class Xxx : PooledObject
		{
		public int Position { get; set; }

		public bool Reset()
			{
				return	true;
			}
		}

	//.

	}
