using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main										.NCO_Constants;
using static	BxS_WorxNCO.RfcFunction.TableReader	.TblRdr_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexFLD : RfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexFLD()
					{
						this.Name		=	cz_StrFLD;
						//.............................................
						this._FldNme	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIELDNAME"	) );
						this._OffSet	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "OFFSET"			) );
						this._Length	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "LENGTH"			) );
						this._Type		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "TYPE"				) );
						this._FldTxt	= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIELDTEXT"	) );
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

				internal	int		FldNme	{ get { return	this.IsLoaded	?	this._FldNme	.Value	:	cz_Neg1	; } }
				internal	int		OffSet	{ get { return	this.IsLoaded	?	this._OffSet	.Value	:	cz_Neg1	; } }
				internal	int		Length	{ get { return	this.IsLoaded	?	this._Length	.Value	:	cz_Neg1	; } }
				internal	int		Type		{ get { return	this.IsLoaded	?	this._Type		.Value	:	cz_Neg1	; } }
				internal	int		FldTxt	{ get { return	this.IsLoaded	?	this._FldTxt	.Value	:	cz_Neg1	; } }

			#endregion

		}
}
