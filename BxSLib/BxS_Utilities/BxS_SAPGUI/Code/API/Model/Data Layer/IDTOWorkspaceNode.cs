using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.DL
{
	public interface IDTOWorkspaceNode
		{
			#region "Properties"

				Guid		UUID				{ get; set; }
				string	Description	{ get; set; }
				//...................................................
				Dictionary<Guid, IDTOWorkspaceItem>	Items { get; set; }

			#endregion

		}
}