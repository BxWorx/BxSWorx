using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML.DTO
{
	internal class DTOWorkspaceNode
		{
			internal string UUID { get; set; }
			internal string Name { get; set; }
			//...................................................
			internal Dictionary<string, DTOWorkspaceNodeItem>	Items { get; set; }
		}
}