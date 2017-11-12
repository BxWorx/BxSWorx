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
				bool							AddUpdateNode	(IDTOWorkspaceNode DTO);
				bool							RemoveNode		(Guid ID);
				IDTOWorkspaceNode	GetNode				(Guid ID);
				//.................................................
				bool							AddUpdateWSItem		(Guid WspaceID, IDTOWorkspaceItem DTO);
				bool							AddUpdateNodeItem	(Guid WspaceID, Guid NodeID, IDTOWorkspaceItem DTO);
				bool							RemoveWSItem			(Guid WspaceID, Guid ID);
				bool							RemoveNodeItem		(Guid WspaceID, Guid NodeID, Guid ID);
				IDTOWorkspaceItem	GetWSItem					(Guid WspaceID, Guid ID);
				IDTOWorkspaceItem	GetNodeItem				(Guid WspaceID, Guid NodeID, Guid ID);
				//.................................................
				void Clear();

			#endregion

		}
}