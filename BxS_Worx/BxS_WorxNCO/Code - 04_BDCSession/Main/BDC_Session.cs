using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;

using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session(		BDCCall_Header				header
															,	DTO_BDC_SessionConfig	config	)
					{
						this._Header		= header	;
						this._Config	= config	;
						//.............................................
						this._Queue			= new	BlockingCollection< DTO_BDC_Transaction >();
						this._Consumers	= new List< Task<int> >	();
						this._Lock			= new object();
						//.............................................
						this.TasksCompleted	= new ConcurrentQueue< Task<int> >();
						this.TasksFaulty		= new ConcurrentQueue< Task<int> >();
						this.TasksOther			= new ConcurrentQueue< Task<int> >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	BDCCall_Header					_Header	;
				private	readonly	DTO_BDC_SessionConfig		_Config	;
				//.................................................
				private	readonly	object							_Lock				;
				private readonly	IList< Task<int> >	_Consumers	;

				private readonly	BlockingCollection< DTO_BDC_Transaction >		_Queue;

			#endregion

			//===========================================================================================
			#region "Properties"

				public int TransactionsProcessed { get; private set; }
				//.................................................
				public ConcurrentQueue< Task<int> >	TasksCompleted	{ get; private set; }
				public ConcurrentQueue< Task<int>	>	TasksFaulty			{ get; private set; }
				public ConcurrentQueue< Task<int>	>	TasksOther			{ get; private set; }

				#pragma	warning	disable	RCS1085
					internal	DTO_BDC_SessionConfig Config	{ get	{	return	this._Config; } }
				#pragma	warning	restore	RCS1085

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigureSession( DTO_BDC_SessionConfig dto )	=>	this._Config.Configure( dto );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				// Returns no of transactions processesed
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> Process_SessionAsync(	DTO_BDC_Session											bdcSession
																										, CancellationToken										CT
																										,	ProgressHandler< DTO_BDC_Progress >	progressHndlr
																										, ObjectPool< BDC_SessionConsumer >		pool
																										,	SMC.RfcDestination									rfcDestination	)
					{
						this.PrepareSession( bdcSession );

						this._Header.Load	( bdcSession.Header );
						this.LoadQueue		( bdcSession.Trans );
						//.............................................
						this.StartConsumers( CT , pool , rfcDestination );

						if ( ! CT.IsCancellationRequested )
							{
								this.TransactionsProcessed	=	await ProcessConsumerResultsAsync(	CT
																																								,	progressHndlr )
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
				#pragma	warning	disable	CSR1132
					protected override void OnReleaseResources()
				#pragma	warning restore	CSR1132
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
				private void PrepareSession( DTO_BDC_Session	bdcSession )
					{
						this.TransactionsProcessed	= 0;
						//.............................................
						this.TasksCompleted		= new ConcurrentQueue< Task< int > >();
						this.TasksFaulty			= new ConcurrentQueue< Task< int > >();
						this.TasksOther				= new ConcurrentQueue< Task< int > >();
						//.............................................
						if ( bdcSession.UseSessionConfig )
							{
								this._Config.Configure( bdcSession.SessionConfig );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task<int> ProcessConsumerResultsAsync(	CancellationToken										CT
																														,	ProgressHandler< DTO_BDC_Progress >	progressHndlr	)
					{
						int	ln_Ret	= 0;
						Task< int > lo_Task;
						//.............................................
						while ( ! this._Consumers.Count.Equals(0) )
							{
								if ( CT.IsCancellationRequested )	break;

								lo_Task		= await Task.WhenAny( this._Consumers ).ConfigureAwait( false );

								if ( ! this._Consumers.Remove( lo_Task ) )
									{
									}

											if ( lo_Task.Status.Equals(TaskStatus.RanToCompletion )	)		{	this.TasksCompleted	.Enqueue(lo_Task); }
								else	if ( lo_Task.Status.Equals(TaskStatus.Faulted					)	)		{	this.TasksFaulty		.Enqueue(lo_Task); }
								else  																														{ this.TasksOther			.Enqueue(lo_Task); }

								ln_Ret	+= lo_Task.Result;
								//.........................................
								if ( progressHndlr.GoingToHit )
									{
										DTO_BDC_Progress x =	progressHndlr.Create();
										x.TasksDne	= ln_Ret;
										progressHndlr.Report( x );
									}

							}
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void StartConsumers(		CancellationToken									CT
																			, ObjectPool< BDC_SessionConsumer >	pool
																			,	SMC.RfcDestination								rfcDestination	)
					{
						for ( int i = 0; i < this._Config.ConsumersNo; i++ )
							{
								if ( CT.IsCancellationRequested )
									{ break; }
								else
									{
										this._Consumers.Add(	Task<int>.Run( ()=>
																						{
																							using (	BDC_SessionConsumer lo_Cons = pool.Acquire() )
																								{
																									lo_Cons.Consume( CT , this._Queue , rfcDestination );
																									return	lo_Cons.TransactionsProcessed;
																								}
																						}
																				) );
									}
							}
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
