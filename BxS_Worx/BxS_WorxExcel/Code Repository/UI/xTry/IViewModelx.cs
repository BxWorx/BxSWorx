//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IViewModelx<T> where T: VWBase
		{
			#region "Properties"

				T	ViewModel		{ get; set; }

			#endregion

		}
}
