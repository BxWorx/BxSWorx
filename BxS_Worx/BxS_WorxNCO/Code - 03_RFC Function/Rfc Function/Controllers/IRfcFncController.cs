using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.SAPMsg;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncController
		{
			#region "Properties"

				IRfcDestination	RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Call Transaction"

				void	RegisterBDCCallProfile( bool loadMetaData = false );
				//.................................................
				BDCCall_Profile		GetAddBDCCallProfile	();
				BDCCall_Function	CreateBDCCallFunction	();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: SAP Message compiler"

				void	RegisterSAPMsgProfile( bool loadMetaData = false );
				//.................................................
				SAPMsg_Profile		GetAddSAPMsgProfile		();
				//SAPMsg_Function		CreateSAPMsgFunction	();

			#endregion

		}
}
