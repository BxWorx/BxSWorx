using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.MVVM
{
	public interface IMvC
		{
			#region "Properties"

				string	ID	{ get; }

			#endregion

				event	EventHandler FormClosing;

			//===========================================================================================
			#region "Methods: Exposed"

				void	Shutdown()		;
				void	ToggleView()	;

			#endregion

		}
}
