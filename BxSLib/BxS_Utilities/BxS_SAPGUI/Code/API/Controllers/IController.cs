using System;
using System.Collections.Generic;
//.........................................................
using BxS_SAPConn.API;
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
				int		NodeCount				{ get; }
				int		ItemCount				{ get; }

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