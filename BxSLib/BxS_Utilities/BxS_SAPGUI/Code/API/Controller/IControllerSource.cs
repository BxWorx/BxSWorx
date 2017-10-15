//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IControllerSource
		{
			//...................................................
			IDTOConnection	GetConnection	(string					connectionID);
			void						GetConnection	(IDTOConnection	dtoConnection);
		}
}