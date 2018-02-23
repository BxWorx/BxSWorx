//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal interface IBDCTranProcessor
		{
			//===========================================================================================
			#region "Methods: Exposed"

				void	Config	( DTO_RFCHeader	Config			);
				void	Process	( DTO_RFCTran		Transaction	);

			#endregion

		}
}
