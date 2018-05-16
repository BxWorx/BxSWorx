using System;
//.........................................................
using BxS_WorxExcel.UI.Core;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal class SAPBDC_ViewModel : ViewModel_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPBDC_ViewModel(	SAPBDC_Model	model
																	,	IView_Handler	viewHandler	)	: base( viewHandler )
					{
						this._Model		=	model;
						//...
						this._Model.GetSettings();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	SAPBDC_Model		_Model;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string		UserID        { get	=>		this._Model.Request.User								;
																					set			{	string	lc_Usr	= string.Empty					;
																										this.SetProperty( ref	lc_Usr	, value )	;
																										this._Model.Request.User	=	lc_Usr			;	} }
				//...
				public	string		SessionName		{ get	=>		this._Model.Request.Name								;
																					set			{ string	lc_SNme	= string.Empty					;
																										this.SetProperty( ref lc_SNme	, value )	;
																										this._Model.Request.Name	= lc_SNme			;	}	}
				//...
				//public	DateTime	StartDate			{ get	=>	this._Model.Request.From	;		set	{ this.SetProperty( ref this._Model.Request.From	, value )	; } }
				//public	DateTime	EndDate				{ get	=>	this._Model.Request.To		;		set	{ this.SetProperty( ref this._Model.Request.To		, value )	; } }

			#endregion

			//===========================================================================================
			#region "EventHandlers"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void OnSave_Click( object sender , EventArgs e )
					{
						this.ViewHandler.LayoutSuspend( true );
						this._Model.ClearList();
						this._Model.FactorySettings();
						this.ViewHandler.LayoutSuspend( false );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void OnPrevious_Click( object sender , EventArgs e )
					{
						this.ViewHandler.LayoutSuspend( true );
						this._Model.ClearList();
						this._Model.GetSettings();
						this.ViewHandler.LayoutSuspend( false );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void OnReset_Click( object sender , EventArgs e )
					{
						this.ViewHandler.LayoutSuspend( true );
						this._Model.ClearList();
						this._Model.GetSettings();
						this.ViewHandler.LayoutSuspend( false );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void OnLoad_Click( object sender , EventArgs e )
					{
						this.ViewHandler.LayoutSuspend( true );
						this._Model.UpdateSAPSessionList();
						this.ViewHandler.LayoutSuspend( false );
					}

			#endregion

		}
}
