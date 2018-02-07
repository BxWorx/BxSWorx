using System;
using System.Collections.Concurrent;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal abstract class ConsumerBase<T> : IConsumer<T>
		{
			#region "Constructors"

				internal ConsumerBase(	BlockingCollection<T>		queue			,
																IProgress<int>					progress	,
																CancellationToken				CT				,
																int											interval	= 10	)
					{
						this._Queue			= queue			;
						this._Progress	= progress	;
						this._CT				= CT				;
						this._Interval	= interval	;
						//.................................................
						this.Successful	= new	ConcurrentQueue<T>();
						this.Faulty			= new	ConcurrentQueue<T>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly BlockingCollection<T>	_Queue			;
				private readonly IProgress<int>					_Progress		;
				private readonly int										_Interval		;

				private CancellationToken		_CT	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int TotalProcessed	{ get { return	this.Successful.Count	+ this.Faulty.Count; } }
				//.................................................
				public	ConcurrentQueue<T>	Successful	{ get; }
				public	ConcurrentQueue<T>	Faulty			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Start()
					{
						int	ln_Cnt	= 0;
						int ln_Int	= 0;
						//.............................................
						foreach (T lo_WorkItem in this._Queue.GetConsumingEnumerable(this._CT))
							{
								if (this.Execute(lo_WorkItem))
									{
										this.Successful.Enqueue(lo_WorkItem);
									}
								else
									{
										this.Faulty.Enqueue(lo_WorkItem);
									}
								//.........................................
								ln_Cnt	++;
								ln_Int	++;

								if (ln_Int.Equals(this._Interval))
									{
										ln_Int	= 0;
										this._Progress.Report(ln_Cnt);
									}
							}
							//...........................................
							if (!ln_Int.Equals(0))	this._Progress.Report(ln_Cnt);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual bool Execute(T workItem)
					{
						return	false;
					}

			#endregion

		}
}
