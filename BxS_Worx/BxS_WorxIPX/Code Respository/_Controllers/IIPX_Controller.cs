using BxS_WorxIPX.BDC;
using BxS_WorxIPX.NCO;

using BxS_WorxUtil.General;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Properties"

				IO					IO					{ get; }
				Serializer	Serializer	{ get; }
				//.................................................
				INCOx_Controller	NCOx_Controller	{ get; }
				IBDCx_Controller	BDCx_Controller	{ get; }

			#endregion

		}
}