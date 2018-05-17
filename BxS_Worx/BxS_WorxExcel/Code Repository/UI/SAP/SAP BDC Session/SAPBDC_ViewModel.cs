using System;
using System.ComponentModel;
//.........................................................
using BxS_WorxExcel.UI.Core;
using BxS_WorxIPX.NCO;
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
						this.BDCList	= new	BindingList<IDTO_Session>( this._Model.List );
						this._Model.GetSettings();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	SAPBDC_Model		_Model;
				//...
				internal	BindingList<IDTO_Session>		BDCList		{ get; }

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
				public	DateTime	DateFrom			{ get	=>		this._Model.Request.From								;
																					set			{ DateTime	ld_Dte	= default( DateTime )	;
																										this.SetProperty( ref ld_Dte	, value )	;
																										this._Model.Request.From	= ld_Dte			;	}	}
				//...
				public	DateTime	DateTo				{ get	=>		this._Model.Request.To									;
																					set			{ DateTime	ld_Dte	= default( DateTime )	;
																										this.SetProperty( ref ld_Dte	, value )	;
																										this._Model.Request.To		= ld_Dte			;	}	}

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
						this.BDCList.ResetBindings();
						this.ViewHandler.LayoutSuspend( false );
					}

			#endregion

		}
}
