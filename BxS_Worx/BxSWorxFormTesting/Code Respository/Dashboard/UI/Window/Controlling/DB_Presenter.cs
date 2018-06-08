using System.Collections.Generic	;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	//***********************************************************************************************
	public sealed class DB_Presenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DB_Presenter(	IDBModel	model
															,	IDB_View	view		)
					{
						this._Model	= model	?? throw	new System.Exception("Model null")	;
						this.View		= view	?? throw	new System.Exception("View null")		;
						//...
						this._ToolBars					= new	Dictionary<string, UC_TBarPresenter>()	;

						this._StartupTBar				= string.Empty	;
						this._StartupScenario		= string.Empty	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IDBModel				_Model	;
				//...
				private	string	_StartupTBar			;
				private	string	_StartupScenario	;
				//...
				private readonly Dictionary<string , UC_TBarPresenter>	_ToolBars	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IDB_View	View	{	get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Startup()
					{
						this._Model.LoadFromSource()	;
						this.AssembleDashboard()			;
						//...
						this.View.Startup()						;

						if ( this._ToolBars.TryGetValue( this._StartupTBar , out UC_TBarPresenter lo_TBar ) )
							{
								lo_TBar.ChangeScenario( this._StartupScenario )	;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	AssembleDashboard()
					{
						this.View.Config		=	this._Model.ViewConfig	;
						//...
						this.AssembleToolbars()	;
						this.AssembleButtons()	;
						//...
						this.LoadToolBarsOntoForm()		;
						this.LoadButtonsOnToolBars()	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Toolbar"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolBarsOntoForm()
					{
						foreach ( UC_TBarPresenter lo_TBar in this._ToolBars.Values )
							{
								lo_TBar.Startup()	;
								//...
								this.View.LoadToolbar( lo_TBar ) ;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleToolbars()
					{
						foreach ( IUC_TBarSetup lo_TBSetup in	this._Model.ToolBarList )
							{
								IUC_TBarModel	lo_Mdl	= DB_Factory.CreateTBModel();


								UC_TBarPresenter	lo_TBP	= DB_Factory.CreateTBPresenter( lo_TBSetup )	;

								this._ToolBars.Add( lo_TBSetup.ID , lo_TBP );
								//...
								if ( ! string.IsNullOrEmpty( lo_TBSetup.ID ) )
									{
										if (		string.IsNullOrEmpty( this._StartupTBar )
												||	lo_TBSetup.IsStartupToolBar									)
											{
												this._StartupTBar		= lo_TBSetup.ID	;
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
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Button"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadButtonsOnToolBars()
					{
						//foreach ( IButtonProfile lo_Btn in this._Model.ToolbarButtonList( ) )
						//	{
						//		if ( this._ToolBars.TryGetValue( lo_Btn.ToolbarID , out UC_TBarPresenter lo_TBar ) )
						//			{
						//				lo_TBar.LoadButton( lo_Btn ) ;
						//			}
						//	}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleButtons()
					{
						//foreach ( IButtonProfile lo_Btn in this._Model.ToolbarButtonList() )
						//	{
						//		lo_Btn.Button		=	DB_Factory.CreateButton( lo_Btn.ButtonType );
						//		lo_Btn.Button.CompileButton();
						//	}
					}

			#endregion

		}
}
