using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.DL
{
	public interface IDTOMsgServer
		{
			Guid	 UUID					{ get; set; }
			string Name					{ get; set; }
			string Description	{ get; set; }
			string Host					{ get; set; }
			string Port					{ get; set; }
		}
}