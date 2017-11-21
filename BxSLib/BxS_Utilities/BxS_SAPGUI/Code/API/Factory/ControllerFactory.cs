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
	public static class ControllerFactory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPXML(string fullPath, bool onlySAPGUI = true)
					{
						IRepository lo_Repos	= CreateRepository();
						var					lo_Parser	=	new XMLParse2ReposDTO();
						//.............................................
						lo_Parser.Load(lo_Repos, fullPath, onlySAPGUI);
						IControllerSource lo_XMLCntlr	= new XMLController(lo_Repos);

						return	new Controller(lo_XMLCntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPINI(string fullPath)
					{
						IRepository				lo_Repos	= CreateRepository();
						var								lo_Parser	=	new INIParse2ReposDTO();
						IControllerSource INICntlr	= new INIController(lo_Repos);
						//.............................................
						lo_Parser.Load(lo_Repos, fullPath);

						return	new Controller(INICntlr);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPUSR(string fullPath)
					{
						var								lo_IO			= new IO();
						var								lo_DCSer	= new DCSerializer();
						IRepository				lo_Repos	= CreateRepository();
						IControllerSource	USRCntlr	= new USRController(lo_Repos, fullPath, lo_IO, lo_DCSer);
						//.............................................
						return	new Controller(USRCntlr, false);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static IRepository CreateRepository()
					{
						return	new Repository(new DataContainer());
					}

			#endregion
		}
}
