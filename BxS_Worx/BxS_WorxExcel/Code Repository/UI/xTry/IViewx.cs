//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IViewX<T> where T: VMBase
		{
			#region "Properties"

				T	ViewModel		{ get; set; }

			#endregion

		}
}
