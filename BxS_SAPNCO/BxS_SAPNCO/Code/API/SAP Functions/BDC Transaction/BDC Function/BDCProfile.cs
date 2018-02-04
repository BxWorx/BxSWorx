using SMC	= SAP.Middleware.Connector;
//.........................................................
using	BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	internal class BDCFncProfile : RfcFncProfileBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCFncProfile( string functionName) : base(functionName)
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties:  Indicies"

				SMC.RfcStructureMetadata	BDCData_StrMetdata;

			#endregion

			//===========================================================================================
			#region "Properties:  Indicies"

				#region "Function Parameters"

					internal	int ParIdx_TCode	{ get; }
					internal	int ParIdx_Skip1	{ get; }
					internal	int ParIdx_BDCDat	{ get; }
					internal	int ParIdx_CTUOpt	{ get; }
					internal	int	ParIdx_TabMsg	{ get; }
					internal	int ParIdx_TabSPA	{ get; }

				#endregion
				//.................................................
				#region "CTUOptions"

					internal	int CTUOpt_DspMde	{ get; }
					internal	int CTUOpt_UpdMde	{ get; }
					internal	int CTUOpt_CATMde	{ get; }
					internal	int CTUOpt_DefSze	{ get; }
					internal	int CTUOpt_NoComm	{ get; }
					internal	int CTUOpt_NoBtcI	{ get; }
					internal	int CTUOpt_NoBtcE	{ get; }

				#endregion
				//.................................................
				#region "SPA Data"

					internal	int SPADat_MID	{ get; }
					internal	int SPADat_Val	{ get; }

				#endregion
				//.................................................
				#region "BDC Data"

					internal	int BDCDat_Prg	{ get; }
					internal	int BDCDat_Dyn	{ get; }
					internal	int BDCDat_Bgn	{ get; }
					internal	int BDCDat_Fld	{ get; }
					internal	int BDCDat_Val	{ get; }

				#endregion
				//.................................................
				#region "Messages"

					internal	int TabMsg_TCode	{ get; }
					internal	int TabMsg_DynNm	{ get; }
					internal	int TabMsg_DynNo	{ get; }
					internal	int TabMsg_MsgTp	{ get; }
					internal	int TabMsg_Lang		{ get; }
					internal	int TabMsg_MsgID	{ get; }
					internal	int TabMsg_MsgNo	{ get; }
					internal	int TabMsg_MsgV1	{ get; }
					internal	int TabMsg_MsgV2	{ get; }
					internal	int TabMsg_MsgV3	{ get; }
					internal	int TabMsg_MsgV4	{ get; }
					internal	int TabMsg_Envir	{ get; }
					internal	int TabMsg_Fldnm	{ get; }

				#endregion

			#endregion

		}
}
