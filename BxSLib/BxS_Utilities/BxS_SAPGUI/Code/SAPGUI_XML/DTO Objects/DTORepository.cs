using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	internal class DTORepository
		{
			#region "Properties"

				internal Dictionary<string, DTOService>	Services		{ get; set; }
				internal Dictionary<string, DTOMsgServer>		MsgServers	{ get; set; }
				internal Dictionary<string, DTOWorkspace>		WorkSpaces	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTORepository()
					{
						this.Services		= new Dictionary<string, DTOService>();
						this.MsgServers	= new Dictionary<string, DTOMsgServer>();
						this.WorkSpaces = new Dictionary<string, DTOWorkspace>();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				internal void Clear()
					{
						this.Services.Clear();
						this.MsgServers.Clear();
						this.WorkSpaces.Clear();
					}

			#endregion

		}
}