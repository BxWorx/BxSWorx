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
						this._View		= view		;
						//...
						this._ToolBars	= new	Dictionary<string, UC_TBarView>()	;
						this._BtnSpecs	= new	Dictionary<string, IButtonSpec>()	;
						//...
						this._StartupTBar	= string.Empty	;
					}

						//this._DBForm		=	new	Lazy<DB_View>	(	()=>		DB_View.Create()
						//																						, LazyThreadSafetyMode.ExecutionAndPublication	);

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IDB_ViewConfig	_Config	;
				private	readonly	IDB_View				_View		;

				private	string	_StartupTBar			;
				private	string	_StartupScenario	;
				//...
				//private readonly	Lazy<iDB_View>		_DBForm		;
				//...
				private	IDBAssembly		_Assembly	;
				//...
				private readonly Dictionary<string , UC_TBarView>		_ToolBars	;
				private readonly Dictionary<string , IButtonSpec>		_BtnSpecs	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IDB_View		View			{	get =>	this._View							; }
				public	IDBAssembly	Assembly	{ set	=>	this._Assembly	= value	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Startup()
					{
						if ( this._ToolBars.TryGetValue( this._StartupTBar , out UC_TBarView lo_TBar ) )
							{
								//lo_TBar.ChangeScenario( this._StartupScenario )	;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	AssembleDashboard()
					{
						if ( this._Assembly	== null	)		return;
						//...
						this._View.Config		=	this._Assembly.FormConfig	;
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
						foreach ( UC_TBarView lo_TBar in this._ToolBars.Values )
							{
								//this._View.LoadToolbar( lo_TBar );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void	AssembleToolbars()
					{
						//foreach ( IToolBarConfig lo_TBCfg in	this._Assembly.ToolBarList )
						//	{
						//		this._ToolBars.Add( lo_TBCfg.ID , UC_TBarView.CreateWithConfig( lo_TBCfg ) )	;
						//		//...
						//		if ( ! string.IsNullOrEmpty( lo_TBCfg.ID ) )
						//			{
						//				if (		string.IsNullOrEmpty( this._StartupTBar )
						//						||	lo_TBCfg.IsStartupToolBar									)
						//					{
						//						this._StartupTBar		= lo_TBCfg.ID	;
						//					}
						//			}
						//		//...
						//		if ( ! string.IsNullOrEmpty( lo_TBCfg.StartupScenario ) )
						//			{
						//				if ( string.IsNullOrEmpty( this._StartupScenario ) )
						//					{
						//						this._StartupScenario		= lo_TBCfg.StartupScenario	;
						//					}
						//			}
						//	}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Button"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadButtonsOnToolBars()
					{
						foreach ( IButtonProfile lo_Btn in this._Assembly.ButtonList )
							{
								if ( this._ToolBars.TryGetValue( lo_Btn.ToolbarID , out UC_TBarView lo_TBar ) )
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
