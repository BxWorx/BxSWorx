//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IView<T> where T: VMBase
		{
			#region "Properties"

				T	ViewModel		{ get; set; }

			#endregion

		}
}
