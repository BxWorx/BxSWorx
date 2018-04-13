using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Destination.API;

using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.SAPMsg;
using BxS_WorxNCO.RfcFunction.TableReader;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcFncController
		{
			#region "Properties"

				IRfcDestination	RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				Task ActivateProfilesAsync()	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Call Transaction"

				void	RegisterBDCProfile( bool TranVersion = false );
				//.................................................
				BDCCall_Profile		GetAddBDCCallProfile()	;
				BDCCall_Function	CreateBDCCallFunction()	;
				//.................................................
				BDCTran_Profile		GetAddBDCTranProfile()	;
				BDCTran_Function	CreateBDCTranFunction()	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: SAP Message compiler"

				void	RegisterSAPMsgProfile();
				//.................................................
				SAPMsg_Profile		GetAddSAPMsgProfile()		;
				SAPMsg_Function		CreateSAPMsgFunction()	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Table Reader"

				void	RegisterTableReaderProfile();
				//.................................................
				TblRdr_Profile		GetAddTblRdrProfile()	;
				TblRdr_Function		CreateTblRdrFunction()	;

			#endregion
		}
}
