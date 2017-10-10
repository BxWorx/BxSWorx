using System.Collections.Generic;
using System.Linq;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			//===========================================================================================
			#region "Methods: Private: XML: Workspace Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, DTOWorkspace> Load_WorkSpaces(	XmlDocument xmlDoc				,
																																	string			forWorkspace	,
																																	string			forNode					)
					{
						Dictionary<string, DTOWorkspace> lt_Workspaces	= new Dictionary<string, DTOWorkspace>();
						//.............................................
						foreach (XmlElement lo_WrkSpace in this.GetWorkSpaces(xmlDoc, forWorkspace))
							{
								DTOWorkspace lo_WsDTO = this.LoadWSAttributtes(lo_WrkSpace);
								//.........................................
								foreach (XmlElement lo_Node in this.GetWorkSpaceNodes(lo_WrkSpace, forNode))
									{
										DTOWorkspaceNode lo_WSNode = this.LoadWSNodeAttributtes(lo_Node);

										foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(lo_Node))
											{
												lo_WSNode.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
											}



									}
							}



						foreach (XmlElement lo_WrkSpace in xmlDoc.GetElementsByTagName("Workspace"))
							{
								if (!forWorkspace.Length.Equals(0))
									{	if (!lo_WrkSpace.GetAttribute("name").Equals(forWorkspace))	continue; }

								//.........................................
								DTOWorkspace lo_WsDTO = this.LoadWSAttributtes(lo_WrkSpace);
								//.........................................
								foreach (XmlElement lo_Node in lo_WrkSpace.GetElementsByTagName("Node"))
									{
										if (!forNode.Length.Equals(0))
											{	if (!lo_Node.GetAttribute("name").Equals(forNode))	continue;	}

								//.........................................
								DTOWorkspaceNode lo_WSNode = this.LoadWSNodeAttributtes(lo_Node);

								foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(lo_Node))
									{
										lo_WSNode.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
									}
								//.........................................
								if (lo_WSNode.Items.Count > 0)
									{
										lo_WsDTO.Nodes.Add(lo_WSNode.UUID, lo_WSNode);
									}
							}
							//...........................................
							if (lo_WsDTO.Nodes.Count > 0 || lo_WsDTO.Items.Count > 0)
								{
									lt_Workspaces.Add(lo_WsDTO.UUID, lo_WsDTO);
								}
							}
							//...........................................
							return	lt_Workspaces;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspace LoadWSAttributtes(XmlElement _xmlelement)
					{
						DTOWorkspace lo_DTO = new DTOWorkspace
							{	UUID	= _xmlelement.GetAttribute("uuid")	,
								Name	= _xmlelement.GetAttribute("name")	,
								//.........................................
								Nodes = new Dictionary<string, DTOWorkspaceNode>()			,
								Items = new Dictionary<string, DTOWorkspaceNodeItem>()		};
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspaceNode LoadWSNodeAttributtes(XmlElement element)
					{
						DTOWorkspaceNode lo_DTO = new DTOWorkspaceNode
							{	UUID	= element.GetAttribute("uuid")	,
								Name	= element.GetAttribute("name")	,
								Items = new Dictionary<string, DTOWorkspaceNodeItem>() };
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private List<DTOWorkspaceNodeItem> GetItemList(XmlElement element)
					{
						List<DTOWorkspaceNodeItem>	lt_List		= new List<DTOWorkspaceNodeItem>();
						//.............................................
						foreach (XmlElement lo_Item in element.GetElementsByTagName("Item"))
							{
								lt_List.Add(	new DTOWorkspaceNodeItem	{	UIID			= lo_Item.GetAttribute("uuid")			,
																													ServiceID = lo_Item.GetAttribute("serviceid")		}	);
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlNodeList GetWorkSpaceNodes(XmlElement workspace	,
																							string			forNode			)
					{
						if (forNode.Length.Equals(0))
							return	workspace.GetElementsByTagName("Node");
						//.............................................
						string	lc_Path	= $"//Workspace/Node[@name='{forNode}']";
						return	workspace.SelectNodes(lc_Path);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlNodeList GetWorkSpaces(XmlDocument xmlDoc				,
																					string			forWorkspace		)
					{
						if (forWorkspace.Length.Equals(0))
							return	xmlDoc.GetElementsByTagName("Workspace");
						//.............................................
						string	lc_Path	= $"//Workspaces/Workspace[@name='{forWorkspace}']";
						return	xmlDoc.SelectNodes(lc_Path);
					}

			#endregion

		}
}
