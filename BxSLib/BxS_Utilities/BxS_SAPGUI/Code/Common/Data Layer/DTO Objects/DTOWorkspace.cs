using System;
using System.Collections.Generic;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOWorkspace :IDTOWorkspace
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOWorkspace()
					{
						this.Nodes	= new	Dictionary<Guid, IDTOWorkspaceNode>();
						this.Items	= new Dictionary<Guid, IDTOWorkspaceItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid		UUID					{ get; set; }
				public string Description		{ get; set; }
				//...................................................
				public Dictionary<Guid, IDTOWorkspaceNode> Nodes	{ get; set; }
				public Dictionary<Guid, IDTOWorkspaceItem> Items	{ get; set; }

			#endregion
		}
}