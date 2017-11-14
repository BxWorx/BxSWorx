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
				IDTOMsgServer			CreateMsgServer();
				IDTOService				CreateService();
				IDTOWorkspace			CreateWorkspace();
				IDTOWorkspaceNode	CreateWorkspaceNode();
				IDTOWorkspaceItem	CreateWorkspaceItem();
				//.................................................
				bool					AddUpdateMsgServer	(IDTOMsgServer DTO);
				bool					RemoveMsgServer			(Guid ID);
				IDTOMsgServer GetMsgServer				(Guid ID);
				//.................................................
				bool				AddUpdateService	(IDTOService DTO);
				bool				RemoveService			(Guid ID);
				IDTOService	GetService				(Guid ID);
				//.................................................
				bool					AddUpdateWorkspace	(IDTOWorkspace DTO);
				bool					RemoveWorkspace			(Guid ID);
				IDTOWorkspace	GetWorkspace				(Guid ID);
				//.................................................
				bool							AddUpdateNode	(Guid WSpaceID, IDTOWorkspaceNode DTO);
				bool							RemoveNode		(Guid WSpaceID, Guid NodeID);
				IDTOWorkspaceNode	GetNode				(Guid WSpaceID, Guid NodeID);
				//.................................................
				bool							AddUpdateWSItem		(Guid WSpaceID, IDTOWorkspaceItem DTO);
				bool							AddUpdateNodeItem	(Guid WSpaceID, Guid NodeID, IDTOWorkspaceItem DTO);
				bool							RemoveWSItem			(Guid WSpaceID, Guid ID);
				bool							RemoveNodeItem		(Guid WSpaceID, Guid NodeID, Guid ID);
				IDTOWorkspaceItem	GetWSItem					(Guid WSpaceID, Guid ID);
				IDTOWorkspaceItem	GetNodeItem				(Guid WSpaceID, Guid NodeID, Guid ID);
				//.................................................
				void Clear();

			#endregion

		}
}