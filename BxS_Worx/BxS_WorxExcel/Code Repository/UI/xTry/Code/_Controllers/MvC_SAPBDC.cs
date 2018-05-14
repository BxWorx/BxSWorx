using System;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.Code_Repository.UI.xTry;
using BxS_WorxExcel.MVVM;

using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Main;
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class MvC_SAPBDC : MvCBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MvC_SAPBDC(	string					id
													,	IIPX_Controller	ipxCntlr	) : base(id)
					{
						this._IPXCntlr	=	ipxCntlr;
						//...
						this._VM	=	new Lazy<VM_SAPBDC>(
							()=>	{
											var y					= new MD_SAPBDC( this._IPXCntlr.NCOx_Controller );
											var lo_VM			=	new	VM_SAPBDC( y );
											this._VMBase	= lo_VM;
											return	lo_VM;
										} );

						this._Model	= new	Lazy<MD_SAPBDC>(	()=>	new	MD_SAPBDC( this._IPXCntlr.NCOx_Controller ) );
					}

			#endregion

				public event	Action ToggleViewX;


				private	readonly	Lazy<MD_SAPBDC>		_Model;

				private	DTO_SAPSessionRequest		Request		{	get	=>	this._Model.Value.Request;	}

				private	string	PName_User	{	get	=>	nameof( this.Request.User ); }


				private const DataSourceUpdateMode DSMODE	= DataSourceUpdateMode.OnPropertyChanged;
				//.................................................
				private	const	string	PNME_VAL		= "Value"		;
				private	const	string	PNME_CHECK	= "Checked"	;
				private	const	string	PNME_TEXT		= "Text"		;
				//.................................................


				private	VW_SAPBDC								XView			{	get	{	return	this._VM.Value.MyView;					}	}

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<VM_SAPBDC>		_VM;
				//...
				private	readonly	IIPX_Controller		_IPXCntlr;

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
						if ( this._VM.Value.View == null )
							{
								this.LoadView();
							}
						//...
						this.ToggleViewX();
						//this._VM.Value.MyView.OnToggleView();	//	.ToggleView();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnFormClosed(object sender , FormClosedEventArgs e)
					{
						this._Model.Value.SaveSettings();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadView()
					{
						var lo_View						=	new	VW_SAPBDC();
						lo_View.FormClosed	 += this.OnFormClosed;

						this.ToggleViewX += lo_View.OnToggleView;

						this._VM.Value.MyView	=	lo_View;
						this._VM.Value.View		= lo_View;

						this._Model.Value.GetSettings();

						this.BindControl( lo_View.xtbx_User		, PNME_TEXT		, this.Request	, nameof( this.Request.User		) );
						this.BindControl( lo_View.xtbx_SsnID	, PNME_TEXT		, this.Request	, nameof( this.Request.Name		) );
						this.BindControl( lo_View.xdtp_Start	, PNME_VAL		, this.Request	, nameof( this.Request.From		) );
						this.BindControl( lo_View.xdtp_End		, PNME_VAL		, this.Request	, nameof( this.Request.To			) );
						this.BindControl( lo_View.xdtp_Start	, PNME_CHECK	, this.Request	, nameof( this.Request.FromX	) );
						this.BindControl( lo_View.xdtp_End		,	PNME_CHECK	, this.Request	, nameof( this.Request.ToX		) );
					}

			#endregion

		}
}
