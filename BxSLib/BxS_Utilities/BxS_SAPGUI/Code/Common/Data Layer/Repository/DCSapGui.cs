using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]
		[KnownType(typeof(DTOMsgServer))]
		[KnownType(typeof(DTOService))]
		[KnownType(typeof(DTOWorkspace))]
		[KnownType(typeof(DTONode))]
		[KnownType(typeof(DTOItem))]
	public class DCSapGui
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DCSapGui()
					{
						this.MsgServers		= new Dictionary<	Guid, IDTOMsgServer > ();
						this.Services			= new Dictionary<	Guid, IDTOService		> ();
						this.WorkSpaces		= new Dictionary<	Guid, IDTOWorkspace > ();
						this.Nodes				= new Dictionary<	Guid, IDTONode			> ();
						this.Items				= new Dictionary<	Guid, IDTOItem			> ();
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	internal Dictionary<Guid, IDTOMsgServer>	MsgServers	{ get; set; }
				[DataMember]	internal Dictionary<Guid, IDTOService>		Services		{ get; set; }
				[DataMember]	internal Dictionary<Guid, IDTOWorkspace>	WorkSpaces	{ get; set; }
				[DataMember]	internal Dictionary<Guid, IDTONode>				Nodes				{ get; set; }
				[DataMember]	internal Dictionary<Guid, IDTOItem>				Items				{ get; set; }
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
						this.Nodes     	.Clear();
						this.Items     	.Clear();
					}

			#endregion

		}
}
