using System;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	internal class UC_ButtonBase : UserControl
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_ButtonBase()
					{
						this.SetBackColour		= Color.FromArgb( 255 , 192 , 255 , 192 )	;
						this.SetFocusColour		= Color.FromArgb( 150 , 192 , 255 , 192 )	;
						//...
						this._HaveFocus		= false	;
						//...
						this.Dock	= DockStyle.Top	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	Color		_BackColour		;
				protected	Color		_FocusColour	;
				//...
				protected	bool		_HaveFocus		;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	bool	SetFocus	{	get	=>	this._HaveFocus							;
																	set	=>	this.SetFocusState( value )	;	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Color	SetBackColour		{	set	=>	this._BackColour		=	value	;	}
				public	Color	SetFocusColour	{	set	=>	this._FocusColour		=	value	;	}

				//public	int			MyTabIndex			{	get	=>	this.xobj_Button.TabIndex					;
				//																	set	=>	this.xobj_Button.TabIndex	= value	;	}
				////...
				//public	string				SetText								{	set	=>	this.xobj_Button.Text		=		value	; }
				//public	string				SetName								{	set	=>	this.xobj_Button.Name		=		value	; }
				//public	string				SetTag								{	set	=>	this.xobj_Button.Tag		=		value	; }
				////...
				//public	Image					SetImage							{	set	=>	this.xpic_Button.Image	=		value	;	}
				//public	EventHandler	SetClickEventHandler	{ set	=>	this.xobj_Button.Click	+=	value	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual void SetFocusState( bool	state	)
					{
						this._HaveFocus		= state	;
					}

		#endregion

				protected virtual void InitializeComponent() { }

		//private void InitializeComponent()
		//	{
		//	this.SuspendLayout();
		//	// 
		//	// UC_ButtonBase
		//	// 
		//	this.Name = "UC_ButtonBase";
		//	this.Size = new System.Drawing.Size(180, 45);
		//	this.ResumeLayout(false);

		//	}
		}
}
