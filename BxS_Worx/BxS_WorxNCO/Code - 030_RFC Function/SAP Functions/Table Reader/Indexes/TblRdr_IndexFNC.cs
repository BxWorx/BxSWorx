using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.Main.NCO_Constants;
using	static	BxS_WorxNCO.RfcFunction.TableReader	.TblRdr_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.TableReader
{
	internal class TblRdr_IndexFNC : RfcFunctionIndex
		{
			#region "Documentation"

				//	*"----------------------------------------------------------------------
				//	*"*"Local Interface:
				//	*"  IMPORTING
				//	*"     VALUE(QUERY_TABLE)	TYPE  DD02L-TABNAME
				//	*"     VALUE(DELIMITER)		TYPE  SONV-FLAG		OPTIONAL
				//	*"     VALUE(NO_DATA)			TYPE  SONV-FLAG		OPTIONAL
				//	*"     VALUE(ROWSKIPS)		TYPE  SOID-ACCNT	OPTIONAL
				//	*"     VALUE(ROWCOUNT)		TYPE  SOID-ACCNT	OPTIONAL
				//	*"  EXPORTING
				//	*"     VALUE(OUT_TABLE) TYPE  DD02L-TABNAME
				//	*"  TABLES
				//	*"      OPTIONS			STRUCTURE  RFC_DB_OPT
				//	*"      FIELDS			STRUCTURE  RFC_DB_FLD
				//	*"      TBLOUT128		STRUCTURE  /BODS/TAB128
				//	*"      TBLOUT512		STRUCTURE  /BODS/TAB512
				//	*"      TBLOUT2048	STRUCTURE  /BODS/TAB2048
				//	*"      TBLOUT8192	STRUCTURE  /BODS/TAB8192
				//	*"      TBLOUT30000	STRUCTURE  /BODS/TAB30K
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal TblRdr_IndexFNC()
					{
						this._QryTable     = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "QUERY_TABLE"	) );
						this._Delimeter    = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "DELIMITER"		) );
						this._NoData       = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "NO_DATA"			) );
						this._SkipRows     = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "ROWSKIPS"			) );
						this._RowsCount    = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "ROWCOUNT"			) );
						this._Options      = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "OPTIONS"			) );
						this._Fields       = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "FIELDS"				) );
						this._OutTable     = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "OUT_TABLE"		) );
						//.............................................
						this._OutTab128    = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_OutTab128		) );
						this._OutTab512    = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_OutTab512		) );
						this._OutTab2048   = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_OutTab2048		) );
						this._OutTab8192   = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_OutTab8192		) );
						this._OutTab30000  = new Lazy<int>( ()=> this.Metadata.TryNameToIndex( cz_OutTab30000	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_QryTable    ;
				private	readonly	Lazy<int>		_Delimeter   ;
				private	readonly	Lazy<int>		_NoData      ;
				private	readonly	Lazy<int>		_SkipRows    ;
				private	readonly	Lazy<int>		_RowsCount   ;
				private	readonly	Lazy<int>		_Options     ;
				private	readonly	Lazy<int>		_Fields      ;
				private	readonly	Lazy<int>		_OutTable    ;
				private	readonly	Lazy<int>		_OutTab128   ;
				private	readonly	Lazy<int>		_OutTab512   ;
				private	readonly	Lazy<int>		_OutTab2048  ;
				private	readonly	Lazy<int>		_OutTab8192  ;
				private	readonly	Lazy<int>		_OutTab30000 ;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int		QryTable			{ get { return	this.IsLoaded	?	this._QryTable		.Value : cz_Neg1	; } }
				internal	int		Delimeter			{ get { return	this.IsLoaded	?	this._Delimeter		.Value : cz_Neg1	; } }
				internal	int		NoData				{ get { return	this.IsLoaded	?	this._NoData			.Value : cz_Neg1	; } }
				internal	int		SkipRows			{ get { return	this.IsLoaded	?	this._SkipRows		.Value : cz_Neg1	; } }
				internal	int		RowsCount			{ get { return	this.IsLoaded	?	this._RowsCount		.Value : cz_Neg1	; } }
				internal	int		Options				{ get { return	this.IsLoaded	?	this._Options			.Value : cz_Neg1	; } }
				internal	int		Fields				{ get { return	this.IsLoaded	?	this._Fields			.Value : cz_Neg1	; } }
				internal	int		OutTable			{ get { return	this.IsLoaded	?	this._OutTable		.Value : cz_Neg1	; } }
				internal	int		OutTab128			{ get { return	this.IsLoaded	?	this._OutTab128		.Value : cz_Neg1	; } }
				internal	int		OutTab512			{ get { return	this.IsLoaded	?	this._OutTab512		.Value : cz_Neg1	; } }
				internal	int		OutTab2048		{ get { return	this.IsLoaded	?	this._OutTab2048	.Value : cz_Neg1	; } }
				internal	int		OutTab8192		{ get { return	this.IsLoaded	?	this._OutTab8192	.Value : cz_Neg1	; } }
				internal	int		OutTab30000		{ get { return	this.IsLoaded	?	this._OutTab30000	.Value : cz_Neg1	; } }

			#endregion

		}
}
