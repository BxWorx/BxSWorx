using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Destination.API;

using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.DDIC;
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

				//void	RegisterBDCProfile( bool TranVersion = false );
				////.................................................
				//BDC_Profile		GetAddBDCProfile	( bool TranVersion = false )	;
				BDC_Function	CreateBDCFunction	( bool UseAltVersion = false )	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: SAP Message compiler"

				//void	RegisterSAPMsgProfile();
				////.................................................
				//SAPMsg_Profile		GetAddSAPMsgProfile()		;
				SAPMsg_Function		CreateSAPMsgFunction()	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Table Reader"

				//void	RegisterTableReaderProfile();
				////.................................................
				//TblRdr_Profile		GetAddTblRdrProfile()	;
				TblRdr_Function		CreateTblRdrFunction()	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: DDIC Info"

				DDICInfo_Function		CreateDDICInfoFunction()	;

			#endregion

		}
}
