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
				private	BindingList<IDTO_Session>	List			{ get	=>	this.MD.List		;	}

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
						this.VM.ViewHandler.View	=	lo_View	;
						//...
						this.LoadBindings( lo_View )			;
						this.LoadEventHandlers( lo_View )	;
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
				private void LoadBindings( SAPBDC_View view )
					{
						this.BindControl( view.xtbx_User	, PNME_TEXT		, this.Request	, nameof( this.Request.User		) );
						this.BindControl( view.xtbx_SsnID	, PNME_TEXT		, this.Request	, nameof( this.Request.Name		) );
						this.BindControl( view.xdtp_Start	, PNME_VAL		, this.Request	, nameof( this.Request.From		) );
						this.BindControl( view.xdtp_End		, PNME_VAL		, this.Request	, nameof( this.Request.To			) );
						this.BindControl( view.xdtp_Start	, PNME_CHECK	, this.Request	, nameof( this.Request.FromX	) );
						this.BindControl( view.xdtp_End		,	PNME_CHECK	, this.Request	, nameof( this.Request.ToX		) );

						//view._BS.DataSource	= this.List;

						view.xdgv_Sessions.DataSource	=	this.List;

						view.xdgv_Sessions.Columns["xdgvCol_SAPID"].DataPropertyName	=	nameof( DTO_Session.UserID				);

			//	public	string		PName_User		{	get	=>	nameof( DTO_Session.UserID				)	; }
			//	public	string		PName_Session	{	get	=>	nameof( DTO_Session.SessionName		)	; }
			//	public	string		PName_SAPTCde	{	get	=>	nameof( DTO_Session.SAPTCode			)	; }
			//	public	string		PName_Date		{	get	=>	nameof( DTO_Session.CreationDate	)	; }

			//{	DataSource = this.ViewModel.List };



				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void ConfigureColumns()
				//	{
				//		const	string	SAPID		= "SAPID";

				//		var lo_C1 = new DataGridViewTextBoxColumn	{
				//																									Name							= SAPID
				//																								,	HeaderText				= "SAP System"
				//																								,	DataPropertyName	= this.ViewModel.PName_User
				//																							};
				//	}

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
