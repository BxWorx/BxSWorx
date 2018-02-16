using System;
using System.Collections.Concurrent;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Pipeline
{
	internal class ConsumerOpEnv<T,P>
		{
			#region "Constructors"

				internal ConsumerOpEnv(	Func<P>								createProgressInfo
															,	IProgress<P>					progressHndlr
															,	CancellationToken			CT
															,	int										queueAddTimeout		= 10
															,	int										progressInterval	= 10 )
					{
						this.CreateProgInfo		= createProgressInfo	;
						this.ProgressHndlr		= progressHndlr				;
						this.CT								= CT									;
						this.ProgressInterval	= progressInterval		;
						this.QueueTimeout			= queueAddTimeout			;
						//.............................................
						this.Queue		= new	BlockingCollection<T>	();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Func< P >								CreateProgInfo	{ get	; }
				internal	BlockingCollection<T>		Queue						{ get	; }
				internal	IProgress<P>						ProgressHndlr		{ get	; }
				internal	CancellationToken				CT							{ get	; }
				//.................................................
				internal	int	QueueTimeout			{ get	; }
				internal	int	ProgressInterval	{ get	; }

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						foreach (T lo_WorkItem in this.Queue.GetConsumingEnumerable())
							{ }
					}

			#endregion

		}
}
