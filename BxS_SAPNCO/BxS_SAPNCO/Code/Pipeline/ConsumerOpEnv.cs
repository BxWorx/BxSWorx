using System;
using System.Collections.Concurrent;
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
																,	CancellationToken	CT
																,	int								progressInterval	= 10 )
					{
						this.CreateProgInfo	= createProgressInfo	;
						//.............................................
						this.ProgressHndlr		= progressHndlr			;
						this.CT								= CT	;
						this.ProgressInterval	= progressInterval	;
						//.............................................
						this.Queue	= new	BlockingCollection<T>	();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	Func< P >								CreateProgInfo	{ get; }
				internal	BlockingCollection<T>		Queue						{ get; }
				internal	IProgress<P>						ProgressHndlr		{ get; }
				internal	CancellationToken				CT							{ get; }
				//.................................................
				internal	int	ProgressInterval	{ get; }

			#endregion

		}
}
