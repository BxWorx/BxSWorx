using System.Collections.Generic		;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar	;
using BxS_Worx.Dashboard.UI.Button	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	//***********************************************************************************************
	public sealed class DB_Presenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DB_Presenter( IDB_View	view )
					{
						this.View		= view		?? throw	new System.Exception("View null")		;
						//...
						this._ToolBars					= new	Dictionary<string, UC_TBarPresenter>() ;
						this._StartupTbars			= new List<string>();

						this._StartupScenario		= string.Empty	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	string	_StartupScenario ;
				//...
				private readonly IList<string>													_StartupTbars	;
				private readonly Dictionary<string , UC_TBarPresenter>	_ToolBars			;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IDB_View	View	{	get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AssembleDashboard( IDBAssembly assembly )
					{
						this.View.Config	=	assembly.ViewConfig	;

						//... Assemble Toolbars
						//...
						foreach ( IUC_TBarSetup lo_TBSetup in	assembly.ToolBarList )
							{
								//... Create toolbar presenter
								//...
								IUC_TBarModel			lo_Mdl	= DB_Factory.CreateTBModel			( lo_TBSetup )	;
								UC_TBarPresenter	lo_TBP	= DB_Factory.CreateTBPresenter	( lo_Mdl )			;

								//... Load TBar model with button profiles
								//...
								foreach ( IButtonProfile lo_BtnProf	in assembly.ToolbarButtonList( lo_TBSetup.ID ) )
									{
										lo_Mdl.LoadButton( lo_BtnProf ) ;
									}
								//...
								this._ToolBars.Add( lo_TBSetup.ID , lo_TBP );
								//...
								if ( ! string.IsNullOrEmpty( lo_TBSetup.ID ) )
									{
										if ( lo_TBSetup.IsStartupToolBar )
											{
												this._StartupTbars.Add( lo_TBSetup.ID	) ;
											}
									}
								//...
								if ( ! string.IsNullOrEmpty( lo_TBSetup.StartupScenario ) )
									{
										if ( string.IsNullOrEmpty( this._StartupScenario ) )
											{
												this._StartupScenario		= lo_TBSetup.StartupScenario	;
											}
									}
							}





						//this.AssembleToolbars()	;
						//this.AssembleButtons()	;
						//...
						//this.LoadToolBarsOntoForm()		;
						//this.LoadButtonsOnToolBars()	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Startup()
					{
						this.View.Startup() ;
						//...
						foreach ( string lc_ID in this._StartupTbars )
							{
								if ( this._ToolBars.TryGetValue( lc_ID , out UC_TBarPresenter lo_TBar ) )
									{
										lo_TBar.Startup()	;
										this.View.LoadToolbar( lo_TBar ) ;
									}
							}
						//...
						//lo_TBar.ChangeScenario( this._StartupScenario )	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Toolbar"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private	void	LoadToolBarsOntoForm()
				//	{
				//		foreach ( UC_TBarPresenter lo_TBar in this._ToolBars.Values )
				//			{
				//				lo_TBar.Startup()	;
				//				//...
				//				this.View.LoadToolbar( lo_TBar ) ;
				//			}
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private	void	AssembleToolbars()
				//	{
				//		foreach ( IUC_TBarSetup lo_TBSetup in	this._Model.ToolBarList )
				//			{
				//				//... Create toolbar presenter
				//				//...
				//				IUC_TBarModel			lo_Mdl		= DB_Factory.CreateTBModel			( lo_TBSetup )	;
				//				UC_TBarPresenter	lo_TBP		= DB_Factory.CreateTBPresenter	( lo_Mdl )			;

				//				//... Load TBar model with button profiles
				//				//...
				//				foreach ( IButtonProfile lo_BtnProf	in this._Model.ToolbarButtonList( lo_TBSetup.ID ) )
				//					{
				//						lo_Mdl.LoadButton( lo_BtnProf ) ;
				//					}
				//				//...
				//				this._ToolBars.Add( lo_TBSetup.ID , lo_TBP );
				//				//...
				//				if ( ! string.IsNullOrEmpty( lo_TBSetup.ID ) )
				//					{
				//						if (		string.IsNullOrEmpty( this._StartupTBar )
				//								||	lo_TBSetup.IsStartupToolBar									)
				//							{
				//								this._StartupTBar		= lo_TBSetup.ID	;
				//							}
				//					}
				//				//...
				//				if ( ! string.IsNullOrEmpty( lo_TBSetup.StartupScenario ) )
				//					{
				//						if ( string.IsNullOrEmpty( this._StartupScenario ) )
				//							{
				//								this._StartupScenario		= lo_TBSetup.StartupScenario	;
				//							}
				//					}
				//			}
				//	}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Button"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void LoadButtonsOnToolBars()
				//	{
						//foreach ( IButtonProfile lo_Btn in this._Model.ToolbarButtonList( ) )
						//	{
						//		if ( this._ToolBars.TryGetValue( lo_Btn.ToolbarID , out UC_TBarPresenter lo_TBar ) )
						//			{
						//				lo_TBar.LoadButton( lo_Btn ) ;
						//			}
						//	}
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private	void	AssembleButtons()
				//	{
						//foreach ( IButtonProfile lo_Btn in this._Model.ToolbarButtonList() )
						//	{
						//		lo_Btn.Button		=	DB_Factory.CreateButton( lo_Btn.ButtonType );
						//		lo_Btn.Button.CompileButton();
						//	}
				//	}

			#endregion

		}
}
