using System.Drawing	;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal class UC_TBarSetup : IUC_TBarSetup
		{
			#region "Properties"

				public	string	ID									{ get;  set; }
				public	int			SeqNo								{ get;  set; }
				//...
				public	bool		IsHorizontal				{ get;  set; }
				public	bool		ShowOnstartup				{ get;  set; }
				//...
				public	string	ButtonType					{ get;  set; }
				//...
				public	bool		IsStartupToolBar		{ get;  set; }
				public	bool		IsStartupSpanMax		{ get;  set; }
				public	string	StartupScenario			{ get;  set; }
				//...
				public	Color		ColourBack					{ get;  set; }
				public	Color		ColourFocus					{ get;  set; }
				//...
				public	int			TransitionSpanMin		{ get;  set; }
				public	int			TransitionSpanMax		{ get;  set; }
				public	int			TransitionSpeed			{ get;  set; }
				//...
				public	DockStyle	FocusDocking			{ get;  set; }

			#endregion

		}
}
