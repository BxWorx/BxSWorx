using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOMsgServer : IDTOMsgServer
		{
			public Guid		UUID				{ get; set; }
			public string Name				{ get; set; }
			public string Description	{ get; set; }
			public string Host				{ get; set; }
			public string Port				{ get; set; }
		}
}