using BxS_SAPGUI.API;
using BxS_SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.CNTLR
{
	internal interface IControllerSource
		{

			#region "Properties"

				IRepository Repository { get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	Save(bool forceSave = false);
				//...................................................
				void	GetConnection	(IDTOConnection	dtoConnection);
				void  AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}