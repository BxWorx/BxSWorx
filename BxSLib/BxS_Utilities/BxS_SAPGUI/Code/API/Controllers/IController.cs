using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public interface IController
		{
			#region "Properties"

				bool	IsReadOnly			{ get; }
				int		MsgServerCount	{ get; }
				int		ServiceCount		{ get; }
				int		WorkspaceCount	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Save();
				//...................................................




				IDTOConnection	CreateConnection(Guid connectionID = default(Guid));
				//...................................................
				IDTOConnection	GetConnection	(Guid						connectionID);
				void						GetConnection	(IDTOConnection	dtoConnection);

				void AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}