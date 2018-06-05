using System											;
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
				internal DB_Presenter(	IDB_ViewConfig	config
															,	IDB_View				view		)
					{
						this._Config	= config	;
						this.View			= view		;
						//...
						this._ToolBars	= new	Dictionary<string, UC_TBarPresenter>()	;
						//...
						this._StartupTBar	= string.Empty	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IDB_ViewConfig	_Config	;

				private	string	_StartupTBar			;
				private	string	_StartupScenario	;
				//...
				private	IDBAssembly		_Assembly	;
				//...
				private readonly Dictionary<string , UC_TBarPresenter>	_ToolBars	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IDB_View		View			{	get; }
				public	IDBAssembly	Assembly	{ set	=>	this._Assembly	= value	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Startup()
					{
						if ( this._ToolBars.TryGetValue( this._StartupTBar , out UC_TBarPresenter lo_TBar ) )
							{
								lo_TBar.ChangeScenario( this._StartupScenario )	;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	AssembleDashboard()
					{
						if ( this._Assembly	== null	)		return;
						//...
						this.View.Config		=	this._Assembly.FormConfig	;
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
						foreach ( IUC_TBarSetup lo_TBSetup in	this._Assembly.ToolBarList )
							{
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
						foreach ( IButtonProfile lo_Btn in this._Assembly.ButtonList )
							{
								if ( this._ToolBars.TryGetValue( lo_Btn.ToolbarID , out UC_TBarPresenter lo_TBar ) )
									{
										lo_TBar.LoadButton( lo_Btn );
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleButtons()
					{
						foreach ( IButtonProfile lo_Btn in this._Assembly.ButtonList )
							{
								lo_Btn.Button		=	DB_Factory.CreateButton( lo_Btn.ButtonType );
							}
					}

			#endregion

		}
}
