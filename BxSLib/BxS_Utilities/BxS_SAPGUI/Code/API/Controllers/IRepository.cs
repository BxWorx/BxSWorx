using SAPGUI.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IRepository
		{

			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				bool AddUpdateMsgServer(IDTOMsgServer DTO);

			#endregion

		}
}