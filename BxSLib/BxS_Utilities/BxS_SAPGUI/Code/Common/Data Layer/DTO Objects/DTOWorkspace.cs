using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOWorkspace
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOWorkspace()
					{
						this.Nodes	= new	Dictionary<Guid, DTOWorkspaceNode>();
						this.Items	= new Dictionary<Guid, DTOWorkspaceItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Guid		UUID				{ get; set; }
				internal string Description	{ get; set; }
				//...................................................
				internal Dictionary<Guid, DTOWorkspaceNode>	Nodes { get; set; }
				internal Dictionary<Guid, DTOWorkspaceItem> Items { get; set; }

			#endregion
		}
}