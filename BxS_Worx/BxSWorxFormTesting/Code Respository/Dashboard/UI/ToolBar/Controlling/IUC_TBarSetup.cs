using System.Drawing	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IUC_TBarSetup
		{
			#region "Properties"

				string		ID										{ get;  set; }
				int				SeqNo									{ get;  set; }
				//...
				bool			IsHorizontal					{ get;  set; }
				bool			ShowOnstartup					{ get;  set; }
				//...
				string		ButtonType						{ get;  set; }
				//...
				bool			IsStartupToolBar			{ get;  set; }
				bool			IsStartupSpanMax			{ get;  set; }
				string		StartupScenario				{ get;  set; }
				//...
				Color			ColourBack						{ get;	set; }
				Color			ColourFocus						{ get;	set; }
				//...
				int				TransitionSpanMin			{ get;  set; }
				int				TransitionSpanMax			{ get;  set; }
				int				TransitionSpeed				{ get;  set; }

			#endregion

		}
}
