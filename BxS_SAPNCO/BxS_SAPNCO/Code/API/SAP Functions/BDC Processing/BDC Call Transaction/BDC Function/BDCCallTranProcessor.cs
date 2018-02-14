using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranProcessor	: RFCFunctionBase
		{
			#region "Documentation"

				//	FUNCTION /isdfps/call_transaction.
				//	*"----------------------------------------------------------------------
				//	*"  IMPORTING
				//	*"     VALUE(IF_TCODE)							TYPE	TCODE
				//	*"     VALUE(IF_SKIP_FIRST_SCREEN)	TYPE	FLAG DEFAULT SPACE
				//	*"     VALUE(IT_BDCDATA)						TYPE	BDCDATA_TAB OPTIONAL
				//	*"     VALUE(IS_OPTIONS)						TYPE	CTU_PARAMS OPTIONAL
				//	*"  EXPORTING
				//	*"     VALUE(ET_MSG)								TYPE	ETTCD_MSG_TABTYPE
				//	*"  TABLES
				//	*"      CT_SETGET_PARAMETER					STRUCTURE	RFC_SPAGPA OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      IMPORT_PARA_ERROR
				//	*"      TCODE_ERROR
				//	*"      AUTH_ERROR
				//	*"      TRANS_ERROR

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProcessor( BDCCallTranProfile	profile	)
									: base(	profile )
					{
						this._Profile		=	profile	;
						//.............................................
						this._IsHeaderConfigured	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCallTranProfile	_Profile;

				private	bool	_IsHeaderConfigured	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Config( DTO_RFCHeader Config )
					{
						if (this.CreateFunction())
							{
								this._RfcFunction.SetValue(	this._Profile.ParIdx_TCode	,	Config.SAPTCode	)	;
								this._RfcFunction.SetValue(	this._Profile.ParIdx_Skip1	, Config.Skip1st	)	;
								this._RfcFunction.SetValue(	this._Profile.ParIdx_CTUOpt	, Config.CTUParms	)	;
								//.............................................
								this._IsHeaderConfigured	= true;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Process( DTO_RFCTran Transaction )
					{
						if (!this._IsHeaderConfigured)	return;
						//.............................................
						try
							{
								this._RfcFunction.SetValue(	this._Profile.ParIdx_TabSPA	, Transaction.SPAData	)	;
								this._RfcFunction.SetValue(	this._Profile.ParIdx_TabBDC	, Transaction.BDCData	)	;
								//.........................................
								Transaction.SuccesStatus	=	this.Invoke();
								Transaction.MSGData				=	this._RfcFunction.GetTable( this._Profile.ParIdx_TabMSG );
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
