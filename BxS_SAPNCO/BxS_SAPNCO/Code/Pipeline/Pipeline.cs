using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_SAPNCO.Pipeline
{
	internal class Pipeline<T,P>	where T:class
																where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline(	CancellationToken	 CT	)
					{
						this._CT				= CT				;
						//.............................................
						this._Consumers	= new	List< IConsumer<T> >				()	;
						this._Tasks			= new	List< Task< IConsumer<T> > >()	;
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task	>()	;
						this.TasksFaulty		= new ConcurrentQueue< Task >()	;
						this.TasksOther			= new ConcurrentQueue< Task	>()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	CancellationToken		_CT	;

				private readonly IList< Task< IConsumer<T> >	>		_Tasks			;
				private readonly IList< IConsumer<T>					>		_Consumers	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int  ConsumerCount		{ get { return	this._Consumers			.Count; } }
				internal int  CompletedCount	{ get { return	this.TasksCompleted	.Count; } }
				internal int  FaultyCount			{ get { return	this.TasksFaulty		.Count; } }
				internal int  OtherCount			{ get { return	this.TasksOther			.Count; } }

				internal ConcurrentQueue< Task >	TasksCompleted	{ get; }
				internal ConcurrentQueue< Task >	TasksFaulty			{ get; }
				internal ConcurrentQueue< Task >	TasksOther			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void AddConsumer( IConsumer<T> consumer)
					{
						this._Consumers.Add(consumer);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<int> StartAsync()
					{
						int ln_Ret	= 0;
						//.............................................
						for (int i = 0; i < this._Consumers.Count; i++)
							{
								if (this._CT.IsCancellationRequested)		return	0;

								IConsumer<T>	lo_CS	= this._Consumers[i];

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
								if (this._CT.IsCancellationRequested)	break;

								lo_Task	= await Task.WhenAny(this._Tasks).ConfigureAwait(false);

								if (this._Tasks.Remove(lo_Task))	ln_Ret++;

											if (lo_Task.Status.Equals(TaskStatus.RanToCompletion)	)		{	this.TasksCompleted.Enqueue(lo_Task); }
								else	if (lo_Task.Status.Equals(TaskStatus.Faulted				)	)		{	this.TasksFaulty.Enqueue(lo_Task);		}
								else  																													{ this.TasksOther	.Enqueue(lo_Task);		}
							}
						//.............................................
						return	ln_Ret;
					}

			#endregion

		}
}
