using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IRepositoryHandler
		{
			#region "Methods: Exposed"

				//.................................................
				IDTOMsgServer	CreateMsgServer();
				IDTOMsgServer GetMsgServer				(Guid ID);
				bool					RemoveMsgServer			(Guid ID);
				bool					AddUpdateMsgServer	(IDTOMsgServer DTO);
				bool					LoadMsgServer				(	Guid		ID		,
																						string	Name	,
																						string	Host	,
																						string	Port	,
																						string	Description	);

				//.................................................
				IDTOService	CreateService();
				IDTOService	GetService				(Guid ID);
				bool				RemoveService			(Guid ID);
				bool				AddUpdateService	(IDTOService DTO);
				bool				LoadService				(	Guid		ID					,
																				string	Name				,
																				string	Description	,
																				string	SystemID		,
																				string	Type				,
																				string	Server			,
																				string	SAPCPGE			,
																				string	DCPG				,
																				string	SNCName			,
																				string	SNCOp				,
																				string	Mode					);

				//.................................................
				IDTOWorkspace	CreateWorkspace();
				IDTOWorkspace	GetWorkspace				(Guid ID);
				bool					RemoveWorkspace			(Guid ID);
				bool					AddUpdateWorkspace	(IDTOWorkspace DTO);
				bool					LoadWorkspace				(	Guid		ID	,
																						string	Description	);

				//.................................................
				IDTONode	CreateNode();
				IDTONode	GetNode				(Guid WSpaceID, Guid NodeID);
				bool			RemoveNode		(Guid WSpaceID, Guid NodeID);
				bool			AddUpdateNode	(IDTONode DTO);
				bool			LoadNode			(	Guid		WSID	,
																	Guid		ID		,
																	string	Description	);

				//.................................................
				IDTOItem	CreateItem();
				IDTOItem	GetItem				(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			RemoveItem		(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			AddUpdateItem	(IDTOItem DTO);
				bool			LoadItem			(	Guid		WSID		,
																	Guid    NodeID	,
																	Guid		ID			,
																	Guid		ServiceID	);

				//.................................................
				void Clear();

			#endregion

		}
}