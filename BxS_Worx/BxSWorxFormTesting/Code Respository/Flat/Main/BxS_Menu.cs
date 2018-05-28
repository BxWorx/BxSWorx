using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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

				private	Dictionary<string , IMItem>			_MenuItems	;
				private	Dictionary<string	,	MenuButton>	_Buttons		;
				//...
				private	string	_MBtnPrevID		;
				private	string	_SBtnPrevID		;
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
						this.ConfigureButtons(item);
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	Color		ColourBack		{ get	=>	this.Config.ColourBack	; }
				private	Color		ColourMove		{ get	=>	this.Config.ColourMove	; }
				private	Color		ColourSlide		{ get	=>	this.Config.ColourSlide	; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupStartup()
					{
						this._MenuItems		= new	Dictionary<string, IMItem>()			;
						//...
						this._Buttons			= new	Dictionary<string, MenuButton>()	;
						this._MBtnPrevID		= string.Empty													;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void BxS_Menu_Load(object sender , EventArgs e)
					{
						this.xpnl_Menu.Width	=	this.Config.MenuWidth	;
						//...
						this.SetupMove()				;
						this.SetupSliderpanel()	;
						//...
						foreach ( MenuButton item in this._Buttons.Values.OrderByDescending( x => x._Button.Index ) )
							{
								this.xpnl_Menu.Controls.Add( (UserControl)item._Button );
							}
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Menu Panel"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnMenuButton_Click( object sender , EventArgs e	)
					{
						var			lo_Btn	= (Panel) sender;
						string	lc_Tag	= lo_Btn.Tag.ToString();
						//...
						lo_Btn.Enabled	= false;
						//...
						if ( lc_Tag.Equals( this._MBtnPrevID ) )
							{
							}
						else
							{
								// shut slide panel first and remove previous buttons, clear select indicator
								//
								if ( ! this.xpnl_SlidePanel.Width.Equals(0) )
									{
										this.ActivateSliderPanel();
									}
								this.xpnl_SlidePanel.Controls.Clear();
								if ( this._Buttons.TryGetValue( this._MBtnPrevID , out MenuButton lo_BtnX ) )
									{
										lo_BtnX._Button.HasFocus	= false;
									}
								// Add slide panel buttons
								//
								if ( this._Buttons.TryGetValue( lc_Tag , out MenuButton lo_Itm ) )
									{
										if ( lo_Itm.SubMenuCount.Equals(0) )
											{
												this.ActivateSliderPanel();
											}
										else
											{
												foreach ( IUC_BtnBase lo_SBtn in lo_Itm.GetSubList() )
													{
														this.xpnl_SlidePanel.Controls.Add( (UserControl)lo_SBtn );
													}
											}
										//...
										lo_Itm._Button.HasFocus	= true;
										this._MBtnPrevID	= lc_Tag;
									}
							}
						//...
						this.ActivateSliderPanel();
						lo_Btn.Enabled	= true;
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Button Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	void ConfigureButtons( IMItem item )
					{
						var x = new MenuButton
							{
								_Button = this.CreateButton( item , this.Config.MenuType , true )
							};
						//...
						foreach ( IMItem lo_Item in item.GetSubList() )
							{
								x._SubButtons.Add ( lo_Item.ID , this.CreateButton( lo_Item , this.Config.SliderType ) );
							}
						//...
						this._Buttons.Add( item.ID , x );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IUC_BtnBase	CreateButton( IMItem	item , ButtonType type , bool IsRootNode = false )
					{
						IUC_BtnBase	lo_Btn	=	type.Equals(ButtonType.Flipflop)	?								new	UC_BtnFlipFlop()
																																		:	(IUC_BtnBase)	new	UC_BtnSelected() ;
						//...
						lo_Btn.SetFocusColour		=	this.Config.ColourFocus	;
						lo_Btn.Index						=	item.TabIndex						;
						lo_Btn.SetName					=	item.ID									;
						lo_Btn.SetTag						= item.ID									;

						if ( ! string.IsNullOrEmpty( item.Text ) )
							{
								lo_Btn.SetText	=	item.Text	;
							}

						if ( ! string.IsNullOrEmpty( item.ImageID	) )
							{
								lo_Btn.SetImage	=	(Image)	Resources.ResourceManager.GetObject( item.ImageID );
							}
						//...
						if ( IsRootNode )
							{
									lo_Btn.SetClickEventHandler	= this.OnMenuButton_Click ;
							}
						else
							{
								//if ( item.OnEventClick != null)
								//	{
										//lo_Btn.SetClickEventHandler		=	item.OnEventClick;
										lo_Btn.SetClickEventHandler		=	this.OnSliderButton_Click;
								//	}
							}
						//...
						return	lo_Btn;
					}

			#endregion

			//===========================================================================================
			#region "Routines: Private: Slider panel"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void OnSliderButton_Click( object sender , EventArgs e	)
					{
						var			lo_Btn	= (Panel) sender;
						string	lc_Tag	= lo_Btn.Tag.ToString();
						//...
						lo_Btn.Enabled	= false;
						//...
						if ( lc_Tag.Equals( this._SBtnPrevID ) )
							{
							}
						else
							{
								if ( this._Buttons.TryGetValue( this._MBtnPrevID , out MenuButton lo_MItm ) )
									{
										if ( lo_MItm._SubButtons.TryGetValue( lc_Tag , out IUC_BtnBase lo_SItm ) )
											{
												//this._SBtnPrevID	= lc_Tag;
												lo_SItm.HasFocus	= ! lo_SItm.HasFocus;
											}
									}
							}
						//...
						lo_Btn.Enabled	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void SetupSliderpanel()
					{
						this._SlideWidth	=	this.Config.SliderWidth	;
						this._SlideStep		= this.Config.SliderStep	;
						//...
						this.xpnl_SlidePanel.Width			=	00								;
						this.xpnl_SlidePanel.BackColor	= this.ColourSlide	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ActivateSliderPanel()
					{
						this._SlideIncr		=	this.Config.SliderType.Equals(ButtonType.Standard)	?	10	: this._SlideStep	;
						//...
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
						#region "Constructors"

							//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
							internal	MenuButton()
								{
									this._SubButtons		=	new	Dictionary<string , IUC_BtnBase>();
								}

						#endregion
 
						//=====================================================================================
						#region "Declarations"

							internal	IUC_BtnBase												_Button		;
							internal	Dictionary<string , IUC_BtnBase>	_SubButtons	;

						#endregion

						//=====================================================================================
						#region "Properties"

							//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
							internal	int	SubMenuCount	{	get	=>	this._SubButtons.Count	;	}

						#endregion

						//=====================================================================================
						#region "Methods: Exposed"

							//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
							internal IList<IUC_BtnBase> GetSubList()	=>	this._SubButtons.Values
																															.OrderByDescending( x=> x.Index )
																																.ToList();

						#endregion

					}

		#endregion

		}
}
