using System;
//using System.Collections.Generic;
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
						//this.MsgServers		= new Dictionary<	Guid, IDTOMsgServer > ();
						//this.Services			= new Dictionary<	Guid, IDTOService		> ();
						//this.WorkSpaces		= new Dictionary<	Guid, IDTOWorkspace > ();

						this.XMsgServers	= new DCTable	<IDTOMsgServer>	( (Guid ID) => new DTOMsgServer()	{ UUID	= ID } );
						this.XServices		= new DCTable	<IDTOService>		( (Guid ID) => new DTOService()		{ UUID	= ID } );
						this.XWorkspaces	= new DCTable	<IDTOWorkspace>	( (Guid ID) => new DTOWorkspace() { UUID	= ID } );
						this.XNodes				= new DCTable	<IDTONode>			( (Guid ID) => new DTONode()			{ UUID	= ID } );
						this.XItems				= new DCTable	<IDTOItem>			( (Guid ID) => new DTOItem()			{ UUID	= ID } );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				//[DataMember]	internal Dictionary<Guid, IDTOMsgServer>	MsgServers	{ get; set; }
				//[DataMember]	internal Dictionary<Guid, IDTOService>		Services		{ get; set; }
				//[DataMember]	internal Dictionary<Guid, IDTOWorkspace>	WorkSpaces	{ get; set; }

				[DataMember]	internal DCTable<IDTOMsgServer> XMsgServers		{ get; set; }
				[DataMember]	internal DCTable<IDTOService>		XServices			{ get; set; }
				[DataMember]	internal DCTable<IDTOWorkspace> XWorkspaces		{ get; set; }
				[DataMember]	internal DCTable<IDTONode>			XNodes				{ get; set; }
				[DataMember]	internal DCTable<IDTOItem>			XItems				{ get; set; }
				//.................................................
				internal bool IsDirty		{ get	{		return	this.XMsgServers.	IsDirty
																							||	this.XServices.		IsDirty
																							||	this.XWorkspaces.	IsDirty
																							||	this.XNodes.			IsDirty
																							||	this.XItems.			IsDirty; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear()
					{
						this.XMsgServers	.Clear();
						this.XServices		.Clear();
						this.XWorkspaces	.Clear();
						this.XNodes				.Clear();
						this.XItems				.Clear();
					}

			#endregion

		}
}
