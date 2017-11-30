using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: Workspace"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOWorkspace CreateWorkspaceDTO(Guid ID = default(Guid))
					{
						return	this._DC.XWorkspaces.Create(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOWorkspace GetWorkspace(Guid ID)
					{
						return	this._DC.XWorkspaces.Get(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool	AddUpdateWorkspace(	Guid		ID					,
																				string	Description		)
					{
						IDTOWorkspace lo_DTO = this.CreateWorkspaceDTO(ID);
						//.............................................
						//lo_DTO.UUID					= ID;
						lo_DTO.Description	= Description;
						//.............................................
						return	this.AddUpdateWorkspace(lo_DTO);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateWorkspace(IDTOWorkspace DTO)
					{
						return	this._DC.XWorkspaces.AddUpdate(DTO.UUID, DTO);
						//bool lb_Ret	= false;
						////.............................................

						//if (this._DC.WorkSpaces.ContainsKey(DTO.UUID))
						//	{
						//		this._DC.WorkSpaces[DTO.UUID]	= DTO;
						//		lb_Ret	= true;
						//	}
						//else
						//	{
						//		lb_Ret	= this._DC.WorkSpaces.TryAdd(DTO.UUID, DTO);
						//	}

						//if (lb_Ret)		this.IsDirty	= true;
						////.............................................
						//return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveWorkspace(Guid ID)
					{
						IList<Guid> lt_Items	= this._DC.XItems.KeyListFor("WSID", ID);
						IList<Guid> lt_Nodes	= this._DC.XNodes.KeyListFor("WSID", ID);
						//.............................................
						if (		this._DC.XItems.Remove(lt_Items).Equals(lt_Items.Count)
								 && this._DC.XNodes.Remove(lt_Nodes).Equals(lt_Nodes.Count)	)
							{
								return	this._DC.XWorkspaces.Remove(ID);
							}
						else
							{	return	false; }



						//bool lb_Ret	= false;
						////.............................................
						//lb_Ret	= this._DC.XWorkspaces.Remove(ID);
						//if (lb_Ret)	this.IsDirty	= true;
						////.............................................
						//return	lb_Ret;
					}

			#endregion

		}
}