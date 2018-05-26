using System.Collections.Generic;
using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	//***********************************************************************************************
	public enum ButtonType
		{
			Standard,
			Flipflop
		}

	//***********************************************************************************************
	public sealed class MConfig : IMConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private MConfig()
					{
						this.LoadWidths();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IMConfig	CreateBlank()	=>	new	MConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IMConfig	CreateWithDefaults()
					{
						return	new MConfig
							{
									ColourBack	= Color.FromArgb(255 , 031 , 031 , 031)
								,	ColourMove	= Color.FromArgb(255 , 045 , 045 , 045)
								,	ColourFocus = Color.FromArgb(150 , 000 , 100 , 000)
								,	ColourSlide	= Color.FromArgb(150 , 024 , 024 , 024)
									//...
								,	MenuButtonType	= ButtonType.Standard
								,	SlideButtonType = ButtonType.Flipflop
							};
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const	int	PANELWIDTHSTD		= 48	;
				private	const	int	PANELWIDTHWIDE	= 180	;
				//...
				private	ButtonType	_MenuType		;
				private	ButtonType	_SliderType	;
				//...
				private	int	_MenuWidth		;
				private	int	_SliderWidth	;
				//...
				private	Dictionary<ButtonType,int>	_Widths;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Color		ColourBack		{ get;  set; }
				public	Color		ColourMove		{ get;  set; }
				public	Color		ColourFocus		{ get;  set; }
				public	Color		ColourSlide		{ get;  set; }
				//...
				public	ButtonType	MenuButtonType
															{	get	=>		this._MenuType;
																//...
																set			{	this._MenuType	= value	;
																					if ( this._Widths.TryGetValue( value , out this._MenuWidth ) )
																						{	this._MenuWidth	= PANELWIDTHSTD; }
																				}
															}
				//...
				public	ButtonType	SlideButtonType
															{	get	=>		this._SliderType;
																//...
																set			{	this._SliderType	= value	;
																					if ( this._Widths.TryGetValue( value , out this._SliderWidth ) )
																						{	this._SliderWidth	= PANELWIDTHWIDE; }
																				}
															}
				//...
				public	int	MenuWidth		{ get	=>	this._MenuWidth		;	}
				public	int	SlideWidth	{ get	=>	this._SliderWidth	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void LoadWidths()
					{
						this._Widths	=	new	Dictionary< ButtonType , int >
							{
									{ ButtonType.Standard , PANELWIDTHSTD		}
								,	{ ButtonType.Flipflop , PANELWIDTHWIDE	}
							};
					}

			#endregion

		}
}
