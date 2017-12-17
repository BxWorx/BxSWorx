using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.ObjectPool
{
	public class ObjectPool<T> where T : IPoolObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPool(Func<T> newObjFnc		,
													int			max			= 10	,
													int			startup	= 5			)
					{
						this.Max					= max;
						this._Startup			=	startup;
						this._NewObjFnc		= newObjFnc	?? throw new ArgumentNullException(nameof(newObjFnc));
						//.............................................
						this._Objects			= new ConcurrentBag<T>();
						this._Lock				= new object();
						this._Count				= 0;
						//.............................................
						this.Startup();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public int Max					{ get; }
				public int Count				{ get { return	this._Count					;	} }
				public int ObjectCount	{ get { return	this._Objects.Count	; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly ConcurrentBag<T>		_Objects		;
				private readonly Func<T>						_NewObjFnc	;
				private	readonly object							_Lock				;
				private					 int								_Count			;
				private					 int								_Startup		;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Acquire()
					{
						if (!this._Objects.TryTake(out T lo_Obj))
							{
								lock (this._Lock)
									{
										if (this.Count < this.Max)
											{
												lo_Obj	= this._NewObjFnc();
												this._Count ++;
											}
										else
											{
												lo_Obj	= default(T);
											}
									}
							}
						//...............................................
						return	lo_Obj;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<bool> ReturnAsync(T Object)
					{
						bool	lb_Ret = await Task.Run( () => Object.ResetAsync() ).ConfigureAwait(false);
						//.............................................
						if (lb_Ret)		this._Objects.Add(Object);
						else					Interlocked.Decrement(ref this._Count);
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Startup()
					{
						if			(this._Startup	< 0)					this._Startup	= 0;
						else if (this._Startup	> this.Max)		this._Startup	= this.Max;
						//.............................................
						for (int i = 1; i <= this._Startup; i++)
							{
								this._Objects.Add(this._NewObjFnc());
								this._Count ++;
							}
					}

			#endregion

		}
}