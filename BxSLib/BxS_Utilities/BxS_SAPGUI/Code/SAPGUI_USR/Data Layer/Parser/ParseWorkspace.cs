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
			#region "Methods: Private: Workspaces"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WorkspaceDT2DTO(DataTable dtWorkspaces, Dictionary<Guid, DTOWorkspace> dto)
					{
						foreach (DataRow lo_Row in dtWorkspaces.Rows)
							{
								var lo_DTO = new DTOWorkspace
									{
										UUID				= (Guid)	lo_Row[this._Ref.UUID],
										Description	=					lo_Row[this._Ref.Description]	.ToString()	,
									};
								//.............................................
								dto.Add(lo_DTO.UUID,	lo_DTO);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WorkspaceDTO2DT(Dictionary<Guid, DTOWorkspace> dto	,	DataTable dtWorkspaces
																																				,	DataTable dtNodes
																																				,	DataTable dtItems				)
					{
						int	ln_SeqNo	= 0;
						foreach (KeyValuePair<Guid, DTOWorkspace> lo_Entry in dto)
							{
								ln_SeqNo++;
								DataRow lo_Row	= dtWorkspaces.NewRow();
								//.............................................
								lo_Row[this._Ref.UUID]				= lo_Entry.Value.UUID;
								lo_Row[this._Ref.Description]	= lo_Entry.Value.Description;
								lo_Row[this._Ref.SeqNo]				= ln_SeqNo.ToString();

								dtWorkspaces.LoadDataRow(lo_Row.ItemArray, true);
								//.............................................
								this.WSNodeDTO2DT(lo_Entry.Value.UUID, lo_Entry.Value.Nodes, dtNodes, dtItems);
								this.WSItemDTO2DT(lo_Entry.Value.UUID, lo_Entry.Value.Items, dtItems);
							}
					}

			#endregion

		}
}
