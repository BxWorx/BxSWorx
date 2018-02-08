using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_SAPNCO.Helpers
{
	internal class Pipeline<T,P>	where T:class
																where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline(	OpEnv<T,P>				OpEnv					,
														IConsumerMaker<T>	consumerMaker		)
					{
						this._OpEnv					= OpEnv					;
						this._ConsumerMaker	= consumerMaker	;
						//.............................................
						this._Tasks		= new	List< Task >()						;
						this._Complt	= new ConcurrentQueue< Task	>()	;
						this._Faulty	= new ConcurrentQueue< Task >()	;
						this._Other		= new ConcurrentQueue< Task	>()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	OpEnv<T,P>					_OpEnv					;
				private	readonly	IConsumerMaker<T>		_ConsumerMaker	;
				//.................................................
				private readonly IList< Task >						_Tasks	;

				private readonly ConcurrentQueue< Task >	_Complt	;
				private readonly ConcurrentQueue< Task >	_Faulty	;
				private readonly ConcurrentQueue< Task >	_Other	;
				//.................................................

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int  CompletedCount	{ get { return	this._Complt.Count; } }
				internal int  FaultyCount			{ get { return	this._Faulty.Count; } }
				internal int  OtherCount			{ get { return	this._Other	.Count; } }

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

								IConsumer<T> lo_Consumer	= this._ConsumerMaker.CreateConsumer();
								this._Tasks.Add( Task.Run( () => lo_Consumer.Start() ) );
							}
						//.............................................
						Task lo_Task;

						while (!this._Tasks.Count.Equals(0))
							{
								if (this._OpEnv.CT.IsCancellationRequested)	break;

								lo_Task	= await Task.WhenAny(this._Tasks).ConfigureAwait(false);
								if (this._Tasks.Remove(lo_Task))	ln_Ret++;

											if (lo_Task.Status.Equals(TaskStatus.RanToCompletion)	)		{	this._Complt.Enqueue(lo_Task); }
								else	if (lo_Task.Status.Equals(TaskStatus.Faulted				)	)		{	this._Faulty.Enqueue(lo_Task); }
								else  																													{ this._Other	.Enqueue(lo_Task); }
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
