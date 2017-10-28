using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.DTO
{
	internal class DTOWorkspace : DTOBase
		{
			internal string UUID { get; set; }
			internal string Name { get; set; }
			//...................................................
			internal Dictionary<string, DTOWorkspaceNode>			Nodes { get; set; }
			internal Dictionary<string, DTOWorkspaceNodeItem> Items { get; set; }
		}
}