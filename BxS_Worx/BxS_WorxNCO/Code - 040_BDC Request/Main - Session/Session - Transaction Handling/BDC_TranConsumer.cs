using System;
using System.Threading;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;

using BxS_WorxUtil.ObjectPool;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_TranConsumer : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_TranConsumer( BDC_Function	function )
					{
						this._Func	= function	;
						//.............................................
						this._BDCHead	= new	Lazy< BDC_Header >	(	()=>	this._Func.MyProfile.Value.CreateBDCHeader() , cz_LM );
						this._BDCData	= new	Lazy< BDC_Data >		(	()=>	this._Func.MyProfile.Value.CreateBDCData	() , cz_LM );
						//.............................................
						this._TallyTotal	= 0	;
						this._TallyRun		= 0	;
						this._TallyOK			= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDC_Function	_Func;
				//.................................................
				private	readonly	Lazy< BDC_Header >	_BDCHead;
				private	readonly	Lazy< BDC_Data >		_BDCData;
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
				internal void Consume(	DTO_BDC_Header														header
															,	CancellationToken													CT
															, BlockingCollection< DTO_BDC_Transaction >	queue
															,	SMC.RfcDestination												rfcDestination	)
					{
						int ln_Run	= 0	;
						int ln_Ok		= 0	;

						this._BDCHead.Value.Load( header );
						//.............................................
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								try
									{
										this._BDCData.Value.Reset();
										//.........................................
										this._BDCData.Value.LoadSPA( lo_Tran.SPAData );
										this._BDCData.Value.LoadBDC( lo_Tran.BDCData );
										//.........................................
										this._Func.Config( this._BDCHead.Value );
										this._Func.Process( this._BDCData.Value , rfcDestination );
										//.........................................
										this._BDCData.Value.LoadMsg( lo_Tran.MSGData );
										this._BDCData.Value.PostProcess();
										//.........................................
										lo_Tran.Successful	= true;
										ln_Ok ++;
									}
								catch
									{
										lo_Tran.Successful	= false;
									}
								finally
									{
										lo_Tran.Processed	= true;
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
