using SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.CNTLR
{
	internal interface IControllerSource
		{

			#region "Properties"

				IRepository Repository { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	GetConnection	(IDTOConnection	dtoConnection);
				//...................................................
				void	Save();
				void  AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}