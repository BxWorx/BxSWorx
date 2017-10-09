using System.Xml;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Definitions"

				private	readonly string				cc_FromWorkspace;
				private	readonly string				cc_FromNode;
				private readonly bool					cb_OnlySAPGUIEntries;
				private readonly List<string>	ct_UnUsedSrvList;
				//....................................................
				private readonly Repository	co_Repos;

			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Loader(string	XML_FullName			,
												string	XML_FromWorkspace	= ""	,
												string	XML_FromNode			= ""	,
												bool		OnlySAPGUIEntries = true	)
					{
						this.cc_FromWorkspace			= XML_FromWorkspace;
						this.cc_FromNode					= XML_FromNode;
						this.cb_OnlySAPGUIEntries	= OnlySAPGUIEntries;
						//.............................................
						this.co_Repos					= new Repository();
						this.ct_UnUsedSrvList	= new List<string>();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Repository Load(string	XML_FullName,	string	XML_FromWorkspace	= "",	string	XML_FromNode = "", bool OnlySAPGUIEntries = true)
					{
						Repository	lo_Repos	= new Repository();
						//.............................................
						XmlDocument lo_XMLDoc = this.LoadXMLDoc(XML_FullName);
						lo_Repos.MsgServers		= this.Load_XML_MsgServers(lo_XMLDoc);
						lo_Repos.Services			= this.Load_XML_Services(lo_XMLDoc);


						this.Load_XML_WorkSpaces(lo_XMLDoc, XML_FromWorkspace, XML_FromNode, OnlySAPGUIEntries);
						this.Load_XML_Cleanup();

						//this.ct_UnUsedSrvList.Add(lo_DTO.UUID);


						//.............................................
						return	lo_Repos;
					}

			#endregion

		}
}
