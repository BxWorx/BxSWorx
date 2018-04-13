using System;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_IndexFNCx : IIndexMapper
		{
			#region "Constructors"

				public	Dictionary< string , int		> SAPIndex				{ get; }
				public	Dictionary<	string , string >	PropertyIndex		{ get; }





				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPMsg_IndexFNCx( SAPMsg_Profile profile )
					{
						this._Profile		= profile;
						//.............................................
						this._Langu		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "LANGUAGE"				) );
						this._MsgID		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_ID"			) );
						this._MsgNo		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_NUMBER"	) );
						this._MsgV1		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_VAR1"		) );
						this._MsgV2		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_VAR2"		) );
						this._MsgV3		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_VAR3"		) );
						this._MsgV4		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_VAR4"		) );
						this._MsgST		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "MESSAGE_TEXT"		) );
						this._MsgLT		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "LONGTEXT"				) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	SAPMsg_Profile		_Profile;
				//.................................................
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

				internal	string	Name { get { return	cz_SAPMsgCompiler; } }
				//.................................................
				internal	int		Langu		{ get { return	this._Profile.IsReady ?	this._Langu.Value : 0	; } }
				internal	int		MsgID		{ get { return	this._Profile.IsReady ?	this._MsgID.Value : 0	; } }
				internal	int		MsgNo		{ get { return	this._Profile.IsReady ?	this._MsgNo.Value : 0	; } }
				internal	int		MsgV1		{ get { return	this._Profile.IsReady ?	this._MsgV1.Value : 0	; } }
				internal	int		MsgV2		{ get { return	this._Profile.IsReady ?	this._MsgV2.Value : 0	; } }
				internal	int		MsgV3		{ get { return	this._Profile.IsReady ?	this._MsgV3.Value : 0	; } }
				internal	int		MsgV4		{ get { return	this._Profile.IsReady ?	this._MsgV4.Value : 0	; } }
				internal	int		MsgST		{ get { return	this._Profile.IsReady ?	this._MsgST.Value : 0	; } }
				internal	int		MsgLT		{ get { return	this._Profile.IsReady ?	this._MsgLT.Value : 0	; } }

			#endregion

		}
}
