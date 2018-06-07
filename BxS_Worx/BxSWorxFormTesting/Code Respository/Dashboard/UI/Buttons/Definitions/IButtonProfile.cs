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

				string	ID					{ get;	set; }
				int			SeqNo				{ get;  set; }
				//...
				string	ScenarioID	{ get;  set; }
				string	ToolbarID		{ get;  set; }
				string	ImageID			{ get;  set; }
				string	Text				{ get;  set; }

				object	Tag					{ get;  set; }
				//...
				int			ChildCount	{ get; }
				string	ButtonType	{ get; set; }

				Color					ColourBack			{ get;  set; }
				Color					ColourFocus			{ get;  set; }
				DockStyle			DockStyle				{ get;  set; }
				DockStyle			FocusDocking		{ get;  set; }
				IUC_Button		Button					{ get;  set; }
				EventHandler	OnClickHandler	{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"

				void	ApplyProfile() ;

				void	AddChild( IButtonProfile	profile ) ;
				//...
				IList<IButtonProfile>	GetSubList() ;

			#endregion

		}
}
