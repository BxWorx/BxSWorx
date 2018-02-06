using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	internal class BDC2RfcParser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC2RfcParser(IBDCProfile profile)
					{
						this._Profile	= profile;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly IBDCProfile	_Profile;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseFrom(IBDCTranData BDCTranData, DTO_RFCData RFCTranData)
					{
						RFCTranData.ProcessedStatus	= BDCTranData.ProcessedStatus	;
						RFCTranData.SuccesStatus		= BDCTranData.SuccesStatus		;
						RFCTranData.SAPTCode				= BDCTranData.SAPTCode				;
						RFCTranData.Skip1st					= BDCTranData.Skip1st					;
						//.................................................
						this.PutCTUOptions	(	BDCTranData.CTUOptions	, RFCTranData.CTUOpts	);
						this.PutSPAData			(	BDCTranData.SPAData			, RFCTranData.SPAData	);
						this.PutBDCData			(	BDCTranData.BDCData			, RFCTranData.BDCData	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseTo(DTO_RFCData RFCTranData, IBDCTranData BDCTranData)
					{
						this.GetMessages(	RFCTranData.MSGData	,	BDCTranData	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutCTUOptions(DTO_CTUOptions ctuOptions, SMC.IRfcStructure ls_CTUOpt)
					{
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_DspMde	,	ctuOptions.DisplayMode		);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_UpdMde	,	ctuOptions.UpdateMode			);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_CATMde	,	ctuOptions.CATTMode				);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_DefSze	,	ctuOptions.DefaultSize		);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_NoComm	,	ctuOptions.NoCommit				);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_NoBtcI	,	ctuOptions.NoBatchInpFor	);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_NoBtcE	,	ctuOptions.NoBatchInpAft	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutSPAData(IList<DTO_SPAEntry>	spaData, SMC.IRfcTable lt_Data)
					{
						lt_Data.Append(spaData.Count);
						//.............................................
						for (int i = 0; i < spaData.Count; i++)
							{
								lt_Data.CurrentIndex	= i;

								lt_Data.SetValue(	this._Profile.SPADat_MID	, spaData[i].MemoryID			);
								lt_Data.SetValue(	this._Profile.SPADat_Val	, spaData[i].MemoryValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutBDCData(IList<DTO_BDCEntry>	bdcData, SMC.IRfcTable lt_Data)
					{
						lt_Data.Append(bdcData.Count);
						//.............................................
						for (int i = 0; i < bdcData.Count; i++)
							{
								lt_Data.CurrentIndex	= i;

								lt_Data.SetValue(this._Profile.BDCDat_Prg, bdcData[i].ProgramName	);
								lt_Data.SetValue(this._Profile.BDCDat_Dyn, bdcData[i].Dynpro			);
								lt_Data.SetValue(this._Profile.BDCDat_Bgn, bdcData[i].Begin				);
								lt_Data.SetValue(this._Profile.BDCDat_Fld, bdcData[i].FieldName		);
								lt_Data.SetValue(this._Profile.BDCDat_Val, bdcData[i].FieldValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void GetMessages(SMC.IRfcTable lt_Data, IBDCTranData BDCTran)
					{
						foreach (SMC.IRfcStructure ls_Msg in lt_Data)
							{
								BDCTran.AddMSGData(	ls_Msg.GetString(	this._Profile.TabMsg_TCode)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_DynNm)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_DynNo)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgTp)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_Lang)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgID)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgNo)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgV1)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgV2)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgV3)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_MsgV4)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_Envir)	,
																		ls_Msg.GetString(	this._Profile.TabMsg_Fldnm)		);
							}
					}

			#endregion

		}
}
