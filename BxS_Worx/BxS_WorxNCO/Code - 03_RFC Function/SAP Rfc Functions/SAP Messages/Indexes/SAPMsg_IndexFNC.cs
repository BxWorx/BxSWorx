using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.SAPMsg
{
	internal class SAPMsg_IndexFNC
		{
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

				internal	string	Name { get { return	cz_SAPMsgCompiler; } }

				internal	SMC.RfcFunctionMetadata	Metadata	{ get; set;	}
				//.................................................
				internal	int		Langu		{ get { return	this._Langu.Value; } }
				internal	int		MsgID		{ get { return	this._MsgID.Value; } }
				internal	int		MsgNo		{ get { return	this._MsgNo.Value; } }
				internal	int		MsgV1		{ get { return	this._MsgV1.Value; } }
				internal	int		MsgV2		{ get { return	this._MsgV2.Value; } }
				internal	int		MsgV3		{ get { return	this._MsgV3.Value; } }
				internal	int		MsgV4		{ get { return	this._MsgV4.Value; } }
				internal	int		MsgST		{ get { return	this._MsgST.Value; } }
				internal	int		MsgLT		{ get { return	this._MsgLT.Value; } }

			#endregion

		}
}
