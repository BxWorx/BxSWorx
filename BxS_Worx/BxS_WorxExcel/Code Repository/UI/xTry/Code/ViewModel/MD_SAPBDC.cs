using BxS_WorxExcel.MVVM;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class MD_SAPBDC
		{
			#region "Declarations"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MD_SAPBDC( IBDC_Controller	ipxBDCCntlr )
					{
						this._IPXBDCCntlr	= ipxBDCCntlr;
						this.Request			= this._IPXBDCCntlr.CreateSAPSessionListRequest();
					}

			#endregion

				private	IBDC_Controller				_IPXBDCCntlr;

				public	DTO_SAPSessionRequest	Request { get; }

		}
}
