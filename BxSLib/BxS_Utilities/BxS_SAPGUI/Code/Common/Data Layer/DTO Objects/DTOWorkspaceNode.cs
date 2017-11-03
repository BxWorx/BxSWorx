using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal class DTOWorkspaceNode : DTOBase
		{
			internal string UUID { get; set; }
			internal string Name { get; set; }
			//...................................................
			internal Dictionary<string, DTOWorkspaceItem>	Items { get; set; }
		}
}