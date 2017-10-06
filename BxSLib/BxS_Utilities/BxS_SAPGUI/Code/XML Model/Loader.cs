using System.Collections.Generic;
using System.Xml;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	public class Loader
	{
		#region "Definitions"

			private readonly string				cc_XMLFullName;
			private	readonly string				cc_FromWorkspace;
			private	readonly string				cc_FromNode;
			private readonly bool					cb_OnlySAPGUIEntries;
			private readonly List<string>	ct_UnUsedSrvList;
			//....................................................
			private Repository	co_Repos;

		#endregion

		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯

		#region "Properties"

			internal Repository													Repository	{	get { return this.co_Repos;							} }
			internal Dictionary<string, DTOMsgService>	Services		{	get { return this.co_Repos.Services;		} }
			internal Dictionary<string, DTOMsgServer>		MsgServers	{	get { return this.co_Repos.MsgServers;	}	}
			internal Dictionary<string, DTOWorkspace>		WorkSpaces	{	get { return this.co_Repos.WorkSpaces;	}	}

		#endregion

		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯

		#region "Constructor"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public Loader(	string	XML_FullName			,
											string	XML_FromWorkspace	,
											string	XML_FromNode			,
											bool		OnlySAPGUIEntries = true	)
				{
					this.cc_XMLFullName				= XML_FullName;
					this.cc_FromWorkspace			= XML_FromWorkspace;
					this.cc_FromNode					= XML_FromNode;
					this.cb_OnlySAPGUIEntries	= OnlySAPGUIEntries;
					//..................................................
					this.ct_UnUsedSrvList	= new List<string>();
					//..................................................
					this.LoadSapGuiXML();
				}

		#endregion
		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#region "Methods: Private: XML Sections"

		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#region "Methods: Private: XML: Header Section"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void LoadSapGuiXML()
				{
					this.co_Repos = new Repository();
					//......................................................
					XmlDocument lo_XMLDoc = this.LoadXMLDoc();

					this.Load_XML_MsgServers(ref lo_XMLDoc);
					this.Load_XML_Services(ref lo_XMLDoc);
					this.Load_XML_WorkSpaces(lo_XMLDoc);
					this.Load_XML_Cleanup();
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private XmlDocument LoadXMLDoc()
				{
					XmlDocument lo_XMLDoc = new XmlDocument();
					//..................................................
					try
							{
								lo_XMLDoc.Load(this.cc_XMLFullName);
							}
						catch (System.SystemException)
							{
							}
					//..................................................
					return lo_XMLDoc;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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

		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯

		#region "Methods: Private: XML: Workspace Section"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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
					//..............................................
					return lt_List;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private DTOWorkspaceNodeItem LoadItemAttributtes(XmlElement _xmlelement)
				{
					DTOWorkspaceNodeItem lo_DTO = new DTOWorkspaceNodeItem
						{	UIID			= _xmlelement.GetAttribute("uuid"),
							ServiceID = _xmlelement.GetAttribute("serviceid")	};
					//...............................................
					return lo_DTO;
				}

		#endregion

		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯

		#region "Methods: Private: XML: Message Server Section"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void Load_XML_MsgServers(ref XmlDocument _xmldoc)
				{
					foreach (XmlElement lo_MsgSvr in _xmldoc.GetElementsByTagName("Messageserver"))
						{
							this.LoadMsgServer(lo_MsgSvr);
						}
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void LoadMsgServer(XmlElement _xmlelement)
				{
					DTOMsgServer lo_DTO = new DTOMsgServer
						{	UUID				= _xmlelement.GetAttribute("uuid"),
							Name				= _xmlelement.GetAttribute("name"),
							Host				= _xmlelement.GetAttribute("host"),
							Port				= _xmlelement.GetAttribute("port"),
							Description	= _xmlelement.GetAttribute("description")	};
					//................................................
					this.co_Repos.MsgServers.Add(lo_DTO.UUID, lo_DTO);
				}

		#endregion

		//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯

		#region "Methods: Private: XML: Services Section"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void Load_XML_Services(ref XmlDocument _xmldoc)
				{
					foreach (XmlElement lo_ServiceElement in _xmldoc.GetElementsByTagName("Service"))
						{
							this.LoadService(lo_ServiceElement);
						}
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private void LoadService(XmlElement _xmlelement)
				{
					string lc_Type = _xmlelement.GetAttribute("type");
					//................................................
					if (this.cb_OnlySAPGUIEntries)
						{
							if (!lc_Type.Equals("SAPGUI"))	return;
						}
					//................................................
					DTOMsgService lo_DTO = new DTOMsgService
						{	Type				= lc_Type,
							UUID				= _xmlelement.GetAttribute("uuid"),
							Name				= _xmlelement.GetAttribute("name"),
							DCPG				= _xmlelement.GetAttribute("dcpg"),
							MSID				= _xmlelement.GetAttribute("msid"),
							SAPCPG			= _xmlelement.GetAttribute("sapcpg"),
							Server			= _xmlelement.GetAttribute("server"),
							SNCName			= _xmlelement.GetAttribute("sncname"),
							SNCOp				= _xmlelement.GetAttribute("sncop"),
							SystemID		= _xmlelement.GetAttribute("systemid"),
							Mode				= _xmlelement.GetAttribute("mode"),
							Description	= _xmlelement.GetAttribute("description")	};
					//................................................
					this.co_Repos.Services.Add(lo_DTO.UUID, lo_DTO);
					this.ct_UnUsedSrvList.Add(lo_DTO.UUID);
				}

			#endregion

		#endregion

	}
}
