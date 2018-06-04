using System.Drawing;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar;
using BxS_Worx.Dashboard.UI.Window;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal static class DB_Factory
		{
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarViewConfig	CreateTBViewConfig()	=>	new	UC_TBarViewConfig	();

				internal	static	IUC_TBarViewConfig	CreateTBViewConfigWithDefaults()
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
				internal	static	IUC_TBarSetup				CreateTBSetup( bool withDefaults	= true )
																								=>	new	UC_TBarSetup	{ ViewConfig = withDefaults ?	CreateTBViewConfigWithDefaults()
																																																	:	CreateTBViewConfig()							} ;

				internal	static	IUC_TBarSetup				CreateTBSetup( IUC_TBarViewConfig viewConfig )
																								=>	new	UC_TBarSetup	{ ViewConfig = viewConfig } ;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarView				CreateTBView()	=>	new UC_TBarView		();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarModel				CreateTBModel()	=>	new	UC_TBarModel	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	UC_TBarPresenter		CreateTBPresenter(	IUC_TBarSetup		setup
																																,	IUC_TBarModel		model
																																,	IUC_TBarView		view		)	=>	new	UC_TBarPresenter(		setup
																																																										,	model
																																																										,	view	)	;

				//=========================================================================================
				//=========================================================================================

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDB_ViewConfig	CreateDBViewConfig()	=>	new	DB_ViewConfig();

				public	static	IDB_ViewConfig	CreateDBViewConfigWithDefaults()
					{
						return	new DB_ViewConfig		{
																						ColourBack	= Color.FromArgb( 255 , 031 , 031 , 031 )
																					,	ColourMove	= Color.FromArgb( 255 , 031 , 031 , 031 )
																					,	ColourHead	= Color.FromArgb( 255 , 100 , 100 , 100 )
																				};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDB_View		CreateDBView()	=>	new	DB_View();

			#endregion

		}
}
