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

				//.................................................
				IDTOService	CreateService();
				IDTOService	GetService				(Guid ID);
				bool				RemoveService			(Guid ID);
				bool				AddUpdateService	(IDTOService DTO);

				//.................................................
				IDTOWorkspace	CreateWorkspace();
				IDTOWorkspace	GetWorkspace				(Guid ID);
				bool					RemoveWorkspace			(Guid ID);
				bool					AddUpdateWorkspace	(IDTOWorkspace DTO);

				//.................................................
				IDTONode	CreateNode();
				IDTONode	GetNode				(Guid WSpaceID, Guid NodeID);
				bool			RemoveNode		(Guid WSpaceID, Guid NodeID);
				bool			AddUpdateNode	(IDTONode DTO);

				//.................................................
				IDTOItem	CreateItem();
				IDTOItem	GetItem				(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			RemoveItem		(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid));
				bool			AddUpdateItem	(IDTOItem DTO);

				//.................................................
				void Clear();

			#endregion

		}
}