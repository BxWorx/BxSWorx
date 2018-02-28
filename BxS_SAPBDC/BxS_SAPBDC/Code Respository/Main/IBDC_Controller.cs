using BxS_SAPBDC.BDC;
using BxS_SAPBDC.Parser;
using BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Main
{
	public interface IBDC_Controller
		{
			#region "Methods: Exposed"

				BDC_Parser CreateBDCParser();
				//.................................................
				BDC_Session		ProcessRequest( DTO_BDCSessionRequest DTORequest );

			#endregion

		}
}