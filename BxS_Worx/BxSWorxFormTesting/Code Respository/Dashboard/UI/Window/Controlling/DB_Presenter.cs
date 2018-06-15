using System												;
using System.Collections.Generic		;
using System.Threading.Tasks				;
using System.Linq										;
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
						this.View		= view		?? throw	new System.Exception("View null")	;
						//...
						this._ToolBars					= new	Dictionary<string, UC_TBarPresenter>()	;
						this._StartupTbars			= new List<string>()													;
						this._BGTasks						= new List<Task>()														;

						this._StartupScenario		= string.Empty																;
						//...
						this._MBarMsg		= new	Progress<string>( this.On_MBMsgChange )			;
						this._MBarSts		= new	Progress<string>( this.On_MBStatusChange )	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly IProgress<string>	_MBarMsg	;
				private readonly IProgress<string>	_MBarSts	;

				private	string	_StartupScenario ;
				//...
				private readonly IList<string>													_StartupTbars	;
				private readonly Dictionary<string , UC_TBarPresenter>	_ToolBars			;
				//...
				private readonly IList<Task>		_BGTasks	;

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
								lo_TBP.TBarClicked += this.OnTBarClick;
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
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Startup()
					{
						this.View.Startup( this._MBarMsg ) ;
						Task.Run( ()=> this.InitiateStartupTBars() ) ;
						////...
						//foreach ( string lc_ID in this._StartupTbars )
						//	{
						//		if ( this._ToolBars.TryGetValue( lc_ID , out UC_TBarPresenter lo_TBar ) )
						//			{
						//				lo_TBar.Startup( this._MBarMsg )	;
						//				this.View.LoadToolbar( lo_TBar ) ;
						//			}
						//	}
						////...
						//this._MBarMsg.Report("Dashboard ready...");
					}

			#endregion

			//===========================================================================================
			#region "Events: Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void On_MBMsgChange( string e )
					{
						this.View.MsgBox.Text		= e ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void On_MBStatusChange( string e )
					{
						this.View.MsgBox.Text		= e ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnTBarClick( object sender , IButtonTag e )
					{
						if ( this._ToolBars.TryGetValue( e.TargetToolBar , out UC_TBarPresenter lo_TBar ) )
							{
								lo_TBar.ChangeScenario( e.TargetScenario );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private async Task InitiateStartupTBars()
					{
						var x = this._ToolBars.Values.Where( tb => tb.IsStartup ).Select( tb => tb.ID ).ToList()	;
						await  Task.Delay(3000).ConfigureAwait(false);
						//...
						this._MBarMsg.Report("Dashboard ready...");
					}

			#endregion

		}
}
