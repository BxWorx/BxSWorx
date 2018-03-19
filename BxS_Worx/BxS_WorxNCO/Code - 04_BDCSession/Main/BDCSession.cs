using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.API.BDC;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	internal class BDCSession : IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession(	IRfcFncController			fncCntlr
														,	DTO_BDC_SessionConfig	config		)
					{
						this._Cntlr_Fnc		= fncCntlr;
						this._OpConfig		= config	;
						//.............................................
						this._Queue		= new	BlockingCollection< DTO_BDC_Trans >();
						this._Tasks		= new List< Task<int> >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRfcFncController											_Cntlr_Fnc;
				private readonly	IList< Task<int> >										_Tasks;
				private readonly	BlockingCollection< DTO_BDC_Trans >		_Queue;
				//.................................................
				private	IConfigSetupDestination		_DestConfig;
				private	DTO_BDC_SessionConfig			_OpConfig;
				private CancellationTokenSource		_CTS;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal ConcurrentQueue< Task< int > >	TasksCompleted	{ get; }
				internal ConcurrentQueue< Task< int >	>	TasksFaulty			{ get; }
				internal ConcurrentQueue< Task< int >	>	TasksOther			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//
				public void ConfigureDestination( IConfigSetupDestination dto )
					{
						this._DestConfig	= dto;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//
				public void ConfigureOperation( DTO_BDC_SessionConfig dto )
					{
						this._OpConfig	= dto;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				// Returns no of tasks that ran
				//
				public async Task< int > Process_SessionAsync( DTO_BDC_Session dto )
					{
						int	ln_Ret	= 0;
						this._CTS		=	new CancellationTokenSource();
						this.SetupDestination();
						//.............................................
						for ( int i = 0; i < this._OpConfig.ConsumersNo; i++ )
							{
								if ( this._CTS.IsCancellationRequested )	break;
								this._Tasks.Add(	Task<int>.Run(	()=> this.Consumer()	)	);
							}
						//.............................................
						if ( this._CTS.IsCancellationRequested )
							{
								this._CTS	= null;
								return	0;
							}
						this.LoadQueue( dto );
						//.............................................
						Task< int > lo_Task;

						while ( ! this._Tasks.Count.Equals(0) )
							{
								if ( this._CTS.IsCancellationRequested )	break;

								lo_Task		= await Task.WhenAny( this._Tasks ).ConfigureAwait( false );

								if ( this._Tasks.Remove( lo_Task ) )	ln_Ret++;

											if ( lo_Task.Status.Equals(TaskStatus.RanToCompletion )	)		{	this.TasksCompleted	.Enqueue(lo_Task); }
								else	if ( lo_Task.Status.Equals(TaskStatus.Faulted					)	)		{	this.TasksFaulty		.Enqueue(lo_Task); }
								else  																														{ this.TasksOther			.Enqueue(lo_Task); }
							}
						//.............................................
						this._CTS		=	null;
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Cancel processing of session
				//
				public void CancelProcessing()
					{
						this._CTS?.Cancel();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupDestination()
					{
						this._Cntlr_Fnc.RfcDestination.LoadConfig( this._DestConfig );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int Consumer()
					{
						BDCCall_Function	lo_Func		= this._Cntlr_Fnc.CreateBDCCallFunction();
						BDCCall_Header		lo_Head		= lo_Func.CreateBDCCallHeader();
						BDCCall_Lines			lo_Line		= lo_Func.CreateBDCCallLines();
						int								ln_Cnt		= 0;
						//.............................................
						foreach ( DTO_BDC_Trans lo_WorkItem in this._Queue.GetConsumingEnumerable( this._CTS.Token ) )
							{
								lo_Line.Reset();
								//.........................................
								lo_Line.BDCData.Append( lo_WorkItem.BDCData.Count );

								for ( int i = 0; i < lo_WorkItem.BDCData.Count; i++ )
									{
										lo_Line.BDCData.CurrentIndex	= i;

										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Prg , lo_WorkItem.BDCData[i].ProgramName );
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Dyn , lo_WorkItem.BDCData[i].Dynpro			);
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Bgn , lo_WorkItem.BDCData[i].Begin				);
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Fld , lo_WorkItem.BDCData[i].FieldName		);
										lo_Line.BDCData.SetValue( lo_Func.MyProfile.Value.BDCDat_Val , lo_WorkItem.BDCData[i].FieldValue	);
									}
								//.........................................
								lo_Line.SPAData.Append( lo_WorkItem.SPAData.Count );

								for ( int i = 0; i < lo_WorkItem.SPAData.Count; i++ )
									{
										lo_Line.SPAData.CurrentIndex	= i;

										lo_Line.SPAData.SetValue( lo_Func.MyProfile.Value.SPADat_MID , lo_WorkItem.SPAData[i].MemoryID		);
										lo_Line.SPAData.SetValue( lo_Func.MyProfile.Value.SPADat_Val , lo_WorkItem.SPAData[i].MemoryValue	);
									}
								//.........................................
								lo_Func.Process( lo_Line );
								ln_Cnt ++;
							}
						//.........................................
						return	ln_Cnt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadQueue( DTO_BDC_Session dto )
					{
						foreach ( KeyValuePair< int , DTO_BDC_Trans > ls_kvp in dto.Transactions )
							{
								this._Queue.Add( ls_kvp.Value );
							}
						this._Queue.CompleteAdding();
					}

			#endregion

		}
}
