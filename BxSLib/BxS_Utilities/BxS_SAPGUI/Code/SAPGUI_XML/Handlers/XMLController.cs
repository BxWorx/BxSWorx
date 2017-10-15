using SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal class XMLController : IController
		{

			//===========================================================================================
			#region "Definitions"

				private readonly	DTORepository		_ReposDTO;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOConnection	CreateConnection(string connectionID	= "")
					{
						var DTO = new DTOConnection
							{	ID = connectionID ?? string.Empty	};

						return	DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IDTOConnection GetConnection(string connectionID)
					{
						IDTOConnection DTO	= this.CreateConnection(connectionID);
						this.LoadDTO(DTO);
						return	DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void GetConnection(IDTOConnection dtoConnection)
					{
						this.LoadDTO(dtoConnection);
					}

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public XMLController(string fullPath, bool onlySAPGUI)
					{
						this._ReposDTO	= new XMLParse2DTO().Load(fullPath, onlySAPGUI);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadDTO(IDTOConnection dto)
					{
						var	dtoSrv	= new DTOService();
						var dtoMsg	= new DTOMsgServer();

						//.............................................
						if (!this._ReposDTO.Services.TryGetValue(dto.ID, out dtoSrv))
							{
								dto.IsValid	= false;
								return;
							}

						//.............................................
						this._ReposDTO.MsgServers.TryGetValue(dtoSrv.MSID, out dtoMsg);

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

						//internal string Type				{ get; set; }
						//internal string SAPCPG			{ get; set; }
						//internal string DCPG				{ get; set; }
						//internal string SNCOp				{ get; set; }
						//internal string Description	{ get; set; }
						//internal string Mode				{ get; set; }

						//	dto.InstanceNo			= dtoSrv.
						//	dto.RouterPath			= dtoSrv.r

					}

			#endregion

		}
}
