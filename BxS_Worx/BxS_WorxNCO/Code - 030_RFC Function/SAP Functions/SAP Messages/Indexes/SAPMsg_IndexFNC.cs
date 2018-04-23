using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_IndexFNC : RfcFunctionIndex
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
				internal SAPMsg_IndexFNC()
					{
						this._Langu		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "LANGUAGE"				) );
						this._MsgID		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_ID"			) );
						this._MsgNo		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_NUMBER"	) );
						this._MsgV1		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_VAR1"		) );
						this._MsgV2		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_VAR2"		) );
						this._MsgV3		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_VAR3"		) );
						this._MsgV4		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_VAR4"		) );
						this._MsgST		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MESSAGE_TEXT"		) );
						this._MsgLT		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "LONGTEXT"				) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_Langu	;
				private	readonly	Lazy<int>		_MsgID	;
				private	readonly	Lazy<int>		_MsgNo	;
				private	readonly	Lazy<int>		_MsgV1	;
				private	readonly	Lazy<int>		_MsgV2	;
				private	readonly	Lazy<int>		_MsgV3	;
				private	readonly	Lazy<int>		_MsgV4	;
				private	readonly	Lazy<int>		_MsgST	;
				private	readonly	Lazy<int>		_MsgLT	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		Langu		{ get { return	this.IsLoaded	?	this._Langu.Value : cz_Neg1	; } }
				internal	int		MsgID		{ get { return	this.IsLoaded	?	this._MsgID.Value : cz_Neg1	; } }
				internal	int		MsgNo		{ get { return	this.IsLoaded	?	this._MsgNo.Value : cz_Neg1	; } }
				internal	int		MsgV1		{ get { return	this.IsLoaded	?	this._MsgV1.Value : cz_Neg1	; } }
				internal	int		MsgV2		{ get { return	this.IsLoaded	?	this._MsgV2.Value : cz_Neg1	; } }
				internal	int		MsgV3		{ get { return	this.IsLoaded	?	this._MsgV3.Value : cz_Neg1	; } }
				internal	int		MsgV4		{ get { return	this.IsLoaded	?	this._MsgV4.Value : cz_Neg1	; } }
				internal	int		MsgST		{ get { return	this.IsLoaded	?	this._MsgST.Value : cz_Neg1	; } }
				internal	int		MsgLT		{ get { return	this.IsLoaded	?	this._MsgLT.Value : cz_Neg1	; } }

			#endregion

		}
}
