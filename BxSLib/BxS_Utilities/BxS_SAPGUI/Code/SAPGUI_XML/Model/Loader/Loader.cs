using System.Xml;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{

			#region "Declarations"

				private XmlDocument		_XmlDoc;
				private DTORepository	_Repos;

		#endregion

		//===========================================================================================
		#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTORepository Load(string	fullName,
																		string	fromWorkspace	= ""	,
																		string	fromNode			= ""	,
																		bool		onlySAPGUI		= true	)
					{
						this._Repos	= this.Create();
						//.............................................
						this._XmlDoc	= this.LoadXMLDoc(fullName);
						if (this._XmlDoc != null)
							{
								this.Load_Services(onlySAPGUI);
								this.Load_MsgServers();
								//.............................................
								this.Load_WorkSpaces(	fromWorkspace ?? string.Empty	,
																			fromNode			?? string.Empty		);
								//.............................................
								this.Load_XML_Cleanup();
							}
						//.............................................
						return	this._Repos;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTORepository Create()
					{
						return	new DTORepository();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private XmlDocument LoadXMLDoc(string fullname)
					{
						XmlDocument lo_XMLDoc = new XmlDocument();
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
				private IList<string> UsedMsgServers()
					{
						IList<string>	lt_List	= new List<string>();
						//.............................................
						foreach (var lo_Srv in this._Repos.Services)
							{
								if (!lt_List.Contains(lo_Srv.Value.MSID))
									lt_List.Add(lo_Srv.Value.MSID);
							}
						//.............................................
						return lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<string> UsedServices()
					{
						IList<string>	lt_List	= new List<string>();
						//.............................................
						foreach (var lo_WS in this._Repos.WorkSpaces)
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

			#endregion

			//===========================================================================================
			#region "Methods: Private: Housekeeping"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Remove unwanted Services and Message Servers
				//
				private void Load_XML_Cleanup()
					{
						IList<string>	lt_Use;
						IList<string>	lt_Rem	= new List<string>();
						//.............................................
						// Cleanup Services
						//
						lt_Use	= this.UsedServices();
						//.............................................
						foreach (var lo_Srv in this._Repos.Services)
							{
								if (!lt_Use.Contains(lo_Srv.Key))
									lt_Rem.Add(lo_Srv.Key);
							}
						//.............................................
						foreach (var lo_Rem in lt_Rem)
							{
								this._Repos.Services.Remove(lo_Rem);
							}

						//.............................................
						// Cleanup Message Servers from cleaned Services
						//
						lt_Use	= this.UsedMsgServers();
						lt_Rem.Clear();
						//.............................................
						foreach (var lo_Msg in this._Repos.MsgServers)
							{
								if (!lt_Use.Contains(lo_Msg.Key))
									lt_Rem.Add(lo_Msg.Key);
							}
						//.............................................
						foreach (var lo_Rem in lt_Rem)
							{
								this._Repos.MsgServers.Remove(lo_Rem);
							}
					}

			#endregion

		}
}
