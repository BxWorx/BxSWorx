using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	internal class MenuItem	: IMenuItem
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MenuItem()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"


				public	Dictionary<string , IMenuItem>	SubMenu;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	UC_MenuButton	Button { get; }

				public	int						TabIndex			{ get;  set; }
				public	string				ID						{ get;  set; }
				public	DockStyle			Dock					{ get;  set; }
				public	string				ImageID				{ get;  set; }
				public	EventHandler	OnEventClick	{ get;  set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnMenuButton_Click(	object sender , EventArgs e	)
					{

					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	void LoadData()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	CreateButton( Color	colourBack )
					{
						this.Button	= new	UC_MenuButton
							{
									FlatStyle		= FlatStyle.Flat
								,	Dock				=	DockStyle.Top
								,	Size				=	new	Size( 45 , 45 )
								,	BackColor		=	colourBack

								,	TabIndex		=	this.TabIndex
								,	Name				=	this.ID
								,	Image				=	(Image)	Resources.ResourceManager.GetObject( this.ImageID )
							};
						//...
						this.Button.FlatAppearance.BorderSize		= 0	;
						//...
						this.Button.Click		+= new System.EventHandler( this.OnEventClick );
						//...
						//this.Button.UseVisualStyleBackColor = true;
						//this.Button.Location = new System.Drawing.Point(1, 40);
						//this.Button.FlatAppearance.BorderColor = System.Drawing.Color.White;
						//this.Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Indigo;
					}




			#endregion

		}
}
