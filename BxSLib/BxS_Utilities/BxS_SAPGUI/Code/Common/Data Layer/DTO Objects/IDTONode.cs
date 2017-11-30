using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal interface IDTONode
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//.................................................
				string	Description	{ get; set; }
				Guid		WSID				{ get; set; }

			#endregion

		}
}