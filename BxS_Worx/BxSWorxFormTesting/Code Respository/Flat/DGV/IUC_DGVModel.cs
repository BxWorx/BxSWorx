using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal interface IUC_DGVModel
		{
			#region "Methods: Exposed"

				IDTO_SessionRequest	CreateRequest();
				IList<IDTO_Session>	FetchData( IDTO_SessionRequest Request );

			#endregion

		}
}
