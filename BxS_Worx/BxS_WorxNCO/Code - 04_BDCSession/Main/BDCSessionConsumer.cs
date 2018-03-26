using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
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
				internal BDCSessionConsumer(	BDCCall_Profile		profile
																		,	BDCCall_Function	function	)
																		//,	BDCCall_Lines			bdcData
																		//, CancellationToken	CT
																		//, BlockingCollection< DTO_BDC_Transaction > queue )
					{
						this._Profile		= profile		;
						this._Func			= function	;
						//this._BDCData		= bdcData		;
						//this._CT				= CT				;
						//this._Queue			= queue			;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCall_Profile		_Profile;
				private readonly	BDCCall_Function	_Func;
				//private readonly	BDCCall_Lines			_BDCData;
				//private	readonly	CancellationToken	_CT;

				//private readonly	BlockingCollection< DTO_BDC_Transaction >	_Queue;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int TransactionsProcessed { get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Consume(	BDCCall_Lines			bdcData
															, CancellationToken	CT
															, BlockingCollection< DTO_BDC_Transaction > queue )
					{
						foreach ( DTO_BDC_Transaction lo_Tran in queue.GetConsumingEnumerable( CT ) )
							{
								bdcData.Reset();
								//.........................................
								this.LoadBDC( bdcData.BDCData , lo_Tran.BDCData );
								this.LoadSPA( bdcData.SPAData , lo_Tran.SPAData );
								//.........................................
								try
									{
										this._Func.Process( bdcData );
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

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSPA(	SMC.IRfcTable SPATable , IList< DTO_BDC_SPA > SPASrce )
					{
						SPATable.Append( SPASrce.Count );

						for ( int i = 0; i < SPASrce.Count; i++ )
							{
								SPATable.CurrentIndex	= i;
								//.........................................
								SPATable.SetValue( this._Profile.SPADat_MID , SPASrce[i].MemoryID		);
								SPATable.SetValue( this._Profile.SPADat_Val , SPASrce[i].MemoryValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBDC(	SMC.IRfcTable BDCTable , IList< DTO_BDC_Data > BDCSrce )
					{
						BDCTable.Append( BDCSrce.Count );

						for ( int i = 0; i < BDCSrce.Count; i++ )
							{
								BDCTable.CurrentIndex	= i;
								//.........................................
								BDCTable.SetValue( this._Profile.BDCDat_Prg , BDCSrce[i].ProgramName	);
								BDCTable.SetValue( this._Profile.BDCDat_Dyn , BDCSrce[i].Dynpro				);
								BDCTable.SetValue( this._Profile.BDCDat_Bgn , BDCSrce[i].Begin				);
								BDCTable.SetValue( this._Profile.BDCDat_Fld , BDCSrce[i].FieldName		);
								BDCTable.SetValue( this._Profile.BDCDat_Val , BDCSrce[i].FieldValue		);
							}
					}

			#endregion

		}
}
