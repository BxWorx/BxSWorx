using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Windows.Forms;
using System.Linq;
//.........................................................
//using BxSWorxFormTesting.Properties;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	public sealed class MItem	: IMItem
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private MItem()
					{
						this._SubItems	= new	Dictionary<string , IMItem>()	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	static	IMItem	Create()	=>	new	MItem();

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private	UC_MenuButton	_UCButton ;

				private readonly Dictionary<string , IMItem>	_SubItems;

			#endregion

			//===========================================================================================
			#region "Properties"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	UC_MenuButton	Button	{	get	{	if ( this._UCButton	== null )
				//																				{	this.CreateButton(); }
				//																			//...
				//																			return	this._UCButton;				}	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	Color					FocusIndicatorColour	{ get;  set; }
				public	int						TabIndex							{ get;  set; }
				public	string				ID										{ get;  set; }
				public	string				ImageID								{ get;  set; }
				public	string				Text									{ get;  set; }
				public	EventHandler	OnEventClick					{ get;  set; }
				public	bool					UseFlipFlop						{ get;  set; }

				//public	bool	Enabled	{ get =>	this.Button.Enabled					;
				//												set	=>	this.Button.Enabled	= value	;	}

				public	int		SubMenuCount	{	get	=>	this._SubItems.Count	;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	AddSubMenuItem( IMItem	item )	=>	this._SubItems.Add( item.ID , item );
				public	IList<IMItem>	GetSubMenuList()				=>	this._SubItems.Values.OrderByDescending( x=> x.TabIndex ).ToList();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//public	void	SetFocusState( bool	state = false )
				//	{
				//		this._UCButton.SetFocus	= state;
				//	}

			#endregion

			////===========================================================================================
			//#region "Methods: Private"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	private void	CreateButton()
			//		{
			//			if ( this._UCButton == null )
			//				{
			//					this._UCButton	= new	UC_MenuButton
			//						{
			//							// Fixed settings
			//								Dock	=	DockStyle.Top

			//							// User Settings
			//							,	TabIndex							=	this.TabIndex
			//							,	Name									=	this.ID
			//							, SetFocusColour				= this.FocusIndicatorColour
			//							,	SetImage							=	(Image)	Resources.ResourceManager.GetObject( this.ImageID )
			//							,	SetClickEventHandler	=	new System.EventHandler( this.OnEventClick )

			//							, ButtonTag	= this.ID
			//						};
			//				}
			//		}

			//#endregion

		}
}
