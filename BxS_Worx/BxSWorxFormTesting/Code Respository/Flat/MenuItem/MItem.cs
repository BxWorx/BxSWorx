using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
//.........................................................
using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	internal class MItem	: IMItem
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MItem()
					{
						this._SubMenuItems	= new	Dictionary<string , IMItem>()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	UC_MenuButton	_UCButton ;

				private readonly Dictionary<string , IMItem>	_SubMenuItems;

			#endregion

			//===========================================================================================
			#region "Properties"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	UC_MenuButton	Button	{	get	{	if ( this._UCButton	== null )
																								{	this.CreateButton(); }
																							//...
																							return	this._UCButton;				}	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	Color					FocusIndicatorColour	{ get;  set; }
				public	int						TabIndex							{ get;  set; }
				public	string				ID										{ get;  set; }
				public	string				ImageID								{ get;  set; }
				public	EventHandler	OnEventClick					{ get;  set; }

				public	bool	Enabled	{ get =>	this.Button.Enabled					;
																set	=>	this.Button.Enabled	= value	;	}

				public	int		SubMenuCount	{	get	=>	this._SubMenuItems.Count	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	AddSubMenuItem( IMItem	item )	=>	this._SubMenuItems.Add( item.ID , item );
				public	IList<IMItem>	GetSubMenuList()				=>	this._SubMenuItems.Values.OrderByDescending( x=> x.TabIndex ).ToList();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IMItem	Create()	=>	new	MItem();

				public	void	SetFocusState( bool	state = false )
					{
						this._UCButton.SetFocus	= state;
					}


			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	CreateButton()
					{
						if ( this._UCButton == null )
							{
								this._UCButton	= new	UC_MenuButton
									{
										// Fixed settings
											Dock	=	DockStyle.Top

										// User Settings
										,	TabIndex							=	this.TabIndex
										,	Name									=	this.ID
										, SetFocusColour				= this.FocusIndicatorColour
										,	SetImage							=	(Image)	Resources.ResourceManager.GetObject( this.ImageID )
										,	SetClickEventHandler	=	new System.EventHandler( this.OnEventClick )

										, ButtonTag	= this.ID
									};
							}
					}

			#endregion

		}
}
