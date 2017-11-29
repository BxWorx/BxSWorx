using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class ReposSAPGui
		{
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (			this._DC	.MsgServers	.Count.Equals(0)
									&&	this._DC	.Services		.Count.Equals(0)
									&&	this._DC	.WorkSpaces	.Count.Equals(0)
									&&	this._DC	.Nodes			.Count.Equals(0)
									&&	this._DC	.Items			.Count.Equals(0)	)
							{
								return;
							}
						//.............................................
						this._DC.Clear();
						this.IsDirty	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Remove unwanted Services and Message Servers
				//
				public void HouseKeeping(bool ClearEmptyNodesWorkspaces = true)
					{
						IList<Guid>	lt_Use;
						IList<Guid>	lt_Rem	= new List<Guid>();

						//.............................................
						// Cleanup Services
						//
						lt_Use	= this.UsedServices();
						//.............................................
						foreach (KeyValuePair<Guid, IDTOService> lo_Srv in this._DC.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								this._DC.Services.Remove(lo_Rem);
							}

						if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers();
						lt_Rem.Clear();
						//.............................................
						foreach (KeyValuePair<Guid, IDTOMsgServer> lo_Msg in this._DC.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								this._DC.MsgServers.Remove(lo_Rem);
							}

						if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Workspaces and Nodes
						//
						if (!ClearEmptyNodesWorkspaces)	return;
						//.............................................
						lt_Use.Clear();

						foreach (KeyValuePair<Guid, IDTOWorkspace> lo_WS in this._DC.WorkSpaces)
							{
								if (lo_WS.Value.Items.Count.Equals(0))
									{
										lt_Rem.Clear();

										foreach (KeyValuePair<Guid, IDTONode> lo_ND in lo_WS.Value.Nodes)
											{
												if (lo_ND.Value.Items.Count.Equals(0))	lt_Rem.Add(lo_ND.Key);
											}
										foreach (Guid lg_ID in lt_Rem)
											{
												this.RemoveNode(lg_ID, lo_WS.Key);
											}

										if (lo_WS.Value.Nodes.Count.Equals(0))	lt_Use.Add(lo_WS.Key);
									}
							}

						foreach (Guid lg_ID in lt_Use)
							{
								this.RemoveWorkspace(lg_ID);
							}
					}

			#endregion

		}
}