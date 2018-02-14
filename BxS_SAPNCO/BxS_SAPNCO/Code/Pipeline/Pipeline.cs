using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPNCO.BDCProcess;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_SAPNCO.Pipeline
{
	internal class Pipeline<T,P>	where T:class
																where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline(	PipelineOpEnv<T,P>	OpEnv	)
					{
						this._OpEnv	= OpEnv	;
						//.............................................
						this._Tasks	= new	List< Task< IConsumer<T> > >()	;
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task	>()	;
						this.TasksFaulty		= new ConcurrentQueue< Task >()	;
						this.TasksOther			= new ConcurrentQueue< Task	>()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	PipelineOpEnv<T,P>	_OpEnv;
				//.................................................
				private readonly IList< Task< IConsumer<T> > >	_Tasks	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int  CompletedCount	{ get { return	this.TasksCompleted	.Count; } }
				internal int  FaultyCount			{ get { return	this.TasksFaulty		.Count; } }
				internal int  OtherCount			{ get { return	this.TasksOther			.Count; } }

				internal BlockingCollection< T >	Queue						{ get { return	this._OpEnv.Queue; } }

				internal ConcurrentQueue< Task >	TasksCompleted	{ get; }
				internal ConcurrentQueue< Task >	TasksFaulty			{ get; }
				internal ConcurrentQueue< Task >	TasksOther			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<int> StartAsync()
					{
						int ln_Ret	= 0;
						//.............................................
						//for (int i = 0; i < this._OpEnv.NoOfConsumers; i++)
						for (int i = 0; i < this._OpEnv.Consumers.Count; i++)
							{
								if (this._OpEnv.CT.IsCancellationRequested)		return	0;

								IConsumer<T>	lo_CS	= this._OpEnv.Consumers[i];

								this._Tasks.Add(
									Task<IConsumer<T>>.Run( () =>	{
																									lo_CS.Start();
																									return	lo_CS;
																								}
																				 )
																);
							}
						//.............................................
						Task<IConsumer<T>> lo_Task;

						while (!this._Tasks.Count.Equals(0))
							{
								if (this._OpEnv.CT.IsCancellationRequested)	break;

								lo_Task	= await Task.WhenAny(this._Tasks).ConfigureAwait(false);

								if (this._Tasks.Remove(lo_Task))	ln_Ret++;

											if (lo_Task.Status.Equals(TaskStatus.RanToCompletion)	)		{	this.TasksCompleted.Enqueue(lo_Task); }
								else	if (lo_Task.Status.Equals(TaskStatus.Faulted				)	)		{	this.TasksFaulty.Enqueue(lo_Task); }
								else  																													{ this.TasksOther	.Enqueue(lo_Task); }
							}
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Post( T entry )
					{
						return	this._OpEnv.Queue.TryAdd( entry, this._OpEnv.QueueTimeout, this._OpEnv.CT );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void AddingCompleted()
					{
						this._OpEnv.Queue.CompleteAdding();
					}

			#endregion

		}
}
