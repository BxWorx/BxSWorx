using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: Service"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOService CreateServiceDTO(Guid ID = default(Guid))
					{
						return	this._DC.XServices.Create(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ServiceExists(Guid ID)
					{
						return	this._DC.XServices.Exists(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOService GetService(Guid ID)
					{
						//IDTOService	lo_DTO	= this.CreateServiceDTO();
						return	this._DC.XServices.Get(ID);
						//return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateService(	Guid		ID					,
																			string	Name				,
																			string	Description	,
																			string	SystemID		,
																			string	Type				,
																			string	Server			,
																			string	SAPCPG			,
																			string	DCPG				,
																			string	SNCName			,
																			string	SNCOp				,
																			Guid		MsgServer		,
																			string	Mode					)
					{
						IDTOService lo_DTO = this.CreateServiceDTO(ID);
						//.............................................
						//lo_DTO.UUID					= ID;
						lo_DTO.Name					= Name;
						lo_DTO.Description	= Description;
						lo_DTO.SystemID			= SystemID;
						lo_DTO.Type					= Type;
						lo_DTO.Server				= Server;
						lo_DTO.SAPCPG				= SAPCPG;
						lo_DTO.DCPG					= DCPG;
						lo_DTO.SNCName			= SNCName;
						lo_DTO.SNCOp				= SNCOp;
						lo_DTO.Mode					= Mode;
						lo_DTO.MSID					= MsgServer;
							//.............................................
						return	this.AddUpdateService(lo_DTO);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateService(IDTOService DTO)
					{
						bool lb_Ret	= false;
						//.............................................
						if ( DTO.MSID.Equals(Guid.Empty) || this._DC.XMsgServers.Exists(DTO.MSID) )
							{
								lb_Ret	= this._DC.XServices.AddUpdate(DTO.UUID, DTO);
								//if (this._DC.Services.ContainsKey(DTO.UUID))
								//	{
								//		this._DC.Services[DTO.UUID]	= DTO;
								//		lb_Ret	= true;
								//	}
								//else
								//	{
								//		lb_Ret	= this._DC.Services.TryAdd(DTO.UUID, DTO);
								//	}

								//if (lb_Ret)		this.IsDirty	= true;
							}
							//.............................................
							return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveService(Guid ID)
					{
						bool lb_Ret	= false;
						//.............................................
						if (!this.ServiceInUse(ID))
							{
								lb_Ret	= this._DC.XServices.Remove(ID);
								//if (lb_Ret)	this.IsDirty	= true;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}