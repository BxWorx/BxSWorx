using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API.DL
{
	public interface IDTOMsgServer
		{
			#region "Properties"

				Guid	UUID	{ get; set; }
				//...................................................
				string	Name				{ get; set; }
				string	Description	{ get; set; }
				string	Host				{ get; set; }
				string	Port				{ get; set; }

			#endregion
		}
}