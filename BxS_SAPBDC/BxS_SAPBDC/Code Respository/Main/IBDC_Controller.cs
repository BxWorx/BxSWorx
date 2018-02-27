using BxS_SAPBDC.Parser;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Main
{
	public interface IBDC_Controller
		{
			#region "Methods: Exposed"

				BDC_Processor CreateBDCProcessor();

			#endregion

		}
}