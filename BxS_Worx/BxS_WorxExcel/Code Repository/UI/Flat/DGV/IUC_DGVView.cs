using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal interface IUC_DGVView
		{
			#region "Methods: Exposed"

				UserControl		ViewUC		{ get; }
				DataGridView	DGView		{ get; }

			#endregion

		}
}
