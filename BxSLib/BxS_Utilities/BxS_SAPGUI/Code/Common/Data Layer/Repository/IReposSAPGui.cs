using System;
using System.Collections.Generic;
//.........................................................
using BxS_SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal interface IReposSAPGui
		{
			#region "Properties"

				bool	IsDirty					{ get; }
				int		MsgServerCount	{ get; }
				int		ServiceCount		{ get; }
				int		WorkspaceCount	{ get; }
				int		NodeCount				{ get; }
				int		ItemCount				{ get; }
				//.................................................
				DCSapGui	DataContainer	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				IDTOMsgServer	CreateMsgServerDTO	(Guid ID = default(Guid));
				IDTOMsgServer GetMsgServer				(Guid ID);
				bool					MsgServerExists			(Guid ID);
				bool					RemoveMsgServer			(Guid ID);
				bool					AddUpdateMsgServer	(IDTOMsgServer DTO);

				bool					AddUpdateMsgServer	(	Guid		ID		,
																						string	Name	,
																						string	Host	,
																						string	Port	,
																						string	Description	);
				IList<IDTOMsgServer>	MsgServerList();

				//.................................................
				IDTOService	CreateServiceDTO	(Guid ID = default(Guid));
				IDTOService	GetService				(Guid ID);
				bool				ServiceExists			(Guid ID);
				bool				RemoveService			(Guid ID);
				bool				AddUpdateService	(IDTOService DTO);

				bool				AddUpdateService	(	Guid		ID					,
																				string	Name				,
																				string	Description	,
																				string	SystemID		,
																				string	Type				,
																				string	Server			,
																				string	SAPCPG			,
																				string	DCPG				,
																				string	SNCName			,
																				string	SNCOp				,
																				Guid    MsgServer		,
																				string	Mode					);
				IList<IDTOService>	ServiceList();

				//.................................................
				IDTOWorkspace	CreateWorkspaceDTO	(Guid ID = default(Guid));
				IDTOWorkspace	GetWorkspace				(Guid ID);
				bool					RemoveWorkspace			(Guid ID);
				bool					AddUpdateWorkspace	(IDTOWorkspace DTO);

				bool					AddUpdateWorkspace	(	Guid		ID	,
																						string	Description	);

				//.................................................
				IDTONode	CreateNodeDTO	(Guid ID = default(Guid));
				IDTONode	GetNode				(Guid NodeID);
				bool			RemoveNode		(Guid NodeID);
				bool			AddUpdateNode	(IDTONode DTO);

				bool			AddUpdateNode	(	Guid		ID					,
																	string	Description	,
																	Guid		ForWSID				);

				//.................................................
				IDTOItem	CreateItemDTO	(Guid ID = default(Guid));
				IDTOItem	GetItem				(Guid ID);
				bool			RemoveItem		(Guid ID);
				bool			AddUpdateItem	(IDTOItem DTO);

				bool			AddUpdateItem	(	Guid	ID					,
																	Guid	ServiceID		,
																	Guid	ForWSpaceID	,
																	Guid  ForNodeID		= default(Guid)	);
				IList<IDTOItem>	ItemList();

				//.................................................
				IList<IDTOConnectionView> GetConnectionViewTree();
				//.................................................
				void Load(DCSapGui DC);
				void Clear();
				void HouseKeeping(bool ClearEmptyNodesWorkspaces = true);

			#endregion

		}
}