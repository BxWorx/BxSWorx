using System.Drawing	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal class UC_TBarSetup : IUC_TBarSetup
		{
			#region "Properties"

				public	string	ID								{ get;  set; }
				public	int			SeqNo							{ get;  set; }
				//...
				public	bool		IsHorizontal			{ get;  set; }
				public	bool		ShowOnstartup			{ get;  set; }
				//...
				public	string	ButtonType				{ get;  set; }
				//...
				public	bool		IsStartupToolBar	{ get;  set; }
				public	string	StartupScenario		{ get;  set; }
				//...
				public	Color		ColourBack				{ get	=>	this.ViewConfig.ColourBack	; }
				public	Color		ColourFocus				{ get	=>	this.ViewConfig.ColourFocus	; }
				//...
				public	IUC_TBarViewConfig	ViewConfig	{ get;  set; }

			#endregion

		}
}
