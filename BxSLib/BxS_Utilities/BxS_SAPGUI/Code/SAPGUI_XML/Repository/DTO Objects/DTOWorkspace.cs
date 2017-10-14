using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML.Repository
{
	internal class DTOWorkspace
		{
			internal string UUID { get; set; }
			internal string Name { get; set; }
			//...................................................
			internal Dictionary<string, DTOWorkspaceNode>			Nodes { get; set; }
			internal Dictionary<string, DTOWorkspaceNodeItem> Items { get; set; }
		}
}