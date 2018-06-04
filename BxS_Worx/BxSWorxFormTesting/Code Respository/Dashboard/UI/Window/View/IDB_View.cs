using System.Collections.Generic;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	public interface IDB_View
		{
			#region "Properties"

				IDB_ViewConfig	Config		{ set; }
				Form						ViewForm	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//void	LoadToolBars( IList<IUC_Button>	buttonList , bool	doLayout = false )	;
				////...
				//void	ApplyConfig()				;
				//void	InvokeTransition()	;

			#endregion

		}
}
