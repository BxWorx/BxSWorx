using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using SAPGUI.COM.DL;
using SAPGUI.API.DL;
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
				internal void Load(Repository repository, string	fullName,	bool onlySAPGUI	= true)
					{
						//.............................................
						XmlDocument XmlDoc	= this.LoadXMLDoc(fullName);
						if (XmlDoc == null)		return;
						//.............................................

						//.............................................
						// Load Services
						//
						foreach (XmlElement lo_Elem in XmlDoc.GetElementsByTagName("Service"))
							{
								string lc_Type = lo_Elem.GetAttribute("type");
								//.........................................
								if (onlySAPGUI && !lc_Type.Equals("SAPGUI"))	continue;

								Guid lg_Key	= this.ParseGuid(lo_Elem.GetAttribute(cz_TagUuid));
								if (lg_Key	== Guid.Empty)	continue;
								//.........................................
								IDTOService lo_DTO = new DTOService
									{	Type				= lc_Type																				,
										UUID				= lg_Key																				,
										Name				= lo_Elem.GetAttribute(cz_TagName)							,
										DCPG				= lo_Elem.GetAttribute("dcpg")									,
										MSID				= this.ParseGuid(lo_Elem.GetAttribute("msid"))	,
										SAPCPG			= lo_Elem.GetAttribute("sapcpg")								,
										Server			= lo_Elem.GetAttribute("server")								,
										SNCName			= lo_Elem.GetAttribute("sncname")								,
										SNCOp				= lo_Elem.GetAttribute("sncop")									,
										SystemID		= lo_Elem.GetAttribute("systemid")							,
										Mode				= lo_Elem.GetAttribute("mode")									,
										Description	= lo_Elem.GetAttribute(cz_TagDesc)								};
								//................................................
								repository.Services.Add(lo_DTO.UUID, lo_DTO);
							}

						//.............................................
						// Load Message Servers
						//
						foreach (XmlElement lo_MsgSvr in XmlDoc.GetElementsByTagName("Messageserver"))
							{
								Guid lg_Key	= this.ParseGuid(lo_MsgSvr.GetAttribute(cz_TagUuid));
								if (lg_Key	!= Guid.Empty)
									{
										IDTOMsgServer lo_DTO = new DTOMsgServer
											{	UUID				= lg_Key															,
												Name				= lo_MsgSvr.GetAttribute(cz_TagName)	,
												Host				= lo_MsgSvr.GetAttribute("host")			,
												Port				= lo_MsgSvr.GetAttribute("port")			,
												Description	= lo_MsgSvr.GetAttribute(cz_TagDesc)		};

										repository.MsgServers.Add(lo_DTO.UUID, lo_DTO);
									}
							}

						//.............................................
						// Load Workspaces
						//
						foreach (XmlElement lo_WrkSpace in XmlDoc.GetElementsByTagName(cz_TagWSpace))
							{
								IDTOWorkspace lo_WSDTO = this.LoadWSAttributtes(lo_WrkSpace);
								//.........................................
								foreach (XmlElement lo_Node in lo_WrkSpace.GetElementsByTagName(cz_TagNode))
									{
										IDTOWorkspaceNode	lo_WSNode = this.LoadWSNodeAttributtes(lo_Node);
										foreach (DTOWorkspaceItem lo_WSNodeItem in this.GetItemList(repository, lo_Node, true))
											{
												lo_WSNode.Items.Add(lo_WSNodeItem.UUID, lo_WSNodeItem);
											}
										//.....................................
										if (lo_WSNode.Items.Count > 0)
											lo_WSDTO.Nodes.Add(lo_WSNode.UUID, lo_WSNode);
									}
								//.........................................
								foreach (DTOWorkspaceItem lo_WSNodeItem in this.GetItemList(repository, lo_WrkSpace, false))
									{
										lo_WSDTO.Items.Add(lo_WSNodeItem.UUID, lo_WSNodeItem);
									}
								//...........................................
								if (lo_WSDTO.Nodes.Count > 0 || lo_WSDTO.Items.Count > 0)
									repository.WorkSpaces.Add(lo_WSDTO.UUID, lo_WSDTO);
							}

						//.............................................
						this.Load_XML_Cleanup(repository);
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
				private IDTOWorkspaceNode LoadWSNodeAttributtes(XmlElement element)
					{
						IDTOWorkspaceNode lo_DTO = new DTOWorkspaceNode
							{	UUID				= Guid.Parse(element.GetAttribute(cz_TagUuid))	,
								Description	= element.GetAttribute(cz_TagName)								};
						//.............................................
						return lo_DTO;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IDTOWorkspace LoadWSAttributtes(XmlElement _xmlelement)
					{
						IDTOWorkspace lo_DTO = new DTOWorkspace
							{	UUID				= Guid.Parse(_xmlelement.GetAttribute(cz_TagUuid))	,
								Description	= _xmlelement.GetAttribute(cz_TagName)								};
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
												.Where(x => x != Guid.Empty)
													.Distinct()
														.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<Guid> UsedServices(Repository repository)
					{
						return	repository.WorkSpaces.SelectMany
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
						foreach (KeyValuePair<Guid, IDTOService> lo_Srv in repository.Services)
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
						foreach (KeyValuePair<Guid, IDTOMsgServer> lo_Msg in repository.MsgServers)
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
