using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxIPX.API.BDC;

using BxS_WorxNCO.BDCSession.API;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDCSession : IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession(	IRfcFncController			fncCntlr
														,	DTO_BDC_SessionConfig	config		)
					{
						this._FncCntlr	= fncCntlr;
						this._OpConfig	= config	;
						//.............................................
						this._Queue			= new	BlockingCollection< DTO_BDC_Trans >();
						this._Consumers	= new List< Task<int> >	();
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task< int > >();
						this.TasksFaulty		= new ConcurrentQueue< Task< int > >();
						this.TasksOther			= new ConcurrentQueue< Task< int > >();
						//.............................................
						this._Profile	= this._FncCntlr.GetAddBDCCallProfile();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRfcFncController				_FncCntlr;
				private	readonly	DTO_BDC_SessionConfig		_OpConfig;
				//.................................................
				private readonly	IList< Task<int> >										_Consumers;
				private readonly	BlockingCollection< DTO_BDC_Trans >		_Queue;
				//.................................................
				private CancellationTokenSource		_CTS;
				//.................................................
				private	readonly	BDCCall_Profile	_Profile;
				private						BDCCall_Header	_Header	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int TransactionsProcessed { get; private set; }
				//.................................................
				public ConcurrentQueue< Task< int > >	TasksCompleted	{ get; private set; }
				public ConcurrentQueue< Task< int >	>	TasksFaulty			{ get; private set; }
				public ConcurrentQueue< Task< int >	>	TasksOther			{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session destination environment
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigureDestination( IConfigSetupDestination dto )
					{
						this._FncCntlr.RfcDestination.LoadConfig( dto );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigureOperation( DTO_BDC_SessionConfig dto )
					{
						this._OpConfig.IsSequential				= dto.IsSequential			;
						//.................................................
						this._OpConfig.ConsumersNo				= dto.ConsumersNo				;
						this._OpConfig.ConsumersMax				= dto.ConsumersMax			;
						this._OpConfig.ConsumerThreshold	= dto.ConsumerThreshold	;
						//.................................................
						this._OpConfig.PauseTime					= dto.PauseTime					;
						this._OpConfig.ProgressInterval		= dto.ProgressInterval	;
						this._OpConfig.QueueAddTimeout		= dto.QueueAddTimeout		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				// Returns no of Transactions processesed
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> Process_SessionAsync( IBDCSessionRequest dto )
					{
						


						this.PrepareSession();
						this.BDCConsumer_LoadHDR( dto.Header );
						this.BDCConsumer_LoadCTU( dto.Header.CTUParms	);
						this.LoadQueue( dto.Trans );
						//.............................................
						this.CreateConsumerPool();

						if ( ! this._CTS.IsCancellationRequested )
							{
								this.TransactionsProcessed	=	await ProcessConsumerResultsAsync().ConfigureAwait(false);
							}
						//.............................................
						this._CTS	=	null;

						return	this.TransactionsProcessed;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Cancel processing of session
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void CancelProcessing()
					{
						this._CTS?.Cancel();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PrepareSession()
					{
						this.TransactionsProcessed	= 0;
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task< int > >();
						this.TasksFaulty		= new ConcurrentQueue< Task< int > >();
						this.TasksOther			= new ConcurrentQueue< Task< int > >();
						//.............................................
						this._CTS						=	new CancellationTokenSource();
						//.............................................
						this._Header				= this._Profile.CreateBDCCallHeader( true );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task<int> ProcessConsumerResultsAsync()
					{
						int	ln_Ret	= 0;
						Task< int > lo_Task;
						//.............................................
						while ( ! this._Consumers.Count.Equals(0) )
							{
								if ( this._CTS.IsCancellationRequested )	break;

								lo_Task		= await Task.WhenAny( this._Consumers ).ConfigureAwait( false );

								if ( ! this._Consumers.Remove( lo_Task ) )
									{

									}

											if ( lo_Task.Status.Equals(TaskStatus.RanToCompletion )	)		{	this.TasksCompleted	.Enqueue(lo_Task); }
								else	if ( lo_Task.Status.Equals(TaskStatus.Faulted					)	)		{	this.TasksFaulty		.Enqueue(lo_Task); }
								else  																														{ this.TasksOther			.Enqueue(lo_Task); }

								ln_Ret	+= lo_Task.Result;
							}
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CreateConsumerPool()
					{
						this._Consumers.Clear();
						//.............................................
						for ( int i = 0; i < this._OpConfig.ConsumersNo; i++ )
							{
								if ( this._CTS.IsCancellationRequested )
									{ break; }
								else
									{
										this._Consumers.Add( Task<int>.Run(	()=> this.Consumer() ) );
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private int Consumer()
					{
						int	ln_Cnt	= 0;

						BDCCall_Function	lo_Func			= this._FncCntlr.CreateBDCCallFunction();
						BDCCall_Lines			lo_BDCData	=	this._Profile.CreateBDCCallLines();
						//.............................................
						lo_Func.Config( this._Header );

						foreach ( DTO_BDC_Trans lo_WorkItem in this._Queue.GetConsumingEnumerable( this._CTS.Token ) )
							{
								lo_BDCData.Reset();
								//.........................................
								this.BDCConsumer_LoadBDC( lo_BDCData.BDCData , lo_WorkItem.BDCData );
								this.BDCConsumer_LoadSPA( lo_BDCData.SPAData , lo_WorkItem.SPAData );
								//.........................................
								try
									{
										lo_Func.Process( lo_BDCData );
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
				private void BDCConsumer_LoadHDR( DTO_BDC_Header dtoHead )
					{
						this._Header.SAPTCode	= dtoHead.SAPTCode	;
						this._Header.Skip1st	= dtoHead.Skip1st		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BDCConsumer_LoadCTU( DTO_BDC_CTU ctuParms )
					{
						this._Header.CTUParms[ this._Profile.CTUOpt_DspMde ].SetValue( ctuParms.DisplayMode		);
						this._Header.CTUParms[ this._Profile.CTUOpt_UpdMde ].SetValue( ctuParms.UpdateMode		);
						this._Header.CTUParms[ this._Profile.CTUOpt_CATMde ].SetValue( ctuParms.CATTMode			);
						this._Header.CTUParms[ this._Profile.CTUOpt_DefSze ].SetValue( ctuParms.DefaultSize		);
						this._Header.CTUParms[ this._Profile.CTUOpt_NoComm ].SetValue( ctuParms.NoCommit			);
						this._Header.CTUParms[ this._Profile.CTUOpt_NoBtcI ].SetValue( ctuParms.NoBatchInpFor	);
						this._Header.CTUParms[ this._Profile.CTUOpt_NoBtcE ].SetValue( ctuParms.NoBatchInpAft	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BDCConsumer_LoadSPA(	SMC.IRfcTable SPATable , IList< DTO_BDC_SPA > SPASrce )
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
				private void BDCConsumer_LoadBDC(	SMC.IRfcTable BDCTable , IList< DTO_BDC_Data > BDCSrce )
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadQueue( ConcurrentQueue<DTO_BDC_Trans> queue )
					{
						foreach ( DTO_BDC_Trans lo_Tran in queue )
							{
								this._Queue.Add( lo_Tran );
							}
						//.............................................
						this._Queue.CompleteAdding();
					}

			#endregion

		}
}
