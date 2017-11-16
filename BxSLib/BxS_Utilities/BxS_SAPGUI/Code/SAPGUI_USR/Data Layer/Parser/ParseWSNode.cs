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
			#region "Methods: Private: Workspace Nodes"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WSNodeDT2DTO(DataTable dtNodes, Dictionary<Guid, IDTOWorkspace> dto)
					{
						foreach (DataRow lo_Row in dtNodes.Rows)
							{
								var lc_WSID	= (Guid)lo_Row[this._Ref.ParentID];

								if (dto.TryGetValue(lc_WSID, out IDTOWorkspace lo_WS))
									{
										IDTONode lo_DTO = new DTOWorkspaceNode
											{
												UUID				= (Guid)	lo_Row[this._Ref.UUID]										,
												Description =					lo_Row[this._Ref.Description]	.ToString()	,
											};
										//.............................................
										lo_WS.Nodes.Add(lo_DTO.UUID, lo_DTO);
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WSNodeDTO2DT(Guid wsID, Dictionary<Guid, IDTONode> dto	,	DataTable dtNodes
																																											,	DataTable dtItems	)
					{
						foreach (KeyValuePair<Guid, IDTONode> lo_Entry in dto)
							{
								DataRow lo_Row	= dtNodes.NewRow();
								//.............................................
								lo_Row[this._Ref.ParentID]			= wsID												;
								lo_Row[this._Ref.UUID]						= lo_Entry.Value.UUID					;
								lo_Row[this._Ref.Description]			= lo_Entry.Value.Description	;

								dtNodes.LoadDataRow(lo_Row.ItemArray, true);
								//.............................................
								this.WSItemDTO2DT(lo_Entry.Value.UUID, lo_Entry.Value.Items, dtItems, wsID);
							}
					}

			#endregion

		}
}
