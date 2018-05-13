//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IViewModelx<T> where T: VMBaseX
		{
			#region "Properties"

				T	ViewModel		{ get; set; }

			#endregion

		}
}
