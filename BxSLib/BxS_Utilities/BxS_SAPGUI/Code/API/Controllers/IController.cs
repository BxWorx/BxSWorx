using System;
using System.Collections.Generic;
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
				//.................................................
				IList<IDTOConnectionView>	GetConnectionViewTree();
				//...................................................
				IDTOConnection	GetConnection		(Guid						id);
				void						LoadConnection	(IDTOConnection	dto);
				//...................................................







				IDTOConnection	CreateConnection(Guid connectionID = default(Guid));

				void AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}