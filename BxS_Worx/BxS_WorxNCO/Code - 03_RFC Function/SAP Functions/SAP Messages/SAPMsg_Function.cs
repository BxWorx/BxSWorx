using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.BDCSession.DTO;

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

				private	int		Idx_Langu		{ get {	return	this.FNCIndex.Langu	; } }
				private	int		Idx_MsgID		{ get {	return	this.FNCIndex.MsgID	; } }
				private	int		Idx_MsgNo		{ get {	return	this.FNCIndex.MsgNo	; } }
				private	int		Idx_MsgV1		{ get {	return	this.FNCIndex.MsgV1	; } }
				private	int		Idx_MsgV2		{ get {	return	this.FNCIndex.MsgV2	; } }
				private	int		Idx_MsgV3		{ get {	return	this.FNCIndex.MsgV3	; } }
				private	int		Idx_MsgV4		{ get {	return	this.FNCIndex.MsgV4	; } }
				private	int		Idx_MsgST		{ get {	return	this.FNCIndex.MsgST	; } }
				private	int		Idx_MsgLT		{	get {	return	this.FNCIndex.MsgLT	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Process(	DTO_BDC_Msg					dto
															, SMC.RfcDestination	rfcDestination )
					{
						try
							{
								this.NCORfcFunction.SetValue( this.Idx_Langu	, dto.MsgLg	);
								this.NCORfcFunction.SetValue( this.Idx_MsgID	, dto.MsgID );
								this.NCORfcFunction.SetValue( this.Idx_MsgNo	, dto.MsgNr );
								this.NCORfcFunction.SetValue( this.Idx_MsgV1	, dto.MsgV1 );
								this.NCORfcFunction.SetValue( this.Idx_MsgV2	, dto.MsgV2 );
								this.NCORfcFunction.SetValue( this.Idx_MsgV3	, dto.MsgV3 );
								this.NCORfcFunction.SetValue( this.Idx_MsgV4	, dto.MsgV4 );
								//.............................................
								this.Invoke( rfcDestination );
								//.............................................
								dto.MsgST	= this.NCORfcFunction.GetString( this.Idx_MsgST );
							}
						catch (Exception)
							{
							throw;
							}
						finally
							{
							}
					}

			#endregion

		}
}
