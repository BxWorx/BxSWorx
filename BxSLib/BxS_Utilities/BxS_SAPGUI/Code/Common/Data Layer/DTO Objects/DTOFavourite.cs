using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal class DTOFavourite
		{
			#region "Properties"

				internal int		SeqNo					{ get; set; }
				internal Guid		ServiceID			{ get; set; }
				internal string	Description		{ get; set; }
				internal string	Client				{ get; set; }
				internal string	SystemID			{ get; set; }

			#endregion

		}
}
