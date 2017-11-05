using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal class DTOWorkspaceNode
		{
			internal Guid		UUID { get; set; }
			internal string Name { get; set; }
			//...................................................
			internal Dictionary<Guid, DTOWorkspaceItem>	Items { get; set; }
		}
}