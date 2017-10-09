using System.Collections.Generic;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Methods: Private: XML: Header Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlDocument LoadXMLDoc(string	XML_FullName)
					{
						XmlDocument lo_XMLDoc = new XmlDocument();
						//..................................................
						try
								{
									lo_XMLDoc.Load(XML_FullName);
								}
							catch (System.SystemException)
								{
								}
						//..................................................
						return lo_XMLDoc;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load_XML_Cleanup()
					{
						foreach (string lc in this.ct_UnUsedSrvList)
							{
								this.co_Repos.Services.Remove(lc);
							}
						//...............................................
						this.ct_UnUsedSrvList.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: XML: Workspace Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load_XML_WorkSpaces(XmlDocument _xmldoc)
					{
						foreach (XmlElement lo_WrkSpace in _xmldoc.GetElementsByTagName("Workspace"))
							{
								if (this.cc_FromWorkspace != null)
									{
										if (!this.cc_FromWorkspace.Length.Equals(0))
											{
												if (!lo_WrkSpace.GetAttribute("name").Equals(this.cc_FromWorkspace))
													{
														continue;
													}
											}
									}

								//...........................................
								DTOWorkspace lo_WsDTO = this.LoadWSAttributtes(lo_WrkSpace);
								//...........................................
								foreach (XmlElement lo_Node in lo_WrkSpace.GetElementsByTagName("Node"))
									{
										if (this.cc_FromNode != null)
											{
												if (!this.cc_FromNode.Length.Equals(0))
													{
														if (!lo_Node.GetAttribute("name").Equals(this.cc_FromNode))
															{
																continue;
															}
													}
											}

								//...........................................
								DTOWorkspaceNode lo_WSNode = this.LoadWSNodeAttributtes(lo_Node);

								foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(lo_Node))
									{
										lo_WSNode.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
									}
								//............................................
								if (lo_WSNode.Items.Count > 0)
									{
										lo_WsDTO.Nodes.Add(lo_WSNode.UUID, lo_WSNode);
									}
							}
							//..............................................
							if (lo_WsDTO.Nodes.Count > 0 || lo_WsDTO.Items.Count > 0)
								{
									this.co_Repos.WorkSpaces.Add(lo_WsDTO.UUID, lo_WsDTO);
								}
						}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspace LoadWSAttributtes(XmlElement _xmlelement)
					{
						DTOWorkspace lo_DTO = new DTOWorkspace
							{	UUID	= _xmlelement.GetAttribute("uuid")	,
								Name	= _xmlelement.GetAttribute("name")	,
								//...........................................
								Nodes = new Dictionary<string, DTOWorkspaceNode>()			,
								Items = new Dictionary<string, DTOWorkspaceNodeItem>()		};
						//...............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspaceNode LoadWSNodeAttributtes(XmlElement _xmlelement)
					{
						DTOWorkspaceNode lo_DTO = new DTOWorkspaceNode
							{	UUID	= _xmlelement.GetAttribute("uuid")	,
								Name	= _xmlelement.GetAttribute("name")	,
								Items = new Dictionary<string, DTOWorkspaceNodeItem>() };
						//...............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private List<DTOWorkspaceNodeItem> GetItemList(XmlElement _xmlelement)
					{
						List<DTOWorkspaceNodeItem>	lt_List		= new List<DTOWorkspaceNodeItem>();
						string											lc_SrvID	= null;
						//..............................................
						foreach (XmlElement lo_XMLItem in _xmlelement.GetElementsByTagName("Item"))
							{
								lc_SrvID = lo_XMLItem.GetAttribute("serviceid");

								if (this.co_Repos.Services.ContainsKey(lc_SrvID))
									{
										lt_List.Add(this.LoadItemAttributtes(lo_XMLItem));
										this.ct_UnUsedSrvList.Remove(lc_SrvID);
									}
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspaceNodeItem LoadItemAttributtes(XmlElement _xmlelement)
					{
						DTOWorkspaceNodeItem lo_DTO = new DTOWorkspaceNodeItem
							{	UIID			= _xmlelement.GetAttribute("uuid"),
								ServiceID = _xmlelement.GetAttribute("serviceid")	};
						//.............................................
						return lo_DTO;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: XML: Message Server Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, DTOMsgServer> Load_XML_MsgServers(XmlDocument _xmldoc)
					{
						Dictionary<string, DTOMsgServer>	lt_MsgSrvrs	= new	Dictionary<string, DTOMsgServer>();
						//.............................................
						foreach (XmlElement lo_MsgSvr in _xmldoc.GetElementsByTagName("Messageserver"))
							{
								DTOMsgServer lo_DTO = new DTOMsgServer
									{	UUID				= lo_MsgSvr.GetAttribute("uuid"),
										Name				= lo_MsgSvr.GetAttribute("name"),
										Host				= lo_MsgSvr.GetAttribute("host"),
										Port				= lo_MsgSvr.GetAttribute("port"),
										Description	= lo_MsgSvr.GetAttribute("description")	};

								lt_MsgSrvrs.Add(lo_DTO.UUID, lo_DTO);
							}
						//.............................................
						return	lt_MsgSrvrs;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: XML: Services Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, DTOMsgService> Load_XML_Services(XmlDocument _xmldoc)
					{
						Dictionary<string, DTOMsgService> lt_Services	= new	Dictionary<string, DTOMsgService>();
						//.............................................
						foreach (XmlElement lo_Elem in _xmldoc.GetElementsByTagName("Service"))
							{
								string lc_Type = lo_Elem.GetAttribute("type");
								//................................................
								if (this.cb_OnlySAPGUIEntries && !lc_Type.Equals("SAPGUI"))	continue;
								//................................................
								DTOMsgService lo_DTO = new DTOMsgService
									{	Type				= lc_Type,
										UUID				= lo_Elem.GetAttribute("uuid"),
										Name				= lo_Elem.GetAttribute("name"),
										DCPG				= lo_Elem.GetAttribute("dcpg"),
										MSID				= lo_Elem.GetAttribute("msid"),
										SAPCPG			= lo_Elem.GetAttribute("sapcpg"),
										Server			= lo_Elem.GetAttribute("server"),
										SNCName			= lo_Elem.GetAttribute("sncname"),
										SNCOp				= lo_Elem.GetAttribute("sncop"),
										SystemID		= lo_Elem.GetAttribute("systemid"),
										Mode				= lo_Elem.GetAttribute("mode"),
										Description	= lo_Elem.GetAttribute("description")	};
								//................................................
								lt_Services.Add(lo_DTO.UUID, lo_DTO);
							}
						//.............................................
						return	lt_Services;
					}

			#endregion

		}
}
