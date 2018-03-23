using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Helpers.ObjectPool
{
	public class PooledObjectWrapper<T> : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public PooledObjectWrapper(T resource)
				{
						if ( EqualityComparer<T>.Default.Equals( resource , default(T) ) )
							{
								throw new ArgumentException( "OBject Pool: Wrapper: Resource cannot be null" );
							}
						//...............................................
						this.InternalResource		= resource;
				}
			#endregion

			//===========================================================================================
			#region "Properties"

				public	Action<T> WrapperReleaseResourcesAction	{ get; set; }
				public	Action<T>	WrapperResetStateAction				{ get; set; }

				public	T	InternalResource	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				protected override void OnReleaseResources()
					{
						this.WrapperReleaseResourcesAction?.Invoke( this.InternalResource );
					}

				protected override void OnResetState()
					{
						this.WrapperResetStateAction?.Invoke( this.InternalResource );
					}

			#endregion

		}
}
