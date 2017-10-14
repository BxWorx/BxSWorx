using System.Xml;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class ParseXML2Repository
		{

			#region "Declarations"

				private const string cz_TagName			= "name";
				private const string cz_TagUuid			= "uuid";
				private const string cz_TagDesc			= "description";

				private const string cz_TagNode			= "Node";
				private const string cz_TagWSpace		= "Workspace";

			#endregion

			//=============================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTORepository Load(string	fullName,	bool onlySAPGUI	= true	)
					{
						var	Repos	= new DTORepository();
						//.............................................
						XmlDocument XmlDoc	= this.LoadXMLDoc(fullName);
						if (XmlDoc == null)		return	Repos;
						//.............................................

						//.............................................
						// Load Services
						//
						foreach (XmlElement lo_Elem in XmlDoc.GetElementsByTagName("Service"))
							{
								string lc_Type = lo_Elem.GetAttribute("type");
								//................................................
								if (onlySAPGUI && !lc_Type.Equals("SAPGUI"))	continue;
								//................................................
								var lo_DTO = new DTOMsgService
									{	Type				= lc_Type															,
										UUID				= lo_Elem.GetAttribute(cz_TagUuid)		,
										Name				= lo_Elem.GetAttribute(cz_TagName)		,
										DCPG				= lo_Elem.GetAttribute("dcpg")				,
										MSID				= lo_Elem.GetAttribute("msid")				,
										SAPCPG			= lo_Elem.GetAttribute("sapcpg")			,
										Server			= lo_Elem.GetAttribute("server")			,
										SNCName			= lo_Elem.GetAttribute("sncname")			,
										SNCOp				= lo_Elem.GetAttribute("sncop")				,
										SystemID		= lo_Elem.GetAttribute("systemid")		,
										Mode				= lo_Elem.GetAttribute("mode")				,
										Description	= lo_Elem.GetAttribute(cz_TagDesc)			};
								//................................................
								Repos.Services.Add(lo_DTO.UUID, lo_DTO);
							}

						//.............................................
						// Load Message Servers
						//
						foreach (XmlElement lo_MsgSvr in XmlDoc.GetElementsByTagName("Messageserver"))
							{
								var lo_DTO = new DTOMsgServer
									{	UUID				= lo_MsgSvr.GetAttribute(cz_TagUuid)		,
										Name				= lo_MsgSvr.GetAttribute(cz_TagName)		,
										Host				= lo_MsgSvr.GetAttribute("host")				,
										Port				= lo_MsgSvr.GetAttribute("port")				,
										Description	= lo_MsgSvr.GetAttribute(cz_TagDesc)			};

								Repos.MsgServers.Add(lo_DTO.UUID, lo_DTO);
							}

						//.............................................
						// Load Workspaces
						//
						foreach (XmlElement lo_WrkSpace in XmlDoc.GetElementsByTagName(cz_TagWSpace))
							{
								DTOWorkspace lo_WSDTO = this.LoadWSAttributtes(lo_WrkSpace);
								//.........................................
								foreach (XmlElement lo_Node in lo_WrkSpace.GetElementsByTagName(cz_TagNode))
									{
										DTOWorkspaceNode	lo_WSNode = this.LoadWSNodeAttributtes(lo_Node);
										foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(Repos, lo_Node, true))
											{
												lo_WSNode.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
											}
										//.....................................
										if (lo_WSNode.Items.Count > 0)
											lo_WSDTO.Nodes.Add(lo_WSNode.UUID, lo_WSNode);
									}
								//.........................................
								foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(Repos, lo_WrkSpace, false))
									{
										lo_WSDTO.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
									}
								//...........................................
								if (lo_WSDTO.Nodes.Count > 0 || lo_WSDTO.Items.Count > 0)
									Repos.WorkSpaces.Add(lo_WSDTO.UUID, lo_WSDTO);
							}

						//.............................................
						this.Load_XML_Cleanup(Repos);

						return	Repos;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

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
				private DTOWorkspaceNode LoadWSNodeAttributtes(XmlElement element)
					{
						var lo_DTO = new DTOWorkspaceNode
							{	UUID	= element.GetAttribute(cz_TagUuid)	,
								Name	= element.GetAttribute(cz_TagName)	,
								Items = new Dictionary<string, DTOWorkspaceNodeItem>() };
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspace LoadWSAttributtes(XmlElement _xmlelement)
					{
						var lo_DTO = new DTOWorkspace
							{	UUID	= _xmlelement.GetAttribute(cz_TagUuid)	,
								Name	= _xmlelement.GetAttribute(cz_TagName)	,
								//.........................................
								Nodes = new Dictionary<string, DTOWorkspaceNode>()			,
								Items = new Dictionary<string, DTOWorkspaceNodeItem>()		};
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private List<DTOWorkspaceNodeItem> GetItemList(DTORepository repository, XmlElement element, bool forNode = true)
					{
						var	lt_List		= new List<DTOWorkspaceNodeItem>();
						//.............................................
						foreach (XmlElement lo_Item in element.GetElementsByTagName("Item"))
							{
								if (forNode || !lo_Item.ParentNode.Name.Equals(cz_TagNode))
									{
										string	lc_ServID	= lo_Item.GetAttribute("serviceid");
										if (repository.Services.ContainsKey(lc_ServID))
											{
												lt_List.Add(	new DTOWorkspaceNodeItem	{	UIID			= lo_Item.GetAttribute(cz_TagUuid)	,
																																	ServiceID = lc_ServID														}	);
											}
									}
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> UsedMsgServers(DTORepository repository)
					{
						IList<string>	lt_List	= new List<string>();
						//.............................................
						foreach (KeyValuePair<string, DTOMsgService> lo_Srv in repository.Services)
							{
								if (!lt_List.Contains(lo_Srv.Value.MSID))
									lt_List.Add(lo_Srv.Value.MSID);
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> UsedServices(DTORepository repository)
					{
						IList<string>	lt_List	= new List<string>();
						//.............................................
						foreach (KeyValuePair<string, DTOWorkspace> lo_WS in repository.WorkSpaces)
							{
								foreach (KeyValuePair<string, DTOWorkspaceNode> lo_Node in lo_WS.Value.Nodes)
									{
										foreach (KeyValuePair<string, DTOWorkspaceNodeItem> lo_Item in	lo_Node.Value.Items)
											{
												if (!lt_List.Contains(lo_Item.Value.ServiceID))
													lt_List.Add(lo_Item.Value.ServiceID);
											}
									}

								foreach (KeyValuePair<string, DTOWorkspaceNodeItem> lo_Item in lo_WS.Value.Items)
									{
										if (!lt_List.Contains(lo_Item.Value.ServiceID))
											lt_List.Add(lo_Item.Value.ServiceID);
									}
							}
						//.............................................
						return lt_List;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Housekeeping"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Remove unwanted Services and Message Servers
				//
				private void Load_XML_Cleanup(DTORepository repository)
					{
						IList<string>	lt_Use;
						IList<string>	lt_Rem	= new List<string>();
						//.............................................
						// Cleanup Services
						//
						lt_Use	= this.UsedServices(repository);
						//.............................................
						foreach (KeyValuePair<string, DTOMsgService> lo_Srv in repository.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (string lo_Rem in lt_Rem)
							{
								repository.Services.Remove(lo_Rem);
							}

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers(repository);
						lt_Rem.Clear();
						//.............................................
						foreach (KeyValuePair<string, DTOMsgServer> lo_Msg in repository.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (string lo_Rem in lt_Rem)
							{
								repository.MsgServers.Remove(lo_Rem);
							}
					}

			#endregion

		}
}
