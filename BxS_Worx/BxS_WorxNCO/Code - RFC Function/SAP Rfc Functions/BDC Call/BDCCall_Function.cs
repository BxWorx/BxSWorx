using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Common;
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
						this._Profile		=	profile	;
						//.............................................
						//this._IsConfigured	= false						;

						//this.Header					= this._CallProfile.OpFncts.CreateRfcHead()	;
						//this.Transaction		= this._CallProfile.OpFncts.CreateRFCTran()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private	bool	_IsConfigured	;
				//.................................................
				private	readonly	BDCCall_Profile	_Profile;

			#endregion

			//===========================================================================================
			#region "Properties"

				//internal	BDCCall_Header		Header				{ get; }
				//internal	BDCCall_Lines			Transaction		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( BDCCall_Header config )
					{
						this.Set_SAPTCode	( config.SAPTCode );
						this.Set_Skip1st	(	config.Skip1st	);
						this.Set_CTU			( config.CTUParms	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process( BDCCall_Lines lines )
					{
						this.Reset();
						//.............................................
						try
							{
								lines.ProcessedStatus	= false;
								lines.SuccesStatus		= false;
								//.............................................
								this.LoadTable( lines.SPAData	, this._Profile.ParIdx_TabSPA );
								this.LoadTable( lines.BDCData ,	this._Profile.ParIdx_TabBDC );
								//.............................................
								this.Invoke( this._Profile.RfcDestination );
								//.............................................
								lines.ProcessedStatus	= true;
							}
						catch (Exception)
							{
							throw;
							}
						finally
							{
								lines.ProcessedStatus	= true;
							}
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal void Process()
				//	{
				//		if ( this.Configure() )
				//			{
				//				try
				//					{
				//						this._RfcFunction.SetValue(	this.Indexer.ParIdx_TCode		,	this.Header.SAPTCode	)	;
				//						this._RfcFunction.SetValue(	this.Indexer.ParIdx_Skip1		, this.Header.Skip1st		)	;
				//						this._RfcFunction.SetValue(	this.Indexer.ParIdx_CTUOpt	, this.Header.CTUParms	)	;
				//						//.........................................
				//						this.Transaction.SuccesStatus	=	this.Invoke();
				//					}
				//				catch (System.Exception)
				//					{
				//						this.Transaction.SuccesStatus	= false;
				//					}
				//				finally
				//					{
				//						this.Transaction.ProcessedStatus	= true;
				//					}
				//			}
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal bool Ready()
				//	{
				//		return	this.Configure();
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this.NCORfcFunction.GetTable( this._Profile.ParIdx_TabSPA ).Clear();
						this.NCORfcFunction.GetTable( this._Profile.ParIdx_TabBDC ).Clear();
						this.NCORfcFunction.GetTable( this._Profile.ParIdx_TabMSG ).Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_SAPTCode( string	tCode )
					{
						this.NCORfcFunction.SetValue( this._Profile.ParIdx_TCode	, tCode );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_Skip1st( string skip = " " )
					{
						this.NCORfcFunction.SetValue( this._Profile.ParIdx_Skip1	, skip );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_CTU( SMC.IRfcStructure ctu )
					{
						SMC.IRfcStructure ls_CTU	= this.NCORfcFunction.GetStructure( this._Profile.ParIdx_CTUOpt );

						for (int i = 0; i < ctu.Count; i++)
							{
								ls_CTU.SetValue( i , ctu.GetValue(i) );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTable( SMC.IRfcTable data , int index )
					{
						SMC.IRfcTable lt_Tbl	= this.NCORfcFunction.GetTable( index );
						lt_Tbl.Append( data );
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal bool Configure()
				//	{
				//		if ( !this._IsConfigured )
				//			{
				//				this._IsConfigured	= this._Profile.Configure(this);
				//				//.........................................
				//				if ( this._IsConfigured )
				//					{
				//						if ( this.CreateFunction() )
				//							{
				//								this._RfcFunction.SetValue(	this.Indexer.ParIdx_TabSPA	, this.Transaction.SPAData	)	;
				//								this._RfcFunction.SetValue(	this.Indexer.ParIdx_TabBDC	, this.Transaction.BDCData	)	;
				//								this._RfcFunction.SetValue(	this.Indexer.ParIdx_TabMSG	, this.Transaction.MSGData	)	;
				//							}
				//						else
				//							{
				//								this._IsConfigured	= false;
				//							}
				//					}
				//			}
				//		//.............................................
				//		return	this._IsConfigured;
				//	}

			#endregion

		}
}
