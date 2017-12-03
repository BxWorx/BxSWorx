using System.Collections.Generic;
//.........................................................
using BxS_SAPGUI.API;
using BxS_SAPGUI.COM.DL;
using BxS_SAPConn.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.CNTLR
{
	internal interface IControllerSource
		{

			#region "Properties"

				IReposSAPGui Repository { get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	Save(bool forceSave = false);
				//...................................................
				IList<IDTOConnectionView>	GetConnectionViewTree();

				void	GetConnection	(IDTOConnection	dtoConnection);
				void  AddConnection(IDTOConnection dtoConnection);

			#endregion

		}
}