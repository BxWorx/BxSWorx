using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: Workspace: Item"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOItem CreateItemDTO()
					{
						return	new DTOItem();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateItem(Guid ID, Guid ServiceID, Guid ForWSpaceID, Guid ForNodeID = default(Guid))
					{
						IDTOItem lo_DTO	= this.CreateItemDTO();
						//.............................................
						lo_DTO.UUID				= ID;
						lo_DTO.ServiceID	= ServiceID;
						lo_DTO.WSID				= ForWSpaceID;
						lo_DTO.NodeID			= ForNodeID;
						//.............................................
						return	this.AddUpdateItem(lo_DTO);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateItem(IDTOItem DTO)
					{
						bool lb_Ret	= false;
						//.............................................
						Dictionary<Guid, IDTOItem> lt_Items	= this.GetItemContainer(DTO.WSID, DTO.NodeID);

						if (lt_Items != null)
							{
								if (this._DataCon.Services.ContainsKey(DTO.ServiceID))
									{
										if (lt_Items.ContainsKey(DTO.UUID))
											{
												lt_Items[DTO.UUID]	= DTO;
												lb_Ret	= true;
											}
										else
											{
												lb_Ret	= lt_Items.TryAdd(DTO.UUID, DTO);
											}

										if (lb_Ret)		this.IsDirty	= true;
									}
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveItem(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid))
					{
						return	false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOItem GetItem(Guid ID, Guid ForWSpaceID, Guid ForNodeID = default(Guid))
					{
						Dictionary<Guid, IDTOItem> lt_Items	= this.GetItemContainer(ForWSpaceID, ForNodeID);
						lt_Items.TryGetValue(ID, out IDTOItem lo_DTO);
						return	lo_DTO;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Workspace: Item"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<Guid, IDTOItem> GetItemContainer(Guid ForWSpaceID, Guid ForNodeID)
					{
						if (ForNodeID == Guid.Empty)
							{
								return	this.GetWorkspace(ForWSpaceID)?.Items;
							}
						else
							{
								return	this.GetNode(ForNodeID, ForWSpaceID)?.Items;
							}
					}

			#endregion

		}
}