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
						this._DC.Clear();
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
						foreach (Guid lg_SrvID in this._DC.Services.KeyListFor())
							{
								if (!lt_Use.Contains(lg_SrvID))
									lt_Rem.Add(lg_SrvID);
							}

						this._DC.Services.Remove(lt_Rem);

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers();
						lt_Rem.Clear();
						//.............................................
						foreach (Guid lg_MsgID in this._DC.MsgServers.KeyListFor())
							{
								if (!lt_Use.Contains(lg_MsgID))
									lt_Rem.Add(lg_MsgID);
							}

						this._DC.MsgServers.Remove(lt_Rem);

						//.............................................
						// Cleanup Workspaces and Nodes
						//
						if (!ClearEmptyNodesWorkspaces)	return;
						//.............................................
						lt_Rem.Clear();

						foreach (Guid lg_Node in this._DC.Nodes.KeyListFor())
							{
								if (this._DC.Items.KeyListFor("NodeID", lg_Node).Count.Equals(0))
									{	lt_Rem.Add(lg_Node); }
							}

						this._DC.Nodes.Remove(lt_Rem);
						//.............................................
						lt_Rem.Clear();

						foreach (Guid lg_WS in this._DC.Workspaces.KeyListFor())
							{
								if (		this._DC.Items.KeyListFor("WSID", lg_WS).Count.Equals(0)
										&&	this._DC.Nodes.KeyListFor("WSID", lg_WS).Count.Equals(0))
									{	lt_Rem.Add(lg_WS); }
							}

						this._DC.Workspaces.Remove(lt_Rem);
					}

			#endregion

		}
}