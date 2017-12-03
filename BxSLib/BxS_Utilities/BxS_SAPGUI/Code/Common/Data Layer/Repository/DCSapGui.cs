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
				internal DCSapGui(DCTable	<IDTOMsgServer	, Guid> msgServer		,
													DCTable	<IDTOService		, Guid> services		,
													DCTable	<IDTOWorkspace	, Guid>	workspaces	,
													DCTable	<IDTONode				, Guid>	nodes				,
													DCTable	<IDTOItem				, Guid>	items					)
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
