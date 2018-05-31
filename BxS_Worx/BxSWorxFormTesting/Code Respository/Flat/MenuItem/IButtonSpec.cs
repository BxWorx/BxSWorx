using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Menu
{
	public interface IButtonSpec
		{
			#region "Properties"

				int						TabIndex							{ get;  set; }
				string				ID										{ get;  set; }
				string				ImageID								{ get;  set; }
				string				Text									{ get;  set; }

			#endregion

		}
}
