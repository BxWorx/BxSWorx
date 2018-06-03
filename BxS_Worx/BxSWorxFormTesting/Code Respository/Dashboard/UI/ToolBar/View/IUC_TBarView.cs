using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal interface IUC_TBarView
		{
			#region "Properties"

				UserControl					ViewUC				{ get; }
				Control							ButtonCanvas	{ get; }
				IUC_TBarViewConfig	Config				{ set; }

			#endregion

			#region "Methods: Exposed"

				void InvokeTransition();

			#endregion

		}
}
