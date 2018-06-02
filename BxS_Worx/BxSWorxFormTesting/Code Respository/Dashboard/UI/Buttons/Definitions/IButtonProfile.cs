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

				string				ID						{ get;  set; }
				int						SeqNo					{ get;  set; }
				//...
				string				ScenarioID		{ get;  set; }
				string				ToolbarID			{ get;  set; }
				//...
				IButtonSpec		Spec					{ get;  set; }
				IUC_Button		Button				{ get;  set; }
				//...
				EventHandler	OnEventClick	{ get;  set; }

				//...
				int			ChildCount	{ get; }
				string	ButtonType	{ get; set; }

				Color			ColourBack					{ get;  set; }
				Color			ColourFocus					{ get;  set; }
				DockStyle	DockStyle						{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"

				void	AddChild( IButtonProfile	profile );
				//...
				IList<IButtonProfile>	GetSubList();

			#endregion

		}
}
