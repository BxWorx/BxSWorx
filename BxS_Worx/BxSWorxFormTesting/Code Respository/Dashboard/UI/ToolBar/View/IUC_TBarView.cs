using System.Collections.Generic;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal interface IUC_TBarView
		{
			#region "Properties"

				IUC_TBarViewConfig	Config	{ set; }
				UserControl					ViewUC	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	LoadButtons( IList<IUC_Button>	buttonList , bool	doLayout = false )	;
				//...
				void	InvokeTransition()	;

			#endregion

		}
}
