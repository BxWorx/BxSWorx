using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal class PipelineOpEnv<T,P>	where T:class
																		where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal PipelineOpEnv(		Func< P >					createProgressInfo
																,	IProgress<P>			progressHndlr
																,	CancellationToken	cancellationToken
																,	int								noOfConsumers			= 01
																,	int								progressInterval	= 10
																,	int								queueAddTimeout		= 10	)
					{
						this.CreateProgressInfo		= createProgressInfo	;
						//.............................................
						this.ProgressHndlr		= progressHndlr			;
						this.CT								= cancellationToken	;
						this.NoOfConsumers		= noOfConsumers			;
						this.ProgressInterval	= progressInterval	;
						this.QueueTimeout			= queueAddTimeout		;
						//.............................................
						this.Queue			= new	BlockingCollection<T>	();
						this.Consumers	= new List< IConsumer<T> >	();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Func< P >									CreateProgressInfo	{ get; }

				internal	BlockingCollection<T>		Queue					{ get; }
				internal	IList<IConsumer<T>>			Consumers			{ get; }

				internal	IProgress<P>						ProgressHndlr	{ get; }
				internal	CancellationToken				CT						{ get; }
				//.................................................
				internal	int	QueueTimeout			{ get;	set;	}
				internal	int	ProgressInterval	{ get;	set;	}
				internal	int	NoOfConsumers			{ get;	set;	}

			#endregion

		}
}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal PipelineOpEnv(		Func<		PipelineOpEnv<T,P>
				//																,	IBDCTranProcessor
				//																,	IConsumer<T>			>	createConsumer
				//												,	Func< P >										createProgressInfo
				//												,	Func<		IBDCProfile
				//																, IBDCTranProcessor	>	createTranProcessor

				//												,	IProgress<P>			progressHndlr
				//												,	CancellationToken	cancellationToken
				//												,	int								noOfConsumers			= 01
				//												,	int								progressInterval	= 10
				//												,	int								queueAddTimeout		= 10	)

						//	this.CreateConsumer				= createConsumer			;
						//this.CreateTranProcessor	= createTranProcessor	;

				//internal Func<	PipelineOpEnv<T,P>
				//							, IBDCTranProcessor
				//							, IConsumer<T>			>	CreateConsumer			{ get; }

				//internal Func<	IBDCProfile
				//							, IBDCTranProcessor	>	CreateTranProcessor	{ get; }

