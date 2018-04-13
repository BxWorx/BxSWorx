using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCTran_Function	: RfcFncBase
		{
			#region "Documentation"

				//	FUNCTION abap4_call_transaction.
				//	*"----------------------------------------------------------------------
				//	*"*"Lokale Schnittstelle:
				//	*"  IMPORTING
				//	*"     VALUE(TCODE)				LIKE  SY-TCODE
				//	*"     VALUE(SKIP_SCREEN)	LIKE  SY-FTYPE DEFAULT SPACE
				//	*"     VALUE(MODE_VAL)		LIKE  SY-FTYPE DEFAULT 'A'
				//	*"     VALUE(UPDATE_VAL)	LIKE  SY-FTYPE DEFAULT 'A'
				//	*"  EXPORTING
				//	*"     VALUE(SUBRC)				LIKE  SY-SUBRC
				//	*"  TABLES
				//	*"      USING_TAB					STRUCTURE  BDCDATA		OPTIONAL
				//	*"      SPAGPA_TAB				STRUCTURE  RFC_SPAGPA OPTIONAL
				//	*"      MESS_TAB					STRUCTURE  BDCMSGCOLL OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      CALL_TRANSACTION_DENIED
				//	*"      TCODE_INVALID
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTran_Function( BDCTran_Profile	profile	)	: base(	profile )
					{
						this.MyProfile	= new Lazy< BDCTran_Profile >	(	()=> (BDCTran_Profile) this.Profile , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	readonly	Lazy< BDCTran_Profile >		MyProfile;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	BDCTran_IndexFNC	FNCIndex	{ get {	return	this.MyProfile.Value._FNCIndex.Value; } }

				private	int		Idx_Tcd		{ get {	return	this.FNCIndex.TCode		; } }
				private	int		Idx_Skp		{ get {	return	this.FNCIndex.Skip1		; } }
				private	int		Idx_Dsp		{ get {	return	this.FNCIndex.Mode		; } }
				private	int		Idx_Upd		{ get {	return	this.FNCIndex.Update	; } }

				private	int		Idx_SPA		{ get {	return	this.FNCIndex.TabSPA	; } }
				private	int		Idx_BDC		{ get {	return	this.FNCIndex.TabBDC	; } }
				private	int		Idx_MSG		{ get {	return	this.FNCIndex.TabMSG	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCTran_Header CreateBDCCallHeader	( bool defaults = true )	=>	this.MyProfile.Value.CreateBDCCallHeader( defaults );
				internal BDCTran_Data		CreateBDCCallLines	()												=>	this.MyProfile.Value.CreateBDCCallData	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( BDCTran_Header config )
					{
						this.Profile.ReadyProfile();
						//.............................................
						this.Set_SAPTCode	( config.SAPTCode	);
						this.Set_Skip1st	(	config.Skip1st	);
						this.Set_CTU			( config.CTUParms	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process(	BDCTran_Data				data
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
