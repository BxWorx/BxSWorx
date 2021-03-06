﻿using System.Collections.Generic;
using System.Drawing;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.UI
{
	//***********************************************************************************************
	public enum ButtonType
		{
			Standard,
			Flipflop
		}

	//***********************************************************************************************
	public sealed class DBConfig : IDBConfig
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DBConfig()
					{
						this.LoadWidths();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDBConfig	CreateBlank()	=>	new	DBConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IDBConfig	CreateWithDefaults()
					{
						return	new DBConfig
							{
									ColourBack	= Color.FromArgb(255 , 031 , 031 , 031)
								,	ColourMove	= Color.FromArgb(255 , 045 , 045 , 045)
								,	ColourFocus = Color.FromArgb(150 , 000 , 100 , 000)
								,	ColourSlide	= Color.FromArgb(150 , 024 , 024 , 024)
									//...
								,	MenuType		= ButtonType.Standard
								,	SliderType	= ButtonType.Flipflop
								, SliderStep	= 70
							};
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const	int	WIDTHSTD		= 48	;
				private	const	int	WIDTHWIDE		= 180	;
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
				public	ButtonType	MenuType
															{	get	=>		this._MenuType;
																//...
																set			{	this._MenuType	= value	;
																					if ( this._Widths.TryGetValue( value , out this._MenuWidth ) )
																						{	this._MenuWidth	= WIDTHSTD; }
																				}
															}
				//...
				public	ButtonType	SliderType
															{	get	=>		this._SliderType;
																//...
																set			{	this._SliderType	= value	;
																					if ( this._Widths.TryGetValue( value , out this._SliderWidth ) )
																						{	this._SliderWidth	= WIDTHWIDE; }
																				}
															}
				//...
				public	int	MenuWidth		{ get	=>	this._MenuWidth		;	}
				public	int	SliderWidth	{ get	=>	this._SliderWidth	;	}
				//...
				public	int	SliderStep	{ get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void LoadWidths()
					{
						this._Widths	=	new	Dictionary< ButtonType , int >
							{
									{ ButtonType.Standard , WIDTHSTD	}
								,	{ ButtonType.Flipflop , WIDTHWIDE	}
							};
					}

			#endregion

		}
}
