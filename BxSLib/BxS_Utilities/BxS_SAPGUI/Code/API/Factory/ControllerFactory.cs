using BxS_SAPGUI.XML;
using BxS_SAPGUI.INI;
using BxS_SAPGUI.USR;
using BxS_SAPGUI.COM.DL;
using BxS_SAPGUI.COM.CNTLR;
using BxS_Toolset.Serialize;
using BxS_Toolset.IO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public class ControllerFactory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPXML(string fullPathName, bool onlySAPGUI = true)
					{
						IReposSAPGui			lo_Repos		= new ReposSAPGui(new DCSapGui());
						IControllerSource lo_XMLCntlr	= new XMLController(lo_Repos);
						//.............................................
						var	lo_Parser		=	new XMLParse2ReposDTO();
						lo_Parser.Load(lo_Repos, fullPathName, onlySAPGUI);
						//.............................................
						return	new Controller(lo_XMLCntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPINI(string fullPathNameINI, string fullPathNameLNK)
					{
						IReposSAPGui			lo_Repos	= new ReposSAPGui(new DCSapGui());
						var								lo_IO			= new IO();
						var								lo_Ser		= new ObjSerializer();
						IControllerSource INICntlr	= new INIController(lo_Repos);
						//.............................................
						var	lo_Parser	=	new INIParse2ReposDTO(lo_IO, lo_Ser, lo_Repos, fullPathNameINI, fullPathNameLNK);
						lo_Parser.Load();
						//.............................................
						return	new Controller(INICntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPUSR(string fullPathName, bool autoLoad = true)
					{
						var								lo_IO			= new IO();
						var								lo_DCSer	= new ObjSerializer();
						IReposSAPGui			lo_Repos	= new ReposSAPGui(new DCSapGui());
						IControllerSource	USRCntlr	= new USRController(lo_Repos, fullPathName, lo_IO, lo_DCSer, autoLoad);
						//.............................................
						return	new Controller(USRCntlr, false);
					}

			#endregion

		}
}
