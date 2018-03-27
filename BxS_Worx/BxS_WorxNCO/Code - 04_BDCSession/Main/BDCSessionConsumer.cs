using System.Threading;
using System.Collections.Concurrent;
//.........................................................
using BxS_WorxNCO.Helpers.ObjectPool;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDCSessionConsumer : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionConsumer(	BDCCall_Function	function
																		,	BDCCall_Lines			bdcData		)
					{
						this._Func			= function	;
						this._BDCData		= bdcData		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDCCall_Function	_Func;
				private readonly	BDCCall_Lines			_BDCData;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TransactionsProcessed		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Consume(	CancellationToken													CT
															, BlockingCollection< DTO_BDC_Transaction >	queue )
					{
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								this._BDCData.Reset();
								//.........................................
								this._BDCData.LoadSPA( lo_Tran.SPAData );
								this._BDCData.LoadBDC( lo_Tran.BDCData );
								//.........................................
								try
									{
										this._Func.Process( this._BDCData );
										this._BDCData.PostProcess();
									}
								catch (System.Exception)
									{
									throw;
									}
								finally
									{
										this.TransactionsProcessed	++;
									}
							}
					}

			#endregion

		}
}
