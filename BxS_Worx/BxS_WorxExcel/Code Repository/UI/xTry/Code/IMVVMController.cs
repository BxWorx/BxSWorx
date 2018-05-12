using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	public interface IMVVMController
		{
			event	EventHandler	ShuttingDown	;
			event EventHandler	ShowDialogue	;

			void Startup();

			#region "Properties"


			#endregion

		}
}
