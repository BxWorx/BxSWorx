using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.Queue
{
	public class QueueManager<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public QueueManager(	Func<BxSQueue<T>>	newQueFnc			,
															int								noQueues = 3		)
					{
						this.MaxQueues	= noQueues	;
						this._NewFnc		= newQueFnc	?? throw new ArgumentNullException(nameof(newQueFnc));
						//.............................................
						this.Setup();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Func<BxSQueue<T>>							_NewFnc	;
				private						Dictionary<int, BxSQueue<T>>	_Queues	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int MaxQueues	{ get; private set; }
				public int QCount			{ get { return	this._Queues.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int Count(int Level = -1)
					{
						if (Level < 0)
							{
								int ln_Cnt = 0;
								foreach (KeyValuePair<int, BxSQueue<T>> ls_kvp in this._Queues)
									{
										ln_Cnt += ls_kvp.Value.Count;
									}
								return	ln_Cnt;
							}
						//...............................................
						if ( this._Queues.TryGetValue( this.GetIndex(Level), out BxSQueue<T> lo_Q ) )
								return	lo_Q.Count;
						//...............................................
						return 0;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Peek(int Level)
					{
						if ( this._Queues.TryGetValue( this.GetIndex(Level), out BxSQueue<T> lo_Q ) )
								return	lo_Q.Peek();

						return null;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Get(int Level)
					{
						if ( this._Queues.TryGetValue( this.GetIndex(Level), out BxSQueue<T> lo_Q ) )
								return	lo_Q.Get();

						return null;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Add(int Level, T item)
					{
						if ( this._Queues.TryGetValue( this.GetIndex(Level), out BxSQueue<T> lo_Q ) )
								lo_Q.Add(item);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T GetNext()
					{
						int ln_CurQ	= this.QCount - 1;
						int ln_Trgt	= ln_CurQ;
						bool	lb_Go	= true;
						T		lo_Obj	= null;

						//...............................................
						// Process algorithm: que 3, 3/2, 3/2/1, 3/2/1/0
						// If entry found, that one is returned and loop
						// is stopped.
						do
							{
								for ( int i = ln_CurQ; i >= ln_Trgt; i-- )
									{
										lo_Obj	=	this._Queues[i].Get();
										if (lo_Obj != null)
											{ lb_Go	= false; break; }
									}

								if (lb_Go)
									{
										ln_Trgt --;
										if (ln_Trgt < 0) lb_Go	= false;
									}
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
						return	(			Level < 0
											||	Level > this.MaxQueues	) ? 0 : Level;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Setup()
					{
						if			(this.MaxQueues	< 01)		this.MaxQueues	= 01	;
						else if (this.MaxQueues	> 10)		this.MaxQueues	= 10	;
						//.............................................
						this._Queues	= new Dictionary<	int, BxSQueue<T> >();

						for (int i = 0; i <= this.MaxQueues; i++)
							{
								this._Queues.Add(	i,	this._NewFnc() );
							}
					}

			#endregion

		}
}