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
				public void Process(DTO_RFCData dtoRFCData)
					{
						try
							{
								if (!this._FncCreated)
									{
										this._RFCFunc.RfcFunction		= this._Profile.RFCFnc;
										this._FncCreated						= !this._FncCreated;
									}
								//.........................................
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TCode	,	dtoRFCData.SAPTCode	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_Skip1	, dtoRFCData.Skip1st	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_CTUOpt	, dtoRFCData.CTUOpts	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabSPA	, dtoRFCData.SPAData	)	;
								this._RFCFunc.RfcFunction.SetValue(	this._Profile.ParIdx_TabBDC	, dtoRFCData.BDCData	)	;
								//.........................................
								dtoRFCData.SuccesStatus	=	this._RFCFunc.Invoke(this._Profile.RfcDestination);
								//.........................................
								dtoRFCData.MSGData	=	this._RFCFunc.RfcFunction.GetTable(this._Profile.ParIdx_TabMSG);
							}
						catch (System.Exception)
							{
								dtoRFCData.SuccesStatus	= false;
							}
						finally
							{
								dtoRFCData.ProcessedStatus	= true;
							}
					}

			#endregion

		}
}
