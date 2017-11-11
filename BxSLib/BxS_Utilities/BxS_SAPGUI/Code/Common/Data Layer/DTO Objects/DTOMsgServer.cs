using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOMsgServer
		{
			internal Guid		UUID				{ get; set; }
			internal string Name				{ get; set; }
			internal string Description	{ get; set; }
			internal string Host				{ get; set; }
			internal string Port				{ get; set; }
		}
}