using System;
//.........................................................
using SAPGUI.API.DL;
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IRepository
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
																				string	SAPCPG			,
																				string	DCPG				,
																				string	SNCName			,
																				string	SNCOp				,
																				Guid    MsgServer		,
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
				IDTONode	GetNode				(Guid NodeID, Guid ForWSpaceID);
				bool			RemoveNode		(Guid NodeID, Guid ForWSpaceID);
				bool			AddUpdateNode	(IDTONode DTO);

				bool			LoadNode			(	Guid		WSID	,
																	Guid		ID		,
																	string	Description	);

				//.................................................
				IDTOItem	CreateItem();
				IDTOItem	GetItem				(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			RemoveItem		(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			AddUpdateItem	(IDTOItem DTO);

				bool			LoadItem			(	Guid	WSID		,
																	Guid  NodeID	,
																	Guid	ID			,
																	Guid	ServiceID	);

				//.................................................
				void Clear();
				void HouseKeeping();

			#endregion

		}
}