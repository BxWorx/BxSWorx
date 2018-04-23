using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				IBDCRequestManager	Create_BDCRequestManager();

			#endregion

		}
}