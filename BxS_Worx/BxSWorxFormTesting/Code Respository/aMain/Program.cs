using System;
using System.Windows.Forms;
//.........................................................
using BxS_Worx.UI.Dashboard;
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

					var DBCntlr	= DBController.Create();
					DBCntlr.Startup();

					Application.Run( DBCntlr.Form );

					//var w	= new BxS_Dashboard
					//	{
					//		Config	=	DBConfig.CreateWithDefaults()
					//	};
					//	w.LoadItem( SetupButton1() );
					//	w.LoadItem( SetupButton2() );

					////...
					//Application.Run( w );
				}

			//...

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static	IMItem SetupButton1()
					{
						IMItem x1			=	MItem.Create()					;
						x1.TabIndex		=	1												;
						x1.ID					=	"Settings"							;
						x1.ImageID		=	"icons8_Settings_25px"	;

						IMItem	x11		= MItem.Create()			;
						x11.TabIndex	=	1										;
						x11.ID				=	"Excel"							;
						x11.ImageID		=	"icons8_Excel_25px"	;

						IMItem	x12		= MItem.Create()		;
						x12.TabIndex	=	2									;
						x12.ID				=	"SAP"							;
						x12.ImageID		=	"icons8_SAP_25px"	;
						x12.Text			=	"My Text";

						x1.AddSubItem( x11 );
						x1.AddSubItem( x12 );

						return	x1;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static	IMItem SetupButton2()
					{
						IMItem x1 =	MItem.Create();

						x1.TabIndex				=	1												;
						x1.ID							=	"FlipFlop"							;
						x1.ImageID				=	"icons8_Settings_25px"	;
						x1.Text						= "Button1";

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
