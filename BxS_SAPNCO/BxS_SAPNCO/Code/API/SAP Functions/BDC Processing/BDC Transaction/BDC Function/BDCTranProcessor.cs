using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCTranProcessor : IBDCTranProcessor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTranProcessor(	IRFCFunction	RfcFunction		,
																		IBDCProfile		RfcFncProfile		)
					{
						this._RFCFunc	= RfcFunction		;
						this._Profile	= RfcFncProfile	;
						//.............................................
						this._FncCreated		= false	;
						this._IsConfigured	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IRFCFunction	_RFCFunc	;
				private readonly	IBDCProfile		_Profile	;

				private	bool	_FncCreated		;
				private	bool	_IsConfigured	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Config( DTO_RFCHeader Config )
					{
						if (!this._FncCreated)
							{
								this._RFCFunc.RfcFunction		= this._Profile.RFCFnc;
								this._FncCreated						= !this._FncCreated;
							}
						//.............................................
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TCode	,	Config.SAPTCode	)	;
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_Skip1	, Config.Skip1st	)	;
						this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_CTUOpt	, Config.CTUParms	)	;
						//.............................................
						this._IsConfigured	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Process( DTO_RFCTran Transaction )
					{
						if (!this._IsConfigured)	return;
						//.............................................
						try
							{
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabSPA	, Transaction.SPAData	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabBDC	, Transaction.BDCData	)	;
								//.........................................
								Transaction.SuccesStatus	=	this._RFCFunc.Invoke(this._Profile.RfcDestination);
								Transaction.MSGData				=	this._RFCFunc.RfcFunction.GetTable(this._Profile.ParIdx_TabMSG);
							}
						catch (System.Exception)
							{
								Transaction.SuccesStatus	= false;
							}
						finally
							{
								Transaction.ProcessedStatus	= true;
							}
					}

			#endregion

		}
}
