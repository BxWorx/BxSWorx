using System;
using System.Xml;
//.........................................................
using BxS_SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.XML
{
	internal partial class XMLParse2ReposDTO
		{
			#region "Declarations"

				private const string cz_TagName			= "name";
				private const string cz_TagUuid			= "uuid";
				private const string cz_TagDesc			= "description";

				private const string cz_TagItem			= "Item";
				private const string cz_TagNode			= "Node";
				private const string cz_TagWSpace		= "Workspace";

				private IReposSAPGui		_Repos;
				private bool					_OnlySAPGUI;

			#endregion

			//=============================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load(IReposSAPGui repository, string xmlFullName,	bool onlySAPGUI	= true)
					{
						//.............................................
						XmlDocument XmlDoc	= this.LoadXMLDoc(xmlFullName);
						if (XmlDoc == null)		return;
						//.............................................
						this._Repos				= repository;
						this._OnlySAPGUI	= onlySAPGUI;
						//.............................................
						foreach (XmlElement lo_MsgSvr in XmlDoc.GetElementsByTagName("Messageserver"))
							{	this.LoadMsgServer(lo_MsgSvr); }

						foreach (XmlElement lo_Srv in XmlDoc.GetElementsByTagName("Service"))
							{	this.LoadService(lo_Srv); }

						//.............................................
						// Load Workspaces
						//
						foreach (XmlElement lo_WrkSpace in XmlDoc.GetElementsByTagName(cz_TagWSpace))
							{
								Guid lg_WSID	= this.LoadWorkspace(lo_WrkSpace);
								//.........................................
								foreach (XmlElement lo_Node in lo_WrkSpace.GetElementsByTagName(cz_TagNode))
									{
										Guid lg_Node	= this.LoadNode(lg_WSID, lo_Node);
										//.....................................
										foreach (XmlElement lo_Item in lo_Node.GetElementsByTagName(cz_TagItem))
											{
												this.LoadItem(lg_WSID, lg_Node, lo_Item );
											}
									}
								//.........................................
								foreach (XmlElement lo_Item in lo_WrkSpace.GetElementsByTagName(cz_TagItem))
									{
										if (lo_Item.ParentNode.Name.Equals(cz_TagWSpace))
											this.LoadItem(lg_WSID, default(Guid), lo_Item );
									}
							}
						//.............................................
						repository.HouseKeeping();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Guid ParseGuid(string guid)
					{
						Guid	lg_Guid;
						//.............................................
						try
							{
								lg_Guid	=	guid?.Length == 0 ? Guid.Empty	: Guid.Parse(guid);
							}
						catch (Exception)
							{	lg_Guid	=	Guid.Empty;	}
						//.............................................
						return	lg_Guid;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlDocument LoadXMLDoc(string fullname)
					{
						var lo_XMLDoc = new XmlDocument();
						//.............................................
						try
								{
									lo_XMLDoc.Load(fullname);
								}
							catch (System.SystemException)
								{
									return null;
								}
						//.............................................
						return lo_XMLDoc;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadMsgServer(XmlElement msgSvr)
					{
						Guid lg_Key	= this.ParseGuid(msgSvr.GetAttribute(cz_TagUuid));
						if (lg_Key == Guid.Empty)	return;
						//.........................................
						this._Repos.AddUpdateMsgServer(	ID:						lg_Key													,
																						Name:					msgSvr.GetAttribute(cz_TagName)	,
																						Host:					msgSvr.GetAttribute("host")			,
																						Port:					msgSvr.GetAttribute("port")			,
																						Description:	msgSvr.GetAttribute(cz_TagDesc)		);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadService(XmlElement srv)
					{
						string lc_Type = srv.GetAttribute("type");
						//.........................................
						if (this._OnlySAPGUI && !lc_Type.Equals("SAPGUI"))	return;
						//.........................................
						Guid lg_Key		= this.ParseGuid(srv.GetAttribute(cz_TagUuid));
						if (lg_Key == Guid.Empty)										return;
						Guid lg_MSID	= this.ParseGuid(srv.GetAttribute("msid"));
						//.........................................
						this._Repos.AddUpdateService(	ID:						lg_Key												,
																					Name:					srv.GetAttribute(cz_TagName)	,
																					Description:	srv.GetAttribute(cz_TagDesc)	,
																					SystemID:			srv.GetAttribute("systemid")	,
																					Type:					lc_Type												,
																					Server:				srv.GetAttribute("server")		,
																					SAPCPG:				srv.GetAttribute("sapcpg")		,
																					DCPG:					srv.GetAttribute("dcpg")			,
																					SNCName:			srv.GetAttribute("sncname")		,
																					SNCOp:				srv.GetAttribute("sncop")			,
																					MsgServer:		lg_MSID												,
																					Mode:					srv.GetAttribute("mode")				);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Guid LoadNode(Guid WSID, XmlElement node)
					{
						Guid lo_ID	= this.ParseGuid(node.GetAttribute(cz_TagUuid));
						this._Repos.AddUpdateNode(	ID:						lo_ID													,
																				Description:	node.GetAttribute(cz_TagName)	,
																				ForWSID:			WSID														);
						return	lo_ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Guid LoadWorkspace(XmlElement ws)
					{
						Guid lo_ID	= this.ParseGuid(ws.GetAttribute(cz_TagUuid));
						this._Repos.AddUpdateWorkspace(	ID:						lo_ID												,
																						Description:	ws.GetAttribute(cz_TagName)		);
						return	lo_ID;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadItem(Guid wsID, Guid nodeID, XmlElement item)
					{
						Guid lg_ID		= this.ParseGuid(item.GetAttribute(cz_TagUuid));
						Guid lg_SrvID	= this.ParseGuid(item.GetAttribute("serviceid"));

						this._Repos.AddUpdateItem(lg_ID, lg_SrvID, wsID, nodeID);
					}

			#endregion

		}
}
