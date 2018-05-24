using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	public interface IMItem
		{
			#region "Properties"

				Color					FocusIndicatorColour	{ get;  set; }
				int						TabIndex							{ get;  set; }
				string				ID										{ get;  set; }
				string				ImageID								{ get;  set; }
				EventHandler	OnEventClick					{ get;  set; }

				//bool	Enabled												{ get;  set; }

				int		SubMenuCount									{ get; }

			#endregion

				//void	SetFocusState( bool	state = false );
				void	AddSubMenuItem( IMItem	item );
				IList<IMItem>	GetSubMenuList();
		}
}
