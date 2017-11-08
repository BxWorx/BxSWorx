using SAPGUI.API;
using SAPGUI.USR.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR
{
	internal class USRController : IControllerSource
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public USRController(string fullPath, DLController dlCntlr)
					{
						this._DirPath			= fullPath;
						this._DLCntlr			= dlCntlr;
						//.............................................
						//this._Repository	= new XMLParse2ReposDTO().Load(fullPath, onlySAPGUI);
					}

			//===========================================================================================
			#region "Declarations"

				private readonly string					_DirPath;
				private readonly DLController		_DLCntlr;
				//.................................................
				//private readonly	XMLRepository		_Repository;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void GetConnection(IDTOConnection dtoConnection)
					{
						//this._Repository.LoadConnectionDTO(dtoConnection);
					}

			#endregion


			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
