using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IButtonProfile
		{
			#region "Properties"

				string				ID						{ get;	set; }
				int						SeqNo					{ get;  set; }
				//...
				string				ScenarioID		{ get;  set; }
				string				ToolbarID			{ get;  set; }
				//...
				IUC_Button		Button				{ get;  set; }
				//...
				EventHandler	OnClickHandler	{ get;  set; }

				//...
				int			ChildCount	{ get; }
				string	ButtonType	{ get; set; }

				Color			ColourBack					{ get;  set; }
				Color			ColourFocus					{ get;  set; }

				DockStyle	DockStyle						{ get;  set; }

				string	Tag					{ get;  set; }
				string	ImageID			{ get;  set; }
				string	Text				{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"

				void	AddChild( IButtonProfile	profile );
				//...
				IList<IButtonProfile>	GetSubList();

			#endregion

		}
}
