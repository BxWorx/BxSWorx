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
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task< int > >();
						this.TasksFaulty		= new ConcurrentQueue< Task< int > >();
						this.TasksOther			= new ConcurrentQueue< Task< int > >();
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
				// Returns no of Transactions processesed
				//
				public async Task< int > Process_SessionAsync( DTO_BDC_Session dto )
					{
						int	ln_Tsk	= 0;
						int	ln_Ret	= 0;
						this._CTS		=	new CancellationTokenSource();
						this.SetupDestination();
						//.............................................
						for ( int i = 0; i < this._OpConfig.ConsumersNo; i++ )
							{
								if ( this._CTS.IsCancellationRequested )	break;
								this._Tasks.Add( Task<int>.Run(	()=> this.Consumer( dto.SessionHeader ) ) );
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

								if ( this._Tasks.Remove( lo_Task ) )	ln_Tsk ++;

											if ( lo_Task.Status.Equals(TaskStatus.RanToCompletion )	)		{	this.TasksCompleted	.Enqueue(lo_Task); }
								else	if ( lo_Task.Status.Equals(TaskStatus.Faulted					)	)		{	this.TasksFaulty		.Enqueue(lo_Task); }
								else  																														{ this.TasksOther			.Enqueue(lo_Task); }

								ln_Ret	+= lo_Task.Result;
							}

						this._CTS		=	null;
						//.............................................
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
						this._Cntlr_Fnc.RfcDestination.LoadConfig	( this._DestConfig );
						this._Cntlr_Fnc.RegisterBDCCallProfile		( true );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int Consumer( DTO_BDC_Header dtoHead  )
					{
						int								ln_Cnt		= 0;
						BDCCall_Function	lo_Func		= this._Cntlr_Fnc.CreateBDCCallFunction();
						BDCCall_Header		lo_Head		= lo_Func.CreateBDCCallHeader( true );
						BDCCall_Lines			lo_Line		= lo_Func.CreateBDCCallLines();
						//.............................................
						BDCCall_Profile lo_Prof = lo_Func.MyProfile.Value;

						lo_Head.SAPTCode	= dtoHead.SAPTCode;
						lo_Head.Skip1st		= dtoHead.Skip1st;

						lo_Head.CTUParms[ lo_Prof.CTUOpt_DspMde ].SetValue( dtoHead.CTUParms.DisplayMode		);
						lo_Head.CTUParms[ lo_Prof.CTUOpt_UpdMde ].SetValue( dtoHead.CTUParms.UpdateMode			);
						lo_Head.CTUParms[ lo_Prof.CTUOpt_CATMde ].SetValue( dtoHead.CTUParms.CATTMode				);
						lo_Head.CTUParms[ lo_Prof.CTUOpt_DefSze ].SetValue( dtoHead.CTUParms.DefaultSize		);
						lo_Head.CTUParms[ lo_Prof.CTUOpt_NoComm ].SetValue( dtoHead.CTUParms.NoCommit				);
						lo_Head.CTUParms[ lo_Prof.CTUOpt_NoBtcI ].SetValue( dtoHead.CTUParms.NoBatchInpFor	);
						lo_Head.CTUParms[ lo_Prof.CTUOpt_NoBtcE ].SetValue( dtoHead.CTUParms.NoBatchInpAft	);

						lo_Func.Config( lo_Head );
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
								try
									{
										lo_Func.Process( lo_Line );
									}
								catch (System.Exception)
									{
									throw;
									}
								finally
									{
										ln_Cnt ++;
									}
							}
						//.........................................
						return	ln_Cnt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadQueue( DTO_BDC_Session dto )
					{
						foreach ( DTO_BDC_Trans lo_Tran in dto.Transactions )
							{
								this._Queue.Add( lo_Tran );
							}
						//.............................................
						this._Queue.CompleteAdding();
					}

			#endregion

		}
}
