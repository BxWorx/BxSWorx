using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Exposed: Pipeline"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private OpEnv<T,P>	CreateOperatingEnvironment<T,P>(	P									progressInfo						,
																																						int								noConsumers				= 1		,
																																						int								progressInterval	= 10	,
																																						int								queueAddTimeout		= 10		)	where T:class
																																																													where P:class
					{
						IProgress<P>	lo_PH	= new Progress<P>()				;
						var						lo_CT	= new CancellationToken()	;
						//.............................................
						return	new OpEnv<T,P>(	lo_PH							,
																										progressInfo			,
																										lo_CT							,
																										noConsumers				,
																										progressInterval	,
																										queueAddTimeout			);
					}

			#endregion

		}
}
