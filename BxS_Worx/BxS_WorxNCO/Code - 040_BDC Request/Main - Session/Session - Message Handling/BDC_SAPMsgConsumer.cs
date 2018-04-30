using System.Threading;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.SAPMsg;

using BxS_WorxUtil.ObjectPool;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_SAPMsgConsumer : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_SAPMsgConsumer( SAPMsg_Function	function )
					{
						this._Func	= function	;
						//.............................................
						this._TallyTotal	= 0	;
						this._TallyRun		= 0	;
						this._TallyOK			= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	SAPMsg_Function		_Func;
				//.................................................
				private	int _TallyTotal	;
				private int _TallyRun		;
				private int _TallyOK		;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TransactionsTotal		{ get	{ return	this._TallyTotal	; } }
				internal	int	TransactionsRun			{ get	{ return	this._TallyRun		; } }
				internal	int	TransactionsOK			{ get	{ return	this._TallyOK			; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Consume(	CancellationToken													CT
															, BlockingCollection< DTO_BDC_Transaction >	queue
															,	SMC.RfcDestination												rfcDestination	)
					{
						int ln_Run	= 0	;
						int ln_Ok		= 0	;
						//.............................................
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								try
									{
										foreach ( DTO_BDC_Msg lo_Msg in lo_Tran.MSGData )
											{
												this._Func.Process( lo_Msg , rfcDestination );
											}
										ln_Ok ++;
									}
								catch
									{	}
								finally
									{
										ln_Run ++;
									}
							}
						//.............................................
						Interlocked.Add			( ref this._TallyTotal	, ln_Run	)	;
						Interlocked.Exchange( ref this._TallyRun		, ln_Run	)	;
						Interlocked.Exchange( ref this._TallyOK			, ln_Ok		)	;
					}

			#endregion

		}
}
