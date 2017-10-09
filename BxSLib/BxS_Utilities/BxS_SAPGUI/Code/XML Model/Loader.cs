using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	public partial class Loader
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

			//===========================================================================================
			#region "Properties"

				internal Repository													Repository	{	get { return this.co_Repos;							} }
				internal Dictionary<string, DTOMsgService>	Services		{	get { return this.co_Repos.Services;		} }
				internal Dictionary<string, DTOMsgServer>		MsgServers	{	get { return this.co_Repos.MsgServers;	}	}
				internal Dictionary<string, DTOWorkspace>		WorkSpaces	{	get { return this.co_Repos.WorkSpaces;	}	}

			#endregion

			//===========================================================================================
			#region "Constructor"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Loader(	string	XML_FullName			,
												string	XML_FromWorkspace	= ""	,
												string	XML_FromNode			= ""	,
												bool		OnlySAPGUIEntries = true	)
					{
						this.cc_XMLFullName				= XML_FullName;
						this.cc_FromWorkspace			= XML_FromWorkspace;
						this.cc_FromNode					= XML_FromNode;
						this.cb_OnlySAPGUIEntries	= OnlySAPGUIEntries;
						//..................................................
						this.co_Repos					= new Repository();
						this.ct_UnUsedSrvList	= new List<string>();
						//..................................................
						this.LoadSapGuiXML();
					}

			#endregion
		}
}
