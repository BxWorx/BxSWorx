using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IController
		{
			IDTOConnection	CreateConnection(Guid connectionID = default(Guid));
			//...................................................
			IDTOConnection	GetConnection	(Guid						connectionID);
			void						GetConnection	(IDTOConnection	dtoConnection);
			//...................................................
			void Save();
		}
}