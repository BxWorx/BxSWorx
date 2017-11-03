//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal abstract class DTOBase
		{
			internal object this[string propertyName]
				{
					get { return	this.GetType().GetProperty(propertyName).GetValue(this, null)					; }
					set {					this.GetType().GetProperty(propertyName).SetValue(this, value, null)	; }
				}
		}
}
