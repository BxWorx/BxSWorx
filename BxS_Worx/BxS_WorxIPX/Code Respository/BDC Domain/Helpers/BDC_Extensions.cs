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
									PropertyInfo[]	lt_PI	= from.GetType().GetProperties(	BindingFlags.Public | BindingFlags.Instance );
									//...
									foreach ( PropertyInfo lo_FP in lt_PI )
										{
											if ( lo_FP.GetCustomAttributes(true).Length <= 1 )
												{
													PropertyInfo lo_Me = self.GetType().GetProperty( lo_FP.Name );
													lo_Me.SetValue(self , lo_FP.GetValue( from ) );
												}
										}
								}
						}
				}
		}
}