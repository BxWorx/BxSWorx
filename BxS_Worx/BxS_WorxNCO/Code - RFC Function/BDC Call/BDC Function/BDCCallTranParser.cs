using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.CTU;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCCall
{
	internal class BDCCallTranParser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranParser(	BDCCallTranIndex indexer )
					{
						this._CallTranIndex	= indexer;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly BDCCallTranIndex		_CallTranIndex;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Header( DTO_SessionHeader bdcHeader , BDCCallHeader rfcHeader )
					{
						rfcHeader.SAPTCode	= bdcHeader.SAPTCode	;
						rfcHeader.Skip1st		= bdcHeader.Skip1st		;

						this.PutCTUParms( bdcHeader.CTUParms , rfcHeader.CTUParms );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseTran(DTO_SessionTran BDCTranData, BDCCallTran RFCTranData)
					{
						RFCTranData.ProcessedStatus	= BDCTranData.ProcessedStatus	;
						RFCTranData.SuccesStatus		= BDCTranData.SuccesStatus		;
						//.................................................
						this.PutSPAData( BDCTranData.SPAData	, RFCTranData.SPAData	);
						this.PutBDCData( BDCTranData.BDCData	, RFCTranData.BDCData	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseResults(BDCCallTran RFCTranData, DTO_SessionTran BDCTranData)
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
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_DspMde	,	ctuBDC.DisplayMode		);
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_UpdMde	,	ctuBDC.UpdateMode			);
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_CATMde	,	ctuBDC.CATTMode				);
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_DefSze	,	ctuBDC.DefaultSize		);
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_NoComm	,	ctuBDC.NoCommit				);
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_NoBtcI	,	ctuBDC.NoBatchInpFor	);
						ctuRFC.SetValue(	this._CallTranIndex.CTUOpt_NoBtcE	,	ctuBDC.NoBatchInpAft	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutSPAData(	IList<DTO_SessionTranSPA>	spaData	, SMC.IRfcTable spaDataRFC )
					{
						spaDataRFC.Append(	spaData.Count	);
						//.............................................
						for (	int i = 0; i < spaData.Count; i++	)
							{
								spaDataRFC.CurrentIndex	= i;

								spaDataRFC.SetValue( this._CallTranIndex.SPADat_MID	, spaData[i].MemoryID		 );
								spaDataRFC.SetValue( this._CallTranIndex.SPADat_Val	, spaData[i].MemoryValue );
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

								lt_Data.SetValue(	this._CallTranIndex.BDCDat_Prg	, bdcData[i].ProgramName );
								lt_Data.SetValue(	this._CallTranIndex.BDCDat_Dyn	, bdcData[i].Dynpro			 );
								lt_Data.SetValue(	this._CallTranIndex.BDCDat_Bgn	, bdcData[i].Begin			 );
								lt_Data.SetValue(	this._CallTranIndex.BDCDat_Fld	, bdcData[i].FieldName	 );
								lt_Data.SetValue(	this._CallTranIndex.BDCDat_Val	, bdcData[i].FieldValue	 );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void GetMessages(	SMC.IRfcTable lt_Data	, DTO_SessionTran BDCTran	)
					{
						foreach (	SMC.IRfcStructure ls_Msg in lt_Data	)
							{
								BDCTran.AddMSGData(	ls_Msg.GetString(	this._CallTranIndex.TabMsg_TCode	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_DynNm	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_DynNo	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgTp	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_Lang	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgID	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgNo	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgV1	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgV2	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgV3	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_MsgV4	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_Envir	)	,
																		ls_Msg.GetString(	this._CallTranIndex.TabMsg_Fldnm	)		);
							}
					}

			#endregion

		}
}
