using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using BxS_Worx.Dashboard.UI.Buttons;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	//***********************************************************************************************
	public sealed class DBController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DBController()
					{
						this._DBForm		=	new	Lazy<BxS_DashboardForm>	(	()=>		BxS_DashboardForm.Create()
																																	, LazyThreadSafetyMode.ExecutionAndPublication	);
						//...
						this._ToolBars	= new	Dictionary<string, UC_ToolBar>()	;
						this._BtnSpecs	= new	Dictionary<string, IButtonSpec>()	;
						//...
						this._StartupTBar	= string.Empty	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static DBController Create()	=>	new DBController();

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	string	_StartupTBar			;
				private	string	_StartupScenario	;
				//...
				private readonly	Lazy<BxS_DashboardForm>		_DBForm		;
				//...
				private	IDBAssembly		_Assembly	;
				//...
				private readonly Dictionary<string , UC_ToolBar>		_ToolBars	;
				private readonly Dictionary<string , IButtonSpec>		_BtnSpecs	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	BxS_DashboardForm		Form			{	get =>	this._DBForm.Value			; }
				public	IDBAssembly					Assembly	{ set	=>	this._Assembly	= value	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Startup()
					{
						if ( this._ToolBars.TryGetValue( this._StartupTBar , out UC_ToolBar lo_TBar ) )
							{
								lo_TBar.ChangeScenario( this._StartupScenario )	;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	AssembleDashboard()
					{
						if ( this._Assembly	== null	)		return;
						//...
						this._DBForm.Value.Config		=	this._Assembly.FormConfig	;
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
				private	void	AssembleToolbars()
					{
						foreach ( IToolBarConfig lo_TBCfg in	this._Assembly.ToolBarList )
							{
								this._ToolBars.Add( lo_TBCfg.ID , UC_ToolBar.CreateWithConfig( lo_TBCfg ) )	;
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	LoadToolBarsOntoForm()
					{
						foreach ( UC_ToolBar lo_TBar in this._ToolBars.Values )
							{
								this._DBForm.Value.LoadToolbar( lo_TBar );
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
								if ( this._ToolBars.TryGetValue( lo_Btn.ToolbarID , out UC_ToolBar lo_TBar ) )
									{
										lo_TBar.LoadButton( lo_Btn.ScenarioID , lo_Btn.Button );
									}
							}
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleButtons()
					{
						foreach ( IButtonProfile lo_Btn in this._Assembly.ButtonList )
							{
								lo_Btn.Button		=	ButtonFactory.CreateButton( lo_Btn.ButtonType );
								//...



							}
					}

			#endregion

		}
}
