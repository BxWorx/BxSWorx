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
						this.HaveFocus				= false	;
						//...
						this.xbtn_Button.FlatAppearance.BorderSize		= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	Color		_FocusColour	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	HaveFocus		{	get;	private	set; }
				//...
				internal	Color					SetFocusColour				{	set	=>	this._FocusColour				=		value	;	}
				internal	Image					SetImage							{	set	=>	this.xbtn_Button.Image	=		value	;	}
				internal	EventHandler	SetClickEventHandler	{ set	=>	this.xbtn_Button.Click	+=	value	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ToggleFocusState()
					{
						this.HaveFocus								= ! this.HaveFocus	;
						this.xpnl_Selected.BackColor	=		this.HaveFocus	?	this._FocusColour	: this.Parent.BackColor	;
					}

			#endregion

		}
}
