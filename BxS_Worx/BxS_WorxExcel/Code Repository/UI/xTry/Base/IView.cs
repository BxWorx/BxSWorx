using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IView
		{
			event Action Closing;

			void LayoutState( bool mode );
			void ToggleView();
			void Shutdown();

			#region "Properties"


			#endregion

		}
}
