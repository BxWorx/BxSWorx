using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPXController
		{
			#region "Methods: Exposed"

				IO					CreateIO();
				Serializer	CreateSerializer();
				//.................................................
				IExcelBDCSessionRequest	CreateBDCSessionRequest	();
				IExcelBDCSessionResult		CreateBDCSessionResult	();

			#endregion

		}
}