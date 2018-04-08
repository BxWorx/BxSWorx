using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Helpers.ObjectPool
{
	public class ObjectPoolConfig<T> where T : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPoolConfig()
					{
						this.SetToDefaults();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const		int	cz_DefMinSize	=	00	;
				private const		int	cz_DefMaxSize	=	03	;
				//.................................................
				private int			_MinimumPoolSize			;
				private int			_MaximumPoolSize			;
				private int			_MaxIdleTime					;
				private bool		_Throttled						;
				private bool		_ActivateDiagnostics	;
				private bool		_AutoStartup					;

				private Func<T>	_Factory							;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	IsDirty		{ get; private set; }
				//.................................................
				public	int		MinimumPoolSize				{ get	{	return	this._MinimumPoolSize			; } set	{	if ( this._MinimumPoolSize			!= value )	{	this._MinimumPoolSize			= value	; this.IsDirty	= true;  } } }
				public	int		MaximumPoolSize				{ get	{	return	this._MaximumPoolSize			; } set	{	if ( this._MaximumPoolSize			!= value )	{	this._MaximumPoolSize			= value	; this.IsDirty	= true;  } } }
				public	int		MaxIdleTime						{ get	{	return	this._MaxIdleTime					; } set	{	if ( this._MaxIdleTime					!= value )	{	this._MaxIdleTime					= value	; this.IsDirty	= true;  } } }
				public	bool	Throttled							{ get	{	return	this._Throttled						; } set	{	if ( this._Throttled						!= value )	{	this._Throttled						= value	; this.IsDirty	= true;  } } }
				public	bool	ActivateDiagnostics		{ get	{	return	this._ActivateDiagnostics	; } set	{	if ( this._ActivateDiagnostics	!= value )	{	this._ActivateDiagnostics	= value	; this.IsDirty	= true;  } } }
				public	bool	AutoStartup						{ get	{	return	this._AutoStartup					; } set	{	if ( this._AutoStartup					!= value )	{	this._AutoStartup					= value	; this.IsDirty	= true;  } } }
				//.................................................
				public	Func<T>	Factory							{ get	{	return	this._Factory							; } set	{	if ( this._Factory							!= value )	{	this._Factory							= value	; this.IsDirty	= true;  } } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ResetDirty()
					{
						this.IsDirty	= false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPoolConfig<T> ShallowCopy()
					{
						return	(ObjectPoolConfig<T>) this.MemberwiseClone();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetToDefaults()
					{
						this.MinimumPoolSize			= cz_DefMinSize;
						this.MaximumPoolSize			= cz_DefMaxSize;
						this.MaxIdleTime					= cz_DefMinSize;
						this.Throttled						= false;
						this.ActivateDiagnostics	= true;
						this.AutoStartup					= false;

						this.Factory	= null;
					}

			#endregion

	}
}
