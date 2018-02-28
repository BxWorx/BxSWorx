using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_WorxUtils.Pipeline
{
	internal class Pipeline<T,P>	where T:class
																where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline(	CancellationToken	 CT	)
					{
						this._CT	= CT	;
						//.............................................
						this._Consumers	= new	List< IConsumer<T> >				()	;
						this._Tasks			= new	List< Task< IConsumer<T> > >()	;
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task< IConsumer<T> >>()	;
						this.TasksFaulty		= new ConcurrentQueue< Task< IConsumer<T> >>()	;
						this.TasksOther			= new ConcurrentQueue< Task< IConsumer<T> >>()	;
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

				internal int  TasksCompletedCount		{ get { return	this.TasksCompleted	.Count; } }
				internal int  TasksFaultyCount			{ get { return	this.TasksFaulty		.Count; } }
				internal int  TasksOtherCount				{ get { return	this.TasksOther			.Count; } }

				internal int  JobsCompletedSuccesfulCount		{ get { return	this.JobsCompletedSuccessful	(); } }
				internal int  JobsCompletedFaultyCount			{ get { return	this.JobsCompletedFaulty			(); } }
				internal int  JobsFaultyCount								{ get { return	this.JobsFaulty								(); } }
				internal int  JobsOtherCount								{ get { return	this.JobsOther								(); } }

				internal ConcurrentQueue< Task< IConsumer<T> > >	TasksCompleted	{ get; }
				internal ConcurrentQueue< Task< IConsumer<T> > >	TasksFaulty			{ get; }
				internal ConcurrentQueue< Task< IConsumer<T> > >	TasksOther			{ get; }

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

											if (lo_Task.Status.Equals(TaskStatus.RanToCompletion)	)		{	this.TasksCompleted	.Enqueue(lo_Task);	}
								else	if (lo_Task.Status.Equals(TaskStatus.Faulted				)	)		{	this.TasksFaulty		.Enqueue(lo_Task);	}
								else  																													{ this.TasksOther			.Enqueue(lo_Task);	}
							}
						//.............................................
						return	ln_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int JobsCompletedSuccessful()
					{
						int x = 0;
						//.............................................
						foreach (Task< IConsumer<T> > item in this.TasksCompleted)
							{
								x +=	item.Result.Successful.Count;
							}
						//.............................................
						return	x;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int JobsCompletedFaulty()
					{
						int x = 0;
						//.............................................
						foreach (Task< IConsumer<T> > item in this.TasksCompleted)
							{
								x +=	item.Result.Faulty.Count;
							}
						//.............................................
						return	x;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int JobsFaulty()
					{
						int x = 0;
						//.............................................
						foreach (Task< IConsumer<T> > item in this.TasksCompleted)
							{
								x +=	item.Result.Successful	.Count;
								x +=	item.Result.Faulty			.Count;
							}
						//.............................................
						return	x;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int JobsOther()
					{
						int x = 0;
						//.............................................
						foreach (Task< IConsumer<T> > item in this.TasksCompleted)
							{
								x +=	item.Result.Successful	.Count;
								x +=	item.Result.Faulty			.Count;
							}
						//.............................................
						return	x;
					}

			#endregion

		}
}
