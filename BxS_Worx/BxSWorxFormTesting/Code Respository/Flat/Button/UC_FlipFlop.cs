using System;
using System.Drawing;
using System.Windows.Forms;

namespace BxS_WorxExcel.UI.UC
{
	internal partial class UC_FlipFlop : UC_BtnBase
		//: UC_ButtonBase , IUC_Button
		{
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UC_FlipFlop() : base()
					{
						InitializeComponent();
					}

			//===========================================================================================
			#region "Declarations"

				//private	Color		_BackColour		;
				//private	Color		_FocusColour	;
				////...
				//private	bool		_HaveFocus		;
				//...
				private	int	_Inc	;
				private	int	_Pad	;
				private	int	_Max	;

			#endregion

			//===========================================================================================
			#region "Properties"

				//...
				//public	bool		SetFocus				{	get	=>	this._HaveFocus							;
				//																	set	=>	this.SetFocusState( value )	;	}

				public	int			MyTabIndex			{	get	=>	this.xobj_Button.TabIndex					;
																					set	=>	this.xobj_Button.TabIndex	= value	;	}
				//...
				public	string				SetText								{	set	=>	this.xobj_Button.Text		=		value	; }
				public	string				SetName								{	set	=>	this.xobj_Button.Name		=		value	; }
				public	string				SetTag								{	set	=>	this.xobj_Button.Tag		=		value	; }
				//...
				//public	Color					SetBackColour					{	set	=>	this._FocusColour				=		value	;	}
				//public	Color					SetFocusColour				{	set	=>	this._FocusColour				=		value	;	}
				public	Image					SetImage							{	set	=>	this.xpic_Button.Image	=		value	;	}
				public	EventHandler	SetClickEventHandler	{ set	=>	this.xobj_Button.Click	+=	value	; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//protected override void SetFocusState( bool	state	)
				//	{
				//		base.SetFocusState( state );
				//		//...
				//		bool	lb_Act	=	true			;
				//		bool	lb_Rev	=	false			;
				//		int		ln_Pad	= this._Pad	;
				//		//...
				//		do
				//			{
				//				if ( lb_Rev )
				//					{
				//						ln_Pad	-=	this._Inc	;
				//						if ( ln_Pad <= this._Pad )
				//							{
				//								ln_Pad	=	this._Pad	;
				//								lb_Act	= false			;
				//							}
				//					}
				//				else
				//					{
				//						ln_Pad	+=	this._Inc	;
				//						if ( ln_Pad >= this._Max )
				//							{
				//								ln_Pad	= this._Max	;
				//								lb_Rev	= true			;
				//								this.xobj_Button.BackColor	=	this._HaveFocus	?	this._FocusColour	: this.Parent.BackColor	;
				//							}
				//					}
				//				//...
				//				this.xobj_Button.Padding	= new	Padding( ln_Pad , 0 , ln_Pad , 0 );

				//			} while ( lb_Act );
				//	}

			#endregion

				private void OnLoad( object sender , EventArgs e )
					{
						this._Pad	= this.xobj_Button.Padding.Left ;
						this._Inc	=	1	;
						this._Max	= this.xobj_Button.Width	/ 2	;
					}

		//.

		}
}
