using System;
using System.Collections.Generic;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOWorkspaceNode : IDTONode
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOWorkspaceNode()
					{
						this.Items	= new Dictionary<Guid, IDTOItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid		UUID				{ get; set; }
				public string	Description	{ get; set; }
				//...................................................
				public Dictionary<Guid, IDTOItem>	Items	{ get; set; }

			#endregion
		}
}