using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal abstract class DTOBase
		{
			internal object this[string propertyName]
				{
					get	{
								try
									{
										var x = this.GetType();
										var y = x.GetProperty(propertyName);
										var z = y.GetValue(this, null);
										return z;

									}
								catch (Exception)
									{
										return	null;
									}
							}

					//get { return	this.GetType().GetProperty(propertyName).GetValue(this, null)					; }
					set {					this.GetType().GetProperty(propertyName).SetValue(this, value, null)	; }
				}
		}
}
