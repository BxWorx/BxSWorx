using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal interface IDTOItem
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//.................................................
				Guid	ServiceID	{ get; set; }
				//.................................................
				Guid	WSID		{ get; set; }
				Guid	NodeID	{ get; set; }

			#endregion

		}
}