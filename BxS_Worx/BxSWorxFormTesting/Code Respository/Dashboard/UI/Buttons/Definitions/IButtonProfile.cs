using System;
using System.Collections.Generic;
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
				int						ChildCount		{ get; }
				//...
				EventHandler	OnEventClick	{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Routines: Exposed"

				void	AddChild( IButtonProfile	profile );
				IList<IButtonProfile>	GetSubList();

			#endregion

		}
}
