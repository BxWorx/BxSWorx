using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	public partial class UC_BtnBase : UserControl , IUC_BtnBase
	//public partial class UC_BtnBase : UserControl , IUC_BtnBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UC_BtnBase()
					{
						InitializeComponent();
						//...
						this._BackColour	= Color.FromArgb( 255 , 192 , 255 , 192 )	;
						this._FocusColour	= Color.FromArgb( 150 , 192 , 255 , 192 )	;
						//...
						this._HaveFocus		= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	Color		_BackColour		;
				protected	Color		_FocusColour	;
				//...
				protected	bool		_HaveFocus		;
				protected Image		_Image				;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	int		Index		{	get; set ; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Image	SetImage				{	set	=>	this._Image					=		value	;	}
				public	Color	SetBackColour		{	set	=>	this._BackColour		=	value	;	}
				public	Color	SetFocusColour	{	set	=>	this._FocusColour		=	value	;	}

			#endregion

		}
}
