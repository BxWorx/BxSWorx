using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	public sealed class ToolBarConfig : IToolBarConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private ToolBarConfig()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IToolBarConfig	CreateBlank()	=>	new	ToolBarConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IToolBarConfig	CreateWithDefaults()
					{
						return	new ToolBarConfig
							{
									ColourBack		= Color.FromArgb( 255 , 200 , 200 , 200 )
								,	ColourFocus		= Color.FromArgb( 155 , 000 , 100 , 000 )
								//...
								, IsHorizontal		= false
								,	ShowOnstartup		= true
								//...
								, TransitionSpanMax	= 48
								, TransitionSpeed	= 1
								,	TransitionSpanMin		= 0
								//...
								,	ButtonType	= ButtonTypes.TypeStandard
								,	Dock				= DockStyle.Left
							};
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	bool	_Horizontal	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ID			{ get;  set; }
				public	int			SeqNo		{ get;  set; }
				//...
				public	Color			ColourBack		{ get;  set; }
				public	Color			ColourFocus		{ get;  set; }
				public	DockStyle	Dock					{ get;  private set; }
				//...
				public	bool		ShowOnstartup		{ get;  set; }
				//...
				public	int			TransitionSpanMin		{ get;  set; }
				public	int			TransitionSpanMax		{ get;  set; }
				public	int			TransitionSpeed			{ get;  set; }
				//...
				public	string	ButtonType		{ get;  set; }
				//...
				//...
				public	bool		IsHorizontal		{ get	=>		this._Horizontal	;
																					set			{	this._Horizontal	= value;
																										this.Dock	=	this._Horizontal	?	DockStyle.Top
																																									:	DockStyle.Left	;	}	}

			#endregion

		}
}
