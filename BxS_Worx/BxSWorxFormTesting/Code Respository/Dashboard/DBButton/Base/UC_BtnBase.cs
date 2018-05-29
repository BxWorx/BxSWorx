using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
	{
		[	TypeDescriptionProvider( typeof( UC_AbstractDescriptionProvider<UC_BtnBase , UserControl> ) ) ]
		public abstract	class UC_BtnBase : UserControl , IUC_BtnBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected UC_BtnBase()
					{
						this._BackColour		= Color.FromArgb( 255 , 192 , 255 , 192 )	;
						this._FocusColour		= Color.FromArgb( 150 , 192 , 255 , 192 )	;
						this._HasFocus			= false	;
						//...
						this.Dock		= DockStyle.Top;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	Panel		_UCImage	;
				protected	Panel		_UCButton	;
				protected	Panel		_UCFocus	;
				protected	Label		_UCText		;
				//...
				protected	Color		_BackColour		;
				protected	Color		_FocusColour	;
				//...
				protected	bool		_HasFocus	;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	int		Index			{	get; set ; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	bool		HasFocus				{	get	=>	this._HasFocus							;
																					set	=>	this.SetFocusState( value )	;	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Color	SetBackColour		{	set	=>	this._BackColour		=	value	;	}
				public	Color	SetFocusColour	{	set	=>	this._FocusColour		=	value	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	virtual	string	SetText		{	set	=>	this._UCText		.Text							=	value	; }
				public	virtual	string	SetName		{	set	=>	this._UCButton	.Name							=	value	; }
				public	virtual	string	SetTag		{	set =>	this._UCButton	.Tag							=	value	; }
				public	virtual	Image		SetImage	{	set	=>	this._UCImage		.BackgroundImage	=	value	; }
				//...
				public	virtual	EventHandler	SetClickEventHandler	{ set	=> this._UCButton.Click	+= value; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual void SetFocusState( bool	state	)
					{
						this._HasFocus	= state	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void InitializeComponent()
					{
						this.SuspendLayout();
						// 
						// UC_XBase
						// 
						this.Name						= "UC_BtnBase"														;
						this.AutoScaleMode	= System.Windows.Forms.AutoScaleMode.None	;
						this.Size						= new System.Drawing.Size(180, 45)				;
						//
						this.ResumeLayout(false);
					}

			#endregion

		}
	}
