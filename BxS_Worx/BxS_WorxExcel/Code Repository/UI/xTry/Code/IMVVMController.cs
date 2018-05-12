using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IMVVMController
		{
			//event	EventHandler	ShuttingDown	;

			void Startup();
			void Shutdown();
			void ToggleView();


			#region "Properties"


			#endregion

		}
}
