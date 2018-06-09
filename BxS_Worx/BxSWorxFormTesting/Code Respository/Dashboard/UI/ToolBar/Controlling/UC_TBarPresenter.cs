using System								;
using System.Collections.Generic;
using System.Windows.Forms	;
//.........................................................
using BxS_Worx.Dashboard.UI.Button	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	public class UC_TBarPresenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarPresenter(	IUC_TBarModel		model
																	,	IUC_TBarView		view	)
					{
						this._Model		=	model	;
						this.View			= view	;
						//...
						this._Scenarios			= new	Dictionary<string, Dictionary<string, IUC_Button>>() ;
						this._CurScenario		= string.Empty	;
						this._IsStarted			= false					;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Dictionary<string , Dictionary<string , IUC_Button>>	_Scenarios	;

				//private readonly	IUC_TBarSetup		_Setup	;
				private	readonly	IUC_TBarModel		_Model	;
				//...
				private	bool		_IsStarted		;
				private	string	_CurScenario	;
				private	string	_CurButton		;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IUC_TBarView	View	{ get; }
				//...
				private		IUC_TBarSetup		_Setup	{ get	=>	this._Model.Setup; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Startup()
					{
						if ( this._IsStarted )		{ return ; }
						//...
						this.View.Startup();
						this.View.InvokeTransition()	;



						//...
						this._IsStarted	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ChangeScenario( string	id )
					{
						if ( ! this._Scenarios.TryGetValue( id , out Dictionary<string , IUC_Button> lt_Btns ) )
							{
								this.AddScenario( id );
								if ( ! this._Scenarios.TryGetValue( id , out lt_Btns ) )
									{	return ; }
							}
						//...
						//lt_Btns.Add( buttonProfile.ID , buttonProfile );

						if ( ! this._CurScenario.Equals(id) )
							{
								//this.View.LoadButtons( this._Model.ScenarioButtons( id ) )	;
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	AddScenario( string id )	=>	this._Scenarios.Add(	id
																																				, new	Dictionary<string , IUC_Button>() );



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
								//IUC_Button lo_BtnSrc	= this._Model.GetButton( this._CurScenario , this._CurButton );
								//lo_BtnSrc.HasFocus		= ! lo_BtnSrc.HasFocus;
							}
						//...
						//IUC_Button lo_BtnTrg	= this._Model.GetButton( lo_Tag.ScenarioID , lo_Tag.ButtonID );
						//lo_BtnTrg.HasFocus		= ! lo_BtnTrg.HasFocus;
						//...
						this._CurScenario	= lo_Tag.ScenarioID	;
						this._CurButton		= lo_Tag.ButtonID		;
					}

			#endregion

		}
}

	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
	//private void OnMouseHover( object sender , System.EventArgs e )
	//	{
	//	}

