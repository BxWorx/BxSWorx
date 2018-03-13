using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Common;
using BxS_WorxNCO.BDCCall;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Function	: RfcFncBase
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
				internal BDCCall_Function( BDCCall_Profile	profile	)	: base(	profile )
					{
						this._CallProfile		=	profile	;
						//.............................................
						this._IsConfigured	= false						;
						this._MyID					= Guid.NewGuid()	;

						//this.Header					= this._CallProfile.OpFncts.CreateRfcHead()	;
						//this.Transaction		= this._CallProfile.OpFncts.CreateRFCTran()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCall_Profile	_CallProfile;

				private	bool	_IsConfigured	;
				private Guid	_MyID					;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	SMC.IRfcStructure	CTU	{ get { return	this.NCORfcFunction.GetStructure(  }}


				internal	BDCCallHeader		Header				{ get; }
				internal	BDCCallTran			Transaction		{ get; }
				//.................................................
				internal	BDCCallTranIndex	Indexer			{ get { return	this._CallProfile.Indexer; } }

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
				internal void Config( BDCCallHeader Config )
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
										this._RfcFunction.SetValue(	this.Indexer.ParIdx_TCode		,	this.Header.SAPTCode	)	;
										this._RfcFunction.SetValue(	this.Indexer.ParIdx_Skip1		, this.Header.Skip1st		)	;
										this._RfcFunction.SetValue(	this.Indexer.ParIdx_CTUOpt	, this.Header.CTUParms	)	;
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
				internal bool Ready()
					{
						return	this.Configure();
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
				internal bool Configure()
					{
						if ( !this._IsConfigured )
							{
								this._IsConfigured	= this._CallProfile.Configure(this);
								//.........................................
								if ( this._IsConfigured )
									{
										if ( this.CreateFunction() )
											{
												this._RfcFunction.SetValue(	this.Indexer.ParIdx_TabSPA	, this.Transaction.SPAData	)	;
												this._RfcFunction.SetValue(	this.Indexer.ParIdx_TabBDC	, this.Transaction.BDCData	)	;
												this._RfcFunction.SetValue(	this.Indexer.ParIdx_TabMSG	, this.Transaction.MSGData	)	;
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
