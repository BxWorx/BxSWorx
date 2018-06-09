using	System.Windows.Forms	;
//.........................................................
using BxS_Worx.Dashboard.UI.Button;
using BxS_Worx.Dashboard.Utilities;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	[ ButtonTypeAttribute( ButtonTypes.TypeStd ) ]
	//...
	public partial class UC_BtnSelected : UC_BtnBase
		{
			#region "Constructors"

				public UC_BtnSelected()	: base()
					{
						InitializeComponent();
						//...
						this.xpnl_Selected	= new Panel()		;
						this.xpnl_Button		= new Panel()		;
						//...
						this.myText					=	string.Empty	;
						//...
						this._UCButton	= this.xpnl_Button		;
						this._UCImage		= this.xpnl_Button		;
						this._UCFocus		= this.xpnl_Selected	;
						this._UCText		=	this.myText					;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Panel		xpnl_Selected	;
				private	readonly	Panel		xpnl_Button		;
				//...
				private readonly	string	myText				;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	override	string	SetText		{	set	=> this._UCText		=	string.Empty	; }

			#endregion

			//===========================================================================================
			#region "Methods: Local"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void CompileButton()
					{
						//
						// xpnl_Selected
						//
						if (		this._FocusDock == DockStyle.Top
								||	this._FocusDock == DockStyle.Bottom )
							{	this.xpnl_Selected.Size		= new System.Drawing.Size( -1 ,	 3 ) ; }
						else
							{	this.xpnl_Selected.Size		= new System.Drawing.Size(  3	, -1 ) ; }
						//...
						this.xpnl_Selected.Dock				=	this._FocusDock	;
						this.xpnl_Selected.Name				= "xpnl_Selected"	;
						this.xpnl_Selected.TabIndex		= 1;

						// 
						// xpnl_Button
						// 
						this.xpnl_Button.BackgroundImageLayout	= ImageLayout.Center;
						this.xpnl_Button.Dock										= DockStyle.Fill;
						this.xpnl_Button.Name										= "xpnl_Button";
						this.xpnl_Button.TabIndex								= 2;
						//...
						this.Controls.Add( this.xpnl_Button		);
						this.Controls.Add( this.xpnl_Selected	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void SetFocusState( bool	state	)
					{
						base.SetFocusState( state );
						//...
						this._UCFocus.BackColor		=	this._HasFocus	?	this._FocusColour
																												: this.Parent.BackColor	;
					}

			#endregion

		}
}
