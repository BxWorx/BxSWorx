using System												;
using System.Collections.Generic		;
using System.Drawing								;
using System.Reflection							;
using System.Threading							;
//.........................................................
using BxS_Worx.Dashboard.UI.Button	;
using BxS_Worx.Dashboard.UI.Toolbar	;
using BxS_Worx.Dashboard.UI.Window	;
using BxS_Worx.Dashboard.Utilities	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	internal static class DB_Factory
		{
			#region "Declarations"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	static	readonly	Lazy<Dictionary<string , Type>>		_BtnTypes
																		= new	Lazy<Dictionary<string , Type>>	(		()=>	GetManifestOf<IUC_Button>()
																																						, LazyThreadSafetyMode.ExecutionAndPublication	);

			#endregion

			//===========================================================================================
			#region "Methods: Toolbar"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarSetup				CreateTBSetup()		=>	new	UC_TBarSetup();

				internal	static	IUC_TBarSetup				CreateTBSetupWithDefaults()
					{
						return	new UC_TBarSetup		{
																								ColourBack					= Color.FromArgb( 255 , 031 , 031 , 031 )
																							,	ColourFocus					= Color.FromArgb( 155 , 041 , 041 , 041 )

																							, IsHorizontal				= false
																							, IsStartupSpanMax		=	false

																							,	TransitionSpanMin		= 05
																							,	TransitionSpanMax		=	48
																							,	TransitionSpeed			=	01
																						};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarView		CreateTBView( IUC_TBarSetup		setup )	=>	new UC_TBarView( setup );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	IUC_TBarModel		CreateTBModel()												=>	new	UC_TBarModel	();
				internal	static	IUC_TBarModel		CreateTBModel( IUC_TBarSetup setup )	=>	new	UC_TBarModel { Setup = setup };

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	static	UC_TBarPresenter		CreateTBPresenter(	IUC_TBarModel		model
																																,	IUC_TBarView		view	)	=>	new	UC_TBarPresenter(		model
																																																										,	view	) ;

				internal	static	UC_TBarPresenter		CreateTBPresenter( IUC_TBarSetup setup )
					{
						IUC_TBarModel			lo_Mdl		= DB_Factory.CreateTBModel			( setup )							;
						IUC_TBarView			lo_View		= DB_Factory.CreateTBView				( lo_Mdl.Setup )			;
						UC_TBarPresenter	lo_TBP		= DB_Factory.CreateTBPresenter	( lo_Mdl , lo_View )	;
						//...
						return	lo_TBP ;
					}

				internal	static	UC_TBarPresenter		CreateTBPresenter( IUC_TBarModel model )
					{
						IUC_TBarView			lo_View		= DB_Factory.CreateTBView				( model.Setup )			;
						UC_TBarPresenter	lo_TBP		= DB_Factory.CreateTBPresenter	( model , lo_View )	;
						//...
						return	lo_TBP ;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Dashboard"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDB_Config		CreateDBViewConfig()	=>	new	DB_Config();

				public	static	IDB_Config		CreateDBViewConfigWithDefaults()
					{
						return	new DB_Config		{
																						ColourBack	= Color.FromArgb( 255 , 031 , 031 , 031 )
																					,	ColourMove	= Color.FromArgb( 155 , 031 , 031 , 031 )
																					,	ColourHead	= Color.FromArgb( 255 , 100 , 100 , 100 )
																				}	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDB_View			CreateDBView()	=>	new	DB_View();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	DB_Presenter	CreateDBPresenter()									=>	new	DB_Presenter(	CreateDBView() ) ;
				public	static	DB_Presenter	CreateDBPresenter( IDB_View	view	)	=>	new	DB_Presenter(	view					 ) ;

			#endregion

			//===========================================================================================
			#region "Methods: Buttons"

				public	static	IButtonTag	CreateButtonTag()		=>	new	ButtonTag()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IButtonProfile	CreateButtonProfile()							=>	new	ButtonProfile();
				public	static	IButtonProfile	CreateButtonProfile( string id )	=>	new	ButtonProfile( id );

				public	static	IButtonProfile	CreateButtonProfile(	string			id
																														,	string			tbarID
																														,	string			scenarioID
																														, int					seqNo
																														,	string			buttonType
																														,	string			imageID
																														,	string			text
																														,	string			targettbarID			= ""
																														,	string			targetScenarioID	=	""	)
					{
						IButtonTag x = new	ButtonTag		{		ScenarioID			= scenarioID
																							,	ButtonID				= id
																							,	TargetToolBar		= targettbarID
																							,	TargetScenario	=	targetScenarioID	}	;
						//...
						return	new ButtonProfile	{ ID					= id
																			, ToolbarID		= tbarID
																			,	ScenarioID	= scenarioID
																			,	SeqNo				=	seqNo
																			, ButtonType	=	buttonType
																			,	ImageID			= imageID
																			,	Text				=	text
																			,	Tag					= x						}	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IUC_Button	CreateButton( IButtonProfile	profile )
					{
						IUC_Button	lo_Btn	= null;
						//...
						if ( _BtnTypes.Value.TryGetValue( profile.ButtonType , out Type lo_BtnType ) )
							{
								lo_Btn	= Activator.CreateInstance( lo_BtnType )	as IUC_Button;
								lo_Btn.SetProfile		= profile	;
							}
						//...
						return	lo_Btn ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IUC_Button	CreateButton( string	buttonType )
					{
						if ( ! _BtnTypes.Value.TryGetValue( buttonType , out Type lo_BtnType ) )
							{	return	null; }
						//...
						return	Activator.CreateInstance( lo_BtnType )	as IUC_Button;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	static Dictionary<string , Type>	GetManifestOf<T>() where T:class
					{
						var lt	=	new Dictionary<string , Type>();
						//...
						foreach ( Type lo_Type in Toolset.TypesImplementingIFaceOf<T>() )
							{
								ButtonTypeAttribute lc_Attr		=	lo_Type.GetCustomAttribute<ButtonTypeAttribute>();
								lt.Add( lc_Attr.BtnType , lo_Type );
							}
						return	lt;
					}

			#endregion

		}
}
