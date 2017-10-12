using System.Xml;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTORepository Load(	string	fullName							,
																			string	fromWorkspace	= ""		,
																			string	fromNode			= ""		,
																			bool		OnlySAPGUI		= true		)
					{
						DTORepository	lo_Repos	= new DTORepository();
						XmlDocument lo_XMLDoc		= this.LoadXMLDoc(fullName);
						//.............................................
						if (lo_XMLDoc != null)
							{
								lo_Repos.Services		= this.Load_Services(lo_XMLDoc, OnlySAPGUI);
								lo_Repos.MsgServers	= this.Load_MsgServers(lo_XMLDoc);
								lo_Repos.WorkSpaces	= this.Load_WorkSpaces(	lo_XMLDoc											,
																														fromWorkspace ?? string.Empty	,
																														fromNode			?? string.Empty ,
																														lo_Repos.Services								);
								//.............................................
								this.Load_XML_Cleanup(lo_Repos);
							}
						//.............................................
						return	lo_Repos;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private: XML: Header Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlDocument LoadXMLDoc(string	fullName)
					{
						XmlDocument lo_XMLDoc = new XmlDocument();
						//.............................................
						try
								{
									lo_XMLDoc.Load(fullName);
								}
							catch (System.SystemException)
								{
									return null;
								}
						//.............................................
						return lo_XMLDoc;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> UsedMsgServers(DTORepository repository)
					{
						IList<string>	lt_List	= new List<string>();
						//.............................................
						foreach (var lo_Srv in repository.Services)
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
						foreach (var lo_WS in repository.WorkSpaces)
							{
								foreach (var lo_Node in lo_WS.Value.Nodes)
									{
										foreach (var lo_Item in	lo_Node.Value.Items)
											{
												if (!lt_List.Contains(lo_Item.Value.ServiceID))
													lt_List.Add(lo_Item.Value.ServiceID);
											}
									}
								foreach (var lo_Item in lo_WS.Value.Items)
									{
										if (!lt_List.Contains(lo_Item.Value.ServiceID))
											lt_List.Add(lo_Item.Value.ServiceID);
									}
							}
						//.............................................
						return lt_List;
					}

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
						foreach (var lo_Srv in repository.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (var lo_Rem in lt_Rem)
							{
								repository.Services.Remove(lo_Rem);
							}

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers(repository);
						lt_Rem.Clear();
						//.............................................
						foreach (var lo_Msg in repository.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (var lo_Rem in lt_Rem)
							{
								repository.MsgServers.Remove(lo_Rem);
							}
					}

			#endregion

		}
}
