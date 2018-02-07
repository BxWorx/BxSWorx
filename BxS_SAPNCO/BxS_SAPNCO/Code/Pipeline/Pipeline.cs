﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••

namespace BxS_SAPNCO.API
{
	internal class Pipeline<T> where T : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Pipeline(	IProgress<int>		progress							,
														CancellationToken	cancellationToken			,
														int								noOfConsumers	= 1				)
					{
						this.co_Progress		= progress					;
						this.co_CT					= cancellationToken	;
						this.NoOfConsumers	= noOfConsumers			;
						//.............................................
						this.ct_Tasks		= new	List< Task >();
						this.co_Q				= new	BlockingCollection<T>();
						this.ct_Proc		= new ConcurrentQueue<T>();
						//.............................................
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private IList< Task >						ct_Tasks;
				private BlockingCollection<T>		co_Q;
				private IProgress<int>					co_Progress;
				private CancellationToken				co_CT;
				//.................................................
				private	ConcurrentQueue<T>			ct_Proc;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal int	NoOfConsumers { get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<int> StartAsync(int noOfConsumers = 0)
					{
						int ln_Ret	= 0;
						//.............................................
						if (noOfConsumers > 0)	this.NoOfConsumers	= noOfConsumers;
						//.............................................
						for (int i = 0; i < this.NoOfConsumers; i++)
							{
								if (this.co_CT.IsCancellationRequested)		return	0;

								var lo_Cons	= Task.Run(() => Consumer() );
								this.ct_Tasks.Add(lo_Cons);
							}
						//.............................................
						Task lo_Task;

						while (			!this.ct_Tasks.Count.Equals(0)
										&&	!this.co_CT.IsCancellationRequested	)
							{
								lo_Task	= await Task.WhenAny(this.ct_Tasks).ConfigureAwait(false);
								if (this.ct_Tasks.Remove(lo_Task))	ln_Ret++;
							}
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Post( T entry)
					{
						return	this.co_Q.TryAdd( entry, 100, this.co_CT );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Complete()
					{
						this.co_Q.CompleteAdding();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				private void Consumer()
					{
						foreach (T lo_WorkItem in this.co_Q.GetConsumingEnumerable(this.co_CT))
							{
								Thread.Sleep(1000);
								this.ct_Proc.Enqueue(lo_WorkItem);
							}
					}

			#endregion

		}
}
