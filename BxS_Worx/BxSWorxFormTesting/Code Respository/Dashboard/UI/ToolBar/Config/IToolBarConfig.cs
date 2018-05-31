using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	public interface IToolBarConfig
		{
			#region "Properties"

				string	ID						{ get;  set; }
				int			SeqNo					{ get;  set; }
				//...
				Color		ColourBack		{ get;  set; }
				Color		ColourFocus		{ get;  set; }
				//...
				bool		IsHorizontal	{ get;  set; }
				bool		ShowOnstartup	{ get;  set; }
				//...
				int			TransitionSpan		{ get;  set; }
				int			TransitionSpeed		{ get;  set; }
				int			TransitionMin			{ get;  set; }
				//...
				string	ButtonType				{ get;  set; }

			#endregion

		}
}
