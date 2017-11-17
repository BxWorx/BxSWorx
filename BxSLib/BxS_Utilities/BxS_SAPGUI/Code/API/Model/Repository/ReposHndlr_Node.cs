using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal partial class Repository
		{
			#region "Methods: Exposed: Workspace: Node"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTONode CreateNodeDTO()
					{
						return	new DTONode();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool	AddUpdateNode(Guid WSID, Guid ID, string	Description)
					{
						IDTONode lo_DTO = this.CreateNodeDTO();
						//.............................................
						lo_DTO.UUID					= ID;
						lo_DTO.Description	= Description;
						lo_DTO.WSID					= WSID;
						//.............................................
						return	this.AddUpdateNode(lo_DTO);
					}
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateNode(IDTONode DTO)
					{
						bool lb_Ret	= false;
						//.............................................
						IDTOWorkspace lo_WS	= this.GetWorkspace(DTO.WSID);

						if (lo_WS.UUID != Guid.Empty)
							{
								if (lo_WS.Nodes.ContainsKey(DTO.UUID))
									{
										lo_WS.Nodes[DTO.UUID]	= DTO;
										lb_Ret	= true;
									}
								else
									{
										lb_Ret	= lo_WS.Nodes.TryAdd(DTO.UUID, DTO);
									}

								if (lb_Ret)		this.IsDirty	= true;
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveNode(Guid NodeID, Guid ForWSpaceID)
					{
						IDTOWorkspace lo_WS	= this.GetWorkspace(ForWSpaceID);

						if (lo_WS.UUID != Guid.Empty)
							{
								return	lo_WS.Nodes.Remove(NodeID);
							}
						//.............................................
						return	false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTONode GetNode(Guid NodeID, Guid ForWSpaceID)
					{
						IDTONode			lo_DTO	= this.CreateNodeDTO();
						IDTOWorkspace lo_WS		= this.GetWorkspace(ForWSpaceID);

						if (lo_WS.UUID != Guid.Empty)
							{
								if (!lo_WS.Nodes.TryGetValue(NodeID, out lo_DTO))
									{	}
							}
						//.............................................
						return	lo_DTO;
					}

			#endregion

		}
}