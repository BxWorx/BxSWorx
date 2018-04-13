using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main								.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.BDCTran	.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDC_IndexMSG : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_IndexMSG( bool tranVersion = false )
					{
						this.Name		=	tranVersion	?	cz_StrMSGTran	:	cz_StrMSG;
						//.............................................
						this._TCode	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TCODE"		) );
						this._DynNm	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DYNAME"	) );
						this._DynNo	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DYNUMB"	) );
						this._MsgTp	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGTYP"	) );
						this._Lang	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGSPRA"	) );
						this._MsgID	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGID"		) );
						this._MsgNo	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGNR"		) );
						this._MsgV1	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGV1"		) );
						this._MsgV2	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGV2"		) );
						this._MsgV3	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGV3"		) );
						this._MsgV4	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "MSGV4"		) );
						this._Envir	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "ENV"			) );
						this._Fldnm	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FLDNAME"	) );
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

				internal	int	TCode		{ get { return	this.IsLoaded	?	this._TCode.Value	:	cz_Neg	; } }
				internal	int	DynNm		{ get { return	this.IsLoaded	?	this._DynNm.Value	:	cz_Neg	; } }
				internal	int	DynNo		{ get { return	this.IsLoaded	?	this._DynNo.Value	:	cz_Neg	; } }
				internal	int	MsgTp		{ get { return	this.IsLoaded	?	this._MsgTp.Value	:	cz_Neg	; } }
				internal	int	Lang		{ get { return	this.IsLoaded	?	this._Lang .Value	:	cz_Neg	; } }
				internal	int	MsgID		{ get { return	this.IsLoaded	?	this._MsgID.Value	:	cz_Neg	; } }
				internal	int	MsgNo		{ get { return	this.IsLoaded	?	this._MsgNo.Value	:	cz_Neg	; } }
				internal	int	MsgV1		{ get { return	this.IsLoaded	?	this._MsgV1.Value	:	cz_Neg	; } }
				internal	int	MsgV2		{ get { return	this.IsLoaded	?	this._MsgV2.Value	:	cz_Neg	; } }
				internal	int	MsgV3		{ get { return	this.IsLoaded	?	this._MsgV3.Value	:	cz_Neg	; } }
				internal	int	MsgV4		{ get { return	this.IsLoaded	?	this._MsgV4.Value	:	cz_Neg	; } }
				internal	int	Envir		{ get { return	this.IsLoaded	?	this._Envir.Value	:	cz_Neg	; } }
				internal	int	Fldnm		{ get { return	this.IsLoaded	?	this._Fldnm.Value	:	cz_Neg	; } }

			#endregion

		}
}
