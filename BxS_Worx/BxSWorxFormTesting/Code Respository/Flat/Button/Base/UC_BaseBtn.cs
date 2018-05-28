using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.UC
{
	//[	TypeDescriptionProvider( typeof( UC_AbstractDescriptionProvider<UC_BaseBtn , UserControl> ) ) ]
	[TypeDescriptionProvider(typeof(AbstractCommunicatorProvider))]
	public abstract	class UC_BaseBtn : UserControl , IUC_BtnBase
		//, IUC_BtnBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected UC_BaseBtn()
					{
						//this._BackColour	= Color.FromArgb( 255 , 192 , 255 , 192 )	;
						//this._FocusColour	= Color.FromArgb( 150 , 192 , 255 , 192 )	;
						////...
						//this._HaveFocus		= false	;
						//...
						this.Dock		= DockStyle.Top;
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
				public	Image	SetImage				{	set	=>	this._Image					=		value	;	}
				public	int		Index		{	get; set ; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Color	SetBackColour		{	set	=>	this._BackColour		=	value	;	}
				public	Color	SetFocusColour	{	set	=>	this._FocusColour		=	value	;	}

		#endregion

		//===========================================================================================
		#region "Methods: Exposed"

		//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

		#endregion

		protected virtual void InitializeComponent()
			{
			this.SuspendLayout();
			// 
			// UC_BaseBtn
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Name = "UC_BaseBtn";
			this.ResumeLayout(false);
			}
		}
}
