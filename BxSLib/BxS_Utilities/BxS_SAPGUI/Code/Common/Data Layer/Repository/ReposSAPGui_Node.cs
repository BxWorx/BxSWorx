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
				public IDTONode CreateNodeDTO(Guid ID = default(Guid))
					{
						return	this._DC.XNodes.Create(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool	AddUpdateNode(	Guid		ID					,
																		string	Description	,
																		Guid		ForWSID				)
					{
						IDTONode lo_DTO = this.CreateNodeDTO(ID);
						//.............................................
						//lo_DTO.UUID					= ID;
						lo_DTO.Description	= Description;
						lo_DTO.WSID					= ForWSID;
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
						if (this._DC.XWorkspaces.Exists(DTO.WSID))
							{
								lb_Ret	= this._DC.XNodes.AddUpdate(DTO.UUID, DTO);
								//if (this._DC.Nodes.ContainsKey(DTO.UUID))
								//	{
								//		this._DC.Nodes[DTO.UUID]	= DTO;
								//		lb_Ret	= true;
								//	}
								//else
								//	{
								//		lb_Ret	= this._DC.Nodes.TryAdd(DTO.UUID, DTO);
								//	}

								//if (lb_Ret)		this.IsDirty	= true;
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveNode(Guid NodeID)
					{
						IList<Guid> lt_Items	= this._DC.XItems.KeyListFor("NodeID", NodeID);
						//.............................................
						if (this._DC.XItems.Remove(lt_Items).Equals(lt_Items.Count))
							{
								return	this._DC.XNodes.Remove(NodeID);
							}
						else
							{	return	false; }

						//	.NodeItems(NodeID,ForWSpaceID);

						//IDTOWorkspace lo_WS	= this.GetWorkspace(ForWSpaceID);
						//return	lo_WS?.Nodes.Remove(NodeID) == true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTONode GetNode(Guid NodeID)
					{
						return	this._DC.XNodes.Get(NodeID);
						//IDTONode			lo_DTO	= null;
						//IDTOWorkspace lo_WS		= this.GetWorkspace(ForWSpaceID);
						//lo_WS?.Nodes.TryGetValue(NodeID, out lo_DTO);
						//return	lo_DTO;
					}

			#endregion


				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private IList<Guid> NodeItems(Guid nodeID, Guid workspaceID)
				//	{
				//		return	this._DC.Items
				//							.Where( Itm =>	Itm.Value.NodeID	== nodeID && 
				//															Itm.Value.WSID		== workspaceID )
				//								.Select( x => x.Key )
				//									.ToList();
				//	}

		}
}