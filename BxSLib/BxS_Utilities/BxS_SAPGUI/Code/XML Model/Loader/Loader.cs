using System.Xml;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Definitions"

				private readonly List<string>	ct_UnUsedSrvList;
				//....................................................
				//private readonly Repository	co_Repos;

			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Loader()
					{
						this.ct_UnUsedSrvList	= new List<string>();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository Load(	string	fullName							,
																	string	fromWorkspace	= ""		,
																	string	fromNode			= ""		,
																	bool		OnlySAPGUI		= true		)
					{
						Repository	lo_Repos	= new Repository();
						XmlDocument lo_XMLDoc = this.LoadXMLDoc(fullName);
						//.............................................
						if (lo_XMLDoc.InnerXml != string.Empty)
							{
								lo_Repos.MsgServers	= this.Load_MsgServers(lo_XMLDoc);
								lo_Repos.Services		= this.Load_Services(lo_XMLDoc, OnlySAPGUI);
								lo_Repos.WorkSpaces	= this.Load_WorkSpaces(	lo_XMLDoc											,
																																fromWorkspace ?? string.Empty	,
																																fromNode			?? string.Empty		);
								//.............................................
								//this.Load_XML_Cleanup();
								//this.ct_UnUsedSrvList.Add(lo_DTO.UUID);
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
						//..................................................
						try
								{
									lo_XMLDoc.Load(fullName);
								}
							catch (System.SystemException)
								{
								}
						//..................................................
						return lo_XMLDoc;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load_XML_Cleanup(Repository repository)
					{
						foreach (string lc in this.ct_UnUsedSrvList)
							{
								//this.co_Repos.Services.Remove(lc);
							}
						//...............................................
						this.ct_UnUsedSrvList.Clear();
					}

			#endregion

		}
}
