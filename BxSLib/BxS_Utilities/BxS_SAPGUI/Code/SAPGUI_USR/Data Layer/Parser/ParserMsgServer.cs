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
			#region "Methods: Private: Msg Servers"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void MsgServersDT2DTO(DataTable dtMsgServers, Dictionary<Guid, DTOMsgServer> dto)
					{
						foreach (DataRow lo_Row in dtMsgServers.Rows)
							{
								var lo_DTO = new DTOMsgServer
									{
										UUID				= (Guid)	lo_Row[this._Ref.UUID]										,

										Name				=					lo_Row[this._Ref.Name]				.ToString()	,
										Description =					lo_Row[this._Ref.Description]	.ToString()	,
										Host				=					lo_Row[this._Ref.Host]				.ToString()	,
										Port				=					lo_Row[this._Ref.Port]				.ToString()
									};
								//.............................................
								dto.Add(lo_DTO.UUID,	lo_DTO);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void MsgServersDTO2DT(Dictionary<Guid, DTOMsgServer> dto, DataTable dtMsgServers)
					{
						foreach (KeyValuePair<Guid, DTOMsgServer> lo_Entry in dto)
							{
								DataRow lo_Row	= dtMsgServers.NewRow();
								//.............................................
								lo_Row[this._Ref.UUID]				= lo_Entry.Value.UUID;
								lo_Row[this._Ref.Name]				= lo_Entry.Value.Name;
								lo_Row[this._Ref.Description]	= lo_Entry.Value.Description;
								lo_Row[this._Ref.Host]				= lo_Entry.Value.Host;
								lo_Row[this._Ref.Port]				= lo_Entry.Value.Port;
								//.............................................
								dtMsgServers.LoadDataRow(lo_Row.ItemArray, true);
							}
					}

			#endregion

		}
}
