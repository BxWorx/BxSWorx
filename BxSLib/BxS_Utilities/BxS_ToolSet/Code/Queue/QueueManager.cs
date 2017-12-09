using System;
using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.Queue
{
	public class QueueManager<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public QueueManager(int NoQueues = 3)
					{
						this._quecount	= NoQueues;
						this.Setup();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private int		_quecount;
				//.................................................
				private readonly Lazy<Queue<T>>	_Q00		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q01		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q02		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q03		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q04		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q05		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q06		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q07		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q08		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q09		= new Lazy<Queue<T>>( new Queue<T>() );
				private readonly Lazy<Queue<T>>	_Q10		= new Lazy<Queue<T>>( new Queue<T>() );
				//private ConcurrentDictionary<int, Queue<T>>	_queue;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int Count	{ get { return this._queue.Count; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Get()
				{
					this._queue.TryDequeue(out T lo_Obj);
					return	lo_Obj	?? null;
				}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add(T item)
				{
					this._queue.Enqueue(item);
				}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				private void Setup()
					{
						if			(this._quecount	< 1)		this._quecount	= 1		;
						else if (this._quecount> 10)		this._quecount	= 10	;
						//.............................................
						this._queue		= new ConcurrentDictionary<int, Queue<T>>();

						for (int i = 0; i < this._quecount; i++)
							{
								this._queue.TryAdd(i, new Queue<T>());
							}
					}

			#endregion

		}
}