//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IViewT<T> where T: VMBase
		{
			#region "Properties"

				T	ViewModel		{ get; set; }

			#endregion

		}
}
