using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Core
{
	public interface IView_Handler
		{
			#region "Properties"

				bool	IsDisposed	{	get; }
				Form	View				{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	LayoutState( bool mode );
				void	ToggleView();
				void	Shutdown();

			#endregion

		}
}
