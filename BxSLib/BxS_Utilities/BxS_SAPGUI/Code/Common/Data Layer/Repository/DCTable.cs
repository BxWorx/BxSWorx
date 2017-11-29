using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal class DCTable<T> where T : class
		{
			//===========================================================================================
			#region "Constructor"

				internal DCTable()
					{
						this.DataTable	= new	Dictionary<Guid, T>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal Dictionary<Guid, T>	DataTable;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Workspace: Item"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal T Get(Guid ID)
					{
						return	this.DataTable.TryGetValue(ID, out T lo_DTO) ? lo_DTO : default(T);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Remove(Guid ID)
					{
						return	this.DataTable.Remove(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal int Remove(IList<Guid> IDList)
					{
						int ln_Cnt = 0;
						//.............................................
						foreach (Guid ID in IDList)
							{
								if (this.DataTable.Remove(ID)) ln_Cnt ++ ;
							}
						//.............................................
						return	ln_Cnt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal T AddUpdate(Guid ID)
					{
						return	this.DataTable.TryGetValue(ID, out T lo_DTO) ? lo_DTO : default(T);
					}



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOItem CreateItemDTO()
					{
						return	new DTOItem();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateItem(	Guid	ID					,
																		Guid	ServiceID		,
																		Guid	ForWSpaceID	,
																		Guid	ForNodeID			= default(Guid)	)
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
						//Dictionary<Guid, IDTOItem> lt_Items	= this.GetItemContainer(DTO.WSID, DTO.NodeID);

						//if (lt_Items != null)
						//	{
								if (this._DC.WorkSpaces.ContainsKey(DTO.WSID))
									{
										if (DTO.NodeID.Equals(Guid.Empty) || this._DC.Nodes.ContainsKey(DTO.NodeID))
											{
												if (this._DC.Services.ContainsKey(DTO.ServiceID))
													{
														if (this._DC.Items.ContainsKey(DTO.UUID))
															{
																this._DC.Items[DTO.UUID]	= DTO;
																lb_Ret	= true;
															}
														else
															{
																lb_Ret	= this._DC.Items.TryAdd(DTO.UUID, DTO);
															}

														if (lb_Ret)		this.IsDirty	= true;
														}
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
						IDTOItem lo_DTO	= this.CreateItemDTO();
						//Dictionary<Guid, IDTOItem> lt_Items	= this.GetItemContainer(ForWSpaceID, ForNodeID);
						this._DC.Items.TryGetValue(ID, out lo_DTO);
						return	lo_DTO;
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