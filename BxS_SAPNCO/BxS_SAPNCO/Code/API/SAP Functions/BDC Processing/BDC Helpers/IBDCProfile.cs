using SMC	= SAP.Middleware.Connector;
//.........................................................
using	BxS_SAPNCO.API.Function;
using BxS_SAPNCO.API.SAPFunctions.BDC.Session;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	internal interface IBDCProfile : IRfcFncProfile
		{
			#region "Properties:  General"

				bool	Ready	{ get; set; }

				SMC.IRfcFunction	RFCFnc	{	get; }
				SMC.IRfcStructure	CTUStr	{	get; }
				SMC.IRfcTable			BDCTbl	{	get; }
				SMC.IRfcTable			SPATbl	{	get; }
				SMC.IRfcTable			MSGTbl	{	get; }

			#endregion
			//...................................................
			#region "Properties:  Indicies"

				#region "Function Parameters"

					int ParIdx_TCode	{ get; set;	}
					int ParIdx_Skip1	{ get; set;	}
					int ParIdx_CTUOpt	{ get; set;	}
					int ParIdx_TabBDC	{ get; set;	}
					int ParIdx_TabSPA	{ get; set;	}
					int	ParIdx_TabMSG	{ get; set;	}

				#endregion
				//.................................................
				#region "CTUOptions"

					int CTUOpt_DspMde	{ get; set;	}
					int CTUOpt_UpdMde	{ get; set;	}
					int CTUOpt_CATMde	{ get; set;	}
					int CTUOpt_DefSze	{ get; set;	}
					int CTUOpt_NoComm	{ get; set;	}
					int CTUOpt_NoBtcI	{ get; set;	}
					int CTUOpt_NoBtcE	{ get; set;	}

				#endregion
				//.................................................
				#region "SPA Data"

					int SPADat_MID	{ get; set;	}
					int SPADat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "BDC Data"

					int BDCDat_Prg	{ get; set;	}
					int BDCDat_Dyn	{ get; set;	}
					int BDCDat_Bgn	{ get; set;	}
					int BDCDat_Fld	{ get; set;	}
					int BDCDat_Val	{ get; set;	}

				#endregion
				//.................................................
				#region "Messages"

					int TabMsg_TCode	{ get; set;	}
					int TabMsg_DynNm	{ get; set;	}
					int TabMsg_DynNo	{ get; set;	}
					int TabMsg_MsgTp	{ get; set;	}
					int TabMsg_Lang		{ get; set;	}
					int TabMsg_MsgID	{ get; set;	}
					int TabMsg_MsgNo	{ get; set;	}
					int TabMsg_MsgV1	{ get; set;	}
					int TabMsg_MsgV2	{ get; set;	}
					int TabMsg_MsgV3	{ get; set;	}
					int TabMsg_MsgV4	{ get; set;	}
					int TabMsg_Envir	{ get; set;	}
					int TabMsg_Fldnm	{ get; set;	}

				#endregion

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Configure(DTO_RFCSessionHeader	DTO);
				void Configure(DTO_RFCTran		DTO);

			#endregion

		}
}
