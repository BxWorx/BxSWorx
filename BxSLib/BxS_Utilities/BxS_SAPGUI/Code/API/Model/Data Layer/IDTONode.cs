using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API.DL
{
	public interface IDTONode
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//.................................................
				string	Description	{ get; set; }

				Dictionary<Guid, IDTOItem>	Items { get; set; }
				//.................................................
				Guid	WSID	{ get; set; }

			#endregion

		}
}