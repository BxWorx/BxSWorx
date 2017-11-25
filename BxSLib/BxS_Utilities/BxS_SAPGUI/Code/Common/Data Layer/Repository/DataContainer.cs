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
	public class DataContainer
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

				[DataMember]	internal Dictionary<Guid, IDTOMsgServer>	MsgServers	{ get; set; }
				[DataMember]	internal Dictionary<Guid, IDTOService>		Services		{ get; set; }
				[DataMember]	internal Dictionary<Guid, IDTOWorkspace>	WorkSpaces	{ get; set; }
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