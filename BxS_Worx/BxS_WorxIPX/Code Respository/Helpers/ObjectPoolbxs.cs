using System;
using System.Collections.Concurrent;
using ST	=	System.Timers;
using System.Threading;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Helpers
{
	public class ObjectPoolxxx<T> where T : IPoolObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPool(	Func<T> newFnc
													,	int			startup	= 0
													,	int			max			= 10
													, int			sleep		= 10
													, int     retries	= 10 )
					{
						this.Max				= max			;

						this._NewFnc		= newFnc	?? throw new ArgumentNullException(nameof(newFnc));

						this._Startup		=	startup	;
						this._Sleep			= sleep		;
						this._Retries		= retries	;
						//.............................................
						this._Objects		= new ConcurrentBag<T>();
						this._Lock			= new object();
						this._LiveObjs	= 0;
						//.............................................
						this.Startup();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public int Max					{ get; }
				public int Count				{ get { return	this._LiveObjs					;	} }
				public int ObjectCount	{ get { return	this._Objects.Count	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly ConcurrentBag<T>		_Objects	;
				private readonly Func<T>						_NewFnc		;
				private	readonly object							_Lock			;
				private bool	_Go;

				private	int		_LiveObjs		;
				private	int		_Startup	;
				private	int		_Sleep		;
				private	int		_Retries	;

				//private	ST.Timer	_Timer;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Pop()
					{
						var		lo_Obj	= default(T);
						bool	lb_LT		= false;
						//...............................................
						try
							{
								Monitor.Enter ( this._Lock, ref lb_LT );
								//...............................................
								this._Go		= false;

								do
									{
										if ( this._Objects.TryTake( out lo_Obj ) )
											{
												this._Go	= true;
											}
										else
											{
												if ( this._LiveObjs >= this.Max )
													{
														Monitor.Wait	( this._Lock );
													}
												else
													{
														lo_Obj	= this._NewFnc();
														this._LiveObjs	++;
														this._Go	= true;
													}
											}
									} while ( ! this._Go );
							}
							catch
								{
								}
							finally
								{
									if ( lb_LT )	Monitor.Exit ( this._Lock );
								}
						//...............................................
						return	lo_Obj;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Push(T Object)
					{
						bool lb_Ret		= Object.Reset();
						//.............................................
						if ( lb_Ret )
							{
								this._Objects.Add( Object );
							}
						else
							{
								Interlocked.Decrement(ref this._LiveObjs);
							}

						Monitor.Pulse( this._Lock );
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private	static void TimerHandler( object sender , EventArgs e)
				//	{
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Startup()
					{
						if			(	this._Startup	< 0					)		this._Startup	= 0;
						else if (	this._Startup	> this.Max	)		this._Startup	= this.Max;
						//.............................................
						for (int i = 1; i < this._Startup; i++)
							{
								this._LiveObjs ++;
								this._Objects.Add(this._NewFnc());
							}
							////.............................................
							//this._Timer = new ST.Timer
							//	{
							//			Interval	= this._Sleep
							//		,	AutoReset = true
							//		,	Enabled		= true
							//	};
					}

			#endregion

		}
}