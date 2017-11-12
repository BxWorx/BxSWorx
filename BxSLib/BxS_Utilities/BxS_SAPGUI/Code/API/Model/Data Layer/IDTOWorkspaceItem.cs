using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.DL
{
	public interface IDTOWorkspaceItem
		{
			Guid UUID			{ get; set; }
			Guid ServiceID { get; set; }
		}
}