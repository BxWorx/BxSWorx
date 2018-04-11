using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_IndexMSG : BDCCall_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_IndexMSG( BDCCall_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.MSGStructure	);
						//.............................................
						this._TCode	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "TCODE"		) );
						this._DynNm	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "DYNAME"		) );
						this._DynNo	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "DYNUMB"		) );
						this._MsgTp	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGTYP"		) );
						this._Lang	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGSPRA"	) );
						this._MsgID	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGID"		) );
						this._MsgNo	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGNR"		) );
						this._MsgV1	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGV1"		) );
						this._MsgV2	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGV2"		) );
						this._MsgV3	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGV3"		) );
						this._MsgV4	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "MSGV4"		) );
						this._Envir	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "ENV"			) );
						this._Fldnm	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "FLDNAME"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_TCode;
				private	readonly	Lazy<int>		_DynNm;
				private	readonly	Lazy<int>		_DynNo;
				private	readonly	Lazy<int>		_MsgTp;
				private	readonly	Lazy<int>		_Lang	;
				private	readonly	Lazy<int>		_MsgID;
				private	readonly	Lazy<int>		_MsgNo;
				private	readonly	Lazy<int>		_MsgV1;
				private	readonly	Lazy<int>		_MsgV2;
				private	readonly	Lazy<int>		_MsgV3;
				private	readonly	Lazy<int>		_MsgV4;
				private	readonly	Lazy<int>		_Envir;
				private	readonly	Lazy<int>		_Fldnm;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		TCode		{ get { return	this._Profile.IsReady	?	this._TCode.Value	:	0	; } }
				internal	int		DynNm		{ get { return	this._Profile.IsReady	?	this._DynNm.Value	:	0	; } }
				internal	int		DynNo		{ get { return	this._Profile.IsReady	?	this._DynNo.Value	:	0	; } }
				internal	int		MsgTp		{ get { return	this._Profile.IsReady	?	this._MsgTp.Value	:	0	; } }
				internal	int		Lang		{ get { return	this._Profile.IsReady	?	this._Lang .Value	:	0	; } }
				internal	int		MsgID		{ get { return	this._Profile.IsReady	?	this._MsgID.Value	:	0	; } }
				internal	int		MsgNo		{ get { return	this._Profile.IsReady	?	this._MsgNo.Value	:	0	; } }
				internal	int		MsgV1		{ get { return	this._Profile.IsReady	?	this._MsgV1.Value	:	0	; } }
				internal	int		MsgV2		{ get { return	this._Profile.IsReady	?	this._MsgV2.Value	:	0	; } }
				internal	int		MsgV3		{ get { return	this._Profile.IsReady	?	this._MsgV3.Value	:	0	; } }
				internal	int		MsgV4		{ get { return	this._Profile.IsReady	?	this._MsgV4.Value	:	0	; } }
				internal	int		Envir		{ get { return	this._Profile.IsReady	?	this._Envir.Value	:	0	; } }
				internal	int		Fldnm		{ get { return	this._Profile.IsReady	?	this._Fldnm.Value	:	0	; } }

			#endregion

		}
}
