using System;
using System.Collections.Generic;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal sealed class DTOWorkspace :IDTOWorkspace
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOWorkspace()
					{
						this.Nodes	= new	Dictionary<Guid, IDTONode>();
						this.Items	= new Dictionary<Guid, IDTOItem>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid		UUID					{ get; set; }
				public string Description		{ get; set; }
				//...................................................
				public Dictionary<Guid, IDTONode> Nodes	{ get; set; }
				public Dictionary<Guid, IDTOItem> Items	{ get; set; }

			#endregion
		}
}