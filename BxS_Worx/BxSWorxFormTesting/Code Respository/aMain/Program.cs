using System;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.UI.Forms;
using BxS_WorxExcel.UI.Menu;

namespace BxSWorxFormTesting
	{
	internal static class Program
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			/// <summary>
			/// The main entry point for the application.
			/// </summary>
			[STAThread]
			private static void Main()
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					//...

					var w	= new BxS_Menu
						{
							Config	=	MConfig.CreateWithDefaults()
						};
						w.LoadItem( SetupButton() );

					//...
					Application.Run( w );
				}

			//...

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static	IMItem SetupButton()
					{
						// *** first button ***
						IMItem x1 =	MItem.Create();
						x1.RootNode				= true;
						x1.TabIndex				=	1												;
						x1.ID							=	"Settings"							;
						x1.ImageID				=	"icons8_Settings_25px"	;

						return	x1;
					}
				//		//...
				//		x1.OnEventClick		=	this.OnMenuButton_Click	;
				//		x1.TabIndex				=	1												;
				//		x1.ID							=	"Settings"							;
				//		x1.ImageID				=	"icons8_Settings_25px"	;
				//		x1.FocusIndicatorColour	= Color.FromArgb( 255 , 0 , 255 , 0 );

				//		IMItem y1 =	MItem.Create();
				//		//...
				//		y1.TabIndex				=	1												;
				//		y1.ID							=	"Settings"							;
				//		y1.ImageID				=	"icons8_Excel_25px"			;
				//		y1.OnEventClick		=	this.OnMenuButton_Click	;

				//		x1.AddSubMenuItem( y1 );
				//		//...

				//		// *** second button ***
				//		IMItem x2 =	MItem.Create();
				//		//...
				//		x2.TabIndex				=	2												;
				//		x2.ID							=	"Menu"									;
				//		x2.ImageID				=	"icons8_Excel_25px"			;
				//		x2.OnEventClick		=	this.OnMenuButton_Click	;
				//		x2.FocusIndicatorColour	= Color.FromArgb( 255 , 0 , 255 , 0 );

				//		IMItem y2 =	MItem.Create();
				//		//...
				//		y2.TabIndex				=	1												;
				//		y2.ID							=	"Menu"									;
				//		y2.ImageID				=	"icons8_SAP_25px"				;
				//		y2.OnEventClick		=	this.OnMenuButton_Click	;

				//		x2.AddSubMenuItem( y2 );

				//		// *** third button ***
				//		IMItem x3 =	MItem.Create();
				//		//...
				//		x3.TabIndex				=	3												;
				//		x3.ID							=	"Action"									;
				//		x3.ImageID				=	"icons8_SAP_25px"			;
				//		x3.OnEventClick		=	this.OnMenuButton_Click	;
				//		x3.FocusIndicatorColour	= Color.FromArgb( 170 , 0 , 255 , 0 );

				//		//...
				//		this._MenuItems.Add( x1.ID , x1 );
				//		this._MenuItems.Add( x2.ID , x2 );
				//		this._MenuItems.Add( x3.ID , x3 );
				//	}

		}
	}
