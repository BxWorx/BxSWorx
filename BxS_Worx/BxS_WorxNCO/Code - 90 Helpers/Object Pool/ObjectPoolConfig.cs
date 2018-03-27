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

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int		MinimumPoolSize					{ get; set; }
				public	int		MaximumPoolSize					{ get; set; }
				public	int		MaxIdleTime							{ get; set; }
				public	bool	Throttled								{ get; set; }
				public	bool	ActivateDiagnostics			{ get; set; }
				public	bool	AutoStartup							{ get; set; }
				//.................................................
				public	Func<T>	Factory			{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPoolConfig<T> ShallowCopy()
					{
						return	(ObjectPoolConfig<T>) this.MemberwiseClone();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SetToDefaults()
					{
						this.MinimumPoolSize			= cz_DefMinSize;
						this.MaximumPoolSize			= cz_DefMaxSize;
						this.MaxIdleTime					= cz_DefMinSize;
						this.Throttled						= true;
						this.ActivateDiagnostics	= false;
						this.AutoStartup					= false;

						this.Factory	= null;
					}

			#endregion

	}
}
