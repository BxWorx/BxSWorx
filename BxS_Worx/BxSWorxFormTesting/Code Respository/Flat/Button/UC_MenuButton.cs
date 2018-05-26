using System;
using System.Drawing;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI.UC
{
	internal partial class UC_MenuButton : UserControl , IUC_Button
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_MenuButton()
					{
						InitializeComponent();
						//...
						this.SetFocusColour		= Color.FromArgb( 150 , 192 , 255 , 192 )	;
						this._HaveFocus				= false	;
						this.Dock							= DockStyle.Top	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	Color		_FocusColour	;
				private	bool		_HaveFocus		;

			#endregion

			//===========================================================================================
			#region "Properties"

				//...
				public	bool		SetFocus				{	get	=>	this._HaveFocus							;
																					set	=>	this.SetFocusState( value )	;	}

				public	int			MyTabIndex			{	get	=>	this.xobj_Button.TabIndex					;
																					set	=>	this.xobj_Button.TabIndex	= value	;	}
				//...
				public	string				SetText								{	set	=>	this.xobj_Button.Text		=		string.Empty; }
				public	string				SetName								{	set	=>	this.xobj_Button.Name		=		value	; }
				public	string				SetTag					{	set	=>	this.xobj_Button.Tag		=		value	; }
				public	Color					SetFocusColour				{	set	=>	this._FocusColour				=		value	;	}
				public	Image					SetImage							{	set	=>	this.xobj_Button.Image	=		value	;	}
				public	EventHandler	SetClickEventHandler	{ set	=>	this.xobj_Button.Click	+=	value	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetFocusState( bool	state	)
					{
						this._HaveFocus								= state	;
						this.xpnl_Selected.BackColor	=	this._HaveFocus	?	this._FocusColour	: this.Parent.BackColor	;
					}

			#endregion

		}
}
