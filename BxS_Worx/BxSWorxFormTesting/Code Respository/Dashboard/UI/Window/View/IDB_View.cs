using System;
using System.Windows.Forms;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	public interface IDB_View
		{
			#region "Properties"

				IDB_Config	Config		{ set; }
				Form				ViewForm	{ get; }
				Label				MsgBox		{	get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Startup( IProgress<string> progress );
				//
				void LoadToolbar( UC_TBarPresenter toolBar );

			#endregion

		}
}
