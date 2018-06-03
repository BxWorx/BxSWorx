using System.Drawing;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal static class UC_TBarFactory
		{
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarViewConfig	CreateViewConfig()	=>	new	UC_TBarViewConfig	();

				internal	static	IUC_TBarViewConfig	CreateViewConfigWithDefaults()
					{
						return	new UC_TBarViewConfig		{
																								ColourBack	= Color.FromArgb( 255 , 031 , 031 , 031 )
																							,	ColourFocus	= Color.FromArgb( 255 , 031 , 031 , 031 )

																							, IsHorizontal				= false
																							,	CanTransition				= true

																							,	TransitionSpanMin		= 05
																							,	TransitionSpanMax		=	48
																							,	TransitionSpeed			=	01
																						};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarView				CreateView()	=>	new UC_TBarView		();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarModel				CreateModel()	=>	new	UC_TBarModel	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarSetup				CreateSetup()	=>	new	UC_TBarSetup	();

				internal	static	UC_TBarPresenter		CreatePresenter(	IUC_TBarSetup		setup
																															,	IUC_TBarModel		model
																															,	IUC_TBarView		view		)	=>	new	UC_TBarPresenter(		setup
																																																										,	model
																																																										,	view		)	;

			#endregion

		}
}
