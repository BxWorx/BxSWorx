using System												;
using System.Collections.Generic		;
using System.Drawing								;
using System.Linq										;
using System.Threading							;
using System.Threading.Tasks				;
using System.Windows.Forms					;
//.........................................................
using BxS_Worx.Dashboard.UI.Button	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	public class UC_TBarPresenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarPresenter(	IDB_TBarFactory		tbarFactory
																	,	IUC_TBarModel			model
																	,	IUC_TBarView			view				)
					{
						this._TBarFactory		= tbarFactory	;
						this._Model					=	model				;
						this.View						= view				;
						//...
						this._Scenarios			= new	Dictionary<string, UC_TBarScenario>() ;

						this._CurScenario		= string.Empty	;
						this._IsStarted			= false					;
						this._QuickView			= false					;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				public event	EventHandler<IButtonTag>	TBarClicked	;
				//...
				private	readonly	IDB_TBarFactory		_TBarFactory	;
				private	readonly	IUC_TBarModel			_Model				;

				private readonly	Dictionary<string , UC_TBarScenario>	_Scenarios	;
				//...
				private	bool		_IsStarted		;
				private	string	_CurScenario	;
				private	string	_CurButton		;

				private	bool		_QuickView		;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IUC_TBarView		View			{ get; }

				internal	bool						IsStartup	{ get	=>	this.Setup.IsStartupToolBar	; }
				internal	string					ID				{ get	=>	this.Setup.ID								; }
				//...
				private		IUC_TBarSetup		Setup			{ get	=>	this._Model.Setup	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task Startup( IProgress<string> progress )
					{
						if ( this._IsStarted )		{ return ; }
						//...
						this.View.Startup() ;
						this.CreateScenarioButtons( this.Setup.StartupScenario )	;
						this.ChangeScenario( this.Setup.StartupScenario )					;
						//...
						this.View.ViewBar.MouseEnter	+= this.TBar_MouseEnter	;
						this.View.ViewBar.MouseLeave	+= this.TBar_MouseLeave	;
						//...
						await	Task.Run( () => this.CreateButtons() ).ConfigureAwait(false);
						this._IsStarted		= true ;
						progress.Report($"TBar: {this.Setup.ID} has been created");
						//await this.CreateButtonsAsync();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ChangeScenario( string	id )
					{
						if ( id == null )		{	return ; }
						//...
						if ( ! this._CurScenario.Equals(id) )
							{
								this.View.LoadButtons( this.ScenarioButtons( id ) )	;
								this._CurScenario		= id	;
							}
						//...
						this.View.InvokeTransition()	;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal void LoadButton( IButtonProfile button )
				//	{
				//		if ( button.OnClickHandler	== null )
				//			{
				//				button.OnClickHandler	=	this.OnButtonClick_Routing ;
				//			}
				//		//...
				//		button.ApplyProfile();
				//		this._Model.LoadButton( button.ScenarioID , button.Button );
				//	}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Button"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private Task CreateButtonsAsync()
				//	{
				//		Task.Run(	()=>	this.CreateButtons() );
				//	}

					//	this._IsStarted	=	await	Task.Run(
					//		()=>	{
					//						//...
					//						try
					//							{
					//								foreach ( string lc_ID in this._Model.Scenarios )
					//									{
					//										this.CreateScenarioButtons( lc_ID );
					//									}
					//								lb_Ret	=	true ;
					//							}
					//						catch
					//							{	}
					//					}
					//								).ConfigureAwait(false);
					//}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CreateButtons()
					{
						foreach ( string lc_ID in this._Model.Scenarios )
							{
								this.CreateScenarioButtons( lc_ID );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<IUC_Button> ScenarioButtons( string scenarioID )
					{
						if ( this._Scenarios.TryGetValue( scenarioID , out UC_TBarScenario lo_Scenario ) )
							{
								return	lo_Scenario.ButtonList();
							}
						//...
						return	new	List<IUC_Button>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	AddScenario( string id )	=>	this._Scenarios.Add(	id
																																				, new UC_TBarScenario( id) );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnButtonClick_Routing( object sender , EventArgs e )
					{
						var	lo_Btn	= (Control)			sender			;
						var	lo_Tag	= (IButtonTag)	lo_Btn.Tag	;
						//...
						if (		! string.IsNullOrEmpty( this._CurScenario )
								&&	! string.IsNullOrEmpty( this._CurButton		) )
							{
								if (		lo_Tag.ScenarioID.Equals( this._CurScenario )
										&&	lo_Tag.ButtonID.Equals	(	this._CurButton		) )
									{ return ; }
								//...
								IUC_Button lo_BtnSrc	= this.GetButton( this._CurScenario , this._CurButton );
								lo_BtnSrc.HasFocus		= ! lo_BtnSrc.HasFocus;
							}
						//...
						IUC_Button lo_BtnTrg	= this.GetButton( lo_Tag.ScenarioID , lo_Tag.ButtonID );
						lo_BtnTrg.HasFocus		= ! lo_BtnTrg.HasFocus;
						//...
						this._CurScenario	= lo_Tag.ScenarioID	;
						this._CurButton		= lo_Tag.ButtonID		;

						this.TBarClicked( sender , lo_Tag );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IUC_Button GetButton	( string scenarioID , string buttonID )
					{
						IUC_Button lo_Btn	= null ;
						//...
						if ( this._Scenarios.TryGetValue( scenarioID , out Dictionary<string , IUC_Button> lt_Btns ) )
							{
								lt_Btns.TryGetValue( buttonID , out lo_Btn )	;
							}
						//...
						return	lo_Btn ;
					}





				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	async Task CreateScenario( string scenarioID )
					{
						if ( ! this._Scenarios.TryGetValue( scenarioID , out UC_TBarScenario lo_Scenario ) )
							{
								var x	= this._TBarFactory.CreateScenario( scenarioID , this._Model.ScenarioButtons( scenarioID ) )	;
								

								lt_Btns.TryGetValue( buttonID , out lo_Btn )	;
							}
						
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Event handlers"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void TBar_MouseLeave( object sender , EventArgs e )
					{
						if ( ! this._QuickView )	{	return ; }
						//...
						Point lo_Pnt	= this.View.ViewBar.PointToClient( Cursor.Position )	;
						if ( this.View.ViewUC.ClientRectangle.Contains( lo_Pnt ) )
							{
								return ;
							}
						//...
						this.View.InvokeTransition() ;
						this._QuickView		= false	;
					}

								//var c		=	this.View.ViewBar.GetChildAtPoint(p);

								//if ( c == null )
								//	{
								//	}
								//else
								//	{
								//		c.MouseLeave	+= this.TBar_MouseLeave	;
								//		//if ( ! this.View.ViewUC.ClientRectangle.Contains( this.View.ViewUC.PointToClient(Cursor.Position)))
								//		//	{
								//		//	}
								//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void TBar_MouseEnter( object sender , EventArgs e )
					{
						if ( ! this.View.IsClosed )		{	return ; }
						//...
						this.View.InvokeTransition( true )	;
						//...
						this._QuickView	= true	;
					}

			#endregion

		}
}

