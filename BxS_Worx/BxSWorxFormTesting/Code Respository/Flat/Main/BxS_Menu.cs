using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//.........................................................
using BxS_WorxExcel.UI.Menu;
using BxS_WorxExcel.UI.UC;

using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	public partial class BxS_Menu : Form
		{
			//===========================================================================================
			#region "Routines: Private: Button handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddButtons()
					{
						foreach ( IMItem lo_Item in this._MenuItems.Values.OrderByDescending( x => x.TabIndex ))
							{
								//this.xpnl_Menu.Controls.Add( lo_Item.Button );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnMenuButton_Click( object sender , EventArgs e	)
					{
						var			lo_Btn	= (Button) sender;
						string	lc_Tag	= lo_Btn.Tag.ToString();
						//...
						lo_Btn.Enabled	= false;
						//...
						if ( lc_Tag.Equals( this._BtnPrevID ) )
							{

							}
						else
							{
								// shut slide panel first and remove previous buttons, clear select indicator
								//
								if ( ! this.xpnl_SlidePanel.Width.Equals(0) )
									{
										this.ActivateSlidePanel();
									}
								this.xpnl_SlidePanel.Controls.Clear();
								if ( this._MenuItems.TryGetValue( this._BtnPrevID , out IMItem lo_BtnX ) )
									{
										//lo_BtnX.SetFocusState( false );
									}
								// Add slide panel buttons
								//
								if ( this._MenuItems.TryGetValue( lc_Tag , out IMItem lo_Itm ) )
									{
										if ( lo_Itm.SubMenuCount.Equals(0) )
											{
												this.ActivateSlidePanel(true);
											}
										else
											{
												foreach ( IMItem lo_SBtn in lo_Itm.GetSubMenuList() )
													{
														//this.xpnl_SlidePanel.Controls.Add( lo_SBtn.Button );
													}
											}
										//...
										//lo_Itm.SetFocusState( true );
										this._BtnPrevID	= lc_Tag;
									}
							}
						//...
						this.ActivateSlidePanel();
						lo_Btn.Enabled	= true;
					}

			#endregion




			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BxS_Menu()
					{
						InitializeComponent()	;
						//...
						this.SetupStartup()		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const	int	BUTTONPANELWIDTH	= 48	;
				//...
				private	Dictionary<string , IMItem>			_MenuItems	;
				private	Dictionary<string	,	MenuButton>	_Buttons		;
				//...
				private	string	_BtnPrevID		;
				//...
				private	bool		_MoveActive		;
				private	Point		_MoveLocation	;
				//...
				private int			_SlideWidth		;
				private	int			_SlideIncr		;
				private int			_SlideStep		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public IMConfig	Config	{ get;	set; }

		#endregion

			//===========================================================================================
			#region "Routines: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadItem( IMItem item )
					{
						this._MenuItems.Add( item.ID , item );
						//...
						//Task.Run(	()	=>	this.ConfigureButtons(item)	).ConfigureAwait(false);
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	Color		ColourBack		{ get	=>	this.Config.ColourBack		; }
				private	Color		ColourMove		{ get	=>	this.Config.ColourMove		; }
				private	Color		ColourSlide		{ get	=>	this.Config.ColourSlide	; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupStartup()
					{
						this._MenuItems		= new	Dictionary<string, IMItem>()			;
						//...
						this._Buttons			= new	Dictionary<string, MenuButton>()	;
						this._BtnPrevID		= string.Empty													;
						//...
						this.xpnl_Menu.Width	= BUTTONPANELWIDTH;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_Menu_Load(object sender , EventArgs e)
					{
						this.SetupMove()				;
						this.SetupSlidepanel()	;
						//...
						foreach ( MenuButton item in this._Buttons.Values.OrderByDescending( x => x._Button.MyTabIndex ) )
							{
								this.xpnl_Menu.Controls.Add( (UserControl)item._Button );
							}
						//await Task.Run(	()=>	{
							//										}
							//						).ConfigureAwait(false);
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Button Handling"


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void ConfigureButtons()
					{
						foreach ( IMItem lo_Item in this._MenuItems.Values )
							{
								var x = new MenuButton
									{
										_Button = this.CreateButton(lo_Item)
									};

								this._Buttons.Add( lo_Item.ID , x );
							}

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IUC_Button	CreateButton( IMItem	item , bool rootNode	= false)
					{
						IUC_Button	lo_Btn	= item.UseFlipFlop	?	new	UC_FlipFlop()	: (IUC_Button) new  UC_MenuButton() ;
						//...
						lo_Btn.SetFocusColour		=	this.Config.ColourFocus	;
						lo_Btn.MyTabIndex				=	item.TabIndex						;
						lo_Btn.SetName					=	item.ID									;
						lo_Btn.SetButtonTag	    = item.ID									;
						lo_Btn.SetImage					=	(Image)	Resources.ResourceManager.GetObject( item.ImageID );
						//...
						if ( rootNode )
							{
								lo_Btn.SetClickEventHandler	= this.OnMenuButton_Click ;
							}
						else
							{
								if ( item.OnEventClick != null)
									{
										lo_Btn.SetClickEventHandler	=	new System.EventHandler( item.OnEventClick );
									}
							}
						//...
						return	lo_Btn;
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Slide panel"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupSlidepanel()
					{
						this._SlideWidth								= BUTTONPANELWIDTH;
						this._SlideStep									= 03	;
						//...
						this.xpnl_SlidePanel.Width			=	00	;
						this.xpnl_SlidePanel.BackColor	= this.ColourSlide;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ActivateSlidePanel( bool	slow	= false )
					{
						this._SlideIncr		=	slow	?	1	: this._SlideStep	;
						if ( !this.xpnl_SlidePanel.Width.Equals(0) )	this._SlideIncr	*= -1;
						//...
						do
							{
								this.xpnl_SlidePanel.Width	+= this._SlideIncr;

							} while (			this.xpnl_SlidePanel.Width	< this._SlideWidth
												&&	this.xpnl_SlidePanel.Width	> 0									);
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
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnWindowHeader_MouseDown( object sender , MouseEventArgs e )
					{
						this._MoveActive		              = ! this._MoveActive	;
						this._MoveLocation	              = e.Location					;
						this.xpnl_WindowHeader.BackColor	=	this.ColourMove		;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnWindowHeader_MouseUp( object sender , MouseEventArgs e )
					{
						this._MoveActive									= ! this._MoveActive	;
						this.xpnl_WindowHeader.BackColor	=		this.ColourBack	;
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
 
			//===========================================================================================
			#region "Private classes"

				//_________________________________________________________________________________________
				private	class MenuButton
					{
						#region "Declarations"

							//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
							internal	MenuButton()
								{
									this._Buttons		=	new	Dictionary<string, IUC_Button>();
								}

						#endregion
 
						//===========================================================================================
						#region "Declarations"

							internal	IUC_Button											_Button		;
							internal	Dictionary<string , IUC_Button>	_Buttons	;

						#endregion

					}

		#endregion

		}
}
