using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDC2RfcParser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC2RfcParser(IBDCProfile profile)
					{
						this._BDCProfile	= profile;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly IBDCProfile	_BDCProfile;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseBDCtoRFC(DTO_SessionTran BDCTranData, DTO_RFCTran RFCTranData)
					{
						RFCTranData.ProcessedStatus	= BDCTranData.ProcessedStatus	;
						RFCTranData.SuccesStatus		= BDCTranData.SuccesStatus		;
						//.................................................
						this.PutSPAData( BDCTranData.SPAData	, RFCTranData.SPAData	);
						this.PutBDCData( BDCTranData.BDCData	, RFCTranData.BDCData	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseRFCtoBDC(DTO_RFCTran RFCTranData, DTO_SessionTran BDCTranData)
					{
						BDCTranData.ProcessedStatus	= RFCTranData.ProcessedStatus	;
						BDCTranData.SuccesStatus		= RFCTranData.SuccesStatus		;
						//.................................................
						this.GetMessages(	RFCTranData.MSGData	,	BDCTranData	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void PutCTUOptions( DTO_CTUParms ctuParms , SMC.IRfcStructure ctuParmsRFC )
					{
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_DspMde	,	ctuParms.DisplayMode		);
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_UpdMde	,	ctuParms.UpdateMode			);
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_CATMde	,	ctuParms.CATTMode				);
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_DefSze	,	ctuParms.DefaultSize		);
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_NoComm	,	ctuParms.NoCommit				);
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_NoBtcI	,	ctuParms.NoBatchInpFor	);
						ctuParmsRFC.SetValue(	this._BDCProfile.CTUOpt_NoBtcE	,	ctuParms.NoBatchInpAft	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void PutSPAData(	IList<DTO_SessionTranSPA>	spaData	, SMC.IRfcTable spaDataRFC )
					{
						spaDataRFC.Append(	spaData.Count	);
						//.............................................
						for (	int i = 0; i < spaData.Count; i++	)
							{
								spaDataRFC.CurrentIndex	= i;

								spaDataRFC.SetValue( this._BDCProfile.SPADat_MID	, spaData[i].MemoryID		 );
								spaDataRFC.SetValue( this._BDCProfile.SPADat_Val	, spaData[i].MemoryValue );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void PutBDCData(	IList<DTO_SessionTranData> bdcData	, SMC.IRfcTable lt_Data	)
					{
						lt_Data.Append(	bdcData.Count	);
						//.............................................
						for (	int i = 0; i < bdcData.Count; i++	)
							{
								lt_Data.CurrentIndex	= i;

								lt_Data.SetValue(	this._BDCProfile.BDCDat_Prg	, bdcData[i].ProgramName );
								lt_Data.SetValue(	this._BDCProfile.BDCDat_Dyn	, bdcData[i].Dynpro			 );
								lt_Data.SetValue(	this._BDCProfile.BDCDat_Bgn	, bdcData[i].Begin			 );
								lt_Data.SetValue(	this._BDCProfile.BDCDat_Fld	, bdcData[i].FieldName	 );
								lt_Data.SetValue(	this._BDCProfile.BDCDat_Val	, bdcData[i].FieldValue	 );
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void GetMessages(	SMC.IRfcTable lt_Data	, DTO_SessionTran BDCTran	)
					{
						foreach (	SMC.IRfcStructure ls_Msg in lt_Data	)
							{
								BDCTran.AddMSGData(	ls_Msg.GetString(	this._BDCProfile.TabMsg_TCode	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_DynNm	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_DynNo	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgTp	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_Lang	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgID	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgNo	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgV1	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgV2	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgV3	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_MsgV4	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_Envir	)	,
																		ls_Msg.GetString(	this._BDCProfile.TabMsg_Fldnm	)		);
							}
					}

			#endregion

		}
}
