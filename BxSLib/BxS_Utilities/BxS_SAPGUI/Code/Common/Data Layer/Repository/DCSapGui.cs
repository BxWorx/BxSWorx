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
				internal DCSapGui(DataTable	<IDTOMsgServer	, Guid> msgServer		,
													DataTable	<IDTOService		, Guid> services		,
													DataTable	<IDTOWorkspace	, Guid>	workspaces	,
													DataTable	<IDTONode				, Guid>	nodes				,
													DataTable	<IDTOItem				, Guid>	items					)
					{
						this.MsgServers		= msgServer		;
						this.Services			= services		;
						this.Workspaces		= workspaces	;
						this.Nodes				= nodes				;
						this.Items				= items				;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	internal DataTable<IDTOMsgServer	, Guid> MsgServers	{ get; set; }
				[DataMember]	internal DataTable<IDTOService		,	Guid>	Services		{ get; set; }
				[DataMember]	internal DataTable<IDTOWorkspace	, Guid> Workspaces	{ get; set; }
				[DataMember]	internal DataTable<IDTONode				, Guid>	Nodes				{ get; set; }
				[DataMember]	internal DataTable<IDTOItem				, Guid>	Items				{ get; set; }
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
