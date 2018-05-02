using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
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

				IBxSDestination		RfcDestination	{ get; }
				SMC.RfcRepository	SMCRepository		{ get; }
				int								ProfileCount		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				Task UpdateProfilesAsync( bool	optimiseMetadataFetch = true )	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Create Functions"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				BDC_Function				CreateBDCFunctionStd		();
				BDC_Function				CreateBDCFunctionAlt		();
				SAPMsg_Function			CreateSAPMsgFunction		();
				TblRdr_Function			CreateTblRdrFunction		();
				DDICInfo_Function		CreateDDICInfoFunction	();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Registration"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void	RegisterBDCStd	();
				void	RegisterBDCAlt	();

				void	RegisterSAPMsg	();
				void	RegisterTblRdr	();
				void	RegisterDDICIno	();

			#endregion

		}
}
