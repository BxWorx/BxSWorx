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
			#region "Methods: Private: Workspace Items"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WSItemDT2DTO(DataTable dtItems, Dictionary<Guid, DTOWorkspace> dto)
					{
						foreach (DataRow lo_Row in dtItems.Rows)
							{
								bool lb_IsItem	= (bool)	lo_Row[this._Ref.ItemType];
								Guid lg_WSID		= lb_IsItem	? (Guid)lo_Row[this._Ref.UUID] : (Guid)lo_Row[this._Ref.WorkspaceID]	;

								if (dto.TryGetValue(lg_WSID, out DTOWorkspace lo_WS))
									{
										var lo_DTO = new DTOWorkspaceItem
											{
												UUID				= (Guid)	lo_Row[this._Ref.UUID],
												ServiceID		= (Guid)	lo_Row[this._Ref.ServiceID]
											};
										//.....................................
										if (lb_IsItem)
											{
												lo_WS.Items.Add(lo_DTO.UUID, lo_DTO);
											}
										else
											{
												if (lo_WS.Nodes.TryGetValue((Guid)lo_Row[this._Ref.UUID], out DTOWorkspaceNode lo_ND))
													{
														lo_ND.Items.Add(lo_DTO.UUID, lo_DTO);
													}
												}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void WSItemDTO2DT(Guid refid, Dictionary<Guid, DTOWorkspaceItem> dto, DataTable dtItems, Guid wsID	= default(Guid))
					{
						foreach (KeyValuePair<Guid, DTOWorkspaceItem> lo_Entry in dto)
							{
								DataRow lo_Row	= dtItems.NewRow();
								//.............................................
								lo_Row[this._Ref.ReferenceID]	= refid											;
								lo_Row[this._Ref.UUID]				= lo_Entry.Value.UUID				;
								lo_Row[this._Ref.ServiceID]		= lo_Entry.Value.ServiceID	;
								lo_Row[this._Ref.WorkspaceID]	= wsID											;
								lo_Row[this._Ref.ItemType]		= (wsID	== default(Guid))	? true	: false	;
								//.............................................
								dtItems.LoadDataRow(lo_Row.ItemArray, true);
							}
					}

			#endregion

		}
}
