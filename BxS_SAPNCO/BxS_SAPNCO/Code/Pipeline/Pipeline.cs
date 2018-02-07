using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_SAPNCO.Helpers
{
	internal class Pipeline<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline(	OperatingEnvironment<T>	OpEnv			,
														Func<IConsumer<T>>			consumer		)
					{
						this._OpEnv	= OpEnv;
						this._Consumer					= consumer					;
						//.............................................
						this._Tasks		= new	List< Task >()					;
						this.ct_Done	= new ConcurrentQueue<T>()		;
						this.ct_Proc	= new ConcurrentQueue<T>()		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	OperatingEnvironment<T>		_OpEnv;
				private Func<IConsumer<T>>			_Consumer	;
				//.................................................
				private IList< Task >						_Tasks		;
				private	ConcurrentQueue<T>			ct_Proc;
				private	ConcurrentQueue<T>			ct_Done;
				//.................................................

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int  Count	{ get { return	this.ct_Proc.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<int> StartAsync( int noOfConsumers = 0 )
					{
						int ln_Ret	= 0;
						//.............................................
						if (noOfConsumers > 0)	this._OpEnv.NoOfConsumers	= noOfConsumers;
						//.............................................
						for (int i = 0; i < this._OpEnv.NoOfConsumers; i++)
							{
								if (this._OpEnv.CT.IsCancellationRequested)		return	0;

								var lo_Cons	= Task.Run( () =>    Consumer() );
								this._Tasks.Add(lo_Cons);
							}
						//.............................................
						Task lo_Task;

						while (!this._Tasks.Count.Equals(0))
							{
								if (this._OpEnv.CT.IsCancellationRequested)	break;

								lo_Task	= await Task.WhenAny(this._Tasks).ConfigureAwait(false);
								if (this._Tasks.Remove(lo_Task))	ln_Ret++;

								if (lo_Task.Status.Equals(TaskStatus.RanToCompletion))
									{
										//this.ct_Done.Enqueue(lo_Task.re);
									}
								else
								{ }
							}
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Post( T entry )
					{
						return	this._OpEnv.Queue.TryAdd( entry, 100, this._OpEnv.CT );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void AddingCompleted()
					{
						this._OpEnv.Queue.CompleteAdding();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				private void Consumer()
					{
						foreach (T lo_WorkItem in this._OpEnv.Queue.GetConsumingEnumerable(this._OpEnv.CT))
							{
								Thread.Sleep(10);
								this.ct_Proc.Enqueue(lo_WorkItem);
							}
					}

			#endregion

		}
}
