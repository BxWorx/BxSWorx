using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
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
								, TransitionSpan	= 48
								, TransitionSpeed	= 1
								,	TransitionMin		= 0
								//...
								,	ButtonType			= ButtonTypes.TypeStandard
							};
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ID			{ get;  set; }
				public	int			SeqNo		{ get;  set; }
				//...
				public	Color		ColourBack		{ get;  set; }
				public	Color		ColourFocus		{ get;  set; }
				//...
				public	bool		IsHorizontal		{ get;  set; }
				public	bool		ShowOnstartup		{ get;  set; }
				//...
				public	int			TransitionSpan		{ get;  set; }
				public	int			TransitionSpeed		{ get;  set; }
				public	int			TransitionMin			{ get;  set; }
				//...
				public	string	ButtonType				{ get;  set; }

			#endregion

		}
}
