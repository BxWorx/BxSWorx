using SMC	= SAP.Middleware.Connector;
//.........................................................
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
						this._CallProfile		=	profile	;
						//.............................................
						this._IsConfigured	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCallTranProfile	_CallProfile;

				private	bool	_IsConfigured	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	DTO_RFCHeader	Header;
				internal	DTO_RFCTran		Transaction;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Set_SAPTCode(string	TCode)
					{
						if ( this.Configure() )
							{
								this.Header.SAPTCode	= TCode;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Set_Skip1st(string skip = " ")
					{
						if ( this.Configure() )
							{
								this.Header.Skip1st	= skip;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Set_CTU(SMC.IRfcStructure ctuParmsRFC)
					{
						if ( this.Configure() )
							{
								for (int i = 0; i < ctuParmsRFC.Count; i++)
									{
										this.Header.CTUParms.SetValue( i , ctuParmsRFC.GetValue(i) );
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( DTO_RFCHeader Config )
					{
						if ( this.Configure() )
							{
								this.Set_SAPTCode	( Config.SAPTCode );
								this.Set_Skip1st	(	Config.Skip1st	);
								this.Set_CTU			( Config.CTUParms	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process()
					{
						if ( this.Configure() )
							{
								try
									{
										this._RfcFunction.SetValue(	this._CallProfile.ParIdx_TCode	,	this.Header.SAPTCode	)	;
										this._RfcFunction.SetValue(	this._CallProfile.ParIdx_Skip1	, this.Header.Skip1st		)	;
										this._RfcFunction.SetValue(	this._CallProfile.ParIdx_CTUOpt	, this.Header.CTUParms	)	;
										//.........................................
										this.Transaction.SuccesStatus	=	this.Invoke();
									}
								catch (System.Exception)
									{
										this.Transaction.SuccesStatus	= false;
									}
								finally
									{
										this.Transaction.ProcessedStatus	= true;
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this.Transaction.ProcessedStatus	= false;
						this.Transaction.SuccesStatus			= false;
						//.........................................
						this.Transaction.SPAData.Clear();
						this.Transaction.BDCData.Clear();
						this.Transaction.MSGData.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool Configure()
					{
						if ( !this._IsConfigured )
							{
								this._IsConfigured	= this._CallProfile.Configure(this);
								//.........................................
								if ( this._IsConfigured )
									{
										if ( this.CreateFunction() )
											{
												this._RfcFunction.SetValue(	this._CallProfile.ParIdx_TabSPA	, this.Transaction.SPAData	)	;
												this._RfcFunction.SetValue(	this._CallProfile.ParIdx_TabBDC	, this.Transaction.BDCData	)	;
												this._RfcFunction.SetValue(	this._CallProfile.ParIdx_TabMSG , this.Transaction.MSGData	)	;
											}
										else
											{
												this._IsConfigured	= false;
											}
									}
							}
						//.............................................
						return	this._IsConfigured;
					}

			#endregion

		}
}
