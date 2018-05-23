using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	internal interface IMItem
		{
			#region "Properties"

				UC_MenuButton	Button								{ get; }
				//...
				Color					FocusIndicatorColour	{ get;  set; }
				int						TabIndex							{ get;  set; }
				string				ID										{ get;  set; }
				//DockStyle			DockStyle							{ get;  set; }
				string				ImageID								{ get;  set; }
				EventHandler	OnEventClick					{ get;  set; }

				bool	Enabled												{ get;  set; }

			#endregion

				void	SetFocusState( bool	state = false );
				void	AddSubMenuItem( IMItem	item );
				IList<IMItem>	GetSubMenuList();
		}
}
