using SAPGUI.XML;
using SAPGUI.INI;
using SAPGUI.USR;
using SAPGUI.USR.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public static class ControllerFactory
		{

			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPXML(string fullPath, bool onlySAPGUI = true)
					{
						IControllerSource XMLCntlr	= new XMLController(fullPath, onlySAPGUI);
						IController				Cntlr			= new Controller(XMLCntlr);
						return	Cntlr;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IController CreateControllerForSAPUSR(string fullPath)
					{
						var lo_Ref			= new References();
						var lo_Parser		= new Parser(lo_Ref);
						var lo_Schema		= new Schema(lo_Ref);
						var lo_DLCntlr	= new DLController(fullPath, lo_Schema, lo_Parser);
						//.............................................
						IControllerSource USRCntlr	= new USRController(fullPath, lo_DLCntlr);
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
