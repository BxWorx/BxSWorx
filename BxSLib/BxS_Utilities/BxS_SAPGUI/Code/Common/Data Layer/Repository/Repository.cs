using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using SAPGUI.API;
using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.DL
{
	internal partial class Repository : IRepository
		{
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository(DataContainer dataContainer)
					{
						this.DataCon	= dataContainer;
						//this.DataCon	= dataContainer;
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

				private DataContainer	DataCon;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ref DataContainer GetDataContainer()
					{
						return ref this.DataCon;
					}



			//===========================================================================================
			#region "Properties"

				public bool	IsDirty	{					get {	return	this.DataCon.IsDirty	; }
															private set { this.DataCon.IsDirty = value	; } }

				public int	MsgServerCount	{ get {	return	this.DataCon.MsgServers.Count	; } }
				public int	ServiceCount		{ get {	return	this.DataCon.Services.Count		; } }
				public int	WorkspaceCount	{ get {	return	this.DataCon.WorkSpaces.Count	; } }

				//public DataContainer	DataCon	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (			this.DataCon	.MsgServers	.Count.Equals(0)
									&&	this.DataCon	.Services		.Count.Equals(0)
									&&	this.DataCon	.WorkSpaces	.Count.Equals(0)	)
							{
								return;
							}
						//.............................................
						this.DataCon.Clear();
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
						foreach (KeyValuePair<Guid, IDTOService> lo_Srv in this.DataCon.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								this.DataCon.Services.Remove(lo_Rem);
							}

						if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers();
						lt_Rem.Clear();
						//.............................................
						foreach (KeyValuePair<Guid, IDTOMsgServer> lo_Msg in this.DataCon.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								this.DataCon.MsgServers.Remove(lo_Rem);
							}

						if (!lt_Rem.Count.Equals(0))		this.IsDirty	= true;

						//.............................................
						// Cleanup Workspaces and Nodes
						//
						if (!ClearEmptyNodesWorkspaces)	return;
						//.............................................
						lt_Use.Clear();

						foreach (KeyValuePair<Guid, IDTOWorkspace> lo_WS in this.DataCon.WorkSpaces)
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
						return	this.DataCon.Services.Select(
											x => x.Value.MSID)
												.Where(x => x != Guid.Empty)
													.Distinct()
														.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedServices()
					{
						return	this.DataCon.WorkSpaces.SelectMany
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
						return	!this.DataCon.Services.Count(kvp => kvp.Value.MSID.Equals(ID)).Equals(0);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool ServiceInUse(Guid ID)
					{
						bool lb_CntItm	= false;
						bool lb_CntNde	= false;
						//.............................................
						lb_CntItm	= this.DataCon.WorkSpaces
													.SelectMany( ws => ws.Value.Items
														.Where( it => it.Key.Equals(ID) ) )
															.Count()
																.Equals(0);

						if (lb_CntItm)
							{
								lb_CntNde	= this.DataCon.WorkSpaces
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