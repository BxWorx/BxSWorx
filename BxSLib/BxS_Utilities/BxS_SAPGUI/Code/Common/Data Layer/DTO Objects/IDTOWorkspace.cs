using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal interface IDTOWorkspace
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//...................................................
				string	Description	{ get; set; }

			#endregion
		}
}