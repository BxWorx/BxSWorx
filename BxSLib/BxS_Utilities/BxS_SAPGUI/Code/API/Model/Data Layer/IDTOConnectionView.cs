using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public interface IDTOConnectionView
		{
			#region "Properties"

				string	HierID				{ get; }
				string	HierID_Parent	{ get; }
				string	Description		{ get; }
				Guid		SAPID					{ get; }

			#endregion

		}
}
