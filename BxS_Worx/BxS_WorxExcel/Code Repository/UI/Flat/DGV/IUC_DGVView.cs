using System.Windows.Forms;
using System.ComponentModel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal interface IUC_DGVView
		{
			UserControl	ViewUC { get; }

			#region "Methods: Exposed"

				void LoadData( IBindingList DataList );

			#endregion

		}
}
