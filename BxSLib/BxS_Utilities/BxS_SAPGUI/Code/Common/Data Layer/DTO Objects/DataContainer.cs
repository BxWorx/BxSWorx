using System;
using System.Collections.Generic;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal class DataContainer
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataContainer()
					{
						this.MsgServers		= new Dictionary<	Guid, IDTOMsgServer > ();
						this.Services			= new Dictionary<	Guid, IDTOService		> ();
						this.WorkSpaces		= new Dictionary<	Guid, IDTOWorkspace > ();
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Dictionary<Guid, IDTOMsgServer>	MsgServers	{ get; }
				internal Dictionary<Guid, IDTOService>		Services		{ get; }
				internal Dictionary<Guid, IDTOWorkspace>	WorkSpaces	{ get; }
				//.................................................
				internal bool IsDirty		{ get; set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear()
					{
						this.Services		.Clear();
						this.MsgServers	.Clear();
						this.WorkSpaces	.Clear();
					}

			#endregion

		}
}