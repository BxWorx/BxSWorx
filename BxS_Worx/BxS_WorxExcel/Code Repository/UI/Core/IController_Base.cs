using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Core
{
	public interface IController_Base
		{
			#region "Declarations"

				event	EventHandler	FormClosed;

			#endregion

			//===========================================================================================
			#region "Properties"

				string	ID	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	Shutdown()		;
				void	ToggleView()	;

			#endregion

		}
}
