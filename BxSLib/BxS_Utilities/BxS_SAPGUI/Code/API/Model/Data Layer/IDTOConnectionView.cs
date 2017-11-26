using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public interface IDTOConnectionView
		{
			#region "Properties"

				string	HierID				{ get; set; }
				string	HierID_Parent	{ get; set; }
				string	Description		{ get; set; }
				Guid		SAPID					{ get; set; }

			#endregion

		}
}
