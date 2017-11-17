using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IRepository
		{
			#region "Methods: Exposed"

				//.................................................
				IDTOMsgServer	CreateMsgServerDTO();
				IDTOMsgServer GetMsgServer				(Guid ID);
				bool					RemoveMsgServer			(Guid ID);
				bool					AddUpdateMsgServer	(IDTOMsgServer DTO);

				bool					AddUpdateMsgServer	(	Guid		ID		,
																						string	Name	,
																						string	Host	,
																						string	Port	,
																						string	Description	);

				//.................................................
				IDTOService	CreateServiceDTO();
				IDTOService	GetService				(Guid ID);
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
				IDTOWorkspace	CreateWorkspaceDTO();
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

				bool			AddUpdateItem	(	Guid	WSID		,
																	Guid  NodeID	,
																	Guid	ID			,
																	Guid	ServiceID	);

				//.................................................
				int MsgServerCount	{ get; }
				int ServiceCount		{ get; }
				int WorkspaceCount	{ get; }
				//.................................................
				void Clear();
				void HouseKeeping();

			#endregion

		}
}