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
						this._UCButton	= this.xpnl_Button		;
						this._UCImage		= this.xpnl_Button		;
						this._UCFocus		= this.xpnl_Selected	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Local"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void SetFocusState( bool	state	)
					{
						base.SetFocusState( state );
						//...
						this._UCFocus.BackColor	=	this._HasFocus	?	this._FocusColour	: this.Parent.BackColor	;
					}

			#endregion

		}
}
