using System;
using System.Collections.Concurrent;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Pipeline
{
	internal interface IConsumerOpEnv<T,P>	where T:class
																					where	P:class
		{
			#region "Properties"

				Func< P >								CreateProgInfo	{ get; }
				BlockingCollection<T>		Queue						{ get; }
				IProgress<P>						ProgressHndlr		{ get; }
				CancellationToken				CT							{ get; }
				//.................................................
				int	QueueTimeout			{ get; }
				int	ProgressInterval	{ get; }

			#endregion

		}
}
