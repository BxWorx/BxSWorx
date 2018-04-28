using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public static class BDC_Extensions
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static void CopyPropertiesFrom( this	object self, object From )
					{
						PropertyInfo[]	fromProperties	= From.GetType().GetProperties();
						PropertyInfo[]	toProperties		=	self.GetType().GetProperties();

						foreach ( PropertyInfo fromProperty in fromProperties )
							{
								PropertyInfo lo_Me = self.GetType().GetProperty( fromProperty.Name );

								lo_Me.SetValue(self , fromProperty.GetValue(From) );


								foreach (var toProperty in toProperties)
									{
										if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
											{
												toProperty.SetValue(self, fromProperty.GetValue(From));
												break;
											}
									}
							}
					}
		}
}