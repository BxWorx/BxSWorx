using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Helpers.ObjectPool
{
	public static class ObjectPoolFactory
		{
			#region "Methods: Static"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static ObjectPoolConfig<T>	CreateConfig<T>( Func<T> factory , bool defaults = true ) where T:PooledObject
					{
						var lo_Cfg = new ObjectPoolConfig<T>
															{
																Factory = factory
															};
						//.............................................
						if ( defaults )
							{
							}
						//.............................................
						return	lo_Cfg;
					}

			#endregion

		}
}
