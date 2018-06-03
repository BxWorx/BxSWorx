using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal sealed class UC_TBarViewConfig : IUC_TBarViewConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private UC_TBarViewConfig()
					{	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IUC_TBarViewConfig	CreateBlank()	=>	new	UC_TBarViewConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IUC_TBarViewConfig	CreateWithDefaults()
					{
						return	new UC_TBarViewConfig
							{
									ColourBack	= Color.FromArgb( 255 , 031 , 031 , 031 )
								,	ColourFocus	= Color.FromArgb( 255 , 031 , 031 , 031 )

								, IsHorizontal				= false
								,	CanTransition				= true

								,	TransitionSpanMin		= 05
								,	TransitionSpanMax		=	48
								,	TransitionSpeed			=	01
							};
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Color	ColourBack					{ get;  set; }
				public	Color	ColourFocus					{ get;  set; }
				//...
				public	bool	IsHorizontal				{ get;  set; }
				public	bool	CanTransition				{ get;  set; }
				//...
				public	int		TransitionSpanMin		{ get;  set; }
				public	int		TransitionSpanMax		{ get;  set; }
				public	int		TransitionSpeed			{ get;  set; }

			#endregion

		}
}
