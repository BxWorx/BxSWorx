using System											;
using System.Collections.Generic	;
//.........................................................
using BxS_Worx.Dashboard.UI.Toolbar	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Window
{
	//***********************************************************************************************
	public sealed class DB_ViewPresenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DB_ViewPresenter(	IDB_ViewConfig	config
																	,	IDB_View				view		)
					{
						this._Config	= config	;
						this.View			= view		;
						//...
						this._ToolBars	= new	Dictionary<string, UC_TBarPresenter>()	;
						this._BtnSpecs	= new	Dictionary<string, IButtonSpec>()				;
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
				private readonly Dictionary<string , IButtonSpec>				_BtnSpecs	;

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
								//lo_TBar.ChangeScenario( this._StartupScenario )	;
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
								lo_TBar.ApplyConfigurations()	;
								//...
								this.View.LoadToolbar( lo_TBar ) ;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleToolbars()
					{
						foreach ( IUC_TBarSetup lo_TBCfg in	this._Assembly.ToolBarList )
							{
								IUC_TBarModel			lo_TBM	= DB_Factory.CreateTBModel()	;
								IUC_TBarView			lo_TBV	= DB_Factory.CreateTBView()		;
								UC_TBarPresenter	lo_TBP	= DB_Factory.CreateTBPresenter( lo_TBCfg , lo_TBM , lo_TBV )	;

								this._ToolBars.Add( lo_TBCfg.ID , lo_TBP );
								//...
								if ( ! string.IsNullOrEmpty( lo_TBCfg.ID ) )
									{
										if (		string.IsNullOrEmpty( this._StartupTBar )
												||	lo_TBCfg.IsStartupToolBar									)
											{
												this._StartupTBar		= lo_TBCfg.ID	;
											}
									}
								//...
								if ( ! string.IsNullOrEmpty( lo_TBCfg.StartupScenario ) )
									{
										if ( string.IsNullOrEmpty( this._StartupScenario ) )
											{
												this._StartupScenario		= lo_TBCfg.StartupScenario	;
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
										//lo_TBar.LoadButton( lo_Btn.ScenarioID , lo_Btn.Button );
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
