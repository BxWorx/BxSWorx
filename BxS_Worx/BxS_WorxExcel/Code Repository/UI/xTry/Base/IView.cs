using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IView
		{
			//EventHandler OnClosing();

			event Action Shutdown;
			event Action Closing;

			#region "Properties"


			#endregion

		}
}
