using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using BxS_SAPGUI.API;
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
				public IList<IDTOConnectionView> CompileHierachicalView()
					{
						IList<IDTOConnectionView>	lt = new List<IDTOConnectionView>();
						int ln_WSNo;
						int ln_NdNo;
						int ln_ItNo	= 0;
						string	lc_WSID;
						string  lc_NDID;
						string	lc_ItID;
						//.............................................
						ln_WSNo	= 0;
						foreach (KeyValuePair<Guid, IDTOWorkspace> ls_WS in this._DataCon.WorkSpaces)
							{
								ln_WSNo ++	;
								ln_NdNo	= 0	;
								ln_ItNo	= 0	;
								lc_WSID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

								lt.Add(new DTOConnectionView(lc_WSID, ls_WS.Value.Description));
								//.........................................
								foreach (KeyValuePair<Guid, IDTONode> ls_Node in ls_WS.Value.Nodes)
									{
										ln_NdNo	++	;
										ln_ItNo	= 0	;
										lc_NDID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

										lt.Add(new DTOConnectionView(lc_NDID, ls_Node.Value.Description, lc_WSID));

										foreach (KeyValuePair<Guid, IDTOItem> ls_Item in ls_Node.Value.Items)
											{
												ln_ItNo	++	;
												lc_ItID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

												lt.Add(new DTOConnectionView(lc_ItID, ls_Item.Value.NodeID.ToString(), lc_WSID));
											}

									}
								//.........................................
								foreach (KeyValuePair<Guid, IDTOItem> ls_Item in ls_WS.Value.Items)
									{
										ln_ItNo	++	;
										lc_ItID	= this.CreateHierID(ln_WSNo, ln_NdNo, ln_ItNo);

										lt.Add(new DTOConnectionView(lc_ItID, ls_Item.Value.NodeID.ToString(), lc_WSID));
									}
							}
						//.............................................
						return	lt;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IDTOConnectionView CreateHierNode(Guid sapid = default(Guid))
					{
						IDTOConnectionView lo_Node = new DTOConnectionView
							{	SAPID = sapid	};
						//...................................................
						return	lo_Node;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string CreateHierID(int wsNo, int ndNo, int itNo)
					{
						string ls_WS	= $"{wsNo:D2}";
						string ls_Nd	= $"{ndNo:D2}";
						string ls_It	= $"{itNo:D2}";

						return $"{ls_WS}.{ls_Nd}.{ls_It}";
					}

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