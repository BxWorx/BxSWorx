using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.BDCTran.BDCCall_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexMSG
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexMSG()
					{
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

				internal	string	Name { get { return	cz_StrMSG; } }

				internal	SMC.RfcStructureMetadata	Metadata	{ get; set;	}
				//.................................................
				internal	int		TCode		{ get { return	this.Metadata == null	?	0	:	this._TCode.Value; } }
				internal	int		DynNm		{ get { return	this.Metadata == null	?	0	:	this._DynNm.Value; } }
				internal	int		DynNo		{ get { return	this.Metadata == null	?	0	:	this._DynNo.Value; } }
				internal	int		MsgTp		{ get { return	this.Metadata == null	?	0	:	this._MsgTp.Value; } }
				internal	int		Lang		{ get { return	this.Metadata == null	?	0	:	this._Lang .Value; } }
				internal	int		MsgID		{ get { return	this.Metadata == null	?	0	:	this._MsgID.Value; } }
				internal	int		MsgNo		{ get { return	this.Metadata == null	?	0	:	this._MsgNo.Value; } }
				internal	int		MsgV1		{ get { return	this.Metadata == null	?	0	:	this._MsgV1.Value; } }
				internal	int		MsgV2		{ get { return	this.Metadata == null	?	0	:	this._MsgV2.Value; } }
				internal	int		MsgV3		{ get { return	this.Metadata == null	?	0	:	this._MsgV3.Value; } }
				internal	int		MsgV4		{ get { return	this.Metadata == null	?	0	:	this._MsgV4.Value; } }
				internal	int		Envir		{ get { return	this.Metadata == null	?	0	:	this._Envir.Value; } }
				internal	int		Fldnm		{ get { return	this.Metadata == null	?	0	:	this._Fldnm.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcStructure	Create()
					{
						return	this.Metadata.CreateStructure();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SMC.IRfcTable	CreateTable()
					{
						return	this.Metadata.CreateTable();
					}

			#endregion

		}
}
