using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IToolBarConfig
		{
			#region "Properties"

				string		ID									{ get;  set; }
				int				SeqNo								{ get;  set; }
				//...
				Color			ColourBack					{ get;  set; }
				Color			ColourFocus					{ get;  set; }
				DockStyle	Dock								{ get;			 }
				//...
				bool			IsHorizontal				{ get;  set; }
				bool			ShowOnstartup				{ get;  set; }
				//...
				int				TransitionSpanMin		{ get;  set; }
				int				TransitionSpanMax		{ get;  set; }
				int				TransitionSpeed			{ get;  set; }
				//...
				string		ButtonType					{ get;  set; }
				//...
				bool			IsStartupToolBar		{ get;  set; }
				string		StartupScenario			{ get;  set; }

			#endregion

		}
}
