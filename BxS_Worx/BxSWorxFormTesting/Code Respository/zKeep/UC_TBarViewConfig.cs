using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal sealed class UC_TBarViewConfig : IUC_TBarViewConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarViewConfig()
					{	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Color	ColourBack					{ get;  set; }
				public	Color	ColourFocus					{ get;  set; }
				//...
				public	bool	IsHorizontal				{ get;  set; }
				//...
				public	int		TransitionSpanMin		{ get;  set; }
				public	int		TransitionSpanMax		{ get;  set; }
				public	int		TransitionSpeed			{ get;  set; }

			#endregion

		}
}
