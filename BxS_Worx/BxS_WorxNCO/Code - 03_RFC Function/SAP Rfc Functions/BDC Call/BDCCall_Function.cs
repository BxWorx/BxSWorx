using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
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
						this.MyProfile	= new Lazy< BDCCall_Profile >
																		(		()=> (BDCCall_Profile) this.Profile
																			, LazyThreadSafetyMode.ExecutionAndPublication											);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal readonly Lazy< BDCCall_Profile >	MyProfile;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	BDCCall_IndexFNC	FNCIndex	{ get {	return	this.MyProfile.Value.FNCIndex; } }
				internal	BDCCall_IndexCTU	CTUIndex	{ get {	return	this.MyProfile.Value.CTUIndex; } }

				private	int		Idx_Tcd		{ get {	return	this.FNCIndex.ParIdx_TCode	; } }
				private	int		Idx_Skp		{ get {	return	this.FNCIndex.ParIdx_Skip1	; } }
				private	int		Idx_CTU		{ get {	return	this.FNCIndex.ParIdx_CTUOpt	; } }

				private	int		Idx_SPA		{ get {	return	this.FNCIndex.ParIdx_TabSPA	; } }
				private	int		Idx_BDC		{ get {	return	this.FNCIndex.ParIdx_TabBDC	; } }
				private	int		Idx_MSG		{ get {	return	this.FNCIndex.ParIdx_TabMSG	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Header CreateBDCCallHeader	( bool defaults = true )	=>	this.MyProfile.Value.CreateBDCCallHeader( defaults );
				internal BDCCall_Lines	CreateBDCCallLines	()												=>	this.MyProfile.Value.CreateBDCCallLines	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( BDCCall_Header config )
					{
						this.Profile.ReadyProfile();
						//.............................................
						this.Set_ShowSAPGUI	( config.ShowSAPGui );
						this.Set_SAPTCode		( config.SAPTCode		);
						this.Set_Skip1st		(	config.Skip1st		);
						this.Set_CTU				( config.CTUParms		);
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
								this.LoadTable( lines.BDCData ,	this.Idx_BDC );
								this.LoadTable( lines.SPAData	, this.Idx_SPA );
								//.............................................
								this.Invoke();
								//.............................................
								this.LoadTable( lines.MSGData	, this.Idx_MSG , true );
								lines.SuccesStatus	= true;
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Reset()
					{
						this.Profile.ReadyProfile();
						//.............................................
						this.NCORfcFunction.GetTable( this.Idx_SPA ).Clear();
						this.NCORfcFunction.GetTable( this.Idx_BDC ).Clear();
						this.NCORfcFunction.GetTable( this.Idx_MSG ).Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_ShowSAPGUI( bool state )
					{
						this.MyProfile.Value.NCODestination.UseSAPGui	= state ? SMC.RfcConfigParameters.RfcUseSAPGui.Use
																																	: SMC.RfcConfigParameters.RfcUseSAPGui.Hidden ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_SAPTCode( string	tCode )
					{
						this.NCORfcFunction.SetValue( this.Idx_Tcd , tCode );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_Skip1st( bool skip = false )
					{
						this.NCORfcFunction.SetValue( this.Idx_Skp , skip ? "X" : " " );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Set_CTU( SMC.IRfcStructure ctu )
					{
						SMC.IRfcStructure ls_CTU	= this.NCORfcFunction.GetStructure( this.Idx_CTU );

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

			#endregion

		}
}
