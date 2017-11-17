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
						this._DC	= dataContainer;
						//.............................................
						this.IsDirty	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly DataContainer	_DC;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool IsDirty { get; private set; }
				//.................................................
				public int MsgServerCount	{ get {	return	this._DC.MsgServers.Count; } }
				public int ServiceCount		{ get {	return	this._DC.Services.Count; } }
				public int WorkspaceCount	{ get {	return	this._DC.WorkSpaces.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Clear()
					{
						if (			this._DC	.MsgServers	.Count.Equals(0)
									&&	this._DC	.Services		.Count.Equals(0)
									&&	this._DC	.WorkSpaces	.Count.Equals(0)	)
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
				public void HouseKeeping()
					{
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
						}
						this.IsDirty	= true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedMsgServers()
					{
						return	this._DC.Services.Select(
											x => x.Value.MSID)
												.Where(x => x != Guid.Empty)
													.Distinct()
														.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedServices()
					{
						return	this._DC.WorkSpaces.SelectMany
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
						return	!this._DC.Services.Count(kvp => kvp.Value.MSID.Equals(ID)).Equals(0);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool ServiceInUse(Guid ID)
					{
						bool lb_CntItm	= false;
						bool lb_CntNde	= false;
						//.............................................
						lb_CntItm	= this._DC.WorkSpaces
													.SelectMany( ws => ws.Value.Items
														.Where( it => it.Key.Equals(ID) ) )
															.Count()
																.Equals(0);

						if (lb_CntItm)
							{
								lb_CntNde	= this._DC.WorkSpaces
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