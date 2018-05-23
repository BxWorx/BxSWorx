using System;
using System.Drawing;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI.Menu
{
	internal partial class UC_MenuButton : UserControl
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_MenuButton()
					{
						InitializeComponent();
						//...
						this.SetFocusColour		= Color.FromArgb( 150 , 192 , 255 , 192 )	;
						this._HaveFocus				= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	Color		_FocusColour	;
				private	bool		_HaveFocus		;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	ButtonTag				{	set	=>	this.xbtn_Button.Tag	= value; }
				//...
				internal	bool		SetFocus				{	get	=>	this._HaveFocus							;
																						set	=>	this.SetFocusState( value )	;	}
				//...
				internal	Color					SetFocusColour				{	set	=>	this._FocusColour				=		value	;	}
				internal	Image					SetImage							{	set	=>	this.xbtn_Button.Image	=		value	;	}
				internal	EventHandler	SetClickEventHandler	{ set	=>	this.xbtn_Button.Click	+=	value	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetFocusState( bool	state	)
					{
						this._HaveFocus								= state	;
						this.xpnl_Selected.BackColor	=		this._HaveFocus	?	this._FocusColour	: this.Parent.BackColor	;
					}

			#endregion

		}
}
