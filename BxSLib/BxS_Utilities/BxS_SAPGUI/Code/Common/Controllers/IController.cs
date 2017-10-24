//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IController
		{
			IDTOConnection	CreateConnection(string connectionID = "");
			//...................................................
			IDTOConnection	GetConnection	(string					connectionID);
			void						GetConnection	(IDTOConnection	dtoConnection);
		}
}