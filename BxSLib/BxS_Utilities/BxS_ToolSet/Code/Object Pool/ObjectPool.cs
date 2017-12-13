using System;
using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.ObjectPool
{
	public class ObjectPool<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPool(Func<T> newObjFnc					,
													int			maxEntries	= 10		)
					{
						this.MaxEntries		= maxEntries;
						this._NewObjFnc		= newObjFnc	?? throw new ArgumentNullException(nameof(newObjFnc));
						//.............................................
						this._Objects			= new ConcurrentBag<T>();
						this._Lock				= new object();
						this.Count				= 0;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public int MaxEntries		{ get; }
				public int Count				{ get;	private set; }
				public int ObjectCount	{ get { return	this._Objects.Count; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly ConcurrentBag<T>		_Objects		;
				private readonly Func<T>						_NewObjFnc	;
				private	readonly object							_Lock				;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T GetObject()
					{
						if (!this._Objects.TryTake(out T lo_Obj))
							{
								lock (this._Lock)
									{
										if (this.Count < this.MaxEntries)
											{
												lo_Obj	= this._NewObjFnc();
												this.Count ++;
											}
										else	{	lo_Obj	= null;	}
									}
							}
						//...............................................
						return	lo_Obj;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void PutObject(T item)
					{
						this._Objects.Add(item);
					}

			#endregion

		}
}