using System;
using System.Runtime.Serialization;
using BxS_Toolset.DataContainer;
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
						this.MsgServers		= new DCTable	<IDTOMsgServer, Guid>	( (Guid ID) => new DTOMsgServer()	{ UUID	= ID } );
						this.Services			= new DCTable	<IDTOService>		( (Guid ID) => new DTOService()		{ UUID	= ID } );
						this.Workspaces		= new DCTable	<IDTOWorkspace>	( (Guid ID) => new DTOWorkspace() { UUID	= ID } );
						this.Nodes				= new DCTable	<IDTONode>			( (Guid ID) => new DTONode()			{ UUID	= ID } );
						this.Items				= new DCTable	<IDTOItem>			( (Guid ID) => new DTOItem()			{ UUID	= ID } );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	internal DCTable<IDTOMsgServer, Guid> MsgServers	{ get; set; }
				[DataMember]	internal DCTable<IDTOService>		Services		{ get; set; }
				[DataMember]	internal DCTable<IDTOWorkspace> Workspaces	{ get; set; }
				[DataMember]	internal DCTable<IDTONode>			Nodes				{ get; set; }
				[DataMember]	internal DCTable<IDTOItem>			Items				{ get; set; }
				//.................................................
				internal bool IsDirty		{ get	{		return	this.MsgServers	.IsDirty
																							||	this.Services		.IsDirty
																							||	this.Workspaces	.IsDirty
																							||	this.Nodes			.IsDirty
																							||	this.Items			.IsDirty; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear()
					{
						this.MsgServers	.Clear();
						this.Services		.Clear();
						this.Workspaces	.Clear();
						this.Nodes			.Clear();
						this.Items			.Clear();
					}

			#endregion

		}
}
