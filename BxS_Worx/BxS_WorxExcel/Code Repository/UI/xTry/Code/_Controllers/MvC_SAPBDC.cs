using System;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.Code_Repository.UI.xTry;
using BxS_WorxExcel.MVVM;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class MvC_SAPBDC : MvCBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MvC_SAPBDC(	string id	) : base(id)
					{
						this._MD	=	new	Lazy<MD_SAPBDC>(	()=>	new	MD_SAPBDC( this.IPXCntlr.NCOx_Controller )			);
						this._VM	=	new Lazy<VM_SAPBDC>(	()=>	new	VM_SAPBDC( this._MD.Value , new ViewHandler() )	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<MD_SAPBDC>		_MD;
				private	readonly	Lazy<VM_SAPBDC>		_VM;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	VM_SAPBDC	VM	{	get	=>	this._VM.Value; }
				private	MD_SAPBDC	MD	{	get	=>	this._MD.Value; }
				//...
				private	IDTO_SessionRequest		Request		{	get	=>	this.MD.Request;	}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void Shutdown()
					{
						//this._VM.Value.View.FormClosed -= this.OnFormClosed;
						//this._VM.Value.View.Close();
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
						var lo_View		=	new	VW_SAPBDC();
						this.VM.ViewHandler.View	=	lo_View;
						this.MD.GetSettings();
						lo_View.FormClosed	+=	this.OnFormClosing;
						//...
						this.LoadBindings( lo_View );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadBindings( VW_SAPBDC view )
					{
						this.BindControl( view.xtbx_User	, PNME_TEXT		, this.Request	, nameof( this.Request.User		) );
						this.BindControl( view.xtbx_SsnID	, PNME_TEXT		, this.Request	, nameof( this.Request.Name		) );
						this.BindControl( view.xdtp_Start	, PNME_VAL		, this.Request	, nameof( this.Request.From		) );
						this.BindControl( view.xdtp_End		, PNME_VAL		, this.Request	, nameof( this.Request.To			) );
						this.BindControl( view.xdtp_Start	, PNME_CHECK	, this.Request	, nameof( this.Request.FromX	) );
						this.BindControl( view.xdtp_End		,	PNME_CHECK	, this.Request	, nameof( this.Request.ToX		) );
					}

			#endregion

			//===========================================================================================
			#region "Events: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnFormClosing( object sender , FormClosedEventArgs e )
					{
						this.MD.SaveSettings();
						//...
						base.OnFormClosing(	this , e );
					}

			#endregion

		}
}
