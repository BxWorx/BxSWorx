using System;
using System.Runtime.Serialization;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	[DataContract]

		[	KnownType(typeof(DTOMsgServer)	)]
		[	KnownType(typeof(DTOService)		)]
		[	KnownType(typeof(DTOWorkspace)	)]
		[	KnownType(typeof(DTONode)				)]
		[	KnownType(typeof(DTOItem)				)]

	public class DCSapGui
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DCSapGui(ToolSet toolset)
					{
						this.MsgServers		= toolset.CreateDCTable<IDTOMsgServer	, Guid>	( (Guid ID) => new DTOMsgServer	()	{ UUID	= ID } );
						//this.MsgServers		= new DCTable	<IDTOMsgServer	, Guid>	( (Guid ID) => new DTOMsgServer	()	{ UUID	= ID } );
						this.Services			= new DCTable	<IDTOService		, Guid>	( (Guid ID) => new DTOService		()	{ UUID	= ID } );
						this.Workspaces		= new DCTable	<IDTOWorkspace	, Guid>	( (Guid ID) => new DTOWorkspace	()	{ UUID	= ID } );
						this.Nodes				= new DCTable	<IDTONode				, Guid>	( (Guid ID) => new DTONode			()	{ UUID	= ID } );
						this.Items				= new DCTable	<IDTOItem				, Guid>	( (Guid ID) => new DTOItem			()	{ UUID	= ID } );
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	internal DCTable<IDTOMsgServer	, Guid> MsgServers	{ get; set; }
				[DataMember]	internal DCTable<IDTOService		,	Guid>	Services		{ get; set; }
				[DataMember]	internal DCTable<IDTOWorkspace	, Guid> Workspaces	{ get; set; }
				[DataMember]	internal DCTable<IDTONode				, Guid>	Nodes				{ get; set; }
				[DataMember]	internal DCTable<IDTOItem				, Guid>	Items				{ get; set; }
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
