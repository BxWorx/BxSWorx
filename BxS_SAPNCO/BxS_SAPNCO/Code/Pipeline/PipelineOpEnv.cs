using System;
using System.Collections.Concurrent;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal class PipelineOpEnv<T,P>	where T:class
																		where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal PipelineOpEnv( Func< PipelineOpEnv<T,P>, IConsumer<T> >	createConsumer			,
																Func< P >																	createProgressInfo	,

																IProgress<P>				progressHndlr			,
																CancellationToken		cancellationToken	,
																int									noOfConsumers			= 01	,
																int									interval					= 10	,
																int									queueAddTimeout		= 10		)
					{
						this.CreateConsumer			= createConsumer			;
						this.CreateProgressInfo	= createProgressInfo	;
						//.............................................
						this.ProgressHndlr		= progressHndlr			;
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

				internal Func< PipelineOpEnv<T,P>, IConsumer<T> >		CreateConsumer			{ get; }
				internal Func< P >																	CreateProgressInfo	{ get; }

				internal	BlockingCollection<T>		Queue					{ get; }
				internal	IProgress<P>						ProgressHndlr	{ get; }
				internal	CancellationToken				CT						{ get; }
				//.................................................
				internal	int	QueueTimeout			{ get;				}
				internal	int	ProgressInterval	{ get;				}
				internal	int	NoOfConsumers			{ get;	set;	}

			#endregion

		}
}
