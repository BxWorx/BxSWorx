using System;
using System.Collections.Concurrent;
using System.Threading;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	internal class OperatingEnvironment<T>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal OperatingEnvironment(	IProgress<int>			progress					,
																				CancellationToken		cancellationToken	,
																				BDC2RfcParser				parser						,
																				int									noOfConsumers			= 01	,
																				int									interval					= 10		)
					{
						this.Progress					= progress					;
						this.CT								= cancellationToken	;
						this.Parser						= parser						;
						this.NoOfConsumers		= noOfConsumers			;
						this.ProgressInterval	= interval					;
						//.............................................
						this.Queue	= new	BlockingCollection<T>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	BlockingCollection<T>		Queue			{ get; }
				internal	BDC2RfcParser						Parser		{ get; }
				internal	IProgress<int>					Progress	{ get; }
				internal	CancellationToken				CT				{ get; }
				//.................................................
				internal	int	ProgressInterval	{ get;				}
				internal	int	NoOfConsumers			{ get;	set;	}

			#endregion

		}
}
