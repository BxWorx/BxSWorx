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

				ButtonType		ButtonType						{ get;  set; }
				int						TabIndex							{ get;  set; }
				string				ID										{ get;  set; }
				string				ImageID								{ get;  set; }
				string				Text									{ get;  set; }
				EventHandler	OnEventClick					{ get;  set; }

				int		SubMenuCount									{ get; }

			#endregion

				void	AddSubItem( IMItem	item );
				IList<IMItem>	GetSubList();
		}
}
