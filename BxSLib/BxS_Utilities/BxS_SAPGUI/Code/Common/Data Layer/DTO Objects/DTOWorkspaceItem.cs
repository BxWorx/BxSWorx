﻿using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOWorkspaceItem :IDTOWorkspaceItem
		{
			public Guid UUID				{ get; set; }
			public Guid ServiceID		{ get; set; }
		}
}