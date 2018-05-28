using System;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
	{
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
					}

			#endregion

				private	int	_Inc	;
				private	int	_Pad	;
				private	int	_Max	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void SetFocusState( bool	state	)
					{
						base.SetFocusState( state );
						//...
						bool	lb_Act	=	true			;
						bool	lb_Rev	=	false			;
						int		ln_Pad	= this._Pad	;
						//...
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
												this.xpnl_Button.BackColor	=	this._HaveFocus	?	this._FocusColour	: this.Parent.BackColor	;
											}
									}
								//...
								this.xpnl_Button.Padding	= new	Padding( ln_Pad , 0 , ln_Pad , 0 );

							} while ( lb_Act );
					}

				private void OnLoad( object sender , EventArgs e )
					{
						this._Pad	= this.xpnl_Button.Padding.Left ;
						this._Inc	=	1	;
						this._Max	= this.xpnl_Button.Width	/ 2	;
					}

		}
	}
