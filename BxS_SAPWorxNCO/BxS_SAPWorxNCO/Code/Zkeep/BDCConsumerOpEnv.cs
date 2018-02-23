using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCConsumerOpEnv<T,P>	: ConsumerOpEnv< T,P >  where T:DTO_RFCTran
																																		where	P:DTO_ProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCConsumerOpEnv(	Func<P>								createProgressInfo
																	,	BDCCallTranProcessor	processor
																	,	IProgress<P>					progressHndlr
																	,	CancellationToken			CT
																	,	int										queueAddTimeout		= 10
																	,	int										progressInterval	= 10 )	: base()
					{
						this._Processor					= processor						;
						this._CreateProgInfo		= createProgressInfo	;
						this._ProgressHndlr			= progressHndlr				;
						this._CT								= CT									;
						this._ProgressInterval	= progressInterval		;
						this._QueueTimeout			= queueAddTimeout			;
						//.............................................
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal BDCCallTranProcessor		_Processor;

			#endregion

		}
}
