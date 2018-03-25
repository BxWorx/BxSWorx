using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxNCO.Helpers.ObjectPool;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_910_Helpers_ObjPool
		{

			private readonly	ObjectPool<UT_Cls>	co_OP;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_910_Helpers_ObjPool()
				{
					this.co_OP	= this.CreateObjectPool<UT_Cls>( MinSize: 3 , MaxSize: 3 , diagnostics: true , autoStart: true , func: ()=> new UT_Cls() );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_10_Instantiate()
				{
					ObjectPool<UT_Cls>	lo_OP		= this.CreateObjectPool<UT_Cls>( func: ()=> new UT_Cls() );
					Assert.IsNotNull	( lo_OP , "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_20_PopPush()
				{
					Guid myguid;
					ObjectPool<UT_Cls>	lo_OP		= this.CreateObjectPool<UT_Cls>( func: ()=> new UT_Cls() , diagnostics: true );

					using (	UT_Cls x0 = lo_OP.Acquire() )
						{
							x0.State	= false;
							myguid	= x0.PoolID;
						}

					Thread.Sleep(100);
					Assert.AreEqual	( 1	, lo_OP.Count	, "A" );
					//...............................................
					UT_Cls x1 = lo_OP.Acquire();
					UT_Cls x2 = lo_OP.Acquire();

					Assert.IsTrue		(						x1.State														, "C" );
					Assert.AreEqual	( myguid	, x1.PoolID													, "0" );
					Assert.AreEqual	( 2				, lo_OP.Diagnostics.InstancesCreated	, "B" );

					Assert.AreNotEqual	( x1.PoolID	, x2.PoolID	, "1" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_30_AutoStart()
				{
					for (int i = 0; i < 10; i++)
						{
							#pragma warning disable RCS1163
							ThreadPool.QueueUserWorkItem( new	WaitCallback( ( o )=> UT_Process() ) );
							#pragma warning restore RCS1163

							Thread.Sleep(100);
						}

					Thread.Sleep(200);

					Assert.AreEqual	( 3	, this.co_OP.Count , "B" );

					Console.WriteLine($"Created:	{this.co_OP.Diagnostics.InstancesCreated}"		);
					Console.WriteLine($"Live:			{this.co_OP.Diagnostics.LiveInstancesCount}"	);
					Console.WriteLine($"Missed:		{this.co_OP.Diagnostics.MissCount}"						);
				}

		//.

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UT_Process()
					{
						using (	UT_Cls x0 = this.co_OP.Acquire() )
							{
								x0.State	= false;
								Thread.Sleep(10);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	ObjectPool<T> CreateObjectPool<T>(	int			MinSize				= 1
																									,	int			MaxSize				= 5
																									, bool		diagnostics		= false
																									, bool		autoStart			= false
																									,	Func<T>	func					= null	)	where T: PooledObject
					{
						return	new ObjectPool<T>( MinSize , MaxSize , diagnostics , autoStart , func );
					}

		//.

		}

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class UT_Cls : PooledObject
		{
			public UT_Cls() : base()
				{
				}

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
