using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Toolset
{
	public static class BDC_Extensions
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public static void CopyPropertiesFrom( this	object self, object from )
				{
					if ( from != null && self != null )
						{
							if ( self.GetType() == from.GetType() )
								{
									PropertyInfo[]	fromProperties	= from.GetType().GetProperties(	BindingFlags.Public | BindingFlags.Instance );
									//...
									foreach ( PropertyInfo fromProperty in fromProperties )
										{
											PropertyInfo lo_Me = self.GetType().GetProperty( fromProperty.Name );

											lo_Me.SetValue(self , fromProperty.GetValue( from ) );
										}
								}
						}
				}
		}
}