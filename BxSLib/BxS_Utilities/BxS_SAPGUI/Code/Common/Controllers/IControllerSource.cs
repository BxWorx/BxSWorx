using BxS_SAPGUI.API;
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

				void	GetConnection	(IDTOConnection	dtoConnection);
				//...................................................
				void	Save(bool forceSave = false);
				void  AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}