using System;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	public interface IViewForm		//<T> where T: Form
		{
				//event Action ToggleView;

				void OnToggleView();



			#region "Properties"

//				T	ViewModel		{ get; set; }

			#endregion

		}
}
