using System;
//.........................................................
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal partial class RepositoryHandler
		{
			#region "Methods: Exposed: Message Server"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOMsgServer CreateMsgServer()
					{
						return	new DTOMsgServer();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool LoadMsgServer(Guid ID, string Name, string Host, string Port, string Description)
					{
						IDTOMsgServer lo_DTO = this.CreateMsgServer();
						//.............................................
						lo_DTO.UUID					= ID;
						lo_DTO.Name					= Name;
						lo_DTO.Host					= Host;
						lo_DTO.Port					= Port;
						lo_DTO.Description	= Description;
						//.............................................
						return	this.AddUpdateMsgServer(lo_DTO);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOMsgServer GetMsgServer(Guid ID)
					{
						IDTOMsgServer	lo_DTO	= this.CreateMsgServer();
						this._DC.MsgServers.TryGetValue(ID, out lo_DTO);
						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateMsgServer(IDTOMsgServer DTO)
					{
						bool lb_Ret	= false;
						//.............................................
						if (this._DC.MsgServers.ContainsKey(DTO.UUID))
							{
								this._DC.MsgServers[DTO.UUID]	= DTO;
								lb_Ret	= true;
							}
						else
							{
								lb_Ret	= this._DC.MsgServers.TryAdd(DTO.UUID, DTO);
							}

						if (lb_Ret)		this.IsDirty	= true;
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool RemoveMsgServer(Guid ID)
					{
						bool lb_Ret	= false;
						//.............................................
						if (this.MsgServerInUse(ID))
							{
								lb_Ret	= this._DC.MsgServers.Remove(ID);
								if (lb_Ret)	this.IsDirty	= true;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}