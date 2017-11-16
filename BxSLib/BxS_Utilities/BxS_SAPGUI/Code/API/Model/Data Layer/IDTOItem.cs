using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.DL
{
	public interface IDTOItem
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//.................................................
				Guid	ServiceID	{ get; set; }
				//.................................................
				Guid	WSID			{ get; set; }
				Guid	NodeID		{ get; set; }

			#endregion

		}
}