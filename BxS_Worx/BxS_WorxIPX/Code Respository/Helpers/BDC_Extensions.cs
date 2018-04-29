using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Toolset
{
	public static class BDC_Extensions
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public static void CopyPropertiesFrom( this	object self, object From )
				{
					if ( self.GetType() == From.GetType() )
						{
							PropertyInfo[]	fromProperties	= From.GetType().GetProperties(	BindingFlags.Public | BindingFlags.Instance);
							//...
							foreach ( PropertyInfo fromProperty in fromProperties )
								{
									PropertyInfo lo_Me = self.GetType().GetProperty( fromProperty.Name );

									lo_Me.SetValue(self , fromProperty.GetValue( From ) );
								}
						}
				}
		}
}