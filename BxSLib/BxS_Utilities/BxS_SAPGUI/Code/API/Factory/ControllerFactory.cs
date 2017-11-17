using SAPGUI.XML;
using SAPGUI.INI;
using SAPGUI.USR;
using SAPGUI.USR.DL;
using SAPGUI.COM.DL;
using SAPGUI.COM.CNTLR;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
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
						IController				lo_Cntlr		= new Controller(lo_XMLCntlr);
						return	lo_Cntlr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPINI(string fullPath)
					{
						IRepository lo_Repos	= CreateRepository();
						var					lo_Parser	=	new INIParse2ReposDTO();
						lo_Parser.Load(lo_Repos, fullPath);
						//.............................................
						IControllerSource INICntlr	= new INIController(lo_Repos);
						IController				Cntlr			= new Controller(INICntlr);
						return	Cntlr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPUSR(string fullPath)
					{
						var lo_Ref			= new References();
						var lo_Parser		= new Parser(lo_Ref);
						var lo_Schema		= new Schema(lo_Ref);
						var lo_DLCntlr	= new DLController(fullPath, lo_Schema, lo_Parser);
						var lo_Repos		= new DataContainer();
						//.............................................
						IControllerSource USRCntlr	= new USRController(lo_DLCntlr, lo_Repos);
						IController				Cntlr			= new Controller(USRCntlr, false);
						return	Cntlr;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static IRepository CreateRepository()
					{
						var					lo_DC			= new DataContainer();
						IRepository	lo_Repos	= new Repository(lo_DC);
						return	lo_Repos;
					}

			#endregion
		}
}
