using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API.Destination;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal interface IRfcFncController
		{
			#region "Properties"

				IRfcDestination	RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Call Transaction"

				//.................................................
				BDCCall_Function	CreateBDCCallFunction	();
				//.................................................

			#endregion

		}
}
