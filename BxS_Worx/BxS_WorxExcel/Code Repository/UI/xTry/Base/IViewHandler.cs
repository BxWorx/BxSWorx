using System;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	public interface IViewHandler
		{
			#region "Declarations"

				event Action FormClosing;		// triggerred when FORM is closed by user

			#endregion

			//===========================================================================================
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
