using System.Collections.Generic;
using System.Linq;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Constants"

				private const string cz_TagName			= "name";
				private const string cz_TagNode			= "Node";
				private const string cz_TagWSpace		= "Workspace";
				private const string cz_TagWSpaces	= "Workspaces";

			#endregion

			//===========================================================================================
			#region "Methods: Private: XML: Workspace Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load_WorkSpaces(string forWorkspace, string forNode)
					{
						foreach (XmlElement lo_WrkSpace in this.GetWorkSpaces(forWorkspace))
							{
								DTOWorkspace lo_WSDTO = this.LoadWSAttributtes(lo_WrkSpace);
								//.........................................
								foreach (XmlElement lo_Node in this.GetWorkSpaceNodes(lo_WrkSpace, forWorkspace, forNode))
									{
										DTOWorkspaceNode	lo_WSNode = this.LoadWSNodeAttributtes(lo_Node);
										foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(lo_Node))
											{
												lo_WSNode.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
											}
										//.....................................
										if (lo_WSNode.Items.Count > 0)
											lo_WSDTO.Nodes.Add(lo_WSNode.UUID, lo_WSNode);
									}
								//.........................................
								if (forNode.Length.Equals(0))
									{
										foreach (DTOWorkspaceNodeItem lo_WSNodeItem in this.GetItemList(lo_WrkSpace, false))
											{
												lo_WSDTO.Items.Add(lo_WSNodeItem.UIID, lo_WSNodeItem);
											}
									}
								//...........................................
								if (lo_WSDTO.Nodes.Count > 0 || lo_WSDTO.Items.Count > 0)
									this._Repos.WorkSpaces.Add(lo_WSDTO.UUID, lo_WSDTO);
							}
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
				private List<DTOWorkspaceNodeItem> GetItemList(XmlElement element, bool forNode = true)
					{
						List<DTOWorkspaceNodeItem>	lt_List		= new List<DTOWorkspaceNodeItem>();
						//.............................................
						foreach (XmlElement lo_Item in element.GetElementsByTagName("Item"))
							{
								if (forNode || !lo_Item.ParentNode.Name.Equals(cz_TagNode))
									{
										string	lc_ServID	= lo_Item.GetAttribute("serviceid");
										if (this._Repos.Services.ContainsKey(lc_ServID))
											{
												lt_List.Add(	new DTOWorkspaceNodeItem	{	UIID			= lo_Item.GetAttribute("uuid")	,
																																	ServiceID = lc_ServID												}	);
											}
									}
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlNodeList GetWorkSpaceNodes(XmlElement	workspace			,
																							string			forWorkspace	,
																							string			forNode					)
					{
						if (forNode.Length.Equals(0))
							return	workspace.GetElementsByTagName(cz_TagNode);
						//.............................................
						string	lc_Path	= $"//{cz_TagWSpaces}/{cz_TagWSpace}[@{cz_TagName}='{forWorkspace}']/{cz_TagNode}[@{cz_TagName}='{forNode}']";
						return	workspace.SelectNodes(lc_Path);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlNodeList GetWorkSpaces(string forWorkspace)
					{
						if (forWorkspace.Length.Equals(0))
							return	this._XmlDoc.GetElementsByTagName(cz_TagWSpace);
						//.............................................
						string	lc_Path	= $"//{cz_TagWSpaces}/{cz_TagWSpace}[@{cz_TagName}='{forWorkspace}']";
						return	this._XmlDoc.SelectNodes(lc_Path);
					}

			#endregion

		}
}
