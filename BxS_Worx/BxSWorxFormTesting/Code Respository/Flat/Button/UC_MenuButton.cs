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
						//...
						this.xbtn_Button.FlatAppearance.BorderSize		= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	bool		_HaveFocus		;
				private	Color		_FocusColour	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	FocusState	{	get	=>	this._HaveFocus; }
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
						this._HaveFocus								= ! this._HaveFocus	;
						this.xpnl_Selected.BackColor	=		this._HaveFocus	?	this._FocusColour	: this.Parent.BackColor	;
					}

			#endregion

		}
}
