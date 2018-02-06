using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCTranProcessor : IBDCTranProcessor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTranProcessor(	IRFCFunction		RfcFunction		,
																		IBDCProfile			RfcFncProfile		)
					{
						this._RFCFunc	= RfcFunction		;
						this._Profile	= RfcFncProfile	;
						//.............................................
						this._FncCreated	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRFCFunction		_RFCFunc	;
				private readonly	IBDCProfile			_Profile	;

				private	bool	_FncCreated	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	RFCFunctionName	{ get	{ return	this._Profile.FunctionName; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Process(IBDCTranData BDCTran)
					{
						try
							{
								if (!this._FncCreated)
									{
										this._RFCFunc.RfcFunction		= this._Profile.RFCFnc;
										this._FncCreated						= !this._FncCreated;
									}
								//.........................................
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TCode,	BDCTran.SAPTCode	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_Skip1, BDCTran.Skip1st		)	;

								this.PutCTUOptions(BDCTran.CTUOptions);
								this.PutBDCData(BDCTran.BDCData);
								this.PutSPAData(BDCTran.SPAData);
								//.........................................
								BDCTran.SuccesStatus	=	this._RFCFunc.Invoke(this._Profile.RfcDestination);
								//.........................................
								this.GetMessages(BDCTran);
							}
						catch (System.Exception)
							{
								BDCTran.SuccesStatus	= false;
							}
						finally
							{
								BDCTran.ProcessedStatus	= true;
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutCTUOptions(DTO_CTUOptions ctuOptions)
					{
						SMC.IRfcStructure	ls_CTUOpt	= this._Profile.CTUStr;
						//.............................................
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_DspMde	,	ctuOptions.DisplayMode		);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_UpdMde	,	ctuOptions.UpdateMode			);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_CATMde	,	ctuOptions.CATTMode				);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_DefSze	,	ctuOptions.DefaultSize		);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_NoComm	,	ctuOptions.NoCommit				);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_NoBtcI	,	ctuOptions.NoBatchInpFor	);
						ls_CTUOpt.SetValue(	this._Profile.CTUOpt_NoBtcE	,	ctuOptions.NoBatchInpAft	);
						//.............................................
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_CTUOpt	, ls_CTUOpt	)	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutSPAData(IList<DTO_SPAEntry>	spaData)
					{
						SMC.IRfcTable	lt_Data	= this._Profile.SPATbl;

						lt_Data.Append(spaData.Count);
						//.............................................
						for (int i = 0; i < spaData.Count; i++)
							{
								lt_Data.CurrentIndex	= i;

								lt_Data.SetValue(	this._Profile.SPADat_MID	, spaData[i].MemoryID			);
								lt_Data.SetValue(	this._Profile.SPADat_Val	, spaData[i].MemoryValue	);
							}
						//.............................................
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabSPA	, lt_Data );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutBDCData(IList<DTO_BDCEntry>	bdcData)
					{
						SMC.IRfcTable	lt_Data	= this._Profile.BDCTbl;

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
						//.............................................
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabBDC	, lt_Data );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void GetMessages(IBDCTranData BDCTran)
					{
						foreach (SMC.IRfcStructure ls_Msg in this._RFCFunc.RfcFunction.GetTable(this._Profile.ParIdx_TabMsg))
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
