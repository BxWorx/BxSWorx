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
				public IFavourites CreateControllerForFavourites(string fullPathName)
					{
						return	new Favourites();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPXML(string fullPathName, bool onlySAPGUI = true)
					{
						IReposSAPGui				lo_Repos		= CreateRepository();
						var								lo_Parser		=	new XMLParse2ReposDTO();
						IControllerSource lo_XMLCntlr	= new XMLController(lo_Repos);
						//.............................................
						lo_Parser.Load(lo_Repos, fullPathName, onlySAPGUI);
						//.............................................
						return	new Controller(lo_XMLCntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPINI(string fullPathName)
					{
						IReposSAPGui				lo_Repos	= CreateRepository();
						var								lo_IO			= new IO();
						var								lo_Parser	=	new INIParse2ReposDTO(lo_IO, lo_Repos, fullPathName);
						IControllerSource INICntlr	= new INIController(lo_Repos);
						//.............................................
						lo_Parser.Load();
						//.............................................
						return	new Controller(INICntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IController CreateControllerForSAPUSR(string fullPathName, bool autoLoad = true)
					{
						var								lo_IO			= new IO();
						var								lo_DCSer	= new DCSerializer();
						IReposSAPGui				lo_Repos	= CreateRepository();
						IControllerSource	USRCntlr	= new USRController(lo_Repos, fullPathName, lo_IO, lo_DCSer, autoLoad);
						//.............................................
						return	new Controller(USRCntlr, false);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IReposSAPGui CreateRepository()
					{
						return	new ReposSAPGui(new DCSapGui());
					}

			#endregion
		}
}
