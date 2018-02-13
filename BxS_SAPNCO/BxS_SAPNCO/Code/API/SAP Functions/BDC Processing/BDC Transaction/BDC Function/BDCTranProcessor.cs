using BxS_SAPNCO.Destination;
using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCTranProcessor	: RFCFunctionBase
		//: IBDCTranProcessor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTranProcessor(	DestinationRfc	destRFC
																	,	IRFCFunction	RfcFunction
																	,	IBDCProfile		RfcFncProfile
																	,	BDCCallTranProfile	parmIndex	)	: base(parmIndex.RfcFnc, parmIndex.RfcDest)
					{
						this._ParmIndex	=	parmIndex;

						this._RFCFunc	= RfcFunction		;
						this._Profile	= RfcFncProfile	;
						//.............................................
						this._FncCreated		= false	;
						this._IsConfigured	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCallTranProfile	_ParmIndex;

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
						this._RFCFunc.RfcFunction.SetValue(	this._ParmIndex.ParIdx_TCode	,	Config.SAPTCode	)	;
						this._RFCFunc.RfcFunction.SetValue(	this._ParmIndex.ParIdx_Skip1	, Config.Skip1st	)	;
						this._RFCFunc.RfcFunction.SetValue(	this._ParmIndex.ParIdx_CTUOpt	, Config.CTUParms	)	;
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
								this._RFCFunc.RfcFunction.SetValue(	this._ParmIndex.ParIdx_TabSPA	, Transaction.SPAData	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._ParmIndex.ParIdx_TabBDC	, Transaction.BDCData	)	;
								//.........................................
								Transaction.SuccesStatus	=	this._RFCFunc.Invoke(this._Profile.RfcDestination);
								Transaction.MSGData				=	this._RFCFunc.RfcFunction.GetTable(this._ParmIndex.ParIdx_TabMSG);
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
