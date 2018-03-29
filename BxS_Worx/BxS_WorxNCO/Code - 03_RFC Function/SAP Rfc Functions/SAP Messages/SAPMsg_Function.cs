using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_Function	: RfcFncBase
		{
			#region "Documentation"

				//	function rpy_message_compose.
				//	*"----------------------------------------------------------------------
				//	*"*"Lokale Schnittstelle:
				//	*"  IMPORTING
				//	*"     VALUE(LANGUAGE)				LIKE  T100-SPRSL DEFAULT SY-LANGU
				//	*"     VALUE(MESSAGE_ID)			LIKE  SY-MSGID
				//	*"     VALUE(MESSAGE_NUMBER)	LIKE  SY-MSGNO
				//	*"     VALUE(MESSAGE_VAR1)		LIKE  SY-MSGV1 DEFAULT SPACE
				//	*"     VALUE(MESSAGE_VAR2)		LIKE  SY-MSGV2 DEFAULT SPACE
				//	*"     VALUE(MESSAGE_VAR3)		LIKE  SY-MSGV3 DEFAULT SPACE
				//	*"     VALUE(MESSAGE_VAR4)		LIKE  SY-MSGV4 DEFAULT SPACE
				//	*"  EXPORTING
				//	*"     VALUE(MESSAGE_TEXT)		LIKE  SY-LISEL
				//	*"  TABLES
				//	*"      LONGTEXT							STRUCTURE  TLINE OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      MESSAGE_NOT_FOUND
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_Function( SAPMsg_Profile	profile	)	: base(	profile )
					{
						//.............................................
						this.MyProfile	= new Lazy< SAPMsg_Profile >	(	()=> (SAPMsg_Profile) this.Profile , cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	readonly	Lazy< SAPMsg_Profile >	MyProfile;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	SAPMsg_IndexFNC	FNCIndex	{ get {	return	this.MyProfile.Value.FNCIndex; } }
				internal	SAPMsg_IndexTXT	TXTIndex	{ get {	return	this.MyProfile.Value.TXTIndex; } }

				private	int		Idx_Langu		{ get {	return	this.FNCIndex.ParIdx_Langu	; } }
				private	int		Idx_MsgID		{ get {	return	this.FNCIndex.ParIdx_MsgID	; } }
				private	int		Idx_MsgNo		{ get {	return	this.FNCIndex.ParIdx_MsgNo	; } }
				private	int		Idx_MsgV1		{ get {	return	this.FNCIndex.ParIdx_MsgV1	; } }
				private	int		Idx_MsgV2		{ get {	return	this.FNCIndex.ParIdx_MsgV2	; } }
				private	int		Idx_MsgV3		{ get {	return	this.FNCIndex.ParIdx_MsgV3	; } }
				private	int		Idx_MsgV4		{ get {	return	this.FNCIndex.ParIdx_MsgV4	; } }
				private	int		Idx_MsgST		{ get {	return	this.FNCIndex.ParIdx_MsgST	; } }
				private	int		Idx_MsgLT		{	get {	return	this.FNCIndex.ParIdx_MsgLT	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal	SAPMsg_Header		CreateBDCCallHeader	( bool defaults = true )	=>	this.MyProfile.Value.CreateBDCCallHeader( defaults );
				//internal	SAPMsg_Lines		CreateBDCCallLines	()												=>	this.MyProfile.Value.CreateBDCCallLines	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Config( SAPMsg_Header config )
					{
						this.Profile.ReadyProfile();
						//.............................................
						this.Set_ShowSAPGUI	( config.ShowSAPGui );
						this.Set_SAPTCode		( config.SAPTCode		);
						this.Set_Skip1st		(	config.Skip1st		);
						this.Set_CTU				( config.CTUParms		);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process(	SAPMsg_Lines lines
															, SMC.RfcCustomDestination rfcDestination )
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
								this.Invoke( rfcDestination );
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
				private void LoadTable(		SMC.IRfcTable	data
																, int						index
																, bool					reverse = false )
					{
						SMC.IRfcTable lt_Tbl	= this.NCORfcFunction.GetTable( index );

						if ( reverse )
							{	data.Append( lt_Tbl ); }
						else
							{	lt_Tbl.Append( data ); }
					}

			#endregion

		}
}
