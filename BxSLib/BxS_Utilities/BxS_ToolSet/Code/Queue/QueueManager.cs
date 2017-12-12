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
				private	Dictionary<int, Queue<T>>		_queues;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int QCount { get { return	this._queues.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int GetCount(int Level = -1)
					{
						if (Level == -1)
							{
								int ln_Cnt = 0;
								foreach (KeyValuePair<int, Queue<T>> ls_kvp in this._queues)
									{
										ln_Cnt += ls_kvp.Value.Count;
									}
								return	ln_Cnt;
							}
						//...............................................
						if (this._queues.TryGetValue( this.GetIndex(Level), out Queue<T> lo_Q) )
								return	lo_Q.Count;

						return 0;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Peek(int Level)
					{
						if (this._queues.TryGetValue( this.GetIndex(Level), out Queue<T> lo_Q) )
								return	lo_Q.Peek();

						return null;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Get(int Level)
					{
						if (this._queues.TryGetValue( this.GetIndex(Level), out Queue<T> lo_Q) )
								return	lo_Q.Get();

						return null;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add(int Level, T item)
					{
						if (this._queues.TryGetValue( this.GetIndex(Level), out Queue<T> lo_Q) )
								lo_Q.Add(item);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T GetNext()
					{
						int ln_CurQ	= this.QCount;
						int ln_Trgt	= ln_CurQ;
						bool	lb_Go	= true;
						T		lo_Obj	= null;
						//...............................................
						do
							{
								for (int i = ln_CurQ; i < ln_Trgt; i--)
									{
										lo_Obj	=	this._queues[i].Get();
										if (lo_Obj != null)
											{ lb_Go	= false; break; }
									}
								if (lb_Go) ln_Trgt --;
							} while (lb_Go);
						//...............................................
						return	lo_Obj;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int GetIndex(int Level)
					{
						return	(Level < 0 || Level > this._maxqueues) ? 0 : Level;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Setup()
					{
						if			(this._maxqueues	< 01)		this._maxqueues	= 01	;
						else if (this._maxqueues	> 10)		this._maxqueues	= 10	;
						//.............................................
						this._queues		= new Dictionary<int, Queue<T>>();

						for (int i = 0; i <= this._maxqueues; i++)
							{
								this._queues.TryAdd(i, new Queue<T>());
							}
					}

			#endregion

		}
}