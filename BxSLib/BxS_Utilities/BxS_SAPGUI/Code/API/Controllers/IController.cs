using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IController
		{

			#region "Properties"

				bool IsReadOnly	{ get;  }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IDTOConnection	CreateConnection(Guid connectionID = default(Guid));
				//...................................................
				IDTOConnection	GetConnection	(Guid						connectionID);
				void						GetConnection	(IDTOConnection	dtoConnection);
				//...................................................
				void Save();

				void AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}