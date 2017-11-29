using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public interface IDTOFavourite
		{
			#region "Properties"

				int			SeqNo					{ get; set; }
				Guid		ServiceID			{ get; set; }
				string	Description		{ get; set; }
				string	Client				{ get; set; }
				string	SystemID			{ get; set; }

			#endregion

		}
}
