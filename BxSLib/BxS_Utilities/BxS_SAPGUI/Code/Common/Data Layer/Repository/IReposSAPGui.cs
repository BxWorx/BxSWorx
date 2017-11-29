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

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				IDTOMsgServer	CreateMsgServerDTO();
				IDTOMsgServer GetMsgServer				(Guid ID);
				bool					MsgServerExists			(Guid ID);
				bool					RemoveMsgServer			(Guid ID);
				bool					AddUpdateMsgServer	(IDTOMsgServer DTO);

				bool					AddUpdateMsgServer	(	Guid		ID		,
																						string	Name	,
																						string	Host	,
																						string	Port	,
																						string	Description	);

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

				//.................................................
				IDTOWorkspace	CreateWorkspaceDTO	(Guid ID = default(Guid));
				IDTOWorkspace	GetWorkspace				(Guid ID);
				bool					RemoveWorkspace			(Guid ID);
				bool					AddUpdateWorkspace	(IDTOWorkspace DTO);

				bool					AddUpdateWorkspace	(	Guid		ID	,
																						string	Description	);

				//.................................................
				IDTONode	CreateNodeDTO();
				IDTONode	GetNode				(Guid NodeID, Guid ForWSpaceID);
				bool			RemoveNode		(Guid NodeID, Guid ForWSpaceID);
				bool			AddUpdateNode	(IDTONode DTO);

				bool			AddUpdateNode	(	Guid		WSID	,
																	Guid		ID		,
																	string	Description	);

				//.................................................
				IDTOItem	CreateItemDTO();
				IDTOItem	GetItem				(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			RemoveItem		(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			AddUpdateItem	(IDTOItem DTO);

				bool			AddUpdateItem	(	Guid	ID					,
																	Guid	ServiceID		,
																	Guid	ForWSpaceID	,
																	Guid  ForNodeID		= default(Guid)	);

				//.................................................
				ref	DCSapGui GetDataContainer();
				//.................................................
				IList<IDTOConnectionView> GetConnectionViewTree();

				void Clear();
				void HouseKeeping(bool ClearEmptyNodesWorkspaces = true);

			#endregion

		}
}