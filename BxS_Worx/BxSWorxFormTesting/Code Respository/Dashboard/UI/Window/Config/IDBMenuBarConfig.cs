using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	public interface IDBMenuBarConfig
		{
			#region "Properties"

				Color	ColourBack		{ get;  set; }
				Color	ColourFocus		{ get;  set; }
				//...
				bool	IsHorizontal	{ get;  set; }

			#endregion

		}
}
