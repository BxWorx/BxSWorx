using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCCallTransaction : IBDCCallTransaction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTransaction(	IRFCFunction		RfcFunction		,
																			IBDCProfile			RfcFncProfile	,
																			DTO_CTUParams		dto_CTUParm		,
																			DTO_BDCData			dto_BDCData		,
																			DTO_SPAData			dto_SPAData		,
																			DTO_MsgData			dto_MsgData			)
					{
						this._RFCFunc	= RfcFunction		;
						this._Profile	= RfcFncProfile	;
						//.............................................
						this.CTUParm	= dto_CTUParm		;
						this.BDCData	= dto_BDCData		;
						this.SPAData	= dto_SPAData		;
						this.MsgData	= dto_MsgData		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRFCFunction		_RFCFunc	;
				private readonly	IBDCProfile			_Profile	;

				private	SMC.IRfcStructure		_CTUOpt	;
				private	SMC.IRfcTable				_BDCDat	;
				private	SMC.IRfcTable				_SPADat	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	SAPTransaction	{ get;	set; }
				public	bool		SkipFirstScreen	{ get;	set; }

				public	DTO_CTUParams		CTUParm	{ get; }
				public	DTO_BDCData			BDCData	{ get; }
				public	DTO_SPAData			SPAData	{ get; }
				public	DTO_MsgData			MsgData	{ get; }

				public	string	RFCFunctionName	{ get	{ return	this._Profile.FunctionName; } }
				public	int			BDCDataCount		{ get	{ return	this.BDCData.Count	;	} }
				public	int			SPADataCount		{ get	{ return	this.SPAData.Count	;	} }
				public	int			MsgDataCount		{ get	{ return	this.MsgData.Count	;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke()
					{
						bool	lb_Ret	= true;
						//.............................................
						try
							{
								this.Prologue();
								lb_Ret	=	this._RFCFunc.Invoke(this._Profile.RfcDestination);
								this.Epilogue();
							}
						catch (System.Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SPAEntry CreateSPAEntry(	string	MemoryID		,
																						string	MemoryValue	,
																						bool		autoAdd			= true )
					{
						var	lo_Entry	= new DTO_SPAEntry(	MemoryID, MemoryValue );

						if (autoAdd)	this.SPAData.Add(lo_Entry);
						//.............................................
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddSPAEntry(DTO_SPAEntry entry)
					{
						this.SPAData.Add(entry);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDCEntry CreateBDCEntry(	string	programName	= BDCConstants.lz_E	,
																						int			dynpro			= 0									,
																						bool		begin				= false							,
																						string	field				= BDCConstants.lz_E	,
																						string	value				= BDCConstants.lz_E	,
																						bool		autoAdd			= true								)
					{
						var	lo_Entry	= new DTO_BDCEntry(	programName, dynpro, begin, field, value );

						if (autoAdd)	this.BDCData.Add(lo_Entry);
						//.............................................
						return	lo_Entry;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void AddBDCEntry(DTO_BDCEntry entry)
					{
						this.BDCData.Add(entry);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this.BDCData.Reset();
						this.SPAData.Reset();
						this.MsgData.Reset();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Prologue()
					{
						this.MsgData.Reset();
						//.............................................
						if (!this._Profile.Ready)
							{
								var lo_BDCProfiler	= new BDCProfileConfigurator();
								lo_BDCProfiler.Configure( this._Profile );
							}
						//.............................................
						this._RFCFunc.RfcFunction		= this._Profile.RFCFunction;

						this._CTUOpt	= this._Profile.CTUStructure;
						this._BDCDat	= this._Profile.BDCTable;
						this._SPADat	= this._Profile.SPATable;
						//.............................................
						this.PutCTUOptions();
						this.PutBDCData();
						this.PutSPAData();
						//.............................................
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TCode	, this.SAPTransaction								)	;
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_Skip1	, this.SkipFirstScreen	? "X" : " "	)	;
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabBDC	, this._BDCDat											)	;
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_CTUOpt	, this._CTUOpt											)	;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Epilogue()
					{
						this.GetMessages();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutSPAData()
					{
						this._SPADat.Clear();
						this._SPADat.Append(this.SPAData.Count);
						//.............................................
						for (int i = 0; i < this.SPAData.Count; i++)
							{
								this._SPADat.CurrentIndex	= i;

								this._BDCDat.SetValue(	this._Profile.SPADat_MID	, this.SPAData.Data[i].MemoryID			);
								this._BDCDat.SetValue(	this._Profile.SPADat_Val	, this.SPAData.Data[i].MemoryValue	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutBDCData()
					{
						this._BDCDat.Clear();
						this._BDCDat.Append(this.BDCData.Count);
						//.............................................
						for (int i = 0; i < this.BDCData.Count; i++)
							{
								this._BDCDat.CurrentIndex	= i;

								this._BDCDat.SetValue(this._Profile.BDCDat_Prg, this.BDCData.Data[i].ProgramName	);
								this._BDCDat.SetValue(this._Profile.BDCDat_Dyn, this.BDCData.Data[i].Dynpro				);
								this._BDCDat.SetValue(this._Profile.BDCDat_Bgn, this.BDCData.Data[i].Begin				);
								this._BDCDat.SetValue(this._Profile.BDCDat_Fld, this.BDCData.Data[i].FieldName		);
								this._BDCDat.SetValue(this._Profile.BDCDat_Val, this.BDCData.Data[i].FieldValue		);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PutCTUOptions()
					{
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_DspMde	,	this.CTUParm.DisplayMode		);
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_UpdMde	,	this.CTUParm.UpdateMode			);
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_CATMde	,	this.CTUParm.CATTMode				);
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_DefSze	,	this.CTUParm.DefaultSize		);
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_NoComm	,	this.CTUParm.NoCommit				);
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_NoBtcI	,	this.CTUParm.NoBatchInpFor	);
						this._CTUOpt.SetValue(	this._Profile.CTUOpt_NoBtcE	,	this.CTUParm.NoBatchInpAft	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void GetMessages()
					{
						foreach (SMC.IRfcStructure ls_Msg in this._RFCFunc.RfcFunction.GetTable(this._Profile.ParIdx_TabMsg))
							{
								var lo_Msg = new DTO_MsgEntry
									{
										TCode	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										DynNm	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										DynNo	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgTp	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgLg	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgID	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgNr	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgV1	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgV2	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgV3	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										MsgV4	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										Envir	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)	,
										FldNm	= (string)ls_Msg.GetValue(	this._Profile.TabMsg_TCode)
									};

								this.MsgData.Add(lo_Msg);
							}
					}

			#endregion

		}
}
