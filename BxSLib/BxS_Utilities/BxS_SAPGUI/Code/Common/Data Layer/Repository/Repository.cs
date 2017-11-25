using System;
using System.Collections.Generic;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	internal partial class Repository : IRepository
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository(DataContainer dataContainer)
					{
						this._DataCon	= dataContainer;
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private DataContainer	_DataCon;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsDirty	{					get {	return	this._DataCon.IsDirty	; }
															private set { this._DataCon.IsDirty = value	; } }

				public int	MsgServerCount	{ get {	return	this._DataCon.MsgServers.Count	; } }
				public int	ServiceCount		{ get {	return	this._DataCon.Services.Count		; } }
				public int	WorkspaceCount	{ get {	return	this._DataCon.WorkSpaces.Count	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: By Ref"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ref DataContainer GetDataContainer()
					{
						return ref this._DataCon;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (			this._DataCon	.MsgServers	.Count.Equals(0)
									&&	this._DataCon	.Services		.Count.Equals(0)
									&&	this._DataCon	.WorkSpaces	.Count.Equals(0)	)
							{
								return;
							}
						//.............................................
						this._DataCon.Clear();
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
						foreach (KeyValuePair<Guid, IDTOService> lo_Srv in this._DataCon.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								this._DataCon.Services.Remove(lo_Rem);
							}

						if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers();
						lt_Rem.Clear();
						//.............................................
						foreach (KeyValuePair<Guid, IDTOMsgServer> lo_Msg in this._DataCon.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								this._DataCon.MsgServers.Remove(lo_Rem);
							}

						if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Workspaces and Nodes
						//
						if (!ClearEmptyNodesWorkspaces)	return;
						//.............................................
						lt_Use.Clear();

						foreach (KeyValuePair<Guid, IDTOWorkspace> lo_WS in this._DataCon.WorkSpaces)
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

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedMsgServers()
					{
						return	this._DataCon.Services.Select(
											x => x.Value.MSID)
												.Where(x => x != Guid.Empty)
													.Distinct()
														.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedServices()
					{
						return	this._DataCon.WorkSpaces.SelectMany
											( ws => ws.Value.Nodes.SelectMany
												( nd => nd.Value.Items.Select( it => it.Value.ServiceID )
														.Where( id => id != Guid.Empty )
												)
												.Concat
													( ws.Value.Items.Select( it => it.Value.ServiceID )
															.Where( id => id != Guid.Empty )
													)
											).ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool MsgServerInUse(Guid ID)
					{
						return	!this._DataCon.Services.Count(kvp => kvp.Value.MSID.Equals(ID)).Equals(0);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool ServiceInUse(Guid ID)
					{
						bool lb_CntItm	= false;
						bool lb_CntNde	= false;
						//.............................................
						lb_CntItm	= this._DataCon.WorkSpaces
													.SelectMany( ws => ws.Value.Items
														.Where( it => it.Key.Equals(ID) ) )
															.Count()
																.Equals(0);

						if (lb_CntItm)
							{
								lb_CntNde	= this._DataCon.WorkSpaces
															.SelectMany( ws => ws.Value.Nodes
																.SelectMany( nd => nd.Value.Items
																	.Where( it => it.Key.Equals(ID) ) ) )
																		.Count()
																			.Equals(0);
							}
						//.............................................
						return	!lb_CntItm || !lb_CntNde;
					}

			#endregion

		}
}