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
						//if (			this._DC	.XMsgServers	.Count.Equals(0)
						//			&&	this._DC	.XServices		.Count.Equals(0)
						//			&&	this._DC	.XWorkspaces	.Count.Equals(0)
						//			&&	this._DC	.XNodes				.Count.Equals(0)
						//			&&	this._DC	.XItems			.Count.Equals(0)	)
						//	{
						//		return;
						//	}
						////.............................................
						this._DC.Clear();
						//this.IsDirty	= true;
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
						foreach (Guid lg_SrvID in this._DC.XServices.KeyListFor())
							{
								if (!lt_Use.Contains(lg_SrvID))
									lt_Rem.Add(lg_SrvID);
							}

						this._DC.XServices.Remove(lt_Rem);
						////.............................................
						//foreach (Guid lo_Rem in lt_Rem)
						//	{
						//		this._DC.Services.Remove(lo_Rem);
						//	}

						//if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers();
						lt_Rem.Clear();
						//.............................................
						foreach (Guid lg_MsgID in this._DC.XMsgServers.KeyListFor())
							{
								if (!lt_Use.Contains(lg_MsgID))
									lt_Rem.Add(lg_MsgID);
							}

						this._DC.XMsgServers.Remove(lt_Rem);
						////.............................................
						//foreach (Guid lo_Rem in lt_Rem)
						//	{
						//		this._DC.MsgServers.Remove(lo_Rem);
						//	}

						//if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Workspaces and Nodes
						//
						if (!ClearEmptyNodesWorkspaces)	return;
						//.............................................
						lt_Rem.Clear();

						foreach (Guid lg_Node in this._DC.XNodes.KeyListFor())
							{
								if (this._DC.XItems.KeyListFor("NodeID", lg_Node).Count.Equals(0))
									{	lt_Rem.Add(lg_Node); }
							}

						this._DC.XNodes.Remove(lt_Rem);
						//.............................................
						lt_Rem.Clear();

						foreach (Guid lg_WS in this._DC.XWorkspaces.KeyListFor())
							{
								if (		this._DC.XItems.KeyListFor("WSID", lg_WS).Count.Equals(0)
										&&	this._DC.XNodes.KeyListFor("WSID", lg_WS).Count.Equals(0))
									{	lt_Rem.Add(lg_WS); }
							}

						this._DC.XWorkspaces.Remove(lt_Rem);
					}
				//if (lo_WS.Value.Items.Count.Equals(0))
				//					{
				//						lt_Rem.Clear();

				//						foreach (KeyValuePair<Guid, IDTONode> lo_ND in lo_WS.Value.Nodes)
				//							{
				//								if (lo_ND.Value.Items.Count.Equals(0))	lt_Rem.Add(lo_ND.Key);
				//							}
				//						foreach (Guid lg_ID in lt_Rem)
				//							{
				//								this.RemoveNode(lg_ID, lo_WS.Key);
				//							}

				//						if (lo_WS.Value.Nodes.Count.Equals(0))	lt_Use.Add(lo_WS.Key);
				//					}
				//			}

				//		foreach (Guid lg_ID in lt_Use)
				//			{
				//				this.RemoveWorkspace(lg_ID);
				//			}
					//}

			#endregion

		}
}