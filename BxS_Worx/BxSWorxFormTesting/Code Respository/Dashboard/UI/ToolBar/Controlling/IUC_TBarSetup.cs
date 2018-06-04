//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IUC_TBarSetup
		{
			#region "Properties"

				string		ID									{ get;  set; }
				int				SeqNo								{ get;  set; }
				//...
				bool			IsHorizontal				{ get;  set; }
				bool			ShowOnstartup				{ get;  set; }
				//...
				string		ButtonType					{ get;  set; }
				//...
				bool			IsStartupToolBar		{ get;  set; }
				string		StartupScenario			{ get;  set; }
				//...
				IUC_TBarViewConfig	ViewConfig	{ get;  set; }

			#endregion

		}
}
