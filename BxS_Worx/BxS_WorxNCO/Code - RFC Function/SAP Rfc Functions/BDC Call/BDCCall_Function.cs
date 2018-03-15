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
						//.............................................
						this.MyProfile	= new Lazy<BDCCall_Profile>( ()=> (BDCCall_Profile) this.Profile );

						//this._IsConfigured	= false						;

						//this.Header					= this._CallProfile.OpFncts.CreateRfcHead()	;
						//this.Transaction		= this._CallProfile.OpFncts.CreateRFCTran()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal readonly Lazy<BDCCall_Profile>	MyProfile;

				//private	bool	_IsConfigured	;

			#endregion

			//===========================================================================================
			#region "Properties"

				//private	BDCCall_Profile MyProfile	{ get {	return	(BDCCall_Profile) this.Profile; } }
				//internal	BDCCall_Header		Header				{ get; }
				//internal	BDCCall_Lines			Transaction		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader()
					{
						BDCCall_Header lo_Head	= this.MyProfile.Value._CreateHeader();
						//.............................................
						lo_Head.CTUParms	= this.MyProfile.Value.GetCTUStructure();
						//.............................................
						return	lo_Head;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Lines CreateBDCCallLines()
					{
						BDCCall_Lines lo_Lines	=	this.MyProfile.Value._CreateLines();
						//.............................................
						lo_Lines.SPAData	= this.MyProfile.Value.CreateSPATable();
						lo_Lines.BDCData	= this.MyProfile.Value.CreateBDCTable();
						lo_Lines.MSGData	= this.MyProfile.Value.CreateMSGTable();
						//.............................................
						return	lo_Lines;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( BDCCall_Header config )
					{
						this.Profile.ReadyProfile();
						//.............................................
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
								this.LoadTable( lines.SPAData	, this.MyProfile.Value.ParIdx_TabSPA );
								this.LoadTable( lines.BDCData ,	this.MyProfile.Value.ParIdx_TabBDC );
								//.............................................
								this.Invoke();
								//.............................................
								this.LoadTable( lines.MSGData	, this.MyProfile.Value.ParIdx_TabMSG , true );
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
						this.Profile.ReadyProfile();
						//.............................................
						this.NCORfcFunction.GetTable( this.MyProfile.Value.ParIdx_TabSPA ).Clear();
						this.NCORfcFunction.GetTable( this.MyProfile.Value.ParIdx_TabBDC ).Clear();
						this.NCORfcFunction.GetTable( this.MyProfile.Value.ParIdx_TabMSG ).Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_SAPTCode( string	tCode )
					{
						this.NCORfcFunction.SetValue( this.MyProfile.Value.ParIdx_TCode	, tCode );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_Skip1st( bool skip = false )
					{
						this.NCORfcFunction.SetValue( this.MyProfile.Value.ParIdx_Skip1	, skip ? "X" : " " );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_CTU( SMC.IRfcStructure ctu )
					{
						SMC.IRfcStructure ls_CTU	= this.NCORfcFunction.GetStructure( this.MyProfile.Value.ParIdx_CTUOpt );

						for (int i = 0; i < ctu.Count; i++)
							{
								ls_CTU.SetValue( i , ctu.GetValue(i) );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadTable( SMC.IRfcTable data , int index , bool invert = false )
					{
						SMC.IRfcTable lt_Tbl	= this.NCORfcFunction.GetTable( index );

						if (invert)
							{	data.Append( lt_Tbl ); }
						else
							{	lt_Tbl.Append( data ); }
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
