using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.ObjectPool
{
	public static class ObjectPoolFactory
		{
			#region "Methods: Static"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static ObjectPoolConfig<T>	CreateConfig<T>(	Func<T> createConsumer
																													, bool		defaults = true ) where T:PooledObject
					{
						var lo_Cfg = new ObjectPoolConfig<T>
															{
																CreateConsumer = createConsumer
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
