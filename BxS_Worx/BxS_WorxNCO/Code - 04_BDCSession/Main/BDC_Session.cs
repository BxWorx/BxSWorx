using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Helpers.Common;
using BxS_WorxNCO.Helpers.ObjectPool;

using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session(		BDCCall_Header										header
															, ObjectPool< BDCSessionConsumer >	consumerPool
															,	DTO_BDC_SessionConfig							config
															, CancellationToken									CT
															, IProgress<ProgressDTO>						progress		)
					{
						this._Header				= header;
						this._ConsumerPool	= consumerPool	;
						this._OpConfig			= config		;
						this._CT						= CT				;
						this._Progress			= progress	;
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

				private readonly	BDCCall_Header										_Header	;
				private	readonly	DTO_BDC_SessionConfig							_OpConfig	;
				private	readonly	CancellationToken									_CT				;
				private	readonly	IProgress<ProgressDTO>						_Progress	;
				private	readonly	ObjectPool< BDCSessionConsumer >	_ConsumerPool;
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

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigureOperation( DTO_BDC_SessionConfig dto )
					{
						this._OpConfig.Configure( dto );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				// Returns no of transactions processesed
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> Process_SessionAsync( DTO_BDC_Session bdcSession )
					{
						this.PrepareSession();

						this._Header.Load( bdcSession.Header );
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
				private void PrepareSession()
					{
						this.TransactionsProcessed	= 0;
						//.............................................
						this.TasksCompleted		= new ConcurrentQueue< Task< int > >();
						this.TasksFaulty			= new ConcurrentQueue< Task< int > >();
						this.TasksOther				= new ConcurrentQueue< Task< int > >();
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
