using System;
using System.Collections.Generic;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOWorkspaceNode : IDTOWorkspaceNode
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOWorkspaceNode()
					{
						this.Items	= new Dictionary<Guid, IDTOWorkspaceItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid		UUID				{ get; set; }
				public string	Description	{ get; set; }
				//...................................................
				public Dictionary<Guid, IDTOWorkspaceItem>	Items	{ get; set; }

			#endregion
		}
}