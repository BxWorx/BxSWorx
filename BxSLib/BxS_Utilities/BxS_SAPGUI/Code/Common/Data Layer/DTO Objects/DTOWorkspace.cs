using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal class DTOWorkspace
		{
			internal Guid		UUID { get; set; }
			internal string Name { get; set; }
			//...................................................
			internal Dictionary<Guid, DTOWorkspaceNode>	Nodes { get; set; }
			internal Dictionary<Guid, DTOWorkspaceItem> Items { get; set; }
		}
}