﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class XMLParse2ReposDTO
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
				internal Repository Load(string	fullName,	bool onlySAPGUI	= true)
					{
						var	lo_Repos	= new Repository();
						//.............................................
						XmlDocument XmlDoc	= this.LoadXMLDoc(fullName);
						if (XmlDoc == null)		return	lo_Repos;
						//.............................................

						//.............................................
						// Load Services
						//
						foreach (XmlElement lo_Elem in XmlDoc.GetElementsByTagName("Service"))
							{
								string lc_Type = lo_Elem.GetAttribute("type");
								//.........................................
								if (onlySAPGUI && !lc_Type.Equals("SAPGUI"))	continue;
								//.........................................
								var lo_DTO = new DTOService
									{	Type				= lc_Type																				,
										UUID				= Guid.Parse(lo_Elem.GetAttribute(cz_TagUuid))	,
										Name				= lo_Elem.GetAttribute(cz_TagName)							,
										DCPG				= lo_Elem.GetAttribute("dcpg")									,
										MSID				= Guid.Parse(lo_Elem.GetAttribute("msid"))			,
										SAPCPG			= lo_Elem.GetAttribute("sapcpg")								,
										Server			= lo_Elem.GetAttribute("server")								,
										SNCName			= lo_Elem.GetAttribute("sncname")								,
										SNCOp				= lo_Elem.GetAttribute("sncop")									,
										SystemID		= lo_Elem.GetAttribute("systemid")							,
										Mode				= lo_Elem.GetAttribute("mode")									,
										Description	= lo_Elem.GetAttribute(cz_TagDesc)								};
								//................................................
								lo_Repos.Services.Add(lo_DTO.UUID, lo_DTO);
							}

						//.............................................
						// Load Message Servers
						//
						foreach (XmlElement lo_MsgSvr in XmlDoc.GetElementsByTagName("Messageserver"))
							{
								var lo_DTO = new DTOMsgServer
									{	UUID				= Guid.Parse(lo_MsgSvr.GetAttribute(cz_TagUuid))	,
										Name				= lo_MsgSvr.GetAttribute(cz_TagName)							,
										Host				= lo_MsgSvr.GetAttribute("host")									,
										Port				= lo_MsgSvr.GetAttribute("port")									,
										Description	= lo_MsgSvr.GetAttribute(cz_TagDesc)								};

								lo_Repos.MsgServers.Add(lo_DTO.UUID, lo_DTO);
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
										foreach (DTOWorkspaceItem lo_WSNodeItem in this.GetItemList(lo_Repos, lo_Node, true))
											{
												lo_WSNode.Items.Add(lo_WSNodeItem.UUID, lo_WSNodeItem);
											}
										//.....................................
										if (lo_WSNode.Items.Count > 0)
											lo_WSDTO.Nodes.Add(lo_WSNode.UUID, lo_WSNode);
									}
								//.........................................
								foreach (DTOWorkspaceItem lo_WSNodeItem in this.GetItemList(lo_Repos, lo_WrkSpace, false))
									{
										lo_WSDTO.Items.Add(lo_WSNodeItem.UUID, lo_WSNodeItem);
									}
								//...........................................
								if (lo_WSDTO.Nodes.Count > 0 || lo_WSDTO.Items.Count > 0)
									lo_Repos.WorkSpaces.Add(lo_WSDTO.UUID, lo_WSDTO);
							}

						//.............................................
						this.Load_XML_Cleanup(lo_Repos);

						return	lo_Repos;
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
							{	UUID				= Guid.Parse(element.GetAttribute(cz_TagUuid))	,
								Description	= element.GetAttribute(cz_TagName)							,
								Items				= new Dictionary<Guid, DTOWorkspaceItem>()				};
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTOWorkspace LoadWSAttributtes(XmlElement _xmlelement)
					{
						var lo_DTO = new DTOWorkspace
							{	UUID	= Guid.Parse(_xmlelement.GetAttribute(cz_TagUuid))	,
								Name	= _xmlelement.GetAttribute(cz_TagName)							,
								//.........................................
								Nodes = new Dictionary<Guid, DTOWorkspaceNode>()				,
								Items = new Dictionary<Guid, DTOWorkspaceItem>()					};
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private List<DTOWorkspaceItem> GetItemList(Repository repository, XmlElement element, bool forNode = true)
					{
						var	lt_List		= new List<DTOWorkspaceItem>();
						//.............................................
						foreach (XmlElement lo_Item in element.GetElementsByTagName("Item"))
							{
								if (forNode || !lo_Item.ParentNode.Name.Equals(cz_TagNode))
									{
										var lc_ServID	= Guid.Parse(lo_Item.GetAttribute("serviceid"));
										if (repository.Services.ContainsKey(lc_ServID))
											{
												lt_List.Add(	new DTOWorkspaceItem	{	UUID			= Guid.Parse(lo_Item.GetAttribute(cz_TagUuid))	,
																															ServiceID = lc_ServID																				}	);
											}
									}
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedMsgServers(Repository repository)
					{
						return	repository.Services.Select(
											x => x.Value.MSID)
												.Where(x => x.ToString().Length != 0)
													.Distinct()
														.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedServices(Repository repository)
					{
						return	repository.WorkSpaces.SelectMany
											( ws => ws.Value.Nodes.SelectMany
												( nd => nd.Value.Items.Select( it => it.Value.ServiceID )
														.Where( id => id.ToString().Length != 0 )
												)
												.Concat
													( ws.Value.Items.Select( it => it.Value.ServiceID )
															.Where( id => id.ToString().Length != 0 )
													)
											).ToList();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: Housekeeping"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Remove unwanted Services and Message Servers
				//
				private void Load_XML_Cleanup(Repository repository)
					{
						IList<Guid>	lt_Use;
						IList<Guid>	lt_Rem	= new List<Guid>();
						//.............................................
						// Cleanup Services
						//
						lt_Use	= this.UsedServices(repository);
						//.............................................
						foreach (KeyValuePair<Guid, DTOService> lo_Srv in repository.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								repository.Services.Remove(lo_Rem);
							}
						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers(repository);
						lt_Rem.Clear();
						//.............................................
						foreach (KeyValuePair<Guid, DTOMsgServer> lo_Msg in repository.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (Guid lo_Rem in lt_Rem)
							{
								repository.MsgServers.Remove(lo_Rem);
							}
					}

			#endregion

		}
}
