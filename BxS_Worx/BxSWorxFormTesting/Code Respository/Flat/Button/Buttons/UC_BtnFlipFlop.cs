using System;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	[ ButtonTypeAttribute( ButtonTypes.TypeFlp ) ]
	//...
	public partial class UC_BtnFlipFlop : UC_BtnBase
		{
			#region "Constructors"

				public UC_BtnFlipFlop()
					{
						InitializeComponent();
						//...
						this._UCButton	= this.xpnl_Button	;
						this._UCImage		= this.xpnl_Image		;
						this._UCFocus		= this.xpnl_Button	;
						this._UCText		= this.xlbl_BtnText	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	int	_Inc	;
				private	int	_Pad	;
				private	int	_Max	;

			#endregion

			//===========================================================================================
			#region "Methods: Local"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	override	string				SetTag								{	set	{	this.xpnl_Button	.Tag	=	value	;
																																			this.xpnl_Image		.Tag	= value	;
																																			this.xlbl_BtnText	.Tag	= value	;	}	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	override	EventHandler	SetClickEventHandler	{	set	{ this.xpnl_Button	.Click	+= value	;
																																			this.xpnl_Image		.Click	+= value	;
																																			this.xlbl_BtnText	.Click	+= value	;	}	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void SetFocusState( bool	state	)
					{
						base.SetFocusState( state );
						//...
						bool		lb_Act	=	true										;
						bool		lb_Rev	=	false										;
						int			ln_Pad	= this._Pad								;
						string	lc_Txt	= this.xlbl_BtnText.Text	;
						//...
						this.xlbl_BtnText.Visible	= false	;

						do
							{
								if ( lb_Rev )
									{
										ln_Pad	-=	this._Inc	;
										if ( ln_Pad <= this._Pad )
											{
												ln_Pad	=	this._Pad	;
												lb_Act	= false			;
											}
									}
								else
									{
										ln_Pad	+=	this._Inc	;
										if ( ln_Pad >= this._Max )
											{
												ln_Pad	= this._Max	;
												lb_Rev	= true			;
												this.xpnl_Button.BackColor	=	this._HasFocus	?	this._FocusColour
																																			: this.Parent.BackColor	;
											}
									}
								//...
								this.xpnl_Button.Padding	= new	Padding( ln_Pad , 0 , ln_Pad , 0 );

							} while ( lb_Act );
						//...
						this.xlbl_BtnText.Visible	= true	;
						this.xpnl_Button.Refresh()				;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnLoad( object sender , EventArgs e )
					{
						this._Pad		= this.xpnl_Button.Padding.Left ;
						this._Inc		=	1															;
						this._Max		= this.xpnl_Button.Width	/ 2		;
					}

			#endregion

		}
}
