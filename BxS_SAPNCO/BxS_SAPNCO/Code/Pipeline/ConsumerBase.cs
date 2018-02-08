using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal abstract class ConsumerBase<T,P> : IConsumer<T>	where T:class
																														where	P:class
		{
			#region "Constructors"

				internal ConsumerBase(	OpEnv<T,P>	OpEnv )
					{
						this._OpEnv	= OpEnv;
						//.................................................
						this.Successful	= new	ConcurrentQueue<T>();
						this.Faulty			= new	ConcurrentQueue<T>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	readonly	OpEnv<T,P>	_OpEnv;

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
						foreach (T lo_WorkItem in this._OpEnv.Queue.GetConsumingEnumerable(this._OpEnv.CT))
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

								if (ln_Int.Equals(this._OpEnv.ProgressInterval))
									{
										ln_Int	= 0;
										this._OpEnv.ProgressHndlr.Report(this._OpEnv.ProgressInfo);
									}
							}
							//...........................................
							if (!ln_Int.Equals(0))	this._OpEnv.ProgressHndlr.Report(this._OpEnv.ProgressInfo);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual bool Execute(T workItem)
					{
						return	false;
					}

			#endregion

		}
}
