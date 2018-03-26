using System;
using System.Threading;
using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Helpers.ObjectPool
{
	public class ObjectPool<T> where T : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPool(	int			minimumPoolSize			= DefaultMinimumSize
													, int			maximumPoolSize			= DefaultMaximumSize
													, bool		limiterOn						= false
													,	bool		activateDiagnostics	= false
													, bool		autoStartup					= false

													, CancellationToken	CT				= default( CancellationToken )
													, Func<T>						factory		= null												)
					{
						this._MinPoolSize		= minimumPoolSize	;
						this._LimiterOn			= limiterOn				;
						this._CT						= CT							;

						this.MaxPoolSize				= maximumPoolSize			;
						this.DiagnosticsActive	= activateDiagnostics	;
						this.Factory						= factory							;
						//.............................................
						this.Pool	= new	ConcurrentBag<T>()	;
						//.............................................
						this._Lock					=	new	object()			;
						this._LockChk				=	new	object()			;
						this._ReturnAction	=	this.ReturnObject	;

						this._Diag					= new	Lazy< ObjectPoolDiagnostics >( ()=>	new	ObjectPoolDiagnostics() );
						//.............................................
						this.SetLimits();

						if (limiterOn)
							{
								_SlimLock	= new	SemaphoreSlim( this.MaxPoolSize );
							}

						if ( autoStartup )
							{
								#pragma warning disable RCS1163
								ThreadPool.QueueUserWorkItem( new	WaitCallback( ( o )=> this.AutoStart() ) );
								#pragma warning restore RCS1163
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const	int	DefaultMinimumSize	=	01;
				private const int DefaultMaximumSize	=	05;

				private int		_MinPoolSize;
				private int		_CurPoolSize;

				private	readonly	bool		_LimiterOn;
				private	readonly	object	_LockChk;
				private	readonly	object	_Lock;

				private	readonly	CancellationToken								_CT;
				private readonly	Action< PooledObject , bool >		_ReturnAction;
				private readonly	Lazy< ObjectPoolDiagnostics >		_Diag;
				private	static		SemaphoreSlim										_SlimLock;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	ConcurrentBag<T>	Pool			{ get; }
				public	Func<T>						Factory		{	get; }

				public	bool		DiagnosticsActive		{ get; set; }
				public	int			MaxPoolSize					{	get; private set;	}
				public	int			Count								{	get { return	this.Pool.Count; }	}

				public ObjectPoolDiagnostics	Diagnostics		{ get { return	this._Diag.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Acquire()
					{
						// get existing object from pool
						//
						if ( this.Pool.TryTake( out T lo_Object ) )
							{
								if ( this.DiagnosticsActive )		this._Diag.Value.UpHitCount();
								return	lo_Object;
							}

						if ( this.DiagnosticsActive )		this._Diag.Value.UpMissCount();
						//.............................................
						// No objects in pool, create or wait according to configuration
						//
						if ( this._LimiterOn )
							{
								//.............................................
								if ( this._CurPoolSize < this.MaxPoolSize )
									{
										lock ( this._LockChk )
											{
												if ( this._CurPoolSize < this.MaxPoolSize )
													{
														return	this.CreateObject();
													}
											}
									}
								//.............................................
								// Wait for an existing object to be returned
								//
								while (true)
									{
										_SlimLock.Wait( this._CT );

										if ( this.Pool.TryTake( out lo_Object ) )
											{
												if ( this.DiagnosticsActive )		this._Diag.Value.UpHitAfterWaitCount();
												return	lo_Object;
											}
									}
							}
						else
							{
								return	this.CreateObject();
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReturnObject(		PooledObject	objectToReturn
																		, bool					reRegisterForFinalization = false	)
					{
						if ( this.DiagnosticsActive && reRegisterForFinalization ) this._Diag.Value.UpRessurectionCount();
						//.........................................
						// Reset object state (if implemented) before returning to the pool.
						// If resetting the object failed, destroy.
						//
						if ( ! objectToReturn.ResetState() )
							{
								if ( this.DiagnosticsActive )	this._Diag.Value.UpResetFailedCount();

								DestroyObject( objectToReturn );
								return;
							}
						//.........................................
						// re-registering for finalization - in case of resurrection (called from Finalize method)
						//
						if ( reRegisterForFinalization )
							{
								GC.ReRegisterForFinalize( objectToReturn );
							}
						//.........................................
						if ( this._LimiterOn )
							{
								this.Pool.Add( (T) objectToReturn );
								_SlimLock.Release();
								if ( this.DiagnosticsActive )	this._Diag.Value.UpReturnedCount();
								return;
							}
						else
							{
								//.............................................
								// Checking that the pool is not full
								if ( this.Pool.Count < this.MaxPoolSize )
									{
										lock ( this._Lock )
											{
												if ( this.Pool.Count < this.MaxPoolSize )
													{
														this.Pool.Add( (T) objectToReturn );
														if ( this.DiagnosticsActive )		this._Diag.Value.UpReturnedCount();
														return;
													}
											}
									}
								//.............................................
								//The Pool's upper limit has exceeded.
								// No need to add this object back into the pool, destroy it.
								//
								if ( this.DiagnosticsActive )		this._Diag.Value.UpOverflowCount();
								this.DestroyObject( objectToReturn );
							}
					}

			#endregion

			//===========================================================================================
			#region "De-Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// The pool is going down, release resources for all objects
				//
				~ObjectPool()
					{
						foreach ( T lo_Obj in this.Pool )
							{
								this.DestroyObject( lo_Obj );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AutoStart()
					{
						if ( this._CurPoolSize < this.MaxPoolSize )
							{
								lock ( this._LockChk )
									{
										if ( this._CurPoolSize < this.MaxPoolSize )
											{
												int ln_Qty	= this.MaxPoolSize - this._CurPoolSize;

												for (	int i = 0; i < ln_Qty; i++ )
													{
														this.Pool.Add( this.CreateObject() );
													}
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private T CreateObject()
					{
						T newObject;

						if ( this.Factory != null )
							{
								newObject = this.Factory();
							}
						else
							{
								// Throws an exception if the type doesn't have default ctor - on purpose!
								// Could've added generic constraint with new ()
								// Didn't want to limit the user and force a parameterless c'tor
								//
								newObject = (T)	Activator.CreateInstance(	typeof(T) );
							}

						// Setting the 'return' action in the newly created pooled object
						//
						newObject.ReturnToPool = ( Action< PooledObject , bool > )	this._ReturnAction;
						//...............................................
						if ( this.DiagnosticsActive )		this._Diag.Value.UpCreatedCount();
						Interlocked.Increment( ref this._CurPoolSize );
						//...............................................
						return newObject;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Make sure that the object is only disposed once
				// In case of application shutting down, we don't control the order of the finalization
				//
				private void DestroyObject(	PooledObject objectToDestroy )
				{
					if ( ! objectToDestroy.Disposed )
						{
							// Deterministically release object resources, nevermind the result, we are destroying the object
							//
							objectToDestroy.ReleaseResources();
							objectToDestroy.Disposed = true;

							if ( this.DiagnosticsActive )	this._Diag.Value.UpDestroyedCount();
						}

						// The object is being destroyed, resources have been already released deterministically
						// No need to fire finalizer
						//
						GC.SuppressFinalize( objectToDestroy );
				}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetLimits()
					{
						this._MinPoolSize	= this._MinPoolSize < 0									? 0 : this._MinPoolSize	;
						this.MaxPoolSize = this.MaxPoolSize < 1									? 1 : this.MaxPoolSize	;
						this._MinPoolSize	= this._MinPoolSize > this.MaxPoolSize	? 1 : this._MinPoolSize ;

						this._CurPoolSize	= 0;
					}

			#endregion

		}
}


