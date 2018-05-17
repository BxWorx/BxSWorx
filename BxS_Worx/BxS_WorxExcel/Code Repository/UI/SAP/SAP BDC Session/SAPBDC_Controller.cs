using System;
using System.ComponentModel;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.UI.Core;
using BxS_WorxExcel.UI.Forms;

using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class SAPBDC_Controller : Controller_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SAPBDC_Controller(	string id	) : base(id)
					{
						this._MD	=	new	Lazy<SAPBDC_Model>			(	()=>	new	SAPBDC_Model( this.IPXCntlr.NCOx_Controller )						);
						this._VM	=	new Lazy<SAPBDC_ViewModel>	(	()=>	new	SAPBDC_ViewModel( this._MD.Value , new View_Handler() )	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<SAPBDC_Model>			_MD;
				private	readonly	Lazy<SAPBDC_ViewModel>	_VM;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	SAPBDC_ViewModel	VM	{	get	=>	this._VM.Value; }
				private	SAPBDC_Model			MD	{	get	=>	this._MD.Value; }
				//...
				private	IDTO_SessionRequest				Request		{	get	=>	this.MD.Request	;	}
				private	BindingList<IDTO_Session>	BDCList		{ get	=>	this.VM.BDCList	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void Shutdown()
					{
						if ( this._VM.IsValueCreated )	this.VM.Shutdown();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void	ToggleView()
					{
						if ( this._VM.Value.ViewHandler.IsDisposed )
							{
								this.LoadView();
							}
						//...
						this._VM.Value.ViewHandler.ToggleView();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadView()
					{
						var lo_View					 =	new	SAPBDC_View()	;
						lo_View.FormClosed	+=	this.OnFormClosed	;		// need to know when then FORM closed by user
						//...
						this.VM.ViewHandler.View	=	lo_View		;
						//...
						this.LoadSelectionBindings	( lo_View )	;
						this.LoadDGVBindings				( lo_View )	;
						this.LoadEventHandlers			( lo_View )	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadEventHandlers( SAPBDC_View view )
					{
						view.xbtn_Load			.Click	+=	this.VM.OnLoad_Click			;
						view.xbtn_Previous	.Click	+=	this.VM.OnPrevious_Click	;
						view.xbtn_Reset			.Click	+=	this.VM.OnReset_Click			;
						view.xbtn_Save			.Click	+=	this.VM.OnSave_Click			;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadDGVBindings( SAPBDC_View view )
					{
						view.xdgv_Sessions	.AutoGenerateColumns	= false;
						view.xdgv_Sessions	.DataSource						=	this.BDCList;
						//...
						view.xdgv_Sessions.Columns[	"xdgvCol_UserID"		].DataPropertyName	=	nameof( DTO_Session.UserID				);
						view.xdgv_Sessions.Columns[	"xdgvCol_Name"			].DataPropertyName	=	nameof( DTO_Session.SessionName		);
						view.xdgv_Sessions.Columns[	"xdgvCol_SAPTCode"	].DataPropertyName	=	nameof( DTO_Session.SAPTCode			);
						view.xdgv_Sessions.Columns[	"xdgvCol_Date"			].DataPropertyName	=	nameof( DTO_Session.CreationDate	);
						view.xdgv_Sessions.Columns[	"xdgvCol_Time"			].DataPropertyName	=	nameof( DTO_Session.CreationTime  );
						view.xdgv_Sessions.Columns[	"xdgvCol_TrnCnt"		].DataPropertyName	=	nameof( DTO_Session.Count         );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSelectionBindings( SAPBDC_View view )
					{
						this.BindControl( view.xtbx_User	, PNME_TEXT		, this.VM	, nameof( this.VM.UserID			) );
						this.BindControl( view.xtbx_SsnID	, PNME_TEXT		, this.VM	, nameof( this.VM.SessionName	) );
						this.BindControl( view.xdtp_Start	, PNME_VAL		, this.VM	, nameof( this.VM.DateFrom		) );
						this.BindControl( view.xdtp_End		, PNME_VAL		, this.VM	, nameof( this.VM.DateTo			) );
					}

			#endregion

			//===========================================================================================
			#region "Events: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnFormClosed( object sender , FormClosedEventArgs e )
					{
						this.MD.SaveSettings();
						//...
						base.OnFormClosed( this , e );		// trigger the base class event in derived class
						//...
						this.VM.ViewHandler.View	=	null;
					}

			#endregion

		}
}
