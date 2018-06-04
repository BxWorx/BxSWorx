using System.Windows.Forms;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	public interface IDB_View
		{
			#region "Properties"

				IDB_ViewConfig	Config		{ set; }
				Form						ViewForm	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void LoadToolbar( UC_TBarPresenter toolBar );

			#endregion

		}
}
