using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranParser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranParser(BDCCallTranIndex indexer)
					{
						this._BDCProfile	= indexer;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly BDCCallTranIndex	_BDCProfile;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Header( DTO_SessionHeader bdcHeader , DTO_RFCHeader rfcHeader )
					{
						rfcHeader.SAPTCode	= bdcHeader.SAPTCode	;
						rfcHeader.Skip1st		= bdcHeader.Skip1st		;

						this.PutCTUParms( bdcHeader.CTUParms , rfcHeader.CTUParms );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseTran(DTO_SessionTran BDCTranData, DTO_RFCTran RFCTranData)
					{
						RFCTranData.ProcessedStatus	= BDCTranData.ProcessedStatus	;
						RFCTranData.SuccesStatus		= BDCTranData.SuccesStatus		;
						//.................................................
						this.PutSPAData( BDCTranData.SPAData	, RFCTranData.SPAData	);
						this.PutBDCData( BDCTranData.BDCData	, RFCTranData.BDCData	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseResults(DTO_RFCTran RFCTranData, DTO_SessionTran BDCTranData)
					{
						BDCTranData.ProcessedStatus	= RFCTranData.ProcessedStatus	;
						BDCTranData.SuccesStatus		= RFCTranData.SuccesStatus		;
						//.................................................
						this.GetMessages(	RFCTranData.MSGData	,	BDCTranData	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutCTUParms( DTO_CTUParms ctuBDC , SMC.IRfcStructure ctuRFC )
					{
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_DspMde	,	ctuBDC.DisplayMode		);
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_UpdMde	,	ctuBDC.UpdateMode			);
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_CATMde	,	ctuBDC.CATTMode				);
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_DefSze	,	ctuBDC.DefaultSize		);
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_NoComm	,	ctuBDC.NoCommit				);
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_NoBtcI	,	ctuBDC.NoBatchInpFor	);
						ctuRFC.SetValue(	this._BDCProfile.CTUOpt_NoBtcE	,	ctuBDC.NoBatchInpAft	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutSPAData(	IList<DTO_SessionTranSPA>	spaData	, SMC.IRfcTable spaDataRFC )
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
				private void PutBDCData(	IList<DTO_SessionTranData> bdcData	, SMC.IRfcTable lt_Data	)
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
