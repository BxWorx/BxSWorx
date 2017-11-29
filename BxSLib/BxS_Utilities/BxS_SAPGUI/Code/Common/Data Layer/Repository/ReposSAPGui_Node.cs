using System;
using System.Collections.Generic;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: Workspace: Node"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTONode CreateNodeDTO()
					{
						return	new DTONode();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool	AddUpdateNode(	Guid		WSID				,
																		Guid		ID					,
																		string	Description		)
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
						//IDTOWorkspace lo_WS	= this.GetWorkspace(DTO.WSID);
						//if (lo_WS == null)	return	false;
						bool lb_Ret	= false;
						//.............................................
						if (this._DC.WorkSpaces.ContainsKey(DTO.WSID))
							{
								if (this._DC.Nodes.ContainsKey(DTO.UUID))
									{
										this._DC.Nodes[DTO.UUID]	= DTO;
										lb_Ret	= true;
									}
								else
									{
										lb_Ret	= this._DC.Nodes.TryAdd(DTO.UUID, DTO);
									}

								if (lb_Ret)		this.IsDirty	= true;
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveNode(Guid NodeID, Guid ForWSpaceID)
					{
						var ItemList	= this.NodeItems(NodeID,ForWSpaceID);

						return	this._DC.Nodes
						IDTOWorkspace lo_WS	= this.GetWorkspace(ForWSpaceID);
						return	lo_WS?.Nodes.Remove(NodeID) == true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTONode GetNode(Guid NodeID, Guid ForWSpaceID)
					{
						IDTONode			lo_DTO	= null;
						IDTOWorkspace lo_WS		= this.GetWorkspace(ForWSpaceID);
						lo_WS?.Nodes.TryGetValue(NodeID, out lo_DTO);
						return	lo_DTO;
					}

			#endregion


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> NodeItems(Guid nodeID, Guid workspaceID)
					{
						return	this._DC.Items
											.Where( Itm =>	Itm.Value.NodeID	== nodeID && 
																			Itm.Value.WSID		== workspaceID )
												.Select( x => x.Key )
													.ToList();
					}

		}
}