using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Helpers.Common;
using BxS_WorxNCO.Helpers.ObjectPool;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session(		IRfcFncController				fncCntlr
															, ObjectPool< BDCSessionConsumer >	consumerPool
															,	DTO_BDC_SessionConfig		config
															, CancellationToken				CT
															, IProgress<ProgressDTO>	progress	)
					{
						this._ConsumerPool	= consumerPool	;
						this._FncCntlr			= fncCntlr	;
						this._OpConfig			= config		;
						this._CT						= CT				;
						this._Progress			= progress	;
						//.............................................
						this._Queue			= new	BlockingCollection< DTO_BDC_Transaction >();
						this._Consumers	= new List< Task<int> >	();
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task<int> >();
						this.TasksFaulty		= new ConcurrentQueue< Task<int> >();
						this.TasksOther			= new ConcurrentQueue< Task<int> >();
						//.............................................
						this._Profile	= this._FncCntlr.GetAddBDCCallProfile();

						this._Lock		= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRfcFncController				_FncCntlr	;
				private	readonly	DTO_BDC_SessionConfig		_OpConfig	;
				private	readonly	CancellationToken				_CT				;
				private	readonly	IProgress<ProgressDTO>	_Progress	;
				//.................................................
				private readonly	IList< Task<int> >													_Consumers;
				private readonly	BlockingCollection< DTO_BDC_Transaction >		_Queue;
				//.................................................
				private	readonly	BDCCall_Profile	_Profile;
				private						BDCCall_Header	_Header	;

				private	readonly	object	_Lock;

				private	readonly	ObjectPool< BDCSessionConsumer >	_ConsumerPool;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int TransactionsProcessed { get; private set; }
				//.................................................
				public ConcurrentQueue< Task<int> >	TasksCompleted	{ get; private set; }
				public ConcurrentQueue< Task<int>	>	TasksFaulty			{ get; private set; }
				public ConcurrentQueue< Task<int>	>	TasksOther			{ get; private set; }

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
				// Returns no of transactions processesed
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> Process_SessionAsync( DTO_BDC_Session bdcSession )
					{
						this.PrepareSession();

						this.LoadHDR	( bdcSession.Header );
						this.LoadCTU	( bdcSession.Header.CTUParms	);
						this.LoadQueue( bdcSession.Trans );
						//.............................................
						this.CreateConsumerPool();

						if ( ! this._CT.IsCancellationRequested )
							{
								this.TransactionsProcessed	=	await ProcessConsumerResultsAsync()
																											.ConfigureAwait(false);
							}
						//.............................................
						this.CloseSession();

						return	this.TransactionsProcessed;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnResetState()
					{
						base.OnResetState();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnReleaseResources()
					{
						base.OnReleaseResources();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CloseSession()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PrepareSession()
					{
						this.TransactionsProcessed	= 0;
						//.............................................
						this.TasksCompleted		= new ConcurrentQueue< Task< int > >();
						this.TasksFaulty			= new ConcurrentQueue< Task< int > >();
						this.TasksOther				= new ConcurrentQueue< Task< int > >();
						//.............................................
						this._Header	= this._Profile.CreateBDCCallHeader( true );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task<int> ProcessConsumerResultsAsync()
					{
						int	ln_Ret	= 0;
						Task< int > lo_Task;
						//.............................................
						while ( ! this._Consumers.Count.Equals(0) )
							{
								if ( this._CT.IsCancellationRequested )	break;

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
						for ( int i = 0; i < this._OpConfig.ConsumersNo; i++ )
							{
								if ( this._CT.IsCancellationRequested )
									{ break; }
								else
									{
										this._Consumers.Add(	Task<int>.Run( ()=>
																						{
																							using (	BDCSessionConsumer lo_Cons = this._ConsumerPool.Acquire() )
																								{
																									lo_Cons.Consume( this._CT , this._Queue );
																									return	lo_Cons.TransactionsProcessed;
																								}
																						}
																				) );
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadHDR( DTO_BDC_Header dtoHead )
					{
						this._Header.SAPTCode	= dtoHead.SAPTCode	;
						this._Header.Skip1st	= dtoHead.Skip1st		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadCTU( DTO_BDC_CTU ctuParms )
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
				private void LoadQueue( ConcurrentQueue< DTO_BDC_Transaction > transactions )
					{
						foreach ( DTO_BDC_Transaction lo_Tran in transactions )
							{
								this._Queue.Add( lo_Tran );
							}
						//.............................................
						this._Queue.CompleteAdding();
					}

			#endregion

		}
}
