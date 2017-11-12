using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using SAPGUI.API.DL;
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API.Repository
{
	internal class MsgServerCollection
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MsgServerCollection(Dictionary<Guid, IDTOMsgServer>	msgServers)
					{
						this.MsgServers	= msgServers;
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal Dictionary<Guid, IDTOMsgServer>	MsgServers	{ get; }

			#endregion

			//===========================================================================================
			#region "Proprties"

				internal bool IsDirty { get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Message Server"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOMsgServer	CreateMsgServer()		{	return	new DTOMsgServer();			}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOMsgServer GetMsgServer(Guid ID)
					{
						IDTOMsgServer	lo_DTO	= this.CreateMsgServer();
						this.MsgServers.TryGetValue(ID, out lo_DTO);
						return	lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool AddUpdateMsgServer(IDTOMsgServer DTO)
					{
						bool lb_Ret	= false;
						//.............................................
						if (this.MsgServers.ContainsKey(DTO.UUID))
							{
								this.MsgServers[DTO.UUID]	= DTO;
								lb_Ret	= true;
							}
						else
							{
								lb_Ret	= this.MsgServers.TryAdd(DTO.UUID, DTO);
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
								lb_Ret	= this.MsgServers.Remove(ID);
								if (lb_Ret)	this.IsDirty	= true;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}