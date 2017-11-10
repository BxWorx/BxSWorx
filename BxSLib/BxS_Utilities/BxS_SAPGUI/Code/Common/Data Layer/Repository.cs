using System;
using System.Collections.Generic;
//.........................................................
using SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal class Repository
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository(bool isReadOnly = true)
					{
						this.MsgServers		= new Dictionary<	Guid, DTOMsgServer > ();
						this.Services			= new Dictionary<	Guid, DTOService	 > ();
						this.WorkSpaces		= new Dictionary<	Guid, DTOWorkspace > ();

						this._IsReadOnly	= isReadOnly;
						//.............................................
						this.IsDirty		= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly bool		_IsReadOnly;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Dictionary<Guid, DTOMsgServer>		MsgServers	{ get; private set; }
				internal Dictionary<Guid, DTOService>			Services		{ get; private set; }
				internal Dictionary<Guid, DTOWorkspace>		WorkSpaces	{ get; private set; }
				//.................................................
				internal bool	IsDirty	{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void LoadConnectionDTO(IDTOConnection dto)
					{
						var	dtoSrv	= new DTOService();
						//.............................................
						if (!this.Services.TryGetValue(dto.ID, out dtoSrv))
							{
								dto.IsValid	= false;
								return;
							}
						//.............................................
						if (this.MsgServers.TryGetValue(dtoSrv.MSID, out DTOMsgServer dtoMsg))
							{
								dto.MSID						= dtoMsg.UUID;
								dto.MSName					= dtoMsg.Name;
								dto.MSHost					= dtoMsg.Host;
								dto.MSPort					= dtoMsg.Port;
								dto.MSDescription		= dtoMsg.Description;
							}
						//.............................................
						dto.ID							= dtoSrv.UUID;
						dto.ServiceName			= dtoSrv.Name;
						dto.AppServer				= dtoSrv.Server;
						dto.SystemID				= dtoSrv.SystemID;
						dto.SNC_PartnerName	= dtoSrv.SNCName;
						//.............................................
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

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddUpdateMsgServer(DTOMsgServer dto)
					{
						if (this._IsReadOnly)	return;
						//.............................................
						this.MsgServers.Add(dto.UUID, dto);
						this.IsDirty	= true;
					}

			#endregion

		}
}