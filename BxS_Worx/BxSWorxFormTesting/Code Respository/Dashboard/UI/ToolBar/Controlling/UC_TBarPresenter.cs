﻿//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI.Toolbar
{
	internal class UC_TBarPresenter
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_TBarPresenter(	IUC_TBarSetup		setup
																	,	IUC_TBarModel		model
																	,	IUC_TBarView		view		)
					{
						this._Setup		= setup	;
						this._Model		=	model	;
						this._View		= view	;
						//...
						this._CurScenario		= string.Empty	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IUC_TBarSetup		_Setup	;
				private	readonly	IUC_TBarModel		_Model	;
				private	readonly	IUC_TBarView		_View		;
				//...
				private	string	_CurScenario	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ChangeScenario( string	id )
					{
						if ( ! this._CurScenario.Equals(id) )
							{
								this._View.LoadButtons( this._Model.ScenarioButtons( id ) )	;
								this._CurScenario		= id;
							}
						//...
						this._View.InvokeTransition()	;
					}

			#endregion

		}
}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void OnClick(	object sender , System.EventArgs e )	=>	this.InvokeTransition();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void OnMouseHover( object sender , System.EventArgs e )
				//	{
				//	}
