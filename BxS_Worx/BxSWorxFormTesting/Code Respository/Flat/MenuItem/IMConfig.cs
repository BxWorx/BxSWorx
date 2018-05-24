using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	public interface IMConfig
		{
			#region "Properties"

				Color		ColourBack		{ get;  set; }
				Color		ColourMove		{ get;  set; }
				Color		ColourSlide		{ get;  set; }
				Color		ColourFocus		{ get;  set; }

			#endregion

		}
}
