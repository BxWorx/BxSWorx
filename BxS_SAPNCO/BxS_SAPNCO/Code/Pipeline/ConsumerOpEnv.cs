using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Pipeline
{
	internal class ConsumerOpEnv<T,P>	where T:class
																		where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ConsumerOpEnv(		Func< P >					createProgressInfo
																,	IProgress<P>			progressHndlr
																,	CancellationToken	cancellationToken
																,	int								progressInterval	= 10 )
					{
						this.CreateProgressInfo		= createProgressInfo	;
						//.............................................
						this.ProgressHndlr		= progressHndlr			;
						this.CT								= cancellationToken	;
						this.ProgressInterval	= progressInterval	;
						//.............................................
						this.Queue			= new	BlockingCollection<T>	();
						this.Consumers	= new List< IConsumer<T> >	();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Func< P >								CreateProgressInfo	{ get; }

				internal	BlockingCollection<T>		Queue					{ get; }
				internal	IList<IConsumer<T>>			Consumers			{ get; }

				internal	IProgress<P>						ProgressHndlr	{ get; }
				internal	CancellationToken				CT						{ get; }
				//.................................................
				internal	int	ProgressInterval	{ get; }

			#endregion

		}
}
