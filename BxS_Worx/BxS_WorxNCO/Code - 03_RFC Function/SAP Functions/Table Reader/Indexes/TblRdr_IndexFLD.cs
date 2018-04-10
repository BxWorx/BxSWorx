using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexFLD : TblRdr_IndexBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexFLD( TblRdr_Profile profile ) : base( profile )
					{
						this._Metadata	=	new	Lazy< SMC.RfcStructureMetadata >( ()=> this._Profile.FieldsStructure );
						//.............................................
						this._FldNme	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "FIELDNAME"	) );
						this._OffSet	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "OFFSET"			) );
						this._Length	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "LENGTH"			) );
						this._Type		= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "TYPE"				) );
						this._FldTxt	= new Lazy<int>( ()=> this._Metadata.Value.TryNameToIndex( "FIELDTEXT"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_FldNme	;
				private	readonly	Lazy<int>		_OffSet	;
				private	readonly	Lazy<int>		_Length	;
				private	readonly	Lazy<int>		_Type		;
				private	readonly	Lazy<int>		_FldTxt	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		FldNme	{ get { return	this._Profile.IsReady ?	this._FldNme	.Value	:	0	; } }
				internal	int		OffSet	{ get { return	this._Profile.IsReady ?	this._OffSet	.Value	:	0	; } }
				internal	int		Length	{ get { return	this._Profile.IsReady ?	this._Length	.Value	:	0	; } }
				internal	int		Type		{ get { return	this._Profile.IsReady ?	this._Type		.Value	:	0	; } }
				internal	int		FldTxt	{ get { return	this._Profile.IsReady ?	this._FldTxt	.Value	:	0	; } }

			#endregion

		}
}
