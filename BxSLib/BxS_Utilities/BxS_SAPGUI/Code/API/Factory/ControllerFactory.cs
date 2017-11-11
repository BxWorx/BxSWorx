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
						var	lo_Repos		= new Repository();
						var	lo_Parser		=	new XMLParse2ReposDTO();
						lo_Parser.Load(lo_Repos, fullPath, onlySAPGUI);
						//.............................................
						IControllerSource XMLCntlr	= new XMLController(lo_Repos);
						IController				Cntlr			= new Controller(XMLCntlr);
						return	Cntlr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPINI(string fullPath)
					{
						var	lo_Repos		= new Repository();
						var	lo_Parser		=	new INIParse2ReposDTO();
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
						var lo_Repos		= new Repository();
						//.............................................
						IControllerSource USRCntlr	= new USRController(lo_DLCntlr, lo_Repos);
						IController				Cntlr			= new Controller(USRCntlr, false);
						return	Cntlr;
					}

			#endregion

		}
}
