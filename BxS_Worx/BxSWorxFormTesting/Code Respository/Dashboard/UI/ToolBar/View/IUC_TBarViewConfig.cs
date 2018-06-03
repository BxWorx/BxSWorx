using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public interface IUC_TBarViewConfig
		{
			#region "Properties"

				Color			ColourBack					{ get;  set; }
				Color			ColourFocus					{ get;  set; }
				//...
				bool			IsHorizontal				{ get;  set; }
				bool			CanTransition				{ get;  set; }
				//...
				int				TransitionSpanMin		{ get;  set; }
				int				TransitionSpanMax		{ get;  set; }
				int				TransitionSpeed			{ get;  set; }

			#endregion

		}
}
