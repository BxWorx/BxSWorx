using SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.COM.CNTLR
{
	internal interface IControllerSource
		{
			void	GetConnection	(IDTOConnection	dtoConnection);
			//...................................................
			void	Save();
		  void  AddConnection(IDTOConnection dtoConnection);
		}
}