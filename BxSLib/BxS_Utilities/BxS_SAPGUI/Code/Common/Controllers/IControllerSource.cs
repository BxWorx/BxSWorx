using SAPGUI.API;
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.CNTLR
{
	internal interface IControllerSource
		{

			#region "Properties"

				Datacontainer Repository { get; }

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