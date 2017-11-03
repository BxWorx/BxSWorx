using System.Collections.Generic;
//.........................................................
using SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal class Repository
		{
			#region "Properties"

				internal Dictionary<string, DTOService>			Services		{ get; set; }
				internal Dictionary<string, DTOMsgServer>		MsgServers	{ get; set; }
				internal Dictionary<string, DTOWorkspace>		WorkSpaces	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository()
					{
						this.Services		= new Dictionary<string, DTOService>();
						this.MsgServers	= new Dictionary<string, DTOMsgServer>();
						this.WorkSpaces = new Dictionary<string, DTOWorkspace>();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadConnectionDTO(IDTOConnection dto)
					{
						var	dtoSrv	= new DTOService();
						var dtoMsg	= new DTOMsgServer();

						//.............................................
						if (!this.Services.TryGetValue(dto.ID, out dtoSrv))
							{
								dto.IsValid	= false;
								return;
							}

						//.............................................
						this.MsgServers.TryGetValue(dtoSrv.MSID, out dtoMsg);

						dto.ID							= dtoSrv.UUID;
						dto.ServiceName			= dtoSrv.Name;
						dto.AppServer				= dtoSrv.Server;
						dto.SystemID				= dtoSrv.SystemID;
						dto.SNC_PartnerName	= dtoSrv.SNCName;

						dto.MSID						= dtoMsg.UUID;
						dto.MSName					= dtoMsg.Name;
						dto.MSHost					= dtoMsg.Host;
						dto.MSPort					= dtoMsg.Port;
						dto.MSDescription		= dtoMsg.Description;

						dto.IsValid					= true;

						// TODO: Resolve all connection fields

						//dto.SNC_UsrPwd			= dtoSrv
						//dto.SNC_Active			= dtoSrv.s
						//dto.SNC_QOP					= dtoSrv
						//dto.LowSpeed				= dtoSrv
						//dto.InstanceNo			= dtoSrv.
						//dto.RouterPath			= dtoSrv.r

						//internal string Type				{ get; set; }
						//internal string SAPCPG			{ get; set; }
						//internal string DCPG				{ get; set; }
						//internal string SNCOp				{ get; set; }
						//internal string Description	{ get; set; }
						//internal string Mode				{ get; set; }

						// TODO: Implement APPServer logic

						//If _msgsvrdto.mode.Equals("1")
						//	lo_ConnDTO.AppServer	= _msgsvrdto.server
						//Else
						//	If Not _msgsvrdto.msid.Length.Equals(0)

						//		Dim lo_MsgDTO					= Me.co_Repos.MsgServers.Item(_msgsvrdto.msid)
						//		lo_ConnDTO.AppServer	= lo_MsgDTO.host

						//	End If

						//End If

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear()
					{
						this.Services.Clear();
						this.MsgServers.Clear();
						this.WorkSpaces.Clear();
					}

			#endregion

		}
}