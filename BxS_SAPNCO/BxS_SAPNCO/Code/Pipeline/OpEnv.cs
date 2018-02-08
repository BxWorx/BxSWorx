using System;
using System.Collections.Concurrent;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal class OpEnv<T,P>	where T:class
														where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal OpEnv(	IProgress<P>				progressHndlr			,
												P										progressInfo			,
												CancellationToken		cancellationToken	,
												int									noOfConsumers			= 01	,
												int									interval					= 10	,
												int									queueAddTimeout		= 10		)
					{
						this.ProgressHndlr		= progressHndlr			;
						this.ProgressInfo			= progressInfo			;
						this.CT								= cancellationToken	;
						this.NoOfConsumers		= noOfConsumers			;
						this.ProgressInterval	= interval					;
						this.QueueTimeout			= queueAddTimeout		;
						//.............................................
						this.Queue	= new	BlockingCollection<T>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	BlockingCollection<T>		Queue					{ get; }
				internal	IProgress<P>						ProgressHndlr	{ get; }
				internal	CancellationToken				CT						{ get; }
				internal	P												ProgressInfo	{ get; }
				//.................................................
				internal	int	QueueTimeout			{ get;				}
				internal	int	ProgressInterval	{ get;				}
				internal	int	NoOfConsumers			{ get;	set;	}

			#endregion

		}
}
