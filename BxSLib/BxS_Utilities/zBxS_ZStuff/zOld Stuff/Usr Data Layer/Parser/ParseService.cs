using System;
using System.Data;
using System.Collections.Generic;
//.........................................................
using SAPGUI.COM.DL;
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
	internal partial class Parser
		{
			#region "Methods: Private: Services"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ServicesDT2DTO(DataTable dtServices, Dictionary<Guid, IDTOService> dto)
					{
						foreach (DataRow lo_Row in dtServices.Rows)
							{
								IDTOService lo_DTO = new DTOService
									{
										UUID				= (Guid)	lo_Row[this._Ref.UUID]										,
										MSID				= (Guid)	lo_Row[this._Ref.MsgSrvID]								,

										Name				=					lo_Row[this._Ref.Name]				.ToString()	,
										Description =					lo_Row[this._Ref.Description]	.ToString()	,
										SystemID		=					lo_Row[this._Ref.SystemID]		.ToString()	,
										Type				=					lo_Row[this._Ref.ConnType]		.ToString()	,
										Server			=					lo_Row[this._Ref.Server]			.ToString()	,
										SNCName			=					lo_Row[this._Ref.SNCName]			.ToString()	,
										SAPCPG			=					lo_Row[this._Ref.CodePage]		.ToString()	,
										DCPG				=					lo_Row[this._Ref.DownUpCPge]	.ToString()	,
										SNCOp				=					lo_Row[this._Ref.SNCOp]				.ToString()	,
									};
								//.............................................
								dto.Add(lo_DTO.UUID,	lo_DTO);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ServicesDTO2DT(Dictionary<Guid, IDTOService> dto, DataTable dtServices)
					{
						foreach (KeyValuePair<Guid, IDTOService> lo_Entry in dto)
							{
								DataRow lo_Row	= dtServices.NewRow();
								//.............................................
								lo_Row[this._Ref.UUID]				= lo_Entry.Value.UUID					;
								lo_Row[this._Ref.Name]				= lo_Entry.Value.Name					;
								lo_Row[this._Ref.Description]	= lo_Entry.Value.Description	;
								lo_Row[this._Ref.ConnType]		= lo_Entry.Value.Type					;
								lo_Row[this._Ref.Server]			= lo_Entry.Value.Server				;
								lo_Row[this._Ref.SystemID]		= lo_Entry.Value.SystemID			;
								lo_Row[this._Ref.SNCName]			= lo_Entry.Value.SNCName			;
								lo_Row[this._Ref.SNCOp]				= lo_Entry.Value.SNCOp				;
								lo_Row[this._Ref.CodePage]		= lo_Entry.Value.SAPCPG				;
								lo_Row[this._Ref.DownUpCPge]	= lo_Entry.Value.DCPG					;
								lo_Row[this._Ref.MsgSrvID]		= lo_Entry.Value.MSID					;
								//.............................................
								dtServices.LoadDataRow(lo_Row.ItemArray, true);
							}
					}

			#endregion

		}
}
