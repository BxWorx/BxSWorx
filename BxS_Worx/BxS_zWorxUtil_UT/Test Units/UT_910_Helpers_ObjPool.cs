using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxNCO.Helpers.ObjectPool;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_910_Helpers_ObjPool
		{
			private	readonly	CancellationTokenSource		co_CTS		;
			private static		Barrier										co_Bar		;

			private readonly	ObjectPool<UT_Cls>				co_OPStd	;
			private readonly	ObjectPool<UT_Cls>				co_OPLtd	;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_910_Helpers_ObjPool()
				{
					co_Bar	= new	Barrier( 20 );

					this.co_CTS			= new CancellationTokenSource();

					this.co_OPStd		= this.CreateObjectPool<UT_Cls>(	MinSize:			3
																													, MaxSize:			5
																													, diagnostics:	true
																													, autoStart:		true
																													, func:					()=> new UT_Cls() );

					this.co_OPLtd		= this.CreateObjectPool<UT_Cls>(	MinSize:			3
																													,	MaxSize:			5
																													,	limiterOn:		true
																													, diagnostics:	true
																													, autoStart:		true
																													, func:					()=> new UT_Cls()	);
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_10_Instantiate()
				{
					ObjectPool<UT_Cls>	lo_OP		= this.CreateObjectPool<UT_Cls>( MinSize: 6 ,  autoStart: true ,  func: ()=> new UT_Cls() );
					Assert.IsNotNull	( lo_OP , "" );
					Thread.Sleep(100);
					Assert.AreEqual	( lo_OP.MinPoolSize	, lo_OP.Count	, "A" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_20_PopPush()
				{
					Guid myguid;
					var	lo_OP		= new ObjectPool<UT_Cls>();

					using (	UT_Cls xx = lo_OP.Acquire() )
						{
							xx.State	= false;
							myguid	= xx.PoolID;
						}

					Thread.Sleep(100);
					Assert.AreEqual	( 1	, lo_OP.Count	, "A" );
					//...............................................
					UT_Cls x1 = lo_OP.Acquire();
					UT_Cls x2 = lo_OP.Acquire();

					for (int i = 0; i < 8; i++)
						{
							#pragma warning disable RCS1163

							ThreadPool.QueueUserWorkItem(
								new	WaitCallback(
									(o)=> {
													using (	UT_Cls zz = lo_OP.Acquire() )
														{
															Thread.Sleep(50);
														}
												} ) );

							#pragma warning restore RCS1163
						}

					Thread.Sleep(200);

					Assert.IsTrue		(						x1.State														, "C" );
					Assert.AreEqual	( myguid	, x1.PoolID														, "0" );
					Assert.AreEqual	( 10			, lo_OP.Diagnostics.InstancesCreated	, "B" );

					Assert.AreNotEqual	( x1.PoolID	, x2.PoolID		, "1" );
					Assert.AreEqual			( 3					, lo_OP.Count	, "@"	);
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_30_STDProdSlow()
				{
					this.UT_FireSTD( 20 , 100 );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_32_STDProdFast()
				{
					this.UT_FireSTD( 20 , 10 );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_40_LTDProdSlow()
				{
					this.UT_FireLTD( 30 , 100 );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_910_ObjPool_42_LTDProdFast()
				{
					this.UT_FireLTD( 43 , 10 );
				}

		//.

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UT_ProcessLtd()
					{
						using (	UT_Cls x0 = this.co_OPLtd.Acquire() )
							{
								x0.State	= false;
								Thread.Sleep(50);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UT_ProcessStd()
					{
						using (	UT_Cls x0 = this.co_OPStd.Acquire() )
							{
								x0.State	= false;
								Thread.Sleep(50);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	ObjectPool<T> CreateObjectPool<T>(	int			MinSize				= 1
																									,	int			MaxSize				= 5
																									, int			MaxIdleTime		= 100
																									, bool		limiterOn			= false
																									, bool		diagnostics		= false
																									, bool		autoStart			= false
																									,	Func<T>	func					= null	)	where T: PooledObject
					{
						ObjectPoolConfig<T> lo_Cfg	= ObjectPoolFactory.CreateConfig<T>( func );

						lo_Cfg.ActivateDiagnostics	= diagnostics;
						lo_Cfg.AutoStartup					= autoStart;
						lo_Cfg.MaxIdleTime					= MaxIdleTime;
						lo_Cfg.MinimumPoolSize			= MinSize;
						lo_Cfg.MaximumPoolSize			=	MaxSize;
						lo_Cfg.Throttled						= limiterOn;

						var	lo_OPL	=	new ObjectPool<T>();
						lo_OPL.ConfigurePool(lo_Cfg);

						return	lo_OPL;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UT_FireLTD( int Qty , int sleep )
					{
						for (int i = 0; i < Qty; i++)
							{
								#pragma warning disable RCS1163
								ThreadPool.QueueUserWorkItem( new	WaitCallback( ( o )=> UT_ProcessLtd() ) );
								#pragma warning restore RCS1163

								Thread.Sleep(sleep);
							}
						Thread.Sleep(200);
						//.............................................
						Console.WriteLine($"Created		:	{	this.co_OPLtd.Diagnostics.InstancesCreated		}"	);
						Console.WriteLine($"Live			:	{	this.co_OPLtd.Diagnostics.LiveInstancesCount	}"	);
						Console.WriteLine($"Missed		:	{	this.co_OPLtd.Diagnostics.MissCount						}"	);
						Console.WriteLine($"WaitFor		:	{	this.co_OPLtd.Diagnostics.HitAfterWaitCount		}"	);
						Console.WriteLine($"Returned	:	{	this.co_OPLtd.Diagnostics.ReturnedCount				}"	);
						Console.WriteLine($"Overflow	:	{	this.co_OPLtd.Diagnostics.OverflowCount				}"	);
						Console.WriteLine($"Pool Size	:	{	this.co_OPLtd.Count														}"	);
						Console.WriteLine("=================================================");

						foreach (UT_Cls item in this.co_OPLtd.Pool)
							{
								Console.WriteLine($"Pool:  ID: {item.PoolID}	/	{item.Count.ToString()}"	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void UT_FireSTD( int Qty , int sleep )
					{
						for (int i = 0; i < Qty; i++)
							{
								#pragma warning disable RCS1163
								ThreadPool.QueueUserWorkItem( new	WaitCallback( ( o )=> UT_ProcessStd() ) );
								#pragma warning restore RCS1163

								Thread.Sleep(sleep);
							}
						Thread.Sleep(200);
						//.............................................
						Console.WriteLine($"Created		:	{	this.co_OPStd.Diagnostics.InstancesCreated		}"	);
						Console.WriteLine($"Live			:	{	this.co_OPStd.Diagnostics.LiveInstancesCount	}"	);
						Console.WriteLine($"Missed		:	{	this.co_OPStd.Diagnostics.MissCount						}"	);
						Console.WriteLine($"WaitFor		:	{	this.co_OPStd.Diagnostics.HitAfterWaitCount		}"	);
						Console.WriteLine($"Returned	:	{	this.co_OPStd.Diagnostics.ReturnedCount				}"	);
						Console.WriteLine($"Overflow	:	{	this.co_OPStd.Diagnostics.OverflowCount				}"	);
						Console.WriteLine($"Pool Size	:	{	this.co_OPStd.Count														}"	);
						Console.WriteLine("=================================================");

						foreach (UT_Cls item in this.co_OPStd.Pool)
							{
								Console.WriteLine($"Pool:  ID: {item.PoolID}	/	{item.Count.ToString()}"	);
							}
					}

		//.

		}

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class UT_Cls : PooledObject
		{
			public UT_Cls() : base()
				{
				}

			public int Count {get; private set; }

			public bool State			{ get; set; }

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			protected override void OnResetState()
				{
					this.Count ++;
					this.State	= true;
					//base.OnResetState();
				}

			internal  bool Reset()
				{
					return	true;
				}
		}

	//.

	}
