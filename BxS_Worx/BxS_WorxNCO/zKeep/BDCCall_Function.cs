using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_Function	: RfcFncBase
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
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Function( BDC_Profile	profile	)	: base(	profile )
					{
						this.MyProfile	= new Lazy< BDC_Profile >	(	()=> (BDC_Profile) this.Profile , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	readonly	Lazy< BDC_Profile >		MyProfile;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	BDC_IndexFNC	FNCIndex	{ get {	return	this.MyProfile.Value._FNCIndex.Value; } }

				private	int		Idx_Tcd		{ get {	return	this.FNCIndex.TCode		; } }
				private	int		Idx_Skp		{ get {	return	this.FNCIndex.Skip1		; } }
				private	int		Idx_CTU		{ get {	return	this.FNCIndex.CTUOpt	; } }

				private	int		Idx_SPA		{ get {	return	this.FNCIndex.TabSPA	; } }
				private	int		Idx_BDC		{ get {	return	this.FNCIndex.TabBDC	; } }
				private	int		Idx_MSG		{ get {	return	this.FNCIndex.TabMSG	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Header		CreateBDCHeader	( bool defaults = true )	=>	this.MyProfile.Value.CreateBDCHeader( defaults );
				internal BDC_Data			CreateBDCData		()												=>	this.MyProfile.Value.CreateBDCData	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( BDC_Header config )
					{
						this.Profile.ReadyProfile();
						//.............................................
						this.Set_SAPTCode	( config.SAPTCode	);
						this.Set_Skip1st	(	config.Skip1st	);
						this.Set_CTU			( config.CTUParms	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process(	BDC_Data				data
															, SMC.RfcDestination	rfcDestination )
					{
						this.Reset();
						//.............................................
						try
							{
								data.ProcessedStatus	= false;
								data.SuccesStatus			= false;
								//.............................................
								this.LoadTable( data.SPAData , this.Idx_SPA , false );
								this.LoadTable( data.BDCData , this.Idx_BDC	, false );

								this.Invoke		( rfcDestination );

								this.LoadTable( data.MSGData , this.Idx_MSG , true );
								//.............................................
								data.SuccesStatus	= true;
							}
						catch (Exception ex)
							{
								data.ProcessedStatus	= true;
								throw new Exception( "BDC Function: Process failure" , ex );
							}
						finally
							{
								data.ProcessedStatus	= true;
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

			#endregion

		}
}
