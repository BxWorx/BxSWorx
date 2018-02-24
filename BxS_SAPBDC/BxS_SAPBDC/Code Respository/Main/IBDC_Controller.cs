using BxS_SAPIPX.Excel;
using BxS_SAPBDC.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Main
{
	public interface IBDC_Controller
		{
			#region "Methods: Exposed"

				BDC_Session	Process( DTO_BDCSessionRequest BDCRequest );

			#endregion

		}
}