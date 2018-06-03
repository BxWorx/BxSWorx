using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	public interface IDB_ViewConfig
		{
			#region "Properties"

				Color	ColourBack	{ get;  set; }
				Color	ColourMove	{ get;  set; }
				Color	ColourHead	{ get;  set; }

			#endregion

		}
}
