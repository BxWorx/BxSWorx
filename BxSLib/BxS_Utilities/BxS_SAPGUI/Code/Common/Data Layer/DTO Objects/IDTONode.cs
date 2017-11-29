using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal interface IDTONode
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