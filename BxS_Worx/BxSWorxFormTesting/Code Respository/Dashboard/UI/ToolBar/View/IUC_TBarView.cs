using System.Collections.Generic;
using System.Windows.Forms;
//.........................................................
using BxS_Worx.Dashboard.UI.Button;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal interface IUC_TBarView
		{
			#region "Properties"

				UserControl		ViewUC	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	Startup()	;

				void	LoadButtons( IList<IUC_Button>	buttonList , bool	doLayout = false )	;
				//...
				void	InvokeTransition()	;

			#endregion

		}
}
