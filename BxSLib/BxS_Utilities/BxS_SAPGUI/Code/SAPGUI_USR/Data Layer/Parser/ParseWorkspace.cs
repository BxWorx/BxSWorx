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
										UUID				= (Guid)		lo_Row[this._Ref.UUID],
										Name				= (string)	lo_Row[this._Ref.Name],
									};
								//.............................................
								dto.Add(lo_DTO.UUID,	lo_DTO);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WorkspaceDTO2DT(Dictionary<Guid, DTOWorkspace> dto	,	DataTable dtWorkspaces
																																				,	DataTable dtItems
																																				,	DataTable dtNodes		)
					{
						foreach (KeyValuePair<Guid, DTOWorkspace> lo_Entry in dto)
							{
								DataRow lo_Row	= dtWorkspaces.NewRow();
								//.............................................
								lo_Row[this._Ref.UUID]				= lo_Entry.Value.UUID;
								lo_Row[this._Ref.Name]				= lo_Entry.Value.Name;

								dtWorkspaces.LoadDataRow(lo_Row.ItemArray, true);
								//.............................................
								this.WSNodeDTO2DT(lo_Entry.Value.UUID, lo_Entry.Value.Nodes, dtNodes, dtItems);
								this.WSItemDTO2DT(lo_Entry.Value.UUID, lo_Entry.Value.Items, dtItems);
							}
					}

			#endregion

		}
}
