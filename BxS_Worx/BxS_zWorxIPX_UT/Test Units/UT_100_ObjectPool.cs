using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.Helpers;
using System.Threading.Tasks;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_ObjectPool
		{
			IIPXController	co_Cntlr;

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
					ObjectPool<xx>	lo_OP		= this.co_Cntlr.CreateObjectPool<xx>( ()=> new xx() );

					Assert.IsNotNull	( lo_OP , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_ObjPool_20_PopPush()
				{
					ObjectPool<xx>	lo_OP		= this.co_Cntlr.CreateObjectPool<xx>( ()=> new xx() );

					xx		x = lo_OP.Pop();
					bool	r = lo_OP.Push( x );
					xx		y = lo_OP.Pop();

					Assert.IsTrue		(			r						, "R" );
					Assert.AreEqual	( 1 , lo_OP.Count , "X" );
					Assert.AreSame	( x , y						, "Y" );
				}

		//.

		}

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class xx : IPoolObject
		{

		public int Position { get; set; }

		public bool Reset()
			{
				return	true;
			}
		}

	//.

	}
