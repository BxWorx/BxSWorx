using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API.DL
{
	public interface IDTOWorkspace
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//...................................................
				string	Description	{ get; set; }

				Dictionary<Guid, IDTONode>	Nodes { get; set; }
				Dictionary<Guid, IDTOItem>	Items { get; set; }

			#endregion
		}
}