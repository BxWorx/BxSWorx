﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//.........................................................
using BxS_WorxIPX.Main;
using BxS_WorxIPX.NCO;
using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	public partial class BxS_Main : Form
		{

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BxS_Main()
					{
						InitializeComponent();
						//...
						this.SetupStartup()			;
						this.SetupMove()				;
						this.SetupSlidepanel()	;
						this.SetupButtons()			;
						this.AddButtons()				;






						this._IPXCntlr	=		new	Lazy<IPX_Controller>	(	()=> IPX_Controller.Instance  ) ;
					}

		private void Xbtn_Menu_Click(	object sender , EventArgs e	)
			{
				this.xbtn_Menu.Enabled	= false;
				this.ActivateSlidePanel();
			}


		//private	BindingList<IDTO_Session>		BDCList		{ get; set; }
		//private Lazy<UC_DGVView>	_DGV;

		private void Button1_Click(object sender , EventArgs e)
			{
				IDTO_SessionRequest x = this._IPXCntlr.Value.NCOx_Controller.NewSAPSessionRequest();
				this._DGVP.Value.LoadData(x);
					//{	this.xpnl_UC.Controls.Remove( this._DGVP.Value.View.ViewUC );	}
					{	this.xspl_UC.Panel2Collapsed	= true;
						this.xspl_UC.Panel1.Controls.Add( this._DGVP.Value.View.ViewUC );	}

				//if (this._DGV.Value.InUse)
				//	{	this.xpnl_UC.Controls.Remove( this._DGV.Value )	;	}
				//else

				//this._DGV.Value.InUse	= ! this._DGV.Value.InUse;
			}

		private readonly Lazy<IPX_Controller>	_IPXCntlr;
		private	Lazy<UC_DGVPresenter>	_DGVP;

		private void BxS_Main_Load(object sender , EventArgs e)
			{
				IUC_DGVModel	lo_Md		= new UC_DGVModel( this._IPXCntlr.Value.NCOx_Controller	)	;
				IUC_DGVView		lo_Vw		=	new	UC_DGVView()	;
				//...
				this._DGVP	= new	Lazy<UC_DGVPresenter>(	()=>	new	UC_DGVPresenter( lo_Md , lo_Vw ) );
				//this._DGV		= new	Lazy<UC_DGVView>(	()=> new UC_DGVView() );

				//var x = new DTO_Session();
				//x.UserID	= "AAAA";
				//this.BDCList.Add(x);

				//this._DGV.Value.LoadData( this.BDCList );
			}



		private void button5_Click(object sender , EventArgs e)
			{
				IDTO_SessionRequest x = this._IPXCntlr.Value.NCOx_Controller.NewSAPSessionRequest();
				x.User	= "100";
				this._DGVP.Value.LoadData(x);
			}

		private void button6_Click(object sender , EventArgs e)
			{
				this.xdlg_Colour.ShowDialog();
				this._DGVP.Value.Colour	=	this.xdlg_Colour.Color;
			}


		private class ButtonDefinition
			{
				public	ButtonDefinition()
					{
						this.Children		= new	Dictionary<string, ButtonDefinition>();
					}

				public	int						TabIndex			{ get;  set; }
				public	string				ID						{ get;  set; }
				public	DockStyle			Dock					{ get;  set; }
				public	string				ImageID				{ get;  set; }
				public	EventHandler	OnEventClick	{ get;  set; }

				public	Dictionary<string , ButtonDefinition>	Children;

				public Button	Button	{ get;	private set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	CreateButton( Color	colourBack )
					{
						this.Button	= new	Button
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	ButtonDefinition	Create()	=>	new	ButtonDefinition();
			}





		//.

			//===========================================================================================
			#region "Declarations"

				private	Dictionary<string , ButtonDefinition>	MButtons;
				//...
				private	Color	_ColourBack		;
				private	Color	_ColourMove		;
				private	Color	_ColourSlide	;
				//...
				private	bool	_MoveActive		;
				private	Point	_MoveLocation	;
				//...
				private int		_SlideWidth	;
				private	int		_SlideIncr	;
				private int		_SlideStep	;

			#endregion

			//===========================================================================================
			#region "Routines: Private: General"



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void SetupButtons()
					{
						var x =	ButtonDefinition.Create();
						//...
						x.TabIndex			=	1												;
						x.ID						=	"Settings"							;
						x.ImageID				=	"icons8_Settings_25px"	;
						x.OnEventClick	=	this.OnMenuButton_Click	;
						x.CreateButton( this._ColourBack );

						var y =	ButtonDefinition.Create();
						//...
						y.TabIndex			=	1												;
						y.ID						=	"Settings"							;
						y.ImageID				=	"icons8_Microsoft_Excel_25px_1"	;
						y.OnEventClick	=	this.OnMenuButton_Click	;
						y.CreateButton( this._ColourBack );

						x.Children.Add( y.ID , y );
						//...
						this.MButtons.Add( x.ID , x );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddButtons()
					{
						foreach ( ButtonDefinition lo_Item in this.MButtons.Values.OrderBy( x => x.TabIndex ) )
							{
								this.xpnl_Menu.Controls.Add( lo_Item.Button );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnMenuButton_Click(	object sender , EventArgs e	)
					{
						var x = (Button) sender;
						//...
						x.Enabled	= false;
						//...
						if ( this.MButtons.TryGetValue( x.Name , out ButtonDefinition lo_Btn ) )
							{
								foreach ( ButtonDefinition lo_SBtn in lo_Btn.Children.Values.OrderBy( v => v.TabIndex ) )
									{
										this.xpnl_SlidePanel.Controls.Add( lo_SBtn.Button );
									}
							}
						//...
						this.ActivateSlidePanel();
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupStartup()
					{
						this.MButtons	= new	Dictionary<string, ButtonDefinition>();
						//...
						this._ColourBack	= Color.FromArgb( 255	, 31 , 31 , 31 );
						this._ColourSlide	= Color.FromArgb( 150	, 24 , 24 , 24 );
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Slide panel"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupSlidepanel()
					{
						this._SlideWidth								= 45	;
						this._SlideStep									= 05	;
						//...
						this.xpnl_SlidePanel.Width			=	00	;
						this.xpnl_SlidePanel.BackColor	= this._ColourSlide;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ActivateSlidePanel()
					{
						this._SlideIncr		=	this.xpnl_SlidePanel.Width.Equals(0) ? this._SlideStep : this._SlideStep * -1 ;
						//...
						this.xtmr_SlidePanel.Start();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void DeactivateSlidePanel()
					{
						this.xtmr_SlidePanel.Stop();
						this.xbtn_Menu.Enabled	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnSlidePanel_Tick( object sender , EventArgs e )
					{
						this.xpnl_SlidePanel.Width	+= this._SlideIncr;
						//...
						if (		this.xpnl_SlidePanel.Width	>= this._SlideWidth
								||	this.xpnl_SlidePanel.Width	<= 0								)
							{
								this.DeactivateSlidePanel();
							}
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Window state"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnFormClose_Click( object sender , EventArgs e )
					{
						this.Close();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnFormMinimise_Click( object sender , EventArgs e )
					{
						this.WindowState	= FormWindowState.Minimized;
					}

			#endregion
 
			//===========================================================================================
			#region "Routines: Private: Window move"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupMove()
					{
						this._ColourMove	=	Color.FromArgb( 150 , 42 , 42 , 42 )	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnWindowHeader_MouseDown( object sender , MouseEventArgs e )
					{
						this._MoveActive		              = ! this._MoveActive	;
						this._MoveLocation	              = e.Location					;
						this.xpnl_WindowHeader.BackColor	=	this._ColourMove		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnWindowHeader_MouseUp( object sender , MouseEventArgs e )
					{
						this._MoveActive									= ! this._MoveActive	;
						this.xpnl_WindowHeader.BackColor	=		this._ColourBack	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnWindowHeader_MouseMove( object sender , MouseEventArgs e )
					{
						if ( this._MoveActive )
							{
								this.Location		= new	Point(	( this.Location.X	- this._MoveLocation.X ) + e.X
																						,	(	this.Location.Y	- this._MoveLocation.Y ) + e.Y );
								this.Update();
							}
					}

			#endregion

		}
}
