using System;
//using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: Workspace: Item"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOItem CreateItemDTO(Guid ID	= default(Guid))
					{
						return	this._DC.XItems.Create(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateItem(	Guid	ID					,
																		Guid	ServiceID		,
																		Guid	ForWSpaceID	,
																		Guid	ForNodeID			= default(Guid)	)
					{
						IDTOItem lo_DTO	= this.CreateItemDTO(ID);
						//.............................................
						//lo_DTO.UUID				= ID;
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
						//Dictionary<Guid, IDTOItem> lt_Items	= this.GetItemContainer(DTO.WSID, DTO.NodeID);

						//if (lt_Items != null)
						//	{
								if (this._DC.XWorkspaces.Exists(DTO.WSID))
									{
										if (DTO.NodeID.Equals(Guid.Empty) || this._DC.XNodes.Exists(DTO.NodeID))
											{
												if (this._DC.XServices.Exists(DTO.ServiceID))
													{
														lb_Ret	=	this._DC.XItems.AddUpdate(DTO.UUID, DTO);
														//if (this._DC.Items.ContainsKey(DTO.UUID))
														//	{
														//		this._DC.Items[DTO.UUID]	= DTO;
														//		lb_Ret	= true;
														//	}
														//else
														//	{
														//		lb_Ret	= this._DC.Items.TryAdd(DTO.UUID, DTO);
														//	}

														//if (lb_Ret)		this.IsDirty	= true;
														}
											}
									}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveItem(Guid ID)
					{
						return	this._DC.XItems.Remove(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOItem GetItem(Guid ID)
					{
						return	this._DC.XItems.Get(ID);
						//Dictionary<Guid, IDTOItem> lt_Items	= this.GetItemContainer(ForWSpaceID, ForNodeID);
						//this._DC.Items.TryGetValue(ID, out lo_DTO);
						//return	lo_DTO;
					}

			#endregion

			////===========================================================================================
			//#region "Methods: Private: Workspace: Item"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	private Dictionary<Guid, IDTOItem> GetItemContainer(Guid ForWSpaceID, Guid ForNodeID)
			//		{
			//			if (ForNodeID == Guid.Empty)
			//				{
			//					return	this.GetWorkspace(ForWSpaceID)?.Items;
			//				}
			//			else
			//				{
			//					return	this.GetNode(ForNodeID, ForWSpaceID)?.Items;
			//				}
			//		}

			//#endregion

		}
}