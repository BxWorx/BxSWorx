using System;
using System.Data;
using System.Collections.Generic;
//.........................................................
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
	internal partial class Parser
		{
			#region "Methods: Private: Services"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ServicesDT2DTO(DataTable dtServices, Dictionary<Guid, DTOService> dto)
					{
						foreach (DataRow lo_Row in dtServices.Rows)
							{
								var lo_DTO = new DTOService
									{
										UUID				= (Guid)		lo_Row[this._Ref.UUID]						,
										Name				= (string)	lo_Row[this._Ref.Name]						,
										Description = (string)	lo_Row[this._Ref.Description]			,
										SystemID		= (string)	lo_Row[this._Ref.SystemID]				,
										Type				= (string)	lo_Row[this._Ref.ConnType]				,
										Server			= (string)	lo_Row[this._Ref.Server]					,
										SNCName			= (string)	lo_Row[this._Ref.SNCName]					,
										SAPCPG			= (string)	lo_Row[this._Ref.CodePage]				,
										DCPG				= (string)	lo_Row[this._Ref.DownUpCodePage]	,
										SNCOp				= (string)	lo_Row[this._Ref.SNCOp]						,
										Mode				= (string)	lo_Row[this._Ref.Mode]						,
										MSID				= (Guid)		lo_Row[this._Ref.MsgSrvID]
									};
								//.............................................
								dto.Add(lo_DTO.UUID,	lo_DTO);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ServicesDTO2DT(Dictionary<Guid, DTOService> dto, DataTable dtServices)
					{
						foreach (KeyValuePair<Guid, DTOService> lo_Entry in dto)
							{
								DataRow lo_Row	= dtServices.NewRow();
								//.............................................
								lo_Row[this._Ref.UUID]						= lo_Entry.Value.UUID					;
								lo_Row[this._Ref.Name]						= lo_Entry.Value.Name					;
								lo_Row[this._Ref.Description]			= lo_Entry.Value.Description	;
								lo_Row[this._Ref.SystemID]				= lo_Entry.Value.SystemID			;
								lo_Row[this._Ref.ConnType]				= lo_Entry.Value.Type					;
								lo_Row[this._Ref.Server]					= lo_Entry.Value.Server				;
								lo_Row[this._Ref.SNCName]					= lo_Entry.Value							;
								lo_Row[this._Ref.CodePage]				= lo_Entry.Value							;
								lo_Row[this._Ref.DownUpCodePage]	= lo_Entry.Value.DCPG					;
								lo_Row[this._Ref.SNCOp]						= lo_Entry.Value							;
								lo_Row[this._Ref.Mode]						= lo_Entry.Value							;
								lo_Row[this._Ref.MsgSrvID]				= lo_Entry.Value							;
								//.............................................
								dtServices.LoadDataRow(lo_Row.ItemArray, true);
							}
					}

			#endregion

		}
}
