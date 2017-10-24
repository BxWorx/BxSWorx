using SAPGUI.XML;
using SAPGUI.INI;
using SAPGUI.USR;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public static class ControllerFactory
		{

			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPXML(string fullPath, bool onlySAPGUI)
					{
						IControllerSource XMLCntlr	= new XMLController(fullPath, onlySAPGUI);
						IController				Cntlr			= new Controller(XMLCntlr);
						return	Cntlr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPUSR(string fullPath, bool onlySAPGUI)
					{
						IControllerSource USRCntlr	= new USRController(fullPath, onlySAPGUI);
						IController				Cntlr			= new Controller(USRCntlr);
						return	Cntlr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPINI(string fullPath, bool onlySAPGUI)
					{
						IControllerSource INICntlr	= new INIController(fullPath, onlySAPGUI);
						IController				Cntlr			= new Controller(INICntlr);
						return	Cntlr;
					}

			#endregion

		}
}
