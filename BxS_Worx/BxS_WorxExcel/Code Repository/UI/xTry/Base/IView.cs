using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	public interface IView
		{
			#region "Declarations"

				event Action Closing;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	LayoutState( bool mode );
				void	ToggleView();
				void	Shutdown();

			#endregion

		}
}
