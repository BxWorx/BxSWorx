using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.DL
{
	public interface IDTOWorkspace
		{
			#region "Properties"

				Guid		UUID				{ get; set; }
				string Description	{ get; set; }
				//...................................................
				Dictionary<Guid, IDTOWorkspaceNode>	Nodes { get; set; }
				Dictionary<Guid, IDTOWorkspaceItem> Items { get; set; }

			#endregion
		}
}