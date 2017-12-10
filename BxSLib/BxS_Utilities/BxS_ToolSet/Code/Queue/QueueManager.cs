using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.Queue
{
	public class QueueManager<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public QueueManager(int NoQueues = 3)
					{
						this._maxqueues	= NoQueues;
						//.............................................
						this.Setup();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private int		_maxqueues;
				//.................................................
				private	Dictionary<int, Queue<T>>		_queue;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int GetCount(int Level = -1)
					{
					if (Level == -1)
						{
							int ln_Cnt = 0;
							foreach (KeyValuePair<int, Queue<T>> ls_kvp in this._queue)
								{
									ln_Cnt += ls_kvp.Value.Count;
								}
							return	ln_Cnt;
						}
					//...............................................
					int ln_Indx	= (Level < 0 || Level > this._maxqueues) ? 0 : Level;

					if (this._queue.TryGetValue(ln_Indx, out Queue<T> lo_Q) )
							return	lo_Q.Count;

					return 0;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Get(int Level)
				{
					int ln_Indx	= (Level < 0 || Level > this._maxqueues) ? 0 : Level;

					if (this._queue.TryGetValue(ln_Indx, out Queue<T> lo_Q) )
							return	lo_Q.Get();

					return null;
				}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add(int Level, T item)
				{
					int ln_Indx	= (Level < 0 || Level > this._maxqueues) ? 0 : Level;
					//...............................................
					if (this._queue.TryGetValue(ln_Indx, out Queue<T> lo_Q) )
							lo_Q.Add(item);
					//...............................................
				}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Setup()
					{
						if			(this._maxqueues	< 01)		this._maxqueues	= 01	;
						else if (this._maxqueues	> 10)		this._maxqueues	= 10	;
						//.............................................
						this._queue		= new Dictionary<int, Queue<T>>();

						for (int i = 0; i < this._maxqueues; i++)
							{
								this._queue.TryAdd(i, new Queue<T>());
							}
					}

			#endregion

		}
}