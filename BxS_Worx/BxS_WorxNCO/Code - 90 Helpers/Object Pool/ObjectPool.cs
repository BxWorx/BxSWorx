﻿#region "Disclaimers"

	﻿/* 
	* Generic Object Pool Implementation
	*  
	* Implemented by Ofir Makmal, 28/1/2013
	*
	* My Blog: Blogs.microsoft.co.il/blogs/OfirMakmal
	* Email:   Ofir.Makmal@gmail.com
	* 
	* The fundamentals of this pooled objected were taking from an extract from Ofir Makmal.
	* It has been adapted for specific requirements
	*/

#endregion
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
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
				public ObjectPool( ObjectPoolConfig<T> config = null )
					{
						this._Config	= CreateConfig();
						//...............................................
						if ( config != null )		this.Configure( config );
						//.............................................
						this.Pool	= new	ConcurrentBag<T>()	;
						//.............................................
						this._Diag					= new	Lazy< ObjectPoolDiagnostics >( ()=>	new	ObjectPoolDiagnostics() );

						this._Lock					=	new	object()			;
						this._LockChk				=	new	object()			;
						this._ReturnAction	=	this.ReturnObject	;
						this._IsActive			= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static	SemaphoreSlim	_SlimLock	;

				private	bool	_IsActive			;
				private int		_CurPoolSize	;

				private	readonly	object	_LockChk	;
				private	readonly	object	_Lock			;
				public	readonly	Func<T>	_Factory	;

				private	readonly	CancellationTokenSource					_CTS					;
				public	readonly	ObjectPoolConfig<T>							_Config				;
				private readonly	Lazy< ObjectPoolDiagnostics >		_Diag					;
				private readonly	Action< PooledObject , bool >		_ReturnAction	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	ObjectPoolConfig<T>	ConfigCopy	{ get { return	this._Config.ShallowCopy(); } }
				public	ConcurrentBag<T>		Pool				{ get; }

				public	int										Count					{	get { return	this.Pool.Count	; }	}
				public	ObjectPoolDiagnostics	Diagnostics		{ get { return	this._Diag.Value; } }

				public	bool	DiagnosticsActive		{ get { return	this._Config.ActivateDiagnostics	; } }
				public	bool	Throttled						{ get { return	this._Config.Throttled						; } }
				public	int		MaxPoolSize					{	get	{ return	this._Config.MaximumPoolSize			; } }
				public	int		MinPoolSize					{	get	{ return	this._Config.MinimumPoolSize			; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Cancel()
					{
						this._CTS?.Cancel();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigurePool( ObjectPoolConfig<T> config )
					{
						this.Configure( config );
					}

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
						if ( this.Throttled )
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
										_SlimLock.Wait( this._CTS.Token );

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
						if ( this.DiagnosticsActive && reRegisterForFinalization )	this._Diag.Value.UpRessurectionCount();
						//.........................................
						// Reset object state (if implemented) before returning to the pool.
						// If resetting the object failed, destroy.
						//
						if ( ! objectToReturn.ResetState() )
							{
								if ( this.DiagnosticsActive )		this._Diag.Value.UpResetFailedCount();

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
						if ( this.Throttled )
							{
								this.Pool.Add( (T) objectToReturn );
								_SlimLock.Release();
								if ( this.DiagnosticsActive )		this._Diag.Value.UpReturnedCount();
								return;
							}
						else
							{
								//.............................................
								// Checking that the pool is not full
								//
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
								// Pool's upper limit has been exceeded.
								// No need to add back into the pool, destroy.
								//
								if ( this.DiagnosticsActive )		this._Diag.Value.UpOverflowCount();
								this.DestroyObject( objectToReturn );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Static"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static ObjectPoolConfig<T>	CreateConfig()
					{
						return	new	ObjectPoolConfig<T>();
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
				private T CreateObject()
					{
						T newObject;

						if ( this._Factory != null )
							{
								newObject = this._Factory();
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

							if ( this.DiagnosticsActive )		this._Diag.Value.UpDestroyedCount();
						}

						// The object is being destroyed, resources have been already released deterministically
						// No need to fire finalizer
						//
						GC.SuppressFinalize( objectToDestroy );
				}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Startup()
					{
						if ( this._Config.Throttled )
							{
								_SlimLock		= new	SemaphoreSlim( this._Config.MaximumPoolSize );
							}
						//...............................................
						if ( this._Config.AutoStartup )
							{
								#pragma warning disable RCS1163
								ThreadPool.QueueUserWorkItem( new	WaitCallback( ( o )=> this.AutoStartMinimumObjects() ) );
								#pragma warning restore RCS1163
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AutoStartMinimumObjects()
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
				private void Configure( ObjectPoolConfig<T> config )
					{
						this._Config.ActivateDiagnostics		=	config.ActivateDiagnostics	;

						this._Config.MinimumPoolSize				=	config.MinimumPoolSize			;
						this._Config.MaximumPoolSize				=	config.MaximumPoolSize			;

						if ( this._IsActive )
							{
								if ( ! config.Throttled )		this._Config.Throttled		=	config.Throttled	;
							}
						else
							{
								this._Config.Throttled			=	config.Throttled		;
								this._Config.AutoStartup		=	config.AutoStartup	;
								this._Config.Factory				=	config.Factory			;
							}
						//...............................................
						this.SetLimits();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetLimits()
					{
						this._Config.MinimumPoolSize	= this.MinPoolSize	< 0									? 0 : this.MinPoolSize	;
						this._Config.MaximumPoolSize	= this.MaxPoolSize	< 1									? 1 : this.MaxPoolSize	;

						this._Config.MinimumPoolSize	= this.MinPoolSize	> this.MaxPoolSize	? this.MaxPoolSize : this.MinPoolSize	;

						if ( ! this._IsActive )		this._CurPoolSize	= 0;
					}

			#endregion

		}
}
